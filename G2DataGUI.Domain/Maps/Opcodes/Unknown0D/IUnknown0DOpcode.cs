namespace G2DataGUI.Domain.Maps.Opcodes.Unknown0D;

/// <summary>
/// Sub-dispatches on a first byte (Unknown0DOption) — same shape as
/// IPauseOpcode/PauseOption, discovered later than the original port.
/// Confirmed against ~350KB of real dialogue data: this opcode had long
/// been defined as a fixed 2 bytes, which happened to look approximately
/// right (Unknown1 was frequently 0x01, so the second "fixed" byte often
/// landed on the first character of following text and produced
/// plausible-looking, if accidental, resyncs). The real structure only
/// became clear from a much larger sample: every single occurrence
/// (184/184) resolves cleanly once read as a genuine sub-dispatch on this
/// first byte.
/// </summary>
public interface IUnknown0DOpcode : IMapOpcode
{
	Unknown0DOption Option { get; }
}
