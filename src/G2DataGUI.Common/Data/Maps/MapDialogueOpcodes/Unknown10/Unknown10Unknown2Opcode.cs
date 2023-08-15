using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown10;
public class Unknown10Unknown2Opcode : IUnknown10Opcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown10;
	public Unknown10Option Option { get; set; } = Unknown10Option.Unknown2;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown10Unknown1Opcode opcode = new();
		return opcode;
	}
}
