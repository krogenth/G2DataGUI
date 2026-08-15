namespace G2DataGUI.Domain.Chr;

/// <summary>
/// One parsed .chr/.chr2 file — every chunk retained losslessly (including
/// POF0 relocation data and unrecognized chunk types), plus the higher-level
/// Models/MotionSets association built on top of them. See
/// ChrArchiveReader for how this gets populated, and PORTING-Chr.md for
/// what's deliberately not built yet (parsed geometry, parsed motion, a
/// writer).
/// </summary>
public sealed class ChrArchive
{
	public string FileName { get; set; } = string.Empty;

	public List<ChrChunk> Chunks { get; set; } = [];
	public CddhInfo? Header { get; set; }

	public List<ChrModel> Models { get; set; } = [];
	public List<MotionSet> MotionSets { get; set; } = [];

	public ChrModel? FindModel(int index) =>
		Models.FirstOrDefault(model => model.Index == index);
}
