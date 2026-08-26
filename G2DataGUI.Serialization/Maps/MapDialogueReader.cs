using G2DataGUI.Domain.Maps;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Read-only, deliberately — the source this was ported from never wrote
/// the dialogue section back either (Map.WriteMap doesn't touch
/// Header.OffsetDialogue/Dialogue at all). That's a genuine limitation
/// carried forward, not an oversight: reconstructing this section
/// correctly would mean recomputing HeaderLength and every offset from
/// whatever the sections end up being, and nothing here has verified what
/// that recomputation should look like. Read-only until someone does.
/// </summary>
public static class MapDialogueReader
{
	public static MapDialogue Read(Stream stream, uint dialogueSectionLength)
	{
		MapDialogue dialogue = new()
		{
			Header = ReadHeader(stream),
		};

		long dialogueStartPosition = stream.Position;

		for (int index = 1; index < dialogue.Header.Offsets.Count; index++)
		{
			uint length = index == dialogue.Header.Offsets.Count - 1
				? dialogueSectionLength - dialogue.Header.HeaderLength - (uint)(dialogue.Header.Offsets[index - 1].Offset * 8)
				: (uint)((dialogue.Header.Offsets[index].Offset - dialogue.Header.Offsets[index - 1].Offset) * 8);

			stream.Seek(dialogueStartPosition + (dialogue.Header.Offsets[index - 1].Offset * 8), SeekOrigin.Begin);
			dialogue.DialogueSections.Add(stream.ReadRawByteArray(length));
		}

		return dialogue;
	}

	private static MapDialogueHeader ReadHeader(Stream stream)
	{
		MapDialogueHeader header = new()
		{
			HeaderLength = stream.ReadRawUInt(),
		};

		for (int index = 0; index < (header.HeaderLength - 4) / 4; index++)
		{
			header.Offsets.Add(new MapDialogueOffset
			{
				Unknown1 = stream.ReadRawUShort(),
				Offset = stream.ReadRawUShort(),
			});
		}

		return header;
	}
}
