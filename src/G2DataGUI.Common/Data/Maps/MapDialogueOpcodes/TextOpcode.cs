using System;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class TextOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Text;
	public string Text { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		
		TextOpcode opcode = new();

		try
		{
			var data = reader.ReadRawByte();
			while (!data.EnumExists<DialogueOpcode>())
			{
				_ = opcode.Text += Convert.ToChar(data);
				data = reader.ReadRawByte();
			}

			reader.Seek(-1, SeekOrigin.Current);
		}
		catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Errors.Error("Maps", ex.Message));
		}

		return opcode;
	}
}
