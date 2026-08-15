using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using G2DataGUI.Domain.Models;

namespace G2DataGUI.Rendering;

/// <summary>
/// Everything NjModel/NjMotion deliberately leave out: world transforms
/// and skinning. Kept separate from the Domain types (which stay pure
/// data) the same way every other feature in this project splits data
/// from algorithm.
///
/// World-transform math (scale * rotation * translation, accumulated
/// child = local * parent) is the exact formula verified extensively
/// against a real FBX export earlier in this project's development —
/// see PORTING-Chr.md. NjBone.Index is guaranteed to already be in a
/// valid evaluation order (every bone's ParentIndex is always smaller
/// than its own Index, since indices are assigned during a depth-first
/// read where a parent is always visited before its children) — so a
/// single forward pass over Bones is enough, no recursion needed.
/// </summary>
public static class NjPoseEvaluator
{
	//private const float AngleScale = MathF.PI * 2f / 4294967296f; // BAMS units are pre-converted to radians already, kept only for reference

	/// <summary>Bind-pose world transform for every bone — what the model
	/// looks like with no animation applied.</summary>
	public static Matrix4x4[] ComputeBindWorldTransforms(NjModel model)
	{
		Matrix4x4[] world = new Matrix4x4[model.Bones.Count];

		for (int i = 0; i < model.Bones.Count; i++)
		{
			NjBone bone = model.Bones[i];

			Matrix4x4 local = CreateTransform(bone.Position, bone.Rotation, bone.Scale);
			Matrix4x4 parent = bone.ParentIndex >= 0 ? world[bone.ParentIndex] : Matrix4x4.Identity;

			world[i] = local * parent;
		}

		return world;
	}

	/// <summary>World transform for every bone at a given frame of a
	/// motion — bones the motion doesn't animate fall back to their own
	/// bind pose, matching how sparse NJS_MOTION data works (not every
	/// bone needs a track).</summary>
	public static Matrix4x4[] ComputeAnimatedWorldTransforms(NjModel model, NjMotion motion, float frame)
	{
		Dictionary<int, NjMotionTrack> tracksByBone = motion.Tracks.ToDictionary(t => t.BoneIndex);

		Matrix4x4[] world = new Matrix4x4[model.Bones.Count];

		for (int i = 0; i < model.Bones.Count; i++)
		{
			NjBone bone = model.Bones[i];

			Vector3 position = bone.Position;
			Vector3 scale = bone.Scale;
			Matrix4x4 rotation = CreateRotation(bone.Rotation);

			if (tracksByBone.TryGetValue(i, out NjMotionTrack? track))
			{
				if (track.PositionKeys.Count > 0)
				{
					position = EvaluateVector(track.PositionKeys, frame, position);
				}

				if (track.ScaleKeys.Count > 0)
				{
					scale = EvaluateVector(track.ScaleKeys, frame, scale);
				}

				if (track.RotationKind == NjRotationKind.Quaternion && track.RotationKeys.Count > 0)
				{
					rotation = Matrix4x4.CreateFromQuaternion(EvaluateQuaternion(track.RotationKeys, frame));
				}
				else if (track.RotationKind == NjRotationKind.Euler32 && track.EulerRotationKeys.Count > 0)
				{
					rotation = CreateRotation(EvaluateVector(track.EulerRotationKeys, frame, bone.Rotation));
				}
			}

			Matrix4x4 local = Matrix4x4.CreateScale(scale) * rotation * Matrix4x4.CreateTranslation(position);
			Matrix4x4 parent = bone.ParentIndex >= 0 ? world[bone.ParentIndex] : Matrix4x4.Identity;

			world[i] = local * parent;
		}

		return world;
	}

	/// <summary>Weighted-blend a vertex's bone-local position into world
	/// space using the given world transforms (bind pose or animated —
	/// same math either way).</summary>
	public static Vector3 SkinPosition(NjVertex vertex, IReadOnlyList<Matrix4x4> worldTransforms)
	{
		if (vertex.Influences.Count == 0)
		{
			return vertex.Position;
		}

		Vector3 result = Vector3.Zero;
		float totalWeight = 0f;

		foreach (NjInfluence influence in vertex.Influences)
		{
			if (influence.Weight <= 0f || influence.BoneIndex < 0 || influence.BoneIndex >= worldTransforms.Count)
			{
				continue;
			}

			result += Vector3.Transform(vertex.Position, worldTransforms[influence.BoneIndex]) * influence.Weight;
			totalWeight += influence.Weight;
		}

		if (totalWeight <= 0.000001f)
		{
			return vertex.Position;
		}

		// Valid weighted vertices normally sum to 1.0. Only normalize when
		// close enough to be rounding error — don't silently reshape a
		// malformed vertex by force-normalizing an arbitrary sum.
		return MathF.Abs(totalWeight - 1f) < 0.02f ? result / totalWeight : result;
	}

	public static Vector3 SkinNormal(NjVertex vertex, IReadOnlyList<Matrix4x4> worldTransforms)
	{
		if (vertex.Influences.Count == 0)
		{
			return SafeNormalize(vertex.Normal);
		}

		Vector3 result = Vector3.Zero;
		float totalWeight = 0f;

		foreach (NjInfluence influence in vertex.Influences)
		{
			if (influence.Weight <= 0f || influence.BoneIndex < 0 || influence.BoneIndex >= worldTransforms.Count)
			{
				continue;
			}

			result += Vector3.TransformNormal(vertex.Normal, worldTransforms[influence.BoneIndex]) * influence.Weight;
			totalWeight += influence.Weight;
		}

		if (totalWeight > 0.000001f)
		{
			result /= totalWeight;
		}

		return SafeNormalize(result);
	}

	private static Vector3 EvaluateVector(IReadOnlyList<NjVectorKey> keys, float frame, Vector3 fallback) =>
		Interpolate(keys, frame, k => k.Frame, k => k.Value, Vector3.Lerp, fallback);

	private static Vector3 EvaluateVector(IReadOnlyList<NjEuler32Key> keys, float frame, Vector3 fallback) =>
		Interpolate(keys, frame, k => k.Frame, k => k.Value, Vector3.Lerp, fallback);

	private static Quaternion EvaluateQuaternion(IReadOnlyList<NjQuaternionKey> keys, float frame) =>
		Interpolate(keys, frame, k => k.Frame, k => k.Value, Quaternion.Slerp, Quaternion.Identity);

	/// <summary>Generic keyframe lookup + lerp: finds the surrounding pair
	/// of keys for `frame` and interpolates between them. Keys are assumed
	/// sorted by Frame ascending (true of everything NjMotionReader
	/// produces).</summary>
	private static T Interpolate<TKey, T>(
		IReadOnlyList<TKey> keys,
		float frame,
		Func<TKey, int> getFrame,
		Func<TKey, T> getValue,
		Func<T, T, float, T> lerp,
		T fallback)
	{
		if (keys.Count == 0)
		{
			return fallback;
		}

		if (keys.Count == 1 || frame <= getFrame(keys[0]))
		{
			return getValue(keys[0]);
		}

		if (frame >= getFrame(keys[^1]))
		{
			return getValue(keys[^1]);
		}

		for (int i = 0; i < keys.Count - 1; i++)
		{
			int frameA = getFrame(keys[i]);
			int frameB = getFrame(keys[i + 1]);

			if (frame < frameA || frame > frameB)
			{
				continue;
			}

			float t = frameB > frameA ? (frame - frameA) / (frameB - frameA) : 0f;

			return lerp(getValue(keys[i]), getValue(keys[i + 1]), t);
		}

		return getValue(keys[^1]);
	}

	private static Matrix4x4 CreateTransform(Vector3 position, Vector3 rotationRadians, Vector3 scale) =>
		Matrix4x4.CreateScale(scale) * CreateRotation(rotationRadians) * Matrix4x4.CreateTranslation(position);

	private static Matrix4x4 CreateRotation(Vector3 radians) =>
		Matrix4x4.CreateRotationX(radians.X) *
		Matrix4x4.CreateRotationY(radians.Y) *
		Matrix4x4.CreateRotationZ(radians.Z);

	private static Vector3 SafeNormalize(Vector3 v) =>
		v.LengthSquared() > 0.000001f ? Vector3.Normalize(v) : Vector3.UnitY;
}
