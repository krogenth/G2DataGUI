using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class PauseOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Pause;
	public byte Unknown1 { get; set; } = 0xFF;
	public byte Unknown2 { get; set; }


	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		PauseOpcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
