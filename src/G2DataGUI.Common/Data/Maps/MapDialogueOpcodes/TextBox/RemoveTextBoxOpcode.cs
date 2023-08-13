using System.Collections.Generic;
using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

public class RemoveTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.RemoveTextBox;
	public IList<IMapDialogueOpcode> NestedOpcodes { get; set; } = new List<IMapDialogueOpcode>();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		RemoveTextBoxOpcode opcode = new();
		return opcode;
	}
}
