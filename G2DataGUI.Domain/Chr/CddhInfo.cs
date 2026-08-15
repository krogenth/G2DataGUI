namespace G2DataGUI.Domain.Chr;

/// <summary>The archive's leading CDDH chunk, decoded. Every .chr/.chr2
/// file must start with one — see ChrArchiveReader.</summary>
public sealed class CddhInfo
{
	public uint Version { get; set; }
	public uint ModelSectionPointer { get; set; }
	public uint MotionSectionPointer { get; set; }
}
