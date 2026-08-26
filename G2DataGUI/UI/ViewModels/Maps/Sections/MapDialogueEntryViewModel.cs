using System;
using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>
/// View-only, deliberately — Opcodes/DecodeError/RawHex have no setters
/// here at all, unlike every other Maps section (which all support real
/// editing via SetModelValue). Editing dialogue means writing opcodes back
/// to bytes, which isn't built (MapDialogueReader/MapWriter don't touch
/// Dialogue at all — see their own doc comments); this section is
/// specifically "let the user see what's actually in here" as asked for,
/// not a claim that editing exists yet.
/// </summary>
public sealed class MapDialogueEntryViewModel(MapDialogueEntry model, IRepository repository)
	: ItemViewModel<MapDialogueEntry>(model, repository)
{
	public int Index => Model.Index;

	public string? DecodeError => Model.DecodeError;

	public string RawHex => Convert.ToHexString(Model.RawBytes);

	public IReadOnlyList<MapOpcodeViewModel> Opcodes { get; } =
		model.Opcodes.Select(opcode => new MapOpcodeViewModel(opcode)).ToList();
}
