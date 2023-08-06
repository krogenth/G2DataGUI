using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class ScriptCallOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.ScriptCall;
	public byte ScriptId { get; set; }
	public byte CharacterId { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		ScriptCallOpcode opcode = new()
		{
			ScriptId = reader.ReadRawByte(),
			CharacterId = reader.ReadRawByte(),
		};
		return opcode;
	}
}
