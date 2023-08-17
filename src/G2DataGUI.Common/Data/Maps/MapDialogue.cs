using System.Collections.Generic;
using System.IO;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;
using G2DataGUI.IO.Streams;
using System.Linq;

namespace G2DataGUI.Common.Data.Maps;
public class MapDialogue
{
    public MapDialogueHeader Header { get; set; }
    public List<byte[]> DialogueSections { get; set; } = new();
	public List<List<IMapDialogueOpcode>> DialogueSectionOpcodes { get; set; } = new();

    public static MapDialogue ReadMapDialogue(Stream reader, uint dialogueSectionLength)
    {
        MapDialogue dialogue = new()
        {
            Header = MapDialogueHeader.ReadMapDialogueHeader(reader),
        };

		var dialogueStartPosition = reader.Position;
		for (var index = 1; index < dialogue.Header.Offsets.Count; index++)
		{
			dialogue.DialogueSectionOpcodes.Add(new List<IMapDialogueOpcode>());
			var length = (index == dialogue.Header.Offsets.Count - 1) ?
				(dialogueSectionLength - dialogue.Header.HeaderLength - (dialogue.Header.Offsets[index - 1].Offset * 8)) :
				(dialogue.Header.Offsets[index].Offset - dialogue.Header.Offsets[index - 1].Offset) * 8;
			using MemoryStream memReader = new();

			// write section to memory stream to force cutoff from overrunning opcodes
			memReader.Write(reader.ReadRawByteArray((uint)length));
			memReader.Seek(0, SeekOrigin.Begin);
			while (memReader.Position < memReader.Length)
			{
				var opcode = IMapDialogueOpcode.ParseNextOpcode(memReader);
				if (opcode != null)
				{
					dialogue.DialogueSectionOpcodes.Last().Add(opcode);
				}
			}
		}

		//var dialogueStartPosition = reader.Position;
		reader.Seek(dialogueStartPosition, SeekOrigin.Begin);
		//var dialogueStartPosition = reader.Position;
		for (var index = 1; index < dialogue.Header.Offsets.Count; index++)
		{
			var length = (index == dialogue.Header.Offsets.Count - 1) ?
				(dialogueSectionLength - dialogue.Header.HeaderLength - (dialogue.Header.Offsets[index - 1].Offset * 8)) :
				(dialogue.Header.Offsets[index].Offset - dialogue.Header.Offsets[index - 1].Offset) * 8;
			reader.Seek(dialogueStartPosition + (dialogue.Header.Offsets[index - 1].Offset * 8), SeekOrigin.Begin);
			dialogue.DialogueSections.Add(reader.ReadRawByteArray((uint)length));
		}

		return dialogue;
    }
}
