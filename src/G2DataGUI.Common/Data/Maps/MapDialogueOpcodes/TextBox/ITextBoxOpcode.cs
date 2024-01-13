using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.TextBox;

public interface ITextBoxOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	TextBoxOption Option { get; set; }
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; }

	public static new IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		var option = reader.ReadRawByte();
		return option.ToEnum<TextBoxOption>() switch
		{
			TextBoxOption.RemoveTextBox => RemoveTextBoxOpcode.ReadOpcode(reader),
			TextBoxOption.CreateTextBox => CreateTextBoxOpcode.ReadOpcode(reader),
			TextBoxOption.Unknown1 => RemoveTextBoxOpcode.ReadOpcode(reader),
			TextBoxOption.CreateSubTextBox => CreateSubTextBoxOpcode.ReadOpcode(reader),
			TextBoxOption.CreateOptionsTextBox => CreateOptionsTextBoxOpcode.ReadOpcode(reader),
			TextBoxOption.CreateOverworldTextBox => CreateOverworldTextBoxOpcode.ReadOpcode(reader),
			_ => null,
		};
	}
}
