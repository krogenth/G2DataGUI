using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class Unknown0COpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Unknown0C;
	public byte Unknown1 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		Unknown0COpcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
