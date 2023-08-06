using System.Collections.Generic;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class CreateTextBoxOpcode : ITextBoxOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.TextBox;
	public TextBoxOptions Option { get; set; } = TextBoxOptions.CreateTextBox;
	public IList<IMapDialogueOpcode> NestedOpcodes { get; set; } = new List<IMapDialogueOpcode>();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		CreateTextBoxOpcode opcode = new();

		// create textbox should always have some text in it
		opcode.NestedOpcodes.Add(TextOpcode.ReadOpcode(reader));

		byte data;
		do
		{
			data = reader.ReadRawByte();
			if (data.EnumExists<DialogueOpcodes>())
			{
				switch (data.ToEnum<DialogueOpcodes>())
				{
					case DialogueOpcodes.ScriptCall:
						opcode.NestedOpcodes.Add(ScriptCallOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcodes.CameraMove:
						opcode.NestedOpcodes.Add(CameraMoveOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcodes.ItemAquire:
						opcode.NestedOpcodes.Add(ItemAcquireOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcodes.CharacterPortait:
						opcode.NestedOpcodes.Add(CharacterPortaitOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcodes.NextPage:
						opcode.NestedOpcodes.Add(NextPageOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcodes.Pause:
						opcode.NestedOpcodes.Add(PauseOpcode.ReadOpcode(reader));
						break;
					case DialogueOpcodes.NextLine:
						opcode.NestedOpcodes.Add(NextLineOpcode.ReadOpcode(reader));
						break;
					// textbox removal should be the only thing that returns the textbox
					// should verify it isn't a textbox within a textbox(is that even possible?)
					case DialogueOpcodes.TextBox:
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
