using System.Collections.Generic;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

/// <summary>
/// Creates a textbox in the top-left of the screen
/// that displays a list of options for the palyer to choose from.
/// </summary>
public class CreateOptionsTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.CreateOptionsTextBox;
	public byte TextBoxLength { get; set; }
	public byte TextBoxHeight { get; set; }
	public IList<IMapDialogueOpcode> NestedOpcodes { get; set; } = new List<IMapDialogueOpcode>();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CreateOptionsTextBoxOpcode opcode = new()
		{
			TextBoxLength = reader.ReadRawByte(),
			TextBoxHeight = reader.ReadRawByte(),
		};

		do
		{
			byte data = reader.ReadRawByte();
			if (data.EnumExists<DialogueOpcode>())
			{
				var nestedOpcode = ParseDialogueOpcode.ParseNextOpcode(reader, data);
				opcode.NestedOpcodes.Add(nestedOpcode);

				if (nestedOpcode is ITextBoxOpcode _)
				{
					return opcode;
				}
			}
			else if (data == 0x02)
			{
				return opcode;
			}
			else
			{
				reader.Seek(-1, SeekOrigin.Current);
				opcode.NestedOpcodes.Add(TextOpcode.ReadOpcode(reader));
			}
		} while (true);
	}
}
