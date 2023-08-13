using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown7;
public class Unknown7Unknown1Opcode : IUnknown7Opcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown7;
	public Unknown7Option Option { get; set; } = Unknown7Option.Unknown1;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown7Unknown1Opcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
			Unknown5 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
