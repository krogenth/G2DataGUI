using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Maps.Opcodes;
using G2DataGUI.Serialization.Maps.Opcodes.Highlight;
using G2DataGUI.Serialization.Maps.Opcodes.Pause;
using G2DataGUI.Serialization.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Maps.Opcodes.Unknown0D;
using G2DataGUI.Serialization.Maps.Opcodes.Unknown10;
using G2DataGUI.Serialization.Maps.Opcodes.Unknown16;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Replaces the old ParseDialogueOpcode switch statement. Reads the leading
/// opcode byte itself, then dispatches to whichever concrete
/// IDataReader<TOpcode> matches — direct port of the original switch,
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
		[DialogueOpcode.Unknown0D] = s => new Opcodes.Unknown0DOpcodeReader().Read(s),
		[DialogueOpcode.Unknown0E] = s => new Unknown0EOpcodeReader().Read(s),
		[DialogueOpcode.Unknown0F] = s => new Unknown0FOpcodeReader().Read(s),
		[DialogueOpcode.Unknown10] = s => new Unknown10OpcodeReader().Read(s),
		[DialogueOpcode.ItemAcquire] = s => new ItemAcquireOpcodeReader().Read(s),
		[DialogueOpcode.Unknown12] = s => new Unknown12OpcodeReader().Read(s),
		[DialogueOpcode.Unknown14] = s => new Unknown14OpcodeReader().Read(s),
		[DialogueOpcode.Unknown15] = s => new Unknown15OpcodeReader().Read(s),
		[DialogueOpcode.Unknown16] = s => new Opcodes.Unknown16OpcodeReader().Read(s),
		[DialogueOpcode.TextBox] = s => new TextBoxOpcodeReader().Read(s),
		[DialogueOpcode.CharacterPortrait] = s => new CharacterPortraitOpcodeReader().Read(s),
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

	/// <summary>
	/// The single, authoritative "is this byte a real, dispatchable opcode
	/// boundary" check -- true only for bytes _readers actually has an
	/// entry for. Text (0x00) is deliberately excluded even though it IS
	/// a defined DialogueOpcode value: it's a label the type system uses
	/// to give TextOpcode something to attach to (TextOpcode.Opcode =>
	/// DialogueOpcode.Text), never a byte that appears in the stream as a
	/// marker, and _readers has no entry for it. Gaps in the enum's own
	/// numeric range (0x0E, 0x0F, 0x12, 0x13, 0x19 -- bytes with no
	/// DialogueOpcode value at all) are excluded too, for the same
	/// underlying reason: nothing can dispatch them.
	///
	/// This exists because that exact distinction -- Enum.IsDefined true
	/// vs. actually dispatchable -- was independently reimplemented
	/// incorrectly in three different places (TextOpcodeReader,
	/// TextBoxNestedOpcodeReader, HighlightStartOpcodeReader), each
	/// treating Text=0x00 or a gap byte as if it were a real boundary. Any
	/// of those callers reading such a byte, dispatching to nothing, and
	/// looping back with the stream position unchanged is an infinite
	/// loop -- confirmed empirically against real dialogue data, where it
	/// reproduces on every single section. Every caller that needs to
	/// decide "is the current byte the start of a real opcode, or is it
	/// text/nested content" now goes through this one method, so there's
	/// exactly one place this logic can ever be wrong again, not
	/// three-plus.
	///
	/// IMPORTANT CAVEAT, found after the above was already believed
	/// sufficient: a true result here means the TOP-LEVEL byte has a
	/// wired reader — it does NOT guarantee ReadFromOpcode will actually
	/// return non-null. TextBox/Highlight/Pause/Unknown10 each read a
	/// SECOND byte (their own sub-option) and can still fail if that
	/// byte isn't a recognized TextBoxOption/HighlightOption/PauseOption/
	/// Unknown10Option. Confirmed on real data (1010.mdt): a bare
	/// Unknown10 (0x10) byte followed by an unrecognized Unknown10Option
	/// byte passes IsDispatchable but ReadFromOpcode still returns null.
	/// Every caller that uses IsDispatchable to decide "is this the start
	/// of a real opcode, or should I read it as text" (TextOpcodeReader,
	/// TextBoxNestedOpcodeReader, HighlightStartOpcodeReader) MUST still
	/// attempt the actual ReadFromOpcode call and handle a null result by
	/// falling back to text — treating IsDispatchable alone as sufficient
	/// reproduces the exact same class of infinite loop this method was
	/// introduced to fix, just one level deeper.
	///
	/// PERFORMANCE: uses only _readers.ContainsKey now, not
	/// Enum.IsDefined — _readers' keys are always real DialogueOpcode
	/// enum members (populated via named references like
	/// [DialogueOpcode.Unknown03] = ..., never a raw cast), so a key
	/// existing already implies the value is defined; the separate
	/// Enum.IsDefined check was pure redundant overhead. That mattered in
	/// practice: this method runs once per byte of every dialogue text
	/// run (potentially tens of thousands of calls for a large map), and
	/// Enum.IsDefined is reflection-based — a real map's parse time went
	/// from ~10 seconds back down to roughly what it was before any of
	/// this dialogue work started once this was removed.
	/// </summary>
	public static bool IsDispatchable(byte value) =>
		_readers.ContainsKey((DialogueOpcode)value);

	/// <summary>
	/// Dispatches for a byte the caller has already read and
	/// identified as a defined DialogueOpcode. Returns null for Text (never
	/// dispatched this way) and for any opcode with no reader wired up.
	/// </summary>
	public static IMapOpcode? ReadFromOpcode(Stream stream, DialogueOpcode opcode) =>
		_readers.TryGetValue(opcode, out Func<Stream, IMapOpcode?>? read) ? read(stream) : null;

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
