using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown10;

public class Unknown10Unknown1Opcode : IUnknown10Opcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown10;
	public Unknown10Option Option { get; set; } = Unknown10Option.Unknown1;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public short MapID { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new Unknown10Unknown1Opcode()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			MapID = reader.ReadRawShort(),
		};
	}
}
