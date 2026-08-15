namespace G2DataGUI.Domain.Models;

/// <summary>
/// One parsed NMDM motion. Only POS_0/ANG_1/QUAT_1/SCL_2 channel
/// combinations are understood — see NjMotionReader. NMDM parsing is known
/// to still fail on some real animations even with those channels handled
/// correctly (unrelated malformed/unfamiliar data) — see PORTING-Chr.md.
/// A motion that fails to parse should surface as an exception from
/// NjMotionReader, not a silently-empty NjMotion with no tracks.
/// </summary>
public sealed class NjMotion
{
	public int FrameCount { get; set; } = 1;

	/// <summary>Raw 32-bit value at NMDM offset 8 — NOT the real
	/// NJS_MOTION.type on its own; see MotionType/InterpolationFunction
	/// for the correctly split fields. Kept for display/back-compat.</summary>
	public uint Type { get; set; }

	/// <summary>NJS_MOTION.type — the real 16-bit channel bitmask
	/// (NJD_MTYPE_*).</summary>
	public ushort MotionType { get; set; }

	/// <summary>NJS_MOTION.inp_fn — interpolation function, a separate
	/// field previously (incorrectly) folded into Type.</summary>
	public ushort InterpolationFunction { get; set; }

	public List<NjMotionTrack> Tracks { get; set; } = [];
}
