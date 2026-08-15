using G2DataGUI.Domain.Models;

namespace G2DataGUI.Domain.Chr;

/// <summary>
/// One animation: an NMDM (motion data) chunk, optionally preceded by a
/// GAMT chunk and/or followed by a POF0 (relocation) chunk. RawNmdm is
/// always available regardless of whether Motion parses successfully —
/// NMDM parsing (NjMotionReader) is known to still fail on some real
/// animations even with the channel-format bugs fixed (see
/// PORTING-Chr.md), so callers that just need to move/copy an animation's
/// bytes between archives never need Motion to parse at all.
/// </summary>
public sealed class MotionAnimation
{
	public int Index { get; set; }
	public int ModelIndex { get; set; }

	public ChrChunk? GamtChunk { get; set; }
	public ChrChunk NmdmChunk { get; set; } = null!;
	public ChrChunk? Pof0Chunk { get; set; }

	public int? Identifier { get; set; }

	public byte[] RawNmdm => NmdmChunk.Payload;

	/// <summary>
	/// Lazily parsed on first access — see ChrModel.Model's doc
	/// comment for why this isn't populated by ChrArchiveReader itself.
	/// </summary>
	public NjMotion? Motion { get; set; }
}

