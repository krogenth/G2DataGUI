namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

public enum TextBoxOption : byte
{
	RemoveTextBox          = 0x00,
	CreateTextBox          = 0x01,
	Unknown1               = 0x02,
	CreateSubTextBox       = 0x20,
	CreateOptionsTextBox   = 0x40,
	CreateOverworldTextBox = 0x80,
}
