using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class CharacterPortaitOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.CharacterPortait;

	/// <summary>
	/// Appears to always be 0x00
	/// </summary>
	public byte Unknown1 { get; set; } = 0x00;

	public byte PortaitOffset { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new CharacterPortaitOpcode()
		{
			Unknown1 = reader.ReadRawByte(),
			PortaitOffset = reader.ReadRawByte(),
		};
	}
}
