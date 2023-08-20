using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

/// <summary>
/// Defines a sub textbox that appears during dialogue.
/// Sub textboxes appear overlapping the primary textbox
/// in the top-right of the primary textbox.
/// </summary>
internal class CreateSubTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.CreateSubTextBox;
	public byte Unknown1 { get; set; }
	public byte TextBoxHeight { get; set; }
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; } = new();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CreateSubTextBoxOpcode opcode = new()
		{
			Unknown1 = reader.ReadRawByte(),
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
			else
			{
				reader.Seek(-1, SeekOrigin.Current);
				opcode.NestedOpcodes.Add(TextOpcode.ReadOpcode(reader));
			}
		} while (true);
	}
}
