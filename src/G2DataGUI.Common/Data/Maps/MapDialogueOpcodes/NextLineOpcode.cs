using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcode;

public class NextLineOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.NextLine;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		NextLineOpcode opcode = new();
		return opcode;
	}
}
