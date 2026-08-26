using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using G2DataGUI.Domain.Maps;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>
/// Wraps one IMapOpcode for display — reflection-based rather than a
/// hand-written ViewModel per opcode type, because there are 40 concrete
/// opcode types (G2DataGUI.Domain.Maps.Opcodes and its sub-namespaces) and
/// this is a view-only pass; hand-writing 40 typed editors is a
/// proportionate amount of work for WHEN editing is actually needed (the
/// same shape of effort as the 9 already-built .mdt sections), not for
/// "let the user see what's in here" on its own. Summary lists every
/// public property except Opcode itself (redundant with TypeName) and
/// NestedOpcodes (shown as Children instead, not inline text).
///
/// Children recurses into NestedOpcodes wherever a concrete opcode type
/// has that property (TextBox variants, at minimum) — found via
/// reflection rather than a type-check list, so a future opcode type
/// gaining its own NestedOpcodes property picks this up automatically
/// without this class needing to change.
/// </summary>
public sealed class MapOpcodeViewModel
{
	public MapOpcodeViewModel(IMapOpcode opcode)
	{
		TypeName = opcode.GetType().Name;
		Opcode = opcode.Opcode;
		Summary = FormatSummary(opcode);

		List<IMapOpcode>? nested = opcode.GetType().GetProperty("NestedOpcodes")?.GetValue(opcode) as List<IMapOpcode>;

		Children = nested?.Select(child => new MapOpcodeViewModel(child)).ToList() ?? [];
	}

	public string TypeName { get; }

	public DialogueOpcode Opcode { get; }

	public string Summary { get; }

	public IReadOnlyList<MapOpcodeViewModel> Children { get; }

	public bool HasChildren => Children.Count > 0;

	private static string FormatSummary(IMapOpcode opcode)
	{
		IEnumerable<string> fields = opcode.GetType()
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.Name is not (nameof(IMapOpcode.Opcode)) && p.PropertyType != typeof(List<IMapOpcode>))
			.Select(p => $"{p.Name}={FormatValue(p.GetValue(opcode))}");

		return string.Join(", ", fields);
	}

	private static string FormatValue(object? value) => value switch
	{
		null => "null",
		byte[] bytes => Convert.ToHexString(bytes),
		string text => $"\"{text}\"",
		_ => value.ToString() ?? "null",
	};
}
