using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Maps;

/// <summary>
/// One map folder — Name is both the folder name and the base filename
/// (map/[Name]/[Name].mdt, and optionally map/[Name]/[Name].chr or .chr2
/// for the associated model/motion archive, and map/[Name]/textures_[Name].pvl/
/// for its DDS textures — same base-name convention this project already
/// relies on elsewhere, e.g. a410.chr).
///
/// Header/Entries/.../Shop are the full .mdt content, via MapReader — not
/// a placeholder anymore. MapName is a display name pulled out of the
/// dialogue section itself (see MapReader's own doc comment on how) —
/// falls back to Name (the folder name) when it can't be found, so
/// there's always something reasonable to show in a list.
/// </summary>
public sealed class Map : IIdentifiable<int>
{
	public int Id { get; set; }

	public required string Name { get; init; }

	public required string MdtPath { get; init; }

	/// <summary>Null when this map has no associated .chr/.chr2 archive —
	/// not every map necessarily has models tied to it (or the archive
	/// just hasn't been placed in the folder).</summary>
	public string? ChrPath { get; init; }

	/// <summary>Null when no textures_[Name].pvl folder was found
	/// alongside the .mdt.</summary>
	public string? TexturesPath { get; init; }

	public MapHeader Header { get; set; } = new();
	public List<MapEntry> Entries { get; set; } = [];
	public List<MapInstance> Instances { get; set; } = [];
	public List<MapHTA> HTAs { get; set; } = [];
	public List<MapScript> Scripts { get; set; } = [];
	public List<MapEnemyPosition> EnemyPositions { get; set; } = [];
	public List<MapEnemyGroup> EnemyGroups { get; set; } = [];
	public List<MapMOS> MOSs { get; set; } = [];
	public MapDialogue Dialogue { get; set; } = new();
	public List<MapIcon> Icons { get; set; } = [];

	/// <summary>Null when Header.ShopLength == 0 — this map has no shop.</summary>
	public MapShop? Shop { get; set; }

	/// <summary>Pulled out of the dialogue section (see MapReader), falls
	/// back to Name when it couldn't be found there.</summary>
	public string MapName { get; set; } = string.Empty;
}
