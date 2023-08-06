using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class CameraMoveOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.CameraMove;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CameraMoveOpcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
		};
		return opcode;
	}
}
