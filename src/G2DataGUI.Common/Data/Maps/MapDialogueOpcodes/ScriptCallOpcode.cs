﻿using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class ScriptCallOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.ScriptCall;
	public byte ScriptId { get; set; }
	public byte CharacterId { get; set; }

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new ScriptCallOpcode()
		{
			ScriptId = reader.ReadRawByte(),
			CharacterId = reader.ReadRawByte(),
		};
	}
}
