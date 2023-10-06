using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class ItemAcquireOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.ItemAquire;
	public byte Unknown1 { get; set; }
	public byte Item1Offset { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new ItemAcquireOpcode()
		{
			Unknown1 = reader.ReadRawByte(),
			Item1Offset = reader.ReadRawByte(),
		};
	}
}
