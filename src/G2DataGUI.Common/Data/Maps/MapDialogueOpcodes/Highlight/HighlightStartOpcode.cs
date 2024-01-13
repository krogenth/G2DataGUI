using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Highlight;

public class HighlightStartOpcode : IHighlightOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Highlight;
	public HighlightOption Option { get; set; } = HighlightOption.Start;
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; } = new();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		HighlightStartOpcode opcode = new();

		do
		{
			byte data = reader.ReadRawByte();
			if (data.EnumExists<DialogueOpcode>())
			{
				var nestedOpcode = ParseDialogueOpcode.ParseNextOpcode(reader, data);
				opcode.NestedOpcodes.Add(nestedOpcode);

				if (nestedOpcode is HighlightEndOpcode _)
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
