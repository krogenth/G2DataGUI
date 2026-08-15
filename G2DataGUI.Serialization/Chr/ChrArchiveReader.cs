using System.Buffers.Binary;
using System.Text;
using G2DataGUI.Domain.Chr;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Chr;

/// <summary>
/// Reads one .chr/.chr2 archive: every chunk retained losslessly, then the
/// Models/MotionSets association built on top. Ported from
/// DreamcastChrViewer's ChrReader.cs, verified chunk-walk-for-chunk-walk
/// against a real archive (a430.chr — 115 chunks, ends exactly at EOF,
/// MIXL payloads match the documented [model][count][ids...]FF layout
/// exactly) before porting — see PORTING-Chr.md. NJCM/GIXL/NMDM payloads
/// are kept as opaque bytes; nothing here interprets Ninja geometry or
/// motion data.
///
/// Does not set ChrArchive.FileName — same convention as Enemy/Boss's
/// FilePath, which the repository sets after reading, not the reader.
/// </summary>
public sealed class ChrArchiveReader : IDataReader<ChrArchive>
{
	public ChrArchive Read(Stream stream)
	{
		ChrArchive archive = new();

		ParseChunks(stream, archive);

		if (archive.Chunks.Count == 0 || archive.Chunks[0].FourCC != "CDDH")
		{
			throw new InvalidDataException("The file does not begin with a CDDH chunk.");
		}

		ParseCddh(archive);
		ParseModels(archive);
		ParseMotionSection(archive);

		return archive;
	}

	public async Task<ChrArchive> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);

	private static void ParseChunks(Stream stream, ChrArchive archive)
	{
		while (stream.Position + 8 <= stream.Length)
		{
			int offset = (int)stream.Position;
			string fourCC = Encoding.ASCII.GetString(stream.ReadRawByteArray(4));
			uint payloadLength = stream.ReadRawUInt();

			if (payloadLength > stream.Length - stream.Position)
			{
				throw new InvalidDataException(
					$"Invalid chunk {fourCC} at 0x{offset:X}: " +
					$"payload length 0x{payloadLength:X} exceeds the file.");
			}

			byte[] payload = stream.ReadRawByteArray(payloadLength);

			archive.Chunks.Add(new ChrChunk
			{
				Offset = offset,
				FourCC = fourCC,
				PayloadLength = payloadLength,
				Payload = payload
			});
		}

		if (stream.Position != stream.Length)
		{
			throw new InvalidDataException(
				$"Archive ended with {stream.Length - stream.Position} unparsed bytes.");
		}
	}

	private static void ParseCddh(ChrArchive archive)
	{
		ChrChunk cddh = archive.Chunks[0];

		if (cddh.Payload.Length < 12)
		{
			throw new InvalidDataException("CDDH payload is too small.");
		}

		archive.Header = new CddhInfo
		{
			Version = U32(cddh.Payload, 0),
			ModelSectionPointer = U32(cddh.Payload, 4),
			MotionSectionPointer = U32(cddh.Payload, 8)
		};
	}

	private static void ParseModels(ChrArchive archive)
	{
		int modelIndex = 0;

		for (int i = 0; i < archive.Chunks.Count; i++)
		{
			ChrChunk chunk = archive.Chunks[i];

			if (chunk.FourCC != "GIXL")
			{
				continue;
			}

			// In these archives GIXL is immediately followed by NJCM.
			if (i + 1 >= archive.Chunks.Count || archive.Chunks[i + 1].FourCC != "NJCM")
			{
				continue;
			}

			ChrChunk njcm = archive.Chunks[i + 1];

			ChrModel model = new()
			{
				Index = modelIndex++,
				GixlChunk = chunk,
				NjcmChunk = njcm
			};

			ParseGixl(chunk, model);

			// Preserve the chunks belonging to the model until the next GIXL
			// or the beginning of the motion section.
			for (int j = i + 2; j < archive.Chunks.Count; j++)
			{
				ChrChunk next = archive.Chunks[j];

				if (next.FourCC == "GIXL" || next.FourCC == "GPHD")
				{
					break;
				}

				model.AssociatedChunks.Add(next);
			}

			archive.Models.Add(model);
		}
	}

	private static void ParseGixl(ChrChunk chunk, ChrModel model)
	{
		byte[] payload = chunk.Payload;

		if (payload.Length < 4)
		{
			return;
		}

		uint count = U32(payload, 0);

		if (count > 0x1000 || 4 + (count * 4) > payload.Length)
		{
			return;
		}

		for (int i = 0; i < count; i++)
		{
			model.GlobalIdentifiers.Add(U32(payload, 4 + (i * 4)));
		}
	}

	private static void ParseMotionSection(ChrArchive archive)
	{
		int gphdIndex = archive.Chunks.FindIndex(chunk => chunk.FourCC == "GPHD");

		if (gphdIndex < 0)
		{
			return;
		}

		MotionSet? currentSet = null;
		int animationIndex = 0;

		for (int i = gphdIndex + 1; i < archive.Chunks.Count; i++)
		{
			ChrChunk chunk = archive.Chunks[i];

			if (chunk.FourCC == "MIXL")
			{
				currentSet = ParseMixl(chunk);
				archive.MotionSets.Add(currentSet);

				animationIndex = 0;

				archive.FindModel(currentSet.ModelIndex)?.Animations.Clear();

				continue;
			}

			if (chunk.FourCC != "NMDM" || currentSet is null)
			{
				continue;
			}

			ChrChunk? gamt = null;
			ChrChunk? pof0 = null;

			// The normal pattern is: GAMT -> NMDM -> POF0.
			if (i > gphdIndex + 1 && archive.Chunks[i - 1].FourCC == "GAMT")
			{
				gamt = archive.Chunks[i - 1];
			}

			if (i + 1 < archive.Chunks.Count && archive.Chunks[i + 1].FourCC == "POF0")
			{
				pof0 = archive.Chunks[i + 1];
			}

			int? identifier = animationIndex < currentSet.MotionIdentifiers.Count
				? currentSet.MotionIdentifiers[animationIndex]
				: null;

			MotionAnimation animation = new()
			{
				Index = animationIndex++,
				ModelIndex = currentSet.ModelIndex,
				GamtChunk = gamt,
				NmdmChunk = chunk,
				Pof0Chunk = pof0,
				Identifier = identifier
			};

			currentSet.Animations.Add(animation);

			archive.FindModel(currentSet.ModelIndex)?.Animations.Add(animation);
		}
	}

	private static MotionSet ParseMixl(ChrChunk chunk)
	{
		byte[] payload = chunk.Payload;

		if (payload.Length == 0)
		{
			throw new InvalidDataException("Empty MIXL chunk.");
		}

		MotionSet result = new()
		{
			ModelIndex = payload[0],
			MixlChunk = chunk
		};

		// Observed form: [model index] [count] [id...] FF
		if (payload.Length >= 2)
		{
			int count = payload[1];

			for (int i = 0; i < count && 2 + i < payload.Length; i++)
			{
				byte id = payload[2 + i];

				if (id == 0xFF)
				{
					break;
				}

				result.MotionIdentifiers.Add(id);
			}
		}

		return result;
	}

	private static uint U32(byte[] bytes, int position) =>
		BinaryPrimitives.ReadUInt32LittleEndian(bytes.AsSpan(position, 4));
}
