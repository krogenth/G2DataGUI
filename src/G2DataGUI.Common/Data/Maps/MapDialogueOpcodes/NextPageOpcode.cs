using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcode;

public class NextPageOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.NextPage;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		NextPageOpcode opcode = new();
		return opcode;
	}
}
