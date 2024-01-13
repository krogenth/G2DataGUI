using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class CameraMoveOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.CameraMove;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new CameraMoveOpcode()
		{
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
		};
	}
}
