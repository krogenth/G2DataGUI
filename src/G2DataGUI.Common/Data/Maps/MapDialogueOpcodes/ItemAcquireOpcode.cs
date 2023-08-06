using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class ItemAcquireOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.ItemAquire;
	public byte Unknown1 { get; set; }
	public byte Item1Offset { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		ItemAcquireOpcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
			Item1Offset = reader.ReadRawByte(),
		};
		return opcode;
	}
}
