using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

public class CreateTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.CreateTextBox;
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; } = new();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CreateTextBoxOpcode opcode = new();

		// create textbox should always have some text in it
		// opcode.NestedOpcodes.Add(TextOpcode.ReadOpcode(reader));

		byte data;
		do
		{
			data = reader.ReadRawByte();
			if (data.EnumExists<DialogueOpcode>())
			{
				var nestedOpcode = ParseDialogueOpcode.ParseNextOpcode(reader, data);
				opcode.NestedOpcodes.Add(nestedOpcode);

				if (nestedOpcode is ITextBoxOpcode _)
				{
					return opcode;
				}
			}
			else
			{
				reader.Seek(-1, SeekOrigin.Current);
				opcode.NestedOpcodes.Add(TextOpcode.ReadOpcode(reader));
			}
		} while (true);
	}
}
