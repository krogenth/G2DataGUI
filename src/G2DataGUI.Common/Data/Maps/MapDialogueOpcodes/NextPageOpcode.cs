using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class NextPageOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.NextPage;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		NextPageOpcode opcode = new();
		return opcode;
	}
}
