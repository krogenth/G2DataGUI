using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Maps.Opcodes;
using G2DataGUI.Serialization.Maps.Opcodes.Highlight;
using G2DataGUI.Serialization.Maps.Opcodes.Pause;
using G2DataGUI.Serialization.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Maps.Opcodes.Unknown10;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Replaces the old ParseDialogueOpcode switch statement. Reads the leading
/// opcode byte itself, then dispatches to whichever concrete
/// IDataReader&lt;TOpcode&gt; matches — direct port of the original switch,
/// including the fact that DialogueOpcode.Text has no entry (Text is only
/// ever produced by TextBoxOpcodeReader/HighlightOpcodeReader falling
/// through to TextOpcodeReader on an unrecognized byte, never dispatched by
/// its own leading byte).
///
/// ReadFromOpcode is exposed separately from Read so TextBoxOpcodeReader
/// and HighlightOpcodeReader — which already know the opcode byte because
/// they read it themselves while scanning for a terminator — can dispatch
/// through the same table without re-reading a byte that's already gone.
/// </summary>
public sealed class MapOpcodeReader : IDataReader<IMapOpcode?>
{
	private static readonly Dictionary<DialogueOpcode, Func<Stream, IMapOpcode?>> _readers = new()
	{
		[DialogueOpcode.Unknown03] = s => new Unknown03OpcodeReader().Read(s),
		[DialogueOpcode.Unknown04] = s => new Unknown04OpcodeReader().Read(s),
		[DialogueOpcode.ScriptCall] = s => new ScriptCallOpcodeReader().Read(s),
		[DialogueOpcode.Unknown06] = s => new Unknown06OpcodeReader().Read(s),
		[DialogueOpcode.Unknown07] = s => new Unknown07OpcodeReader().Read(s),
		[DialogueOpcode.Unknown08] = s => new Unknown08OpcodeReader().Read(s),
		[DialogueOpcode.Unknown09] = s => new Unknown09OpcodeReader().Read(s),
		[DialogueOpcode.Unknown0A] = s => new Unknown0AOpcodeReader().Read(s),
		[DialogueOpcode.CameraMove] = s => new CameraMoveOpcodeReader().Read(s),
		[DialogueOpcode.Unknown0C] = s => new Unknown0COpcodeReader().Read(s),
		[DialogueOpcode.Unknown0D] = s => new Unknown0DOpcodeReader().Read(s),
		[DialogueOpcode.Unknown10] = s => new Unknown10OpcodeReader().Read(s),
		[DialogueOpcode.ItemAquire] = s => new ItemAcquireOpcodeReader().Read(s),
		[DialogueOpcode.Unknown14] = s => new Unknown14OpcodeReader().Read(s),
		[DialogueOpcode.Unknown15] = s => new Unknown15OpcodeReader().Read(s),
		[DialogueOpcode.Unknown16] = s => new Unknown16OpcodeReader().Read(s),
		[DialogueOpcode.TextBox] = s => new TextBoxOpcodeReader().Read(s),
		[DialogueOpcode.CharacterPortait] = s => new CharacterPortraitOpcodeReader().Read(s),
		[DialogueOpcode.NextPage] = s => new NextPageOpcodeReader().Read(s),
		[DialogueOpcode.Unknown1B] = s => new Unknown1BOpcodeReader().Read(s),
		[DialogueOpcode.Unknown1C] = s => new Unknown1COpcodeReader().Read(s),
		[DialogueOpcode.Pause] = s => new PauseOpcodeReader().Read(s),
		[DialogueOpcode.Highlight] = s => new HighlightOpcodeReader().Read(s),
		[DialogueOpcode.NextLine] = s => new NextLineOpcodeReader().Read(s),
	};

	public IMapOpcode? Read(Stream stream)
	{
		byte opcodeByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(DialogueOpcode), opcodeByte))
		{
			return null;
		}

		return ReadFromOpcode(stream, (DialogueOpcode)opcodeByte);
	}

	/// <summary>Dispatches for a byte the caller has already read and
	/// identified as a defined DialogueOpcode. Returns null for Text (never
	/// dispatched this way) and for any opcode with no reader wired up.</summary>
	public static IMapOpcode? ReadFromOpcode(Stream stream, DialogueOpcode opcode) =>
		_readers.TryGetValue(opcode, out Func<Stream, IMapOpcode?>? read) ? read(stream) : null;

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
