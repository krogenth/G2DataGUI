using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown8Opcode : IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown8;

	public static IMapDialogueOpcode ReadOpcode(Stream _)
	{
		Unknown8Opcode opcode = new();
		return opcode;
	}
}
