using System.Collections.Generic;
using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapDialogue
{
    public MapDialogueHeader Header { get; set; }
    public List<byte[]> DialogueSections { get; set; } = new();

    public static MapDialogue ReadMapDialogue(Stream reader, uint dialogueSectionLength)
    {
        MapDialogue dialogue = new()
        {
            Header = MapDialogueHeader.ReadMapDialogueHeader(reader),
        };

        var dialogueStartPosition = reader.Position;
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
