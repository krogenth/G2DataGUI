using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown0DOpcode : IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown0D;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new Unknown0DOpcode()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
		};
	}
}
