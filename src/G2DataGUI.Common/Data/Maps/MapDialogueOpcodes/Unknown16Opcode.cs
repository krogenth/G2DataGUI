using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown16Opcode : IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown16;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new Unknown16Opcode()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
			Unknown5 = reader.ReadRawByte(),
		};
	}
}
