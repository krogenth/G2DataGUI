﻿using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

/// <summary>
/// Creates a textbox at the top center of the screen,
/// generally used for screen transition labels.
/// i.e. - to Carbo Village
/// </summary>
public class CreateOverworldTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.CreateOverworldTextBox;
	public byte TextBoxLength { get; set; }
	public byte TextBoxHeight { get; set; }
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; } = new();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CreateOverworldTextBoxOpcode opcode = new()
		{
			TextBoxLength = reader.ReadRawByte(),
			TextBoxHeight = reader.ReadRawByte(),
		};

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
