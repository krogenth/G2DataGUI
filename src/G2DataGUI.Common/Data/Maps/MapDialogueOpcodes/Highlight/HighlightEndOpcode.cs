﻿using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Maps.MapDialogueOpcodes.Highlight;

public class HighlightEndOpcode : IHighlightOpcode
{
	public DialogueOpcode Opcode { get; set; } = DialogueOpcode.Highlight;
	public HighlightOption Option { get; set; } = HighlightOption.End;
	public ObservableCollection<IMapDialogueOpcode> NestedOpcodes { get; set; } = new();

	public static IMapDialogueOpcode ReadOpcode(Stream reader)
	{
		return new HighlightEndOpcode();
	}
}
