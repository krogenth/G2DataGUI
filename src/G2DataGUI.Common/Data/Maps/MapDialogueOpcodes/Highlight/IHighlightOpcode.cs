using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Highlight;

public interface IHighlightOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public HighlightOption Option { get; set; }
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; }

	public static new IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		var option = reader.ReadRawByte();
		if (option.EnumExists<HighlightOption>())
		{
			return option.ToEnum<HighlightOption>() switch
			{
				HighlightOption.Start => HighlightStartOpcode.ReadOpcode(reader),
				HighlightOption.End => HighlightEndOpcode.ReadOpcode(reader),
				_ => null,
			};
		}

		reader.Seek(-1, SeekOrigin.Current);
		return null;
	}
}
