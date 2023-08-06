using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public interface IMapDialogueOpcode
{
	DialogueOpcodes Opcode { get; set; }

	public static IMapDialogueOpcode ParseNextOpcode(Stream reader)
	{
		var opcode = reader.ReadRawByte();
		if (!opcode.EnumExists<DialogueOpcodes>())
		{
			return null;
		}

		return opcode.ToEnum<DialogueOpcodes>() switch
		{
			DialogueOpcodes.ScriptCall => ScriptCallOpcode.ReadOpcode(reader),
			DialogueOpcodes.CameraMove => CameraMoveOpcode.ReadOpcode(reader),
			DialogueOpcodes.ItemAquire => ItemAcquireOpcode.ReadOpcode(reader),
			DialogueOpcodes.TextBox => ITextBoxOpcode.ReadOpcode(reader),
			DialogueOpcodes.CharacterPortait => CharacterPortaitOpcode.ReadOpcode(reader),
			DialogueOpcodes.NextPage => NextPageOpcode.ReadOpcode(reader),
			DialogueOpcodes.Pause => PauseOpcode.ReadOpcode(reader),
			DialogueOpcodes.NextLine => NextPageOpcode.ReadOpcode(reader),
			_ => null,
		};
	}
}

public interface IMapDialogueOpcodeReader
{
	abstract static IMapDialogueOpcode ReadOpcode(Stream reader);
}
