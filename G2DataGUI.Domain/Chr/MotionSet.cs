namespace G2DataGUI.Domain.Chr;

/// <summary>
/// One MIXL chunk's worth of motion associations for a single
/// model: which motion identifiers exist and, once matched up during
/// parsing, the NMDM-backed animations themselves.
/// </summary>
public sealed class MotionSet
{
	public int ModelIndex { get; set; }
	public ChrChunk MixlChunk { get; set; } = null!;

	public List<byte> MotionIdentifiers { get; set; } = [];
	public List<MotionAnimation> Animations { get; set; } = [];
}
