using System.Collections.Generic;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

public class CreateOverworldTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.TextBox;
	public TextBoxOption Option { get; set; } = TextBoxOption.CreateOverworldTextBox;
	public byte TextBoxLength { get; set; }
	public byte TextBoxHeight { get; set; }
	public IList<IMapDialogueOpcode> NestedOpcodes { get; set; } = new List<IMapDialogueOpcode>();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CreateOverworldTextBoxOpcode opcode = new()
		{
			TextBoxLength = reader.ReadRawByte(),
			TextBoxHeight = reader.ReadRawByte(),
		};

		// create overworld textbox should always have some text in it
		opcode.NestedOpcodes.Add(TextOpcode.ReadOpcode(reader));

		byte data;
		do
		{
			data = reader.ReadRawByte();
			if (data.EnumExists<DialogueOpcode>())
			{
				switch (data.ToEnum<DialogueOpcode>())
				{
					case DialogueOpcode.ScriptCall:
						opcode.NestedOpcodes.Add(ScriptCallOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcode.CameraMove:
						opcode.NestedOpcodes.Add(CameraMoveOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcode.ItemAquire:
						opcode.NestedOpcodes.Add(ItemAcquireOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcode.CharacterPortait:
						opcode.NestedOpcodes.Add(CharacterPortaitOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcode.NextPage:
						opcode.NestedOpcodes.Add(NextPageOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcode.Pause:
						opcode.NestedOpcodes.Add(PauseOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcode.NextLine:
						opcode.NestedOpcodes.Add(NextLineOpcode.ReadOpcode(reader));
						break;
					// textbox removal should be the only thing that returns the textbox
					// should verify it isn't a textbox within a textbox(is that even possible?)
					case DialogueOpcode.TextBox:
						opcode.NestedOpcodes.Add(ITextBoxOpcode.ReadOpcode(reader));
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
