using System.Collections.Generic;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public interface ITextBoxOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	TextBoxOptions Option { get; set; }
	public IList<IMapDialogueOpcode> NestedOpcodes { get; set; }

	public static new IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		var option = reader.ReadRawByte();
		return option.ToEnum<TextBoxOptions>() switch
		{
			TextBoxOptions.RemoveTextBox => RemoveTextBoxOpcode.ReadOpcode(reader),
			TextBoxOptions.CreateTextBox => CreateTextBoxOpcode.ReadOpcode(reader),
			TextBoxOptions.CreateOverworldTextBox => CreateOverworldTextBoxOpcode.ReadOpcode(reader),
			_ => null,
		};
	}
}
