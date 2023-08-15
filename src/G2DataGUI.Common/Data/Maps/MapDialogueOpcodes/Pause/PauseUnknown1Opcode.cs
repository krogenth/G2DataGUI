using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Pause;

public class PauseUnknown1Opcode : IPauseOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Pause;
	public PauseOption Option { get; set; } = PauseOption.Unknown1;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		PauseUnknown1Opcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
