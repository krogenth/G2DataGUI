﻿using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

public class NextLineOpcode : IMapDialogueOpcode, IMapDialogueOpcodeReader
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.NextLine;

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new NextLineOpcode();
	}
}
