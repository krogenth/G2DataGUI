using System.IO;
using G2DataGUI.Common.Extensions;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Pause;

public interface IPauseOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	PauseOption Option { get; set; }

	public static new IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		var option = reader.ReadRawByte();
		return option.ToEnum<PauseOption>() switch
		{
			PauseOption.Unknown1 => PauseUnknown1Opcode.ReadOpcode(reader),
			PauseOption.Unknown2 => PauseUnknown2Opcode.ReadOpcode(reader),
			_ => null,
		};
	}
}
