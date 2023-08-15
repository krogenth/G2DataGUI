using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public interface IMapDialogueOpcode
{
	DialogueOpcode Opcode { get; set; }

	public static IMapDialogueOpcode ParseNextOpcode(Stream reader)
	{
		var opcode = reader.ReadRawByte();
		if (!opcode.EnumExists<DialogueOpcode>())
		{
			return null;
		}

		return ParseDialogueOpcode.ParseNextOpcode(reader, opcode);
	}
}

public interface IMapDialogueOpcodeReader
{
	abstract static IMapDialogueOpcode ReadOpcode(Stream reader);
}
