using System.IO;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown7;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public interface IMapDialogueOpcode
{
	DialogueOpcode Opcode { get; set; }

	public static IMapDialogueOpcode ParseNextOpcode(Stream reader)
	{
		var opcode = reader.ReadRawByte();
		if (!opcode.EnumExists<DialogueOpcode>())
		{
			return null;
		}

		return opcode.ToEnum<DialogueOpcode>() switch
		{
			DialogueOpcode.Unknown1 => Unknown1Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown2 => Unknown2Opcode.ReadOpcode(reader),
			DialogueOpcode.ScriptCall => ScriptCallOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown3 => Unknown3Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown4 => Unknown4Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown5 => Unknown5Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown6 => Unknown6Opcode.ReadOpcode(reader),
			DialogueOpcode.CameraMove => CameraMoveOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown7 => IUnknown7Opcode.ReadOpcode(reader),
			DialogueOpcode.ItemAquire => ItemAcquireOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown8 => Unknown8Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown9 => Unknown9Opcode.ReadOpcode(reader),
			DialogueOpcode.TextBox => ITextBoxOpcode.ReadOpcode(reader),
			DialogueOpcode.CharacterPortait => CharacterPortaitOpcode.ReadOpcode(reader),
			DialogueOpcode.NextPage => NextPageOpcode.ReadOpcode(reader),
			DialogueOpcode.Pause => PauseOpcode.ReadOpcode(reader),
			DialogueOpcode.NextLine => NextPageOpcode.ReadOpcode(reader),
			_ => null,
		};
	}
}

public interface IMapDialogueOpcodeReader
{
	abstract static IMapDialogueOpcode ReadOpcode(Stream reader);
}
