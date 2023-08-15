using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown04Opcode : IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown04;
	public byte Unknown1 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown04Opcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
