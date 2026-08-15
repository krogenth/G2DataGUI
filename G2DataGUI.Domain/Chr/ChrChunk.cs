namespace G2DataGUI.Domain.Chr;

/// <summary>
/// One raw FourCC-tagged chunk from a .chr/.chr2 archive — the container
/// format's atomic unit. Payload is kept as opaque bytes here; interpreting
/// it (NJCM geometry, NMDM motion, GIXL texture-id list, ...) is a later
/// pass over specific chunks, not this type's job. Ported from
/// DreamcastChrViewer's ChrArchive.cs — see PORTING-Chr.md.
/// </summary>
public sealed class ChrChunk
{
	public int Offset { get; set; }
	public string FourCC { get; set; } = string.Empty;
	public uint PayloadLength { get; set; }
	public byte[] Payload { get; set; } = [];
}
