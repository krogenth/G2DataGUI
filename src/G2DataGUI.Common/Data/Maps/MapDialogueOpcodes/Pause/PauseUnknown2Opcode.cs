using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Pause;

public class PauseUnknown2Opcode : IPauseOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Pause;
	public PauseOption Option { get; set; } = PauseOption.Unknown2;
	public byte Unknown1 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		PauseUnknown2Opcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
