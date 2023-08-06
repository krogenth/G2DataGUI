using System.Collections.Generic;
using System.IO;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;
using G2DataGUI.IO.Streams;
using G2DataGUI.Common.Extensions;
using System.Linq;

namespace G2DataGUI.Common.Data.Maps;
public class MapDialogue
{
    public MapDialogueHeader Header { get; set; }
    public List<byte[]> DialogueSections { get; set; } = new();

    public static MapDialogue ReadMapDialogue(Stream reader, uint dialogueSectionLength, string mapname)
    {
        MapDialogue dialogue = new()
        {
            Header = MapDialogueHeader.ReadMapDialogueHeader(reader),
        };

		// var dialogueStartPosition = reader.Position;
		// for (var index = 1; index < dialogue.Header.Offsets.Count; index++)
		// {
		// 	var length = (index == dialogue.Header.Offsets.Count - 1) ?
		// 		(dialogueSectionLength - dialogue.Header.HeaderLength - (dialogue.Header.Offsets[index - 1].Offset * 8)) :
		// 		(dialogue.Header.Offsets[index].Offset - dialogue.Header.Offsets[index - 1].Offset) * 8;
		// 	using MemoryStream memReader = new();

		// 	// write section to memory stream to force cutoff from overrunning opcodes
		// 	memReader.Write(reader.ReadRawByteArray((uint)length));
		// 	memReader.Seek(0, SeekOrigin.Begin);
		// 	while (memReader.Position < memReader.Length)
		// 	{
		// 		var opcode = IMapDialogueOpcode.ParseNextOpcode(memReader);
		// 	}
		// }

		// if (!Directory.Exists("dialogue_sections"))
		// {
		// 	Directory.CreateDirectory("dialogue_sections");
		// }

		// if (!Directory.Exists($"dialogue_sections/{mapname}"))
		// {
		// 	Directory.CreateDirectory($"dialogue_sections/{mapname}");
		// }

		var dialogueStartPosition = reader.Position;
		reader.Seek(dialogueStartPosition, SeekOrigin.Begin);
		//var dialogueStartPosition = reader.Position;
		for (var index = 1; index < dialogue.Header.Offsets.Count; index++)
		{
			// using var file = File.Open($"dialogue_sections/{mapname}/{index}.dlg", FileMode.OpenOrCreate, FileAccess.Write);
			var length = (index == dialogue.Header.Offsets.Count - 1) ?
				(dialogueSectionLength - dialogue.Header.HeaderLength - (dialogue.Header.Offsets[index - 1].Offset * 8)) :
				(dialogue.Header.Offsets[index].Offset - dialogue.Header.Offsets[index - 1].Offset) * 8;
			reader.Seek(dialogueStartPosition + (dialogue.Header.Offsets[index - 1].Offset * 8), SeekOrigin.Begin);
			dialogue.DialogueSections.Add(reader.ReadRawByteArray((uint)length));
			// file.WriteRawByteArray(dialogue.DialogueSections.Last());
		}

		return dialogue;
    }
}
