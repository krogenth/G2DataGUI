namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.EnemyGroups — up to 4 enemy encounter slots
/// (Enemies) plus two blocks of not-yet-understood surrounding data
/// (Unknown1/Unknown2). Grouped into this one file rather than split
/// across four — MapEnemyGroupFirstUnknown/SecondUnknown/MapEnemyList/
/// MapEnemyType only ever appear as parts of a MapEnemyGroup, never
/// independently.</summary>
public sealed class MapEnemyGroup
{
	public ushort Index { get; set; }
	public MapEnemyGroupFirstUnknown Unknown1 { get; set; } = new();
	public MapEnemyList Enemies { get; set; } = new();
	public MapEnemyGroupSecondUnknown Unknown2 { get; set; } = new();
}

/// <summary>Not yet understood — Unknown6 is the one float among an
/// otherwise all-short block, which is at least a concrete enough
/// observation to keep in mind for whoever investigates this next.</summary>
public sealed class MapEnemyGroupFirstUnknown
{
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public short Unknown5 { get; set; }
	public float Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }
}

/// <summary>Not yet understood — 20 consecutive shorts, no float/byte
/// fields breaking up the pattern the way FirstUnknown has one.</summary>
public sealed class MapEnemyGroupSecondUnknown
{
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public short Unknown5 { get; set; }
	public short Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }
	public short Unknown11 { get; set; }
	public short Unknown12 { get; set; }
	public short Unknown13 { get; set; }
	public short Unknown14 { get; set; }
	public short Unknown15 { get; set; }
	public short Unknown16 { get; set; }
	public short Unknown17 { get; set; }
	public short Unknown18 { get; set; }
	public short Unknown19 { get; set; }
	public short Unknown20 { get; set; }
}

/// <summary>Always exactly 4 enemy-type slots per group.</summary>
public sealed class MapEnemyList
{
	public MapEnemyType Enemy1 { get; set; } = new();
	public MapEnemyType Enemy2 { get; set; } = new();
	public MapEnemyType Enemy3 { get; set; } = new();
	public MapEnemyType Enemy4 { get; set; } = new();
}

/// <summary>EnemyOffset is presumably a raw Enemy-table index (same
/// "resolve to a name later" convention as elsewhere), but that hasn't
/// been confirmed the way e.g. ManaeggSpell.SpellOffset was — kept as a
/// plain short until it has.</summary>
public sealed class MapEnemyType
{
	public short Index { get; set; }
	public short EnemyCount { get; set; }
	public short EnemyOffset { get; set; }
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }
}
