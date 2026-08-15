namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Defines which character portrait to display. Domain type name
/// is spelled correctly; the DialogueOpcode enum member (CharacterPortait)
/// keeps the original's typo — see the note on DialogueOpcode.</summary>
public sealed class CharacterPortraitOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.CharacterPortait;

	/// <summary>Appears to always be 0x00 in the original.</summary>
	public byte Unknown1 { get; set; }

	public byte PortraitOffset { get; set; }
}
