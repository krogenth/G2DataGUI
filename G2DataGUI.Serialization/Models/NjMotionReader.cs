using System.Buffers.Binary;
using System.Numerics;
using G2DataGUI.Domain.Models;

namespace G2DataGUI.Serialization.Models;

/// <summary>
/// Reader for the NMDM/NJS_MOTION format. See PORTING-Chr.md for the full
/// verification history: the real NJS_MOTION.type is only the low 16 bits
/// of what earlier attempts treated as one 32-bit value (the high 16 bits
/// are a separate inp_fn field), and the motion table's per-bone row width
/// depends on which of POS_0/ANG_1/QUAT_1/SCL_2 are animated — verified by
/// reconstructing real archives' motion tables and confirming every
/// keyframe data region ends exactly at the table's own start offset with
/// zero gap or overlap. Any other NJD_MTYPE_* bit is refused explicitly
/// rather than guessed at.
///
/// NMDM parsing can still legitimately throw on real, unfamiliar data —
/// that's intentional (see NjMotion's doc comment): a caller should treat
/// a failed Read() as "this specific animation isn't understood yet," not
/// crash the whole archive load. Catch InvalidDataException/OverflowException
/// per-animation, not around the whole archive.
/// </summary>
public static class NjMotionReader
{
	// NJD_MTYPE_* bits actually observed and verified in real archives.
	private const ushort MTypePosition = 0x0001; // NJD_MTYPE_POS_0
	private const ushort MTypeAngle = 0x0002;    // NJD_MTYPE_ANG_1 (32-bit Euler)
	private const ushort MTypeScale = 0x0004;    // NJD_MTYPE_SCL_2
	private const ushort MTypeQuaternion = 0x2000; // NJD_MTYPE_QUAT_1
	private const ushort KnownChannelMask = MTypePosition | MTypeAngle | MTypeScale | MTypeQuaternion;

	private const float AngleScale = MathF.PI * 2.0f / 4294967296.0f;

	private enum Channel
	{
		Position,
		EulerAngle,
		Quaternion,
		Scale
	}

	public static NjMotion Read(byte[] nmdmPayload, int boneCount)
	{
		if (nmdmPayload.Length < 12)
		{
			throw new InvalidDataException("NMDM payload is too small.");
		}

		uint motionTableOffset = U32(nmdmPayload, 0);
		int frameCount = checked((int)U32(nmdmPayload, 4));
		uint type = U32(nmdmPayload, 8);
		ushort motionType = U16(nmdmPayload, 8);
		ushort interpFn = U16(nmdmPayload, 10);

		if ((motionType & ~KnownChannelMask) != 0)
		{
			throw new InvalidDataException(
				$"NMDM motion type 0x{motionType:X4} sets bits outside POS_0/ANG_1/QUAT_1/SCL_2 " +
				"(the only channel combination verified against real archive data so far) — " +
				"refusing to guess at this row layout rather than risk silently misreading it.");
		}

		bool hasPosition = (motionType & MTypePosition) != 0;
		bool hasAngle = (motionType & MTypeAngle) != 0;
		bool hasQuaternion = (motionType & MTypeQuaternion) != 0;
		bool hasScale = (motionType & MTypeScale) != 0;

		if (hasAngle && hasQuaternion)
		{
			throw new InvalidDataException(
				$"NMDM motion type 0x{motionType:X4} sets both NJD_MTYPE_ANG_1 and NJD_MTYPE_QUAT_1 — " +
				"only one rotation representation is expected at a time and this combination is unverified.");
		}

		List<Channel> channels = [];

		if (hasPosition)
		{
			channels.Add(Channel.Position);
		}

		if (hasAngle)
		{
			channels.Add(Channel.EulerAngle);
		}

		if (hasQuaternion)
		{
			channels.Add(Channel.Quaternion);
		}

		if (hasScale)
		{
			channels.Add(Channel.Scale);
		}

		if (channels.Count == 0)
		{
			throw new InvalidDataException($"NMDM motion type 0x{motionType:X4} has no recognized animated channels.");
		}

		if (motionTableOffset >= nmdmPayload.Length)
		{
			throw new InvalidDataException($"NMDM motion table offset 0x{motionTableOffset:X} is outside the payload.");
		}

		int tableOffset = checked((int)motionTableOffset);
		int channelCount = channels.Count;
		int rowBytes = channelCount * 8;
		int tableBytes = checked(boneCount * rowBytes);

		if (tableOffset + tableBytes > nmdmPayload.Length)
		{
			throw new InvalidDataException(
				$"NMDM motion table for {boneCount} bones ({channelCount} channel(s), {rowBytes} bytes/row) " +
				"exceeds the payload.");
		}

		var motion = new NjMotion
		{
			FrameCount = Math.Max(frameCount, 1),
			Type = type,
			MotionType = motionType,
			InterpolationFunction = interpFn
		};

		for (int bone = 0; bone < boneCount; bone++)
		{
			int rowStart = tableOffset + (bone * rowBytes);

			var offsets = new uint[channelCount];
			var counts = new uint[channelCount];

			for (int c = 0; c < channelCount; c++)
			{
				offsets[c] = U32(nmdmPayload, rowStart + (c * 4));
			}

			for (int c = 0; c < channelCount; c++)
			{
				counts[c] = U32(nmdmPayload, rowStart + (channelCount * 4) + (c * 4));
			}

			var track = new NjMotionTrack { BoneIndex = bone };

			for (int c = 0; c < channelCount; c++)
			{
				uint offset = offsets[c];
				uint count = counts[c];

				if (offset == 0 || count == 0)
				{
					continue;
				}

				switch (channels[c])
				{
					case Channel.Position:
						track.PositionKeys.AddRange(ReadVectorKeys(nmdmPayload, checked((int)offset), checked((int)count)));
						break;

					case Channel.EulerAngle:
						track.RotationKind = NjRotationKind.Euler32;
						track.EulerRotationKeys.AddRange(ReadEuler32Keys(nmdmPayload, checked((int)offset), checked((int)count)));
						break;

					case Channel.Quaternion:
						track.RotationKind = NjRotationKind.Quaternion;
						track.RotationKeys.AddRange(ReadQuaternionKeys(nmdmPayload, checked((int)offset), checked((int)count)));
						break;

					case Channel.Scale:
						track.ScaleKeys.AddRange(ReadVectorKeys(nmdmPayload, checked((int)offset), checked((int)count)));
						break;
				}
			}

			// A zeroed motion table entry is a valid "no animation for
			// this bone" entry — keep the track anyway so callers can see
			// full bone coverage.
			motion.Tracks.Add(track);
		}

		return motion;
	}

	private static IEnumerable<NjVectorKey> ReadVectorKeys(byte[] data, int offset, int count)
	{
		const int Stride = 16;
		Ensure(data, offset, checked(count * Stride));

		for (int i = 0; i < count; i++)
		{
			int p = offset + (i * Stride);

			yield return new NjVectorKey
			{
				Frame = checked((int)U32(data, p)),
				Value = new Vector3(F32(data, p + 4), F32(data, p + 8), F32(data, p + 12))
			};
		}
	}

	private static IEnumerable<NjQuaternionKey> ReadQuaternionKeys(byte[] data, int offset, int count)
	{
		const int Stride = 20;
		Ensure(data, offset, checked(count * Stride));

		for (int i = 0; i < count; i++)
		{
			int p = offset + (i * Stride);

			// Dreamcast Ninja quaternion keys are stored as W, X, Y, Z
			// after the frame number. System.Numerics.Quaternion's
			// constructor order is X, Y, Z, W — a frame-0 identity key is
			// (1,0,0,0) i.e. W=1, not X=1; treating the first value as X
			// rotates the whole skeleton 180° around X.
			float w = F32(data, p + 4);
			float x = F32(data, p + 8);
			float y = F32(data, p + 12);
			float z = F32(data, p + 16);

			var q = new Quaternion(x, y, z, w);
			q = q.LengthSquared() < 0.000001f ? Quaternion.Identity : Quaternion.Normalize(q);

			yield return new NjQuaternionKey
			{
				Frame = checked((int)U32(data, p)),
				Value = q
			};
		}
	}

	private static IEnumerable<NjEuler32Key> ReadEuler32Keys(byte[] data, int offset, int count)
	{
		const int Stride = 16;
		Ensure(data, offset, checked(count * Stride));

		for (int i = 0; i < count; i++)
		{
			int p = offset + (i * Stride);

			// Converted to radians here (BAMS turns * AngleScale) so
			// NjEuler32Key.Value is already usable — same convention as
			// NjBone.Rotation.
			yield return new NjEuler32Key
			{
				Frame = checked((int)U32(data, p)),
				Value = new Vector3(I32(data, p + 4), I32(data, p + 8), I32(data, p + 12)) * AngleScale
			};
		}
	}

	private static void Ensure(byte[] data, int offset, int length)
	{
		if (offset < 0 || length < 0 || offset > data.Length || length > data.Length - offset)
		{
			throw new InvalidDataException(
				$"NMDM keyframe range 0x{offset:X}+0x{length:X} is outside the payload (0x{data.Length:X}).");
		}
	}

	private static ushort U16(byte[] data, int offset) =>
		BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(offset, 2));

	private static uint U32(byte[] data, int offset) =>
		BinaryPrimitives.ReadUInt32LittleEndian(data.AsSpan(offset, 4));

	private static int I32(byte[] data, int offset) =>
		BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(offset, 4));

	private static float F32(byte[] data, int offset) =>
		BinaryPrimitives.ReadSingleLittleEndian(data.AsSpan(offset, 4));
}
