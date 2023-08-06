using System;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class TextOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcodes Opcode { get; set; } = DialogueOpcodes.Text;
	public string Text { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		TextOpcode opcode = new();

		var data = reader.ReadRawByte();
		while (!data.EnumExists<DialogueOpcodes>())
		{
			_ = opcode.Text += Convert.ToChar(data);
			data = reader.ReadRawByte();
		}

		reader.Seek(-1, SeekOrigin.Current);

		return opcode;
	}
}
