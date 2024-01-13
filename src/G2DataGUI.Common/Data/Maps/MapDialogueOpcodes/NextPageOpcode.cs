using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class NextPageOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.NextPage;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new NextPageOpcode();
	}
}
