using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown1COpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown1C;
	public byte Unknown1 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown1COpcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
