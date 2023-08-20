using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

public class RemoveTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.RemoveTextBox;
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; } = new();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		RemoveTextBoxOpcode opcode = new();
		return opcode;
	}
}
