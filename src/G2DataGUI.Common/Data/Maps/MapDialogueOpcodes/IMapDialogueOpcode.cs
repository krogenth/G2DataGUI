using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcode;

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
			DialogueOpcode.ScriptCall => ScriptCallOpcode.ReadOpcode(reader),
			DialogueOpcode.CameraMove => CameraMoveOpcode.ReadOpcode(reader),
			DialogueOpcode.ItemAquire => ItemAcquireOpcode.ReadOpcode(reader),
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
