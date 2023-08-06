using System;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public enum DialogueOpcodes : byte
{
	/// <summary>
	/// Not sure how to handle text
	/// so considering this a No Op
	/// opcode, to house the text itself
	/// </summary>
	Text             = 0x00,

	/// <summary>
	/// Seems to define a call to a script
	/// within the Map's script list
	/// </summary>
	ScriptCall       = 0x05,

	/// <summary>
	/// Completely unsure
	/// </summary>
	Unknown1         = 0x08,

	/// <summary>
	/// Seems to deal with camera movement
	/// </summary>
	CameraMove       = 0x0B,

	/// <summary>
	/// Adds items to inventory
	/// </summary>
	ItemAquire       = 0x11,

	/// <summary>
	/// Completely unsure, seems to have 4 parameters
	/// </summary>
	Unknown2         = 0x15,

	/// <summary>
	/// Defines a action around textboxes
	/// </summary>
	TextBox          = 0x17,

	/// <summary>
	/// Defines which character protrait to display
	/// </summary>
	CharacterPortait = 0x18,

	/// <summary>
	/// Clears/Next pages within the textbox
	/// </summary>
	NextPage         = 0x1A,

	/// <summary>
	/// Pauses text rendering for a time(tick based?)
	/// </summary>
	Pause            = 0x1D,

	/// <summary>
	/// Moves text rendering to next line
	/// </summary>
	NextLine         = 0x1F,
}

public enum TextBoxOptions : byte
{
	RemoveTextBox          = 0x00,
	CreateTextBox          = 0x01,
	CreateOverworldTextBox = 0x80,
}
