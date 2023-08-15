namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public enum DialogueOpcode : byte
{
	/// <summary>
	/// Not sure how to handle text
	/// so considering this a No Op
	/// opcode, to house the text itself
	/// </summary>
	Text = 0x00,

	/// <summary>
	/// Completely unsure, likely 3 parameters
	/// </summary>
	Unknown03 = 0x03,

	/// <summary>
	/// Completely unknown, likely 1 parameter
	/// </summary>
	Unknown04 = 0x04,

	/// <summary>
	/// Seems to define a call to a script
	/// within the Map's script list
	/// </summary>
	ScriptCall = 0x05,

	/// <summary>
	/// Completely unknown, may be 4 parameters
	/// </summary>
	Unknown06 = 0x06,

	/// <summary>
	/// Completely unknown, may be 1 or 4 parameters
	/// </summary>
	Unknown07 = 0x07,

	/// <summary>
	/// Completely unknown, likely 2 parameters
	/// </summary>
	Unknown08 = 0x08,

	/// <summary>
	/// Completely unknown, may be 4 parameters
	/// </summary>
	Unknown09 = 0x09,

	/// <summary>
	/// Seems to deal with camera movement
	/// </summary>
	CameraMove = 0x0B,

	/// <summary>
	/// Completely unknown, seems to have 1 parameter
	/// </summary>
	Unknown0C = 0x0C,

	/// <summary>
	/// Completely unknown, seems to have 2 parameters
	/// </summary>
	Unknown0D = 0x0D,

	/// <summary>
	/// Completely unknown, no clue on parameters(1 parameter?).
	/// See Map 1000, section 8, offset 0x24.
	/// Also see Map 1000, section 10, offset 0x1F.
	/// </summary>
	Unknown10 = 0x10,

	/// <summary>
	/// Adds items to inventory
	/// </summary>
	ItemAquire = 0x11,

	/// <summary>
	/// Completely unknown, no clue on parameters
	/// </summary>
	Unknown14 = 0x14,

	/// <summary>
	/// Completely unknown, likely 4 parameters
	/// </summary>
	Unknown15 = 0x15,

	/// <summary>
	/// Defines a action around textboxes
	/// </summary>
	TextBox = 0x17,

	/// <summary>
	/// Defines which character protrait to display
	/// </summary>
	CharacterPortait = 0x18,

	/// <summary>
	/// Clears/Next pages within the textbox
	/// </summary>
	NextPage = 0x1A,

	/// <summary>
	/// Completely unknown, seems to have 1 parameter
	/// </summary>
	Unknown1C = 0x1C,

	/// <summary>
	/// Pauses text rendering for a time(tick based?)
	/// </summary>
	Pause = 0x1D,

	/// <summary>
	/// Moves text rendering to next line
	/// </summary>
	NextLine = 0x1F,
}
