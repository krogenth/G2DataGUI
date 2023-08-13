using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Unknown7;

public interface IUnknown7Opcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public Unknown7Option Option { get; set; }

	public static new IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		var option = reader.ReadRawByte();
		return option.ToEnum<Unknown7Option>() switch
		{
			Unknown7Option.Unknown1 => Unknown7Unknown1Opcode.ReadOpcode(reader),
			Unknown7Option.Unknown2 => Unknown7Unknown2Opcode.ReadOpcode(reader),
			_ => null,
		};
	}
}
