using System.Buffers.Binary;
using System.Numerics;
using G2DataGUI.Domain.Models;

namespace G2DataGUI.Serialization.Models;

/// <summary>
/// Reader for the Ninja NJCM model format. Operates directly on an NJCM
/// chunk's payload (ChrModel.NjcmChunk.Payload) — no outer chunk wrapper,
/// unlike the DreamcastChrViewer original this was ported from. See
/// PORTING-Chr.md for the verification history (FBX ground-truth
/// comparison against real archives): vertex chaining, the 0x2C/0x2B
/// unification, and triangle winding were all confirmed against a real
/// model's own exported geometry, not just internal self-consistency.
///
/// Deliberately does NOT bake world-space positions — NjVertex stays
/// bone-local (see NjBone's doc comment). That split is new in this port:
/// the original baked bind-pose world positions directly into each
/// vertex at read time, which only supports static viewing. Keeping
/// vertices local + weighted lets NjPoseEvaluator (Rendering layer)
/// re-skin against any animation frame, not just the bind pose.
/// </summary>
public static class NjModelReader
{
	private const byte VertexMin = 0x20;
	private const byte VertexPositionNormal = 0x29;
	private const byte VertexPositionNormalFlags = 0x2B;

	/// <summary>
	/// Not documented anywhere as a Ninja/PSO vertex format, but
	/// verified (against a real FBX export) to be byte-identical to 0x2B.
	/// </summary>
	private const byte VertexPositionNormalFlagsAlt = 0x2C;

	private const byte StripMin = 0x40;
	private const byte StripMax = 0x4B;

	private const byte TinyTid = 0x08;
	private const byte TinyTid2 = 0x09;

	private const byte MaterialMin = 0x10;
	private const byte MaterialMax = 0x17;

	private const byte EndChunk = 0xFF;

	public static NjModel Read(byte[] njcmPayload)
	{
		var model = new NjModel();

		int nextBoneIndex = 0;
		var vertexState = new NjVertexStateData();
		var sharedIdRemap = new Dictionary<int, int>();

		ReadObject(
			njcmPayload,
			0,
			Matrix4x4.Identity,
			model,
			new HashSet<int>(),
			ref nextBoneIndex,
			-1,
			vertexState,
			sharedIdRemap);

		ComputeBounds(model);

		return model;
	}

	private static void ReadObject(
		byte[] data,
		int objectOffset,
		Matrix4x4 parentWorldTransform,
		NjModel model,
		HashSet<int> visited,
		ref int nextBoneIndex,
		int parentBoneIndex,
		NjVertexStateData vertexState,
		Dictionary<int, int> sharedIdRemap)
	{
		int payloadLength = data.Length;

		if (!IsRangeValid(objectOffset, 52, payloadLength) || !visited.Add(objectOffset))
		{
			return;
		}

		int p = objectOffset;

		uint evalFlags = U32(data, p);
		uint modelOffset = U32(data, p + 4);

		Vector3 position = V3(data, p + 8);

		var rotationTurns = new Vector3(
			I32(data, p + 20),
			I32(data, p + 24),
			I32(data, p + 28));

		Vector3 scale = V3(data, p + 32);

		uint childOffset = U32(data, p + 44);
		uint siblingOffset = U32(data, p + 48);

		// Convert BAMS (32-bit turn units) to radians once, here, so
		// NjBone.Rotation is already usable radians for pose evaluation.
		Vector3 rotationRadians = rotationTurns * (MathF.PI / 2147483648f);

		Matrix4x4 localTransform = CreateNjTransform(position, rotationRadians, scale);
		Matrix4x4 worldTransform = localTransform * parentWorldTransform;

		int boneIndex = nextBoneIndex++;

		model.Bones.Add(new NjBone
		{
			Index = boneIndex,
			ParentIndex = parentBoneIndex,
			EvalFlags = evalFlags,
			Position = position,
			Rotation = rotationRadians,
			Scale = scale
		});

		if (modelOffset != 0 && IsRangeValid(checked((int)modelOffset), 24, payloadLength))
		{
			int meshPos = checked((int)modelOffset);

			uint vertexChunkOffset = U32(data, meshPos);
			uint polygonChunkOffset = U32(data, meshPos + 4);

			// See PORTING-Chr.md for the full history of this split.
			// BOTH present: self-contained mesh, local buffer. Only one
			// present: participates in the shared, persistent buffer.
			if (vertexChunkOffset != 0 && vertexChunkOffset < payloadLength &&
				polygonChunkOffset != 0 && polygonChunkOffset < payloadLength)
			{
				var localVertexState = new NjVertexStateData();

				ReadVertexChunkIntoState(data, checked((int)vertexChunkOffset), boneIndex, localVertexState);

				List<NjTriangleLocal> localTriangles = ReadPolygons(
					data, checked((int)polygonChunkOffset), localVertexState, boneIndex);

				var localIdRemap = new Dictionary<int, int>();
				MergeIntoModel(model, localVertexState, localTriangles, localIdRemap);
			}
			else
			{
				if (vertexChunkOffset != 0 && vertexChunkOffset < payloadLength)
				{
					ReadVertexChunkIntoState(data, checked((int)vertexChunkOffset), boneIndex, vertexState);
				}

				if (polygonChunkOffset != 0 && polygonChunkOffset < payloadLength)
				{
					List<NjTriangleLocal> sharedTriangles = ReadPolygons(
						data, checked((int)polygonChunkOffset), vertexState, boneIndex);

					MergeIntoModel(model, vertexState, sharedTriangles, sharedIdRemap);
				}
			}
		}

		if (childOffset != 0)
		{
			ReadObject(data, checked((int)childOffset), worldTransform, model, visited, ref nextBoneIndex, boneIndex, vertexState, sharedIdRemap);
		}

		if (siblingOffset != 0)
		{
			ReadObject(data, checked((int)siblingOffset), parentWorldTransform, model, visited, ref nextBoneIndex, parentBoneIndex, vertexState, sharedIdRemap);
		}
	}

	/// <summary>
	/// Copies whichever vertices a batch of local-id-referencing triangles
	/// actually needs into model.Vertices (deduplicated via idRemap, which
	/// is scoped to whichever NjVertexStateData produced these triangles —
	/// a fresh dictionary for each self-contained mesh's own local buffer,
	/// or one long-lived dictionary reused across every call that touches
	/// the shared buffer), remapping local NjVertexLocal.Id values to
	/// model-wide NjVertex.Id values along the way, then appends the
	/// triangles themselves using the remapped ids.
	///
	/// Vertex positions/normals are stored bone-local, exactly as read.
	/// The bone each vertex is weighted to already has its own bind-pose
	/// local transform in model.Bones (set in ReadObject); pose evaluation
	/// (Rendering layer) reconstructs world space from that later.
	/// </summary>
	private static void MergeIntoModel(
		NjModel model,
		NjVertexStateData vertexState,
		List<NjTriangleLocal> triangles,
		Dictionary<int, int> idRemap)
	{
		foreach (NjTriangleLocal triangle in triangles)
		{
			int a = GetOrAddVertex(model, vertexState, triangle.A, idRemap);
			int b = GetOrAddVertex(model, vertexState, triangle.B, idRemap);
			int c = GetOrAddVertex(model, vertexState, triangle.C, idRemap);

			model.Triangles.Add(new NjTriangle
			{
				A = a,
				B = b,
				C = c,
				UvA = triangle.UvA,
				UvB = triangle.UvB,
				UvC = triangle.UvC,
				HasUv = triangle.HasUv,
				TextureIndex = triangle.TextureIndex,
				TextureFlags = triangle.TextureFlags
			});
		}
	}

	private static int GetOrAddVertex(
		NjModel model,
		NjVertexStateData vertexState,
		int localId,
		Dictionary<int, int> idRemap)
	{
		if (idRemap.TryGetValue(localId, out int existingId))
		{
			return existingId;
		}

		NjVertexLocal local = vertexState.AllVertices[localId];

		var vertex = new NjVertex
		{
			Id = model.Vertices.Count,
			Position = local.Position,
			Normal = local.Normal,
			Flags = local.Flags
		};

		foreach ((int boneIdx, float weight) in local.Influences)
		{
			vertex.Influences.Add(new NjInfluence { BoneIndex = boneIdx, Weight = weight });
		}

		model.Vertices.Add(vertex);
		idRemap[localId] = vertex.Id;

		return vertex.Id;
	}

	private static void ReadVertexChunkIntoState(
		byte[] data,
		int offset,
		int boneIndex,
		NjVertexStateData state)
	{
		// A vertexChunkOffset can point to a CHAIN of one or more vertex
		// sub-chunks — see PORTING-Chr.md. Verified against a real
		// archive: further sub-chunks can follow immediately (zero byte
		// gap), ending in an explicit 0xFF terminator (the same value
		// ReadPolygons uses for "end of chunk list").
		int currentOffset = offset;

		while (IsRangeValid(currentOffset, 1, data.Length) && data[currentOffset] != EndChunk)
		{
			if (!TryReadVertexSubChunk(data, currentOffset, boneIndex, state, out int consumedBytes))
			{
				break;
			}

			currentOffset += consumedBytes;
		}
	}

	private static bool TryReadVertexSubChunk(
		byte[] data,
		int offset,
		int boneIndex,
		NjVertexStateData state,
		out int consumedBytes)
	{
		consumedBytes = 0;
		int payloadLength = data.Length;

		if (!IsRangeValid(offset, 8, payloadLength))
		{
			return false;
		}

		int p = offset;

		byte head = data[p];
		byte flags = data[p + 1];

		ushort vertexOffset = U16(data, p + 4);
		ushort vertexCount = U16(data, p + 6);

		int stride = GetVertexStride(head);

		if (stride == 0)
		{
			return false;
		}

		long vertexBytes = (long)vertexCount * stride;

		if (!IsRangeValid(offset + 8, checked((int)vertexBytes), payloadLength))
		{
			return false;
		}

		int q = p + 8;

		for (int i = 0; i < vertexCount; i++)
		{
			Vector3 position = V3(data, q);

			Vector3 normal =
				head == VertexPositionNormal ||
				head == VertexPositionNormalFlags ||
				head == VertexPositionNormalFlagsAlt
					? SafeNormalize(V3(data, q + 12))
					: Vector3.UnitY;

			if (head == VertexPositionNormalFlags || head == VertexPositionNormalFlagsAlt)
			{
				// NinjaFlags32-style: explicit slot = vertexOffset + ninjaOffset.
				ushort ninjaOffset = U16(data, q + 24);
				ushort weightValue = U16(data, q + 26);

				int slot = checked(vertexOffset + ninjaOffset);
				float weight = weightValue / 255.0f;

				int influenceSlot = flags switch
				{
					0x80 => 0,
					0x81 => 1,
					0x82 => 2,
					_ => -1
				};

				NjVertexLocal vertex;

				if (influenceSlot > 0 && state.CurrentSlots.TryGetValue(slot, out NjVertexLocal? previous))
				{
					// Reusing a weighted vertex slot creates a NEW vertex
					// snapshot, preserving prior influences and adding
					// this bone as another one — overwriting instead
					// causes visible pinching artifacts.
					vertex = previous.Clone(state.NextId++);
				}
				else
				{
					vertex = new NjVertexLocal(state.NextId++, position, normal);
				}

				vertex.Position = position;
				vertex.Normal = normal;
				vertex.Flags = flags;

				if (influenceSlot == 0)
				{
					vertex.Influences.Clear();
				}

				if (influenceSlot >= 0)
				{
					vertex.SetInfluence(influenceSlot, boneIndex, weight);
				}
				else
				{
					vertex.Influences.Clear();
					vertex.SetInfluence(0, boneIndex, 1.0f);
				}

				state.CurrentSlots[slot] = vertex;
				state.AllVertices[vertex.Id] = vertex;
			}
			else
			{
				int slot = checked(vertexOffset + i);

				var vertex = new NjVertexLocal(state.NextId++, position, normal);

				// Ordinary NJ vertices are implicitly weighted 1.0 to the
				// bone/object that defines the vertex chunk.
				vertex.SetInfluence(0, boneIndex, 1.0f);
				vertex.Flags = flags;

				state.CurrentSlots[slot] = vertex;
				state.AllVertices[vertex.Id] = vertex;
			}

			q += stride;
		}

		consumedBytes = 8 + checked((int)vertexBytes);
		return true;
	}

	private static List<NjTriangleLocal> ReadPolygons(
		byte[] data,
		int offset,
		NjVertexStateData vertexState,
		int boneIndex)
	{
		var triangles = new List<NjTriangleLocal>();
		int payloadLength = data.Length;

		if (!IsRangeValid(offset, 2, payloadLength))
		{
			return triangles;
		}

		int p = offset;
		int end = payloadLength;

		int textureIndex = -1;
		byte textureFlags = 0;

		while (p + 2 <= end)
		{
			byte head = data[p];

			if (head == EndChunk)
			{
				break;
			}

			// Tiny texture chunk: [head][flags][texture id]. Low 13 bits
			// are the texture-list index.
			if (head == TinyTid || head == TinyTid2)
			{
				if (p + 4 > end)
				{
					break;
				}

				textureFlags = data[p + 1];
				ushort textureData = U16(data, p + 2);
				textureIndex = textureData & 0x1FFF;

				p += 4;
				continue;
			}

			// Material chunk: [head][flags][size:ushort][data...], size in
			// 16-bit words.
			if (head >= MaterialMin && head <= MaterialMax)
			{
				if (p + 4 > end)
				{
					break;
				}

				ushort size = U16(data, p + 2);
				int bytes = checked(4 + (size * 2));

				if (p + bytes > end)
				{
					break;
				}

				p += bytes;
				continue;
			}

			// A vertex chunk can legally occur in a polygon stream — it
			// updates the active vertex slots used by subsequent strips.
			if (head >= VertexMin && head < StripMin)
			{
				int stride = GetVertexStride(head);

				if (stride == 0 || p + 8 > end)
				{
					break;
				}

				ushort vertexCount = U16(data, p + 6);
				int bytes = checked(8 + (vertexCount * stride));

				if (p + bytes > end)
				{
					break;
				}

				ReadVertexChunkIntoState(data, p, boneIndex, vertexState);

				p += bytes;
				continue;
			}

			if (head >= StripMin && head <= StripMax)
			{
				if (p + 6 > end)
				{
					break;
				}

				ushort chunkLength = U16(data, p + 2);
				ushort stripInfo = U16(data, p + 4);

				int stripCount = stripInfo & 0x3FFF;
				int userOffset = stripInfo >> 14;

				// NJ strip chunks use an 8-byte prefix for the length
				// calculation (head+flags+length+stripInfo), plus
				// length*2 bytes of payload including the 4-byte
				// FF 00 00 00 terminator.
				long chunkEndLong = (long)p + 8 + (chunkLength * 2L);

				if (chunkEndLong > payloadLength)
				{
					break;
				}

				int chunkEnd = checked((int)chunkEndLong);

				bool hasUv = head >= 0x41;
				float uvScale = head == 0x41 ? 255.0f : 1023.0f;

				int q = p + 6;

				for (int stripIndex = 0; stripIndex < stripCount && q + 2 <= chunkEnd; stripIndex++)
				{
					short signedLength = I16(data, q);
					q += 2;

					int vertexCount = Math.Abs(signedLength);

					if (vertexCount < 3)
					{
						break;
					}

					var strip = new StripVertex[vertexCount];

					for (int i = 0; i < vertexCount; i++)
					{
						if (q + 2 > chunkEnd)
						{
							break;
						}

						int index = U16(data, q);
						q += 2;

						Vector2 uv = Vector2.Zero;

						if (hasUv)
						{
							if (q + 4 > chunkEnd)
							{
								break;
							}

							ushort u = U16(data, q);
							ushort v = U16(data, q + 2);
							q += 4;

							uv = new Vector2(u / uvScale, v / uvScale);
						}

						strip[i] = new StripVertex(index, uv);

						// User data begins after the third vertex and
						// occurs after every subsequent vertex.
						if (userOffset != 0 && i >= 2)
						{
							int userBytes = userOffset * 2;

							if (q + userBytes > chunkEnd)
							{
								q = chunkEnd;
								break;
							}

							q += userBytes;
						}
					}

					AddStripTriangles(triangles, strip, signedLength >= 0, vertexState, textureIndex, textureFlags, hasUv);
				}

				p = chunkEnd;
				continue;
			}

			// Bits chunks (0x04/0x05) are state/control chunks — two-byte
			// commands, no geometry for static viewing.
			p += 2;
		}

		return triangles;
	}

	private static void AddStripTriangles(
		List<NjTriangleLocal> output,
		StripVertex[] strip,
		bool clockwise,
		NjVertexStateData vertexState,
		int textureIndex,
		byte textureFlags,
		bool hasUv)
	{
		for (int i = 2; i < strip.Length; i++)
		{
			StripVertex a;
			StripVertex b;
			StripVertex c;

			// Ninja alternates winding for successive strip triangles.
			if ((clockwise && (i & 1) == 0) || (!clockwise && (i & 1) != 0))
			{
				a = strip[i - 2];
				b = strip[i];
				c = strip[i - 1];
			}
			else
			{
				a = strip[i - 2];
				b = strip[i - 1];
				c = strip[i];
			}

			if (!vertexState.CurrentSlots.ContainsKey(a.Index) ||
				!vertexState.CurrentSlots.ContainsKey(b.Index) ||
				!vertexState.CurrentSlots.ContainsKey(c.Index))
			{
				continue;
			}

			if (a.Index == b.Index || b.Index == c.Index || a.Index == c.Index)
			{
				continue;
			}

			// B/C swapped from the "natural" strip order — verified
			// against a real FBX export's own triangle winding: 175
			// triangles sampled across 5 different meshes, every single
			// one came out reversed relative to Neosis's own output with
			// zero exceptions before this swap. See PORTING-Chr.md.
			output.Add(new NjTriangleLocal(
				vertexState.CurrentSlots[a.Index].Id,
				vertexState.CurrentSlots[c.Index].Id,
				vertexState.CurrentSlots[b.Index].Id,
				a.Uv,
				c.Uv,
				b.Uv,
				textureIndex,
				textureFlags,
				hasUv));
		}
	}

	/// <summary>
	/// Takes rotation already in RADIANS — the BAMS-to-radians
	/// conversion happens once in ReadObject (see NjBone.Rotation's doc
	/// comment), not here.
	/// </summary>
	private static Matrix4x4 CreateNjTransform(Vector3 position, Vector3 rotationRadians, Vector3 scale)
	{
		Matrix4x4 rotationMatrix =
			Matrix4x4.CreateRotationX(rotationRadians.X) *
			Matrix4x4.CreateRotationY(rotationRadians.Y) *
			Matrix4x4.CreateRotationZ(rotationRadians.Z);

		return
			Matrix4x4.CreateScale(scale) *
			rotationMatrix *
			Matrix4x4.CreateTranslation(position);
	}

	private static int GetVertexStride(byte head) =>
		head switch
		{
			// x,y,z
			>= 0x20 and <= 0x28 => 12,

			// x,y,z,nx,ny,nz
			>= 0x29 and <= 0x2A => 24,

			// x,y,z,nx,ny,nz,ninjaOffset,weight
			0x2B => 28,

			// Verified (against an FBX export of the real model) to be
			// byte-identical to 0x2B despite the different head byte —
			// see PORTING-Chr.md.
			0x2C => 28,

			_ => 0
		};

	/// <summary>
	/// Bind-pose bounding sphere. Uses each vertex's FIRST influence's
	/// bind-pose bone transform — good enough for camera framing (the
	/// only consumer), not meant to be exact for heavily multi-influence
	/// meshes.
	/// </summary>
	private static void ComputeBounds(NjModel model)
	{
		if (model.Vertices.Count == 0)
		{
			model.Center = Vector3.Zero;
			model.Radius = 1.0f;
			return;
		}

		Matrix4x4[] boneWorldTransforms = ComputeBindPoseWorldTransforms(model);

		var min = new Vector3(float.MaxValue);
		var max = new Vector3(float.MinValue);

		foreach (NjVertex vertex in model.Vertices)
		{
			Vector3 worldPosition = SkinBindPosition(vertex, boneWorldTransforms);

			min = Vector3.Min(min, worldPosition);
			max = Vector3.Max(max, worldPosition);
		}

		model.Center = (min + max) * 0.5f;
		model.Radius = Math.Max((max - min).Length() * 0.5f, 0.001f);
	}

	private static Matrix4x4[] ComputeBindPoseWorldTransforms(NjModel model)
	{
		var transforms = new Matrix4x4[model.Bones.Count];

		for (int i = 0; i < model.Bones.Count; i++)
		{
			NjBone bone = model.Bones[i];
			Matrix4x4 local = CreateNjTransform(bone.Position, bone.Rotation, bone.Scale);

			transforms[i] = bone.ParentIndex >= 0
				? local * transforms[bone.ParentIndex]
				: local;
		}

		return transforms;
	}

	private static Vector3 SkinBindPosition(NjVertex vertex, Matrix4x4[] boneWorldTransforms)
	{
		if (vertex.Influences.Count == 0)
		{
			return vertex.Position;
		}

		NjInfluence influence = vertex.Influences[0];

		if (influence.BoneIndex < 0 || influence.BoneIndex >= boneWorldTransforms.Length)
		{
			return vertex.Position;
		}

		return Vector3.Transform(vertex.Position, boneWorldTransforms[influence.BoneIndex]);
	}

	private static bool IsRangeValid(int offset, int length, int total) =>
		offset >= 0 && length >= 0 && offset <= total && length <= total - offset;

	private static Vector3 SafeNormalize(Vector3 v)
	{
		float lengthSquared = v.LengthSquared();

		return lengthSquared > 1e-12f ? v / MathF.Sqrt(lengthSquared) : Vector3.UnitY;
	}

	private static ushort U16(byte[] data, int offset) =>
		BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(offset, 2));

	private static short I16(byte[] data, int offset) =>
		BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(offset, 2));

	private static uint U32(byte[] data, int offset) =>
		BinaryPrimitives.ReadUInt32LittleEndian(data.AsSpan(offset, 4));

	private static int I32(byte[] data, int offset) =>
		BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(offset, 4));

	private static float F32(byte[] data, int offset) =>
		BinaryPrimitives.ReadSingleLittleEndian(data.AsSpan(offset, 4));

	private static Vector3 V3(byte[] data, int offset) =>
		new(F32(data, offset), F32(data, offset + 4), F32(data, offset + 8));
}
