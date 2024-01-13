using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown10;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Pause;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Highlight;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public static class ParseDialogueOpcode
{
	public static IMapDialogueOpcode ParseNextOpcode(Stream reader, byte opcode)
	{
		return opcode.ToEnum<DialogueOpcode>() switch
		{
			DialogueOpcode.Unknown03 => Unknown03Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown04 => Unknown04Opcode.ReadOpcode(reader),
			DialogueOpcode.ScriptCall => ScriptCallOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown06 => Unknown06Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown07 => Unknown07Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown08 => Unknown08Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown09 => Unknown09Opcode.ReadOpcode(reader),
			DialogueOpcode.CameraMove => CameraMoveOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown0C => Unknown0COpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown0D => Unknown0DOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown10 => IUnknown10Opcode.ReadOpcode(reader),
			DialogueOpcode.ItemAquire => ItemAcquireOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown14 => Unknown14Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown15 => Unknown15Opcode.ReadOpcode(reader),
			DialogueOpcode.Unknown16 => Unknown16Opcode.ReadOpcode(reader),
			DialogueOpcode.TextBox => ITextBoxOpcode.ReadOpcode(reader),
			DialogueOpcode.CharacterPortait => CharacterPortaitOpcode.ReadOpcode(reader),
			DialogueOpcode.NextPage => NextPageOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown1B => Unknown1BOpcode.ReadOpcode(reader),
			DialogueOpcode.Unknown1C => Unknown1COpcode.ReadOpcode(reader),
			DialogueOpcode.Pause => IPauseOpcode.ReadOpcode(reader),
			DialogueOpcode.Highlight => IHighlightOpcode.ReadOpcode(reader),
			DialogueOpcode.NextLine => NextLineOpcode.ReadOpcode(reader),
			_ => null,
		};
	}
}
