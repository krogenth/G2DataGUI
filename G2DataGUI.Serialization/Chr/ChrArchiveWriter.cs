using System.Buffers.Binary;
using System.Text;
using G2DataGUI.Domain.Chr;

namespace G2DataGUI.Serialization.Chr;

/// <summary>
/// Writes a ChrArchive back out to a .chr/.chr2 file, reconstructed from
/// its Models/MotionSets rather than a byte-for-byte copy of the original —
/// this is what makes moving a model between archives possible at all.
///
/// The load-bearing fact that makes this SAFE without understanding every
/// byte of NJCM/NMDM: every internal pointer inside a chunk's payload
/// (NJS_Object child/sibling/model offsets, NMDM's own vertex-table offset,
/// strip data, etc.) is relative to THAT PAYLOAD's own start, not to an
/// absolute file position — verified repeatedly while reverse-engineering
/// these formats (see PORTING-Chr.md). That means a chunk's payload bytes
/// can be copied verbatim to a different position in a different file and
/// every internal reference inside it stays correct — only the OUTER
/// chunk sequence (which chunks exist, in what order) needs rebuilding,
/// never the payload contents themselves.
///
/// POF0 is the one exception worth flagging: it's still not understood
/// well enough to say with certainty it never encodes anything
/// file-absolute (see PORTING-Chr.md's original caveat on this). It's
/// carried over verbatim alongside its NJCM/NMDM, which is correct if its
/// own contents are payload-relative like everything else observed so
/// far — this hasn't been round-trip tested (write → read back → same
/// result) against a real archive, so treat it as a reasoned first
/// implementation, not a verified one.
///
/// CDDH's two pointer fields ARE recomputed (not copied) since they're
/// cheap to get right: ModelSectionPointer is always the offset of the
/// first chunk after CDDH, MotionSectionPointer is GPHD's offset if a
/// motion section exists.
/// </summary>
public sealed class ChrArchiveWriter
{
	public void Write(Stream stream, ChrArchive archive)
	{
		List<ChrChunk> outputChunks = BuildChunkSequence(archive);

		// CDDH is always first and always 12 bytes (version + 2 pointers).
		int cddhTotalSize = 8 + 12;

		int modelSectionOffset = cddhTotalSize;

		int motionSectionOffset = 0;
		int runningOffset = cddhTotalSize;

		foreach (ChrChunk chunk in outputChunks)
		{
			if (chunk.FourCC == "GPHD" && motionSectionOffset == 0)
			{
				motionSectionOffset = runningOffset;
			}

			runningOffset += 8 + chunk.Payload.Length;
		}

		byte[] cddhPayload = new byte[12];

		BinaryPrimitives.WriteUInt32LittleEndian(
			cddhPayload.AsSpan(0, 4),
			archive.Header?.Version ?? 0);

		BinaryPrimitives.WriteUInt32LittleEndian(
			cddhPayload.AsSpan(4, 4),
			(uint)modelSectionOffset);

		BinaryPrimitives.WriteUInt32LittleEndian(
			cddhPayload.AsSpan(8, 4),
			(uint)motionSectionOffset);

		WriteChunk(stream, "CDDH", cddhPayload);

		foreach (ChrChunk chunk in outputChunks)
		{
			WriteChunk(stream, chunk.FourCC, chunk.Payload);
		}
	}

	/// <summary>
	/// Reconstructs the full chunk sequence (everything after
	/// CDDH) from Models/MotionSets: each model's GIXL, NJCM, and
	/// associated chunks in order, then (if any motion data exists) the
	/// original GPHD marker followed by each motion set's MIXL and its
	/// animations' GAMT/NMDM/POF0 chunks.
	/// </summary>
	private static List<ChrChunk> BuildChunkSequence(ChrArchive archive)
	{
		List<ChrChunk> chunks = [];

		foreach (ChrModel model in archive.Models)
		{
			chunks.Add(model.GixlChunk);
			chunks.Add(model.NjcmChunk);
			chunks.AddRange(model.AssociatedChunks);
		}

		if (archive.MotionSets.Count > 0)
		{
			ChrChunk? gphd = archive.Chunks.FirstOrDefault(c => c.FourCC == "GPHD");

			if (gphd is not null)
			{
				chunks.Add(gphd);
			}

			foreach (MotionSet motionSet in archive.MotionSets)
			{
				chunks.Add(motionSet.MixlChunk);

				foreach (MotionAnimation animation in motionSet.Animations)
				{
					if (animation.GamtChunk is not null)
					{
						chunks.Add(animation.GamtChunk);
					}

					chunks.Add(animation.NmdmChunk);

					if (animation.Pof0Chunk is not null)
					{
						chunks.Add(animation.Pof0Chunk);
					}
				}
			}
		}

		return chunks;
	}

	private static void WriteChunk(Stream stream, string fourCC, byte[] payload)
	{
		Span<byte> header = stackalloc byte[8];

		Encoding.ASCII.GetBytes(fourCC, header[..4]);

		BinaryPrimitives.WriteUInt32LittleEndian(
			header[4..8],
			(uint)payload.Length);

		stream.Write(header);
		stream.Write(payload);
	}
}
