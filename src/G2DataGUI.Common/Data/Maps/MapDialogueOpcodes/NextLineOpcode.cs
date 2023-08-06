using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class NextLineOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.NextLine;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		NextLineOpcode opcode = new();
		return opcode;
	}
}
