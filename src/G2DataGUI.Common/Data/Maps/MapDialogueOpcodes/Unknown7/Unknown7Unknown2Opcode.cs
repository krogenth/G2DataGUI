using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown7;
public class Unknown7Unknown2Opcode : IUnknown7Opcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown7;
	public Unknown7Option Option { get; set; } = Unknown7Option.Unknown2;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown7Unknown1Opcode opcode = new();
		return opcode;
	}
}
