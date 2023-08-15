using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown15Opcode : IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown15;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown15Opcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
