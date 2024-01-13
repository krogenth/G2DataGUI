using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown1BOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown1B;

	public static IMapDialogueOpcode ReadOpcode(Stream _)
	{
		return new Unknown1BOpcode();
	}
}
