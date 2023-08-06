using System.Collections.Generic;
using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class RemoveTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.TextBox;
	public TextBoxOptions Option { get; set; } = TextBoxOptions.RemoveTextBox;
	public IList<IMapDialogueOpcode> NestedOpcodes { get; set; } = new List<IMapDialogueOpcode>();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		RemoveTextBoxOpcode opcode = new();
		return opcode;
	}
}
