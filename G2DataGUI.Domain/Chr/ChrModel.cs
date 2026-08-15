using G2DataGUI.Domain.Models;

namespace G2DataGUI.Domain.Chr;

/// <summary>
/// One model within the archive — a GIXL (texture-id list) chunk
/// immediately followed by an NJCM (Ninja geometry) chunk, plus whatever
/// chunks follow until the next GIXL or the motion section starts (POF0
/// relocation data, MCS_ variants, or extra NJCM/POF0 pairs — see
/// ChrArchiveReader).
/// </summary>
public sealed class ChrModel
{
	public int Index { get; set; }

	public ChrChunk GixlChunk { get; set; } = null!;
	public ChrChunk NjcmChunk { get; set; } = null!;

	public List<uint> GlobalIdentifiers { get; set; } = [];
	public List<ChrChunk> AssociatedChunks { get; set; } = [];

	public List<MotionAnimation> Animations { get; set; } = [];

	/// <summary>Lazily parsed on first access by whatever needs it (a
	/// viewer, a pose evaluator) — not populated by ChrArchiveReader
	/// itself, since not every caller needs geometry (e.g. an archive
	/// editor moving whole models between files never needs to parse
	/// NJCM contents at all — see PORTING-Chr.md).</summary>
	public NjModel? Model { get; set; }
}

