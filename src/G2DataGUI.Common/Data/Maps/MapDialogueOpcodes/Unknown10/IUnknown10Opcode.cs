using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown10;

public interface IUnknown10Opcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public Unknown10Option Option { get; set; }

	public static new IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		var option = reader.ReadRawByte();
		if (option.EnumExists<Unknown10Option>())
		{
			return option.ToEnum<Unknown10Option>() switch
			{
				Unknown10Option.Unknown1 => Unknown10Unknown1Opcode.ReadOpcode(reader),
				Unknown10Option.Unknown2 => Unknown10Unknown2Opcode.ReadOpcode(reader),
				_ => null,
			};
		}

		reader.Seek(-1, SeekOrigin.Current);
		return null;
	}
}
