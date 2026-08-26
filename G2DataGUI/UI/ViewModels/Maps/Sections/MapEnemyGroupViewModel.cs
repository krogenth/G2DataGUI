using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>
/// Flattened, not wrapped in further sub-ViewModels — Unknown1/Enemies/
/// Unknown2 are all reference types (classes, not structs, unlike the
/// Vector3 fields elsewhere in Maps), so direct nested property access
/// (Model.Unknown1.Unknown1, etc.) mutates the real object correctly
/// without needing a whole-object reconstruction the way Vector3 does.
/// EnemyOffset (on each Enemy1-4) stays a raw short — see MapEnemyType's
/// own doc comment on why it isn't resolved to an Enemy name yet.
/// </summary>
public sealed class MapEnemyGroupViewModel(MapEnemyGroup model, IRepository repository) : ItemViewModel<MapEnemyGroup>(model, repository)
{

	public ushort Index
	{
		get => Model.Index;
		set => SetModelValue(value, m => m.Index, (m, v) => m.Index = v);
	}

	public short FirstUnknown1
	{
		get => Model.Unknown1.Unknown1;
		set => SetModelValue(value, m => m.Unknown1.Unknown1, (m, v) => m.Unknown1.Unknown1 = v);
	}

	public short FirstUnknown2
	{
		get => Model.Unknown1.Unknown2;
		set => SetModelValue(value, m => m.Unknown1.Unknown2, (m, v) => m.Unknown1.Unknown2 = v);
	}

	public short FirstUnknown3
	{
		get => Model.Unknown1.Unknown3;
		set => SetModelValue(value, m => m.Unknown1.Unknown3, (m, v) => m.Unknown1.Unknown3 = v);
	}

	public short FirstUnknown4
	{
		get => Model.Unknown1.Unknown4;
		set => SetModelValue(value, m => m.Unknown1.Unknown4, (m, v) => m.Unknown1.Unknown4 = v);
	}

	public short FirstUnknown5
	{
		get => Model.Unknown1.Unknown5;
		set => SetModelValue(value, m => m.Unknown1.Unknown5, (m, v) => m.Unknown1.Unknown5 = v);
	}

	public float FirstUnknown6
	{
		get => Model.Unknown1.Unknown6;
		set => SetModelValue(value, m => m.Unknown1.Unknown6, (m, v) => m.Unknown1.Unknown6 = v);
	}

	public short FirstUnknown7
	{
		get => Model.Unknown1.Unknown7;
		set => SetModelValue(value, m => m.Unknown1.Unknown7, (m, v) => m.Unknown1.Unknown7 = v);
	}

	public short FirstUnknown8
	{
		get => Model.Unknown1.Unknown8;
		set => SetModelValue(value, m => m.Unknown1.Unknown8, (m, v) => m.Unknown1.Unknown8 = v);
	}

	public short FirstUnknown9
	{
		get => Model.Unknown1.Unknown9;
		set => SetModelValue(value, m => m.Unknown1.Unknown9, (m, v) => m.Unknown1.Unknown9 = v);
	}

	public short FirstUnknown10
	{
		get => Model.Unknown1.Unknown10;
		set => SetModelValue(value, m => m.Unknown1.Unknown10, (m, v) => m.Unknown1.Unknown10 = v);
	}

	public short Enemy1Index
	{
		get => Model.Enemies.Enemy1.Index;
		set => SetModelValue(value, m => m.Enemies.Enemy1.Index, (m, v) => m.Enemies.Enemy1.Index = v);
	}

	public short Enemy1EnemyCount
	{
		get => Model.Enemies.Enemy1.EnemyCount;
		set => SetModelValue(value, m => m.Enemies.Enemy1.EnemyCount, (m, v) => m.Enemies.Enemy1.EnemyCount = v);
	}

	public short Enemy1EnemyOffset
	{
		get => Model.Enemies.Enemy1.EnemyOffset;
		set => SetModelValue(value, m => m.Enemies.Enemy1.EnemyOffset, (m, v) => m.Enemies.Enemy1.EnemyOffset = v);
	}

	public short Enemy1Unknown1
	{
		get => Model.Enemies.Enemy1.Unknown1;
		set => SetModelValue(value, m => m.Enemies.Enemy1.Unknown1, (m, v) => m.Enemies.Enemy1.Unknown1 = v);
	}

	public short Enemy1Unknown2
	{
		get => Model.Enemies.Enemy1.Unknown2;
		set => SetModelValue(value, m => m.Enemies.Enemy1.Unknown2, (m, v) => m.Enemies.Enemy1.Unknown2 = v);
	}

	public short Enemy1Unknown3
	{
		get => Model.Enemies.Enemy1.Unknown3;
		set => SetModelValue(value, m => m.Enemies.Enemy1.Unknown3, (m, v) => m.Enemies.Enemy1.Unknown3 = v);
	}

	public short Enemy1Unknown4
	{
		get => Model.Enemies.Enemy1.Unknown4;
		set => SetModelValue(value, m => m.Enemies.Enemy1.Unknown4, (m, v) => m.Enemies.Enemy1.Unknown4 = v);
	}

	public short Enemy2Index
	{
		get => Model.Enemies.Enemy2.Index;
		set => SetModelValue(value, m => m.Enemies.Enemy2.Index, (m, v) => m.Enemies.Enemy2.Index = v);
	}

	public short Enemy2EnemyCount
	{
		get => Model.Enemies.Enemy2.EnemyCount;
		set => SetModelValue(value, m => m.Enemies.Enemy2.EnemyCount, (m, v) => m.Enemies.Enemy2.EnemyCount = v);
	}

	public short Enemy2EnemyOffset
	{
		get => Model.Enemies.Enemy2.EnemyOffset;
		set => SetModelValue(value, m => m.Enemies.Enemy2.EnemyOffset, (m, v) => m.Enemies.Enemy2.EnemyOffset = v);
	}

	public short Enemy2Unknown1
	{
		get => Model.Enemies.Enemy2.Unknown1;
		set => SetModelValue(value, m => m.Enemies.Enemy2.Unknown1, (m, v) => m.Enemies.Enemy2.Unknown1 = v);
	}

	public short Enemy2Unknown2
	{
		get => Model.Enemies.Enemy2.Unknown2;
		set => SetModelValue(value, m => m.Enemies.Enemy2.Unknown2, (m, v) => m.Enemies.Enemy2.Unknown2 = v);
	}

	public short Enemy2Unknown3
	{
		get => Model.Enemies.Enemy2.Unknown3;
		set => SetModelValue(value, m => m.Enemies.Enemy2.Unknown3, (m, v) => m.Enemies.Enemy2.Unknown3 = v);
	}

	public short Enemy2Unknown4
	{
		get => Model.Enemies.Enemy2.Unknown4;
		set => SetModelValue(value, m => m.Enemies.Enemy2.Unknown4, (m, v) => m.Enemies.Enemy2.Unknown4 = v);
	}

	public short Enemy3Index
	{
		get => Model.Enemies.Enemy3.Index;
		set => SetModelValue(value, m => m.Enemies.Enemy3.Index, (m, v) => m.Enemies.Enemy3.Index = v);
	}

	public short Enemy3EnemyCount
	{
		get => Model.Enemies.Enemy3.EnemyCount;
		set => SetModelValue(value, m => m.Enemies.Enemy3.EnemyCount, (m, v) => m.Enemies.Enemy3.EnemyCount = v);
	}

	public short Enemy3EnemyOffset
	{
		get => Model.Enemies.Enemy3.EnemyOffset;
		set => SetModelValue(value, m => m.Enemies.Enemy3.EnemyOffset, (m, v) => m.Enemies.Enemy3.EnemyOffset = v);
	}

	public short Enemy3Unknown1
	{
		get => Model.Enemies.Enemy3.Unknown1;
		set => SetModelValue(value, m => m.Enemies.Enemy3.Unknown1, (m, v) => m.Enemies.Enemy3.Unknown1 = v);
	}

	public short Enemy3Unknown2
	{
		get => Model.Enemies.Enemy3.Unknown2;
		set => SetModelValue(value, m => m.Enemies.Enemy3.Unknown2, (m, v) => m.Enemies.Enemy3.Unknown2 = v);
	}

	public short Enemy3Unknown3
	{
		get => Model.Enemies.Enemy3.Unknown3;
		set => SetModelValue(value, m => m.Enemies.Enemy3.Unknown3, (m, v) => m.Enemies.Enemy3.Unknown3 = v);
	}

	public short Enemy3Unknown4
	{
		get => Model.Enemies.Enemy3.Unknown4;
		set => SetModelValue(value, m => m.Enemies.Enemy3.Unknown4, (m, v) => m.Enemies.Enemy3.Unknown4 = v);
	}

	public short Enemy4Index
	{
		get => Model.Enemies.Enemy4.Index;
		set => SetModelValue(value, m => m.Enemies.Enemy4.Index, (m, v) => m.Enemies.Enemy4.Index = v);
	}

	public short Enemy4EnemyCount
	{
		get => Model.Enemies.Enemy4.EnemyCount;
		set => SetModelValue(value, m => m.Enemies.Enemy4.EnemyCount, (m, v) => m.Enemies.Enemy4.EnemyCount = v);
	}

	public short Enemy4EnemyOffset
	{
		get => Model.Enemies.Enemy4.EnemyOffset;
		set => SetModelValue(value, m => m.Enemies.Enemy4.EnemyOffset, (m, v) => m.Enemies.Enemy4.EnemyOffset = v);
	}

	public short Enemy4Unknown1
	{
		get => Model.Enemies.Enemy4.Unknown1;
		set => SetModelValue(value, m => m.Enemies.Enemy4.Unknown1, (m, v) => m.Enemies.Enemy4.Unknown1 = v);
	}

	public short Enemy4Unknown2
	{
		get => Model.Enemies.Enemy4.Unknown2;
		set => SetModelValue(value, m => m.Enemies.Enemy4.Unknown2, (m, v) => m.Enemies.Enemy4.Unknown2 = v);
	}

	public short Enemy4Unknown3
	{
		get => Model.Enemies.Enemy4.Unknown3;
		set => SetModelValue(value, m => m.Enemies.Enemy4.Unknown3, (m, v) => m.Enemies.Enemy4.Unknown3 = v);
	}

	public short Enemy4Unknown4
	{
		get => Model.Enemies.Enemy4.Unknown4;
		set => SetModelValue(value, m => m.Enemies.Enemy4.Unknown4, (m, v) => m.Enemies.Enemy4.Unknown4 = v);
	}

	public short SecondUnknown1
	{
		get => Model.Unknown2.Unknown1;
		set => SetModelValue(value, m => m.Unknown2.Unknown1, (m, v) => m.Unknown2.Unknown1 = v);
	}

	public short SecondUnknown2
	{
		get => Model.Unknown2.Unknown2;
		set => SetModelValue(value, m => m.Unknown2.Unknown2, (m, v) => m.Unknown2.Unknown2 = v);
	}

	public short SecondUnknown3
	{
		get => Model.Unknown2.Unknown3;
		set => SetModelValue(value, m => m.Unknown2.Unknown3, (m, v) => m.Unknown2.Unknown3 = v);
	}

	public short SecondUnknown4
	{
		get => Model.Unknown2.Unknown4;
		set => SetModelValue(value, m => m.Unknown2.Unknown4, (m, v) => m.Unknown2.Unknown4 = v);
	}

	public short SecondUnknown5
	{
		get => Model.Unknown2.Unknown5;
		set => SetModelValue(value, m => m.Unknown2.Unknown5, (m, v) => m.Unknown2.Unknown5 = v);
	}

	public short SecondUnknown6
	{
		get => Model.Unknown2.Unknown6;
		set => SetModelValue(value, m => m.Unknown2.Unknown6, (m, v) => m.Unknown2.Unknown6 = v);
	}

	public short SecondUnknown7
	{
		get => Model.Unknown2.Unknown7;
		set => SetModelValue(value, m => m.Unknown2.Unknown7, (m, v) => m.Unknown2.Unknown7 = v);
	}

	public short SecondUnknown8
	{
		get => Model.Unknown2.Unknown8;
		set => SetModelValue(value, m => m.Unknown2.Unknown8, (m, v) => m.Unknown2.Unknown8 = v);
	}

	public short SecondUnknown9
	{
		get => Model.Unknown2.Unknown9;
		set => SetModelValue(value, m => m.Unknown2.Unknown9, (m, v) => m.Unknown2.Unknown9 = v);
	}

	public short SecondUnknown10
	{
		get => Model.Unknown2.Unknown10;
		set => SetModelValue(value, m => m.Unknown2.Unknown10, (m, v) => m.Unknown2.Unknown10 = v);
	}

	public short SecondUnknown11
	{
		get => Model.Unknown2.Unknown11;
		set => SetModelValue(value, m => m.Unknown2.Unknown11, (m, v) => m.Unknown2.Unknown11 = v);
	}

	public short SecondUnknown12
	{
		get => Model.Unknown2.Unknown12;
		set => SetModelValue(value, m => m.Unknown2.Unknown12, (m, v) => m.Unknown2.Unknown12 = v);
	}

	public short SecondUnknown13
	{
		get => Model.Unknown2.Unknown13;
		set => SetModelValue(value, m => m.Unknown2.Unknown13, (m, v) => m.Unknown2.Unknown13 = v);
	}

	public short SecondUnknown14
	{
		get => Model.Unknown2.Unknown14;
		set => SetModelValue(value, m => m.Unknown2.Unknown14, (m, v) => m.Unknown2.Unknown14 = v);
	}

	public short SecondUnknown15
	{
		get => Model.Unknown2.Unknown15;
		set => SetModelValue(value, m => m.Unknown2.Unknown15, (m, v) => m.Unknown2.Unknown15 = v);
	}

	public short SecondUnknown16
	{
		get => Model.Unknown2.Unknown16;
		set => SetModelValue(value, m => m.Unknown2.Unknown16, (m, v) => m.Unknown2.Unknown16 = v);
	}

	public short SecondUnknown17
	{
		get => Model.Unknown2.Unknown17;
		set => SetModelValue(value, m => m.Unknown2.Unknown17, (m, v) => m.Unknown2.Unknown17 = v);
	}

	public short SecondUnknown18
	{
		get => Model.Unknown2.Unknown18;
		set => SetModelValue(value, m => m.Unknown2.Unknown18, (m, v) => m.Unknown2.Unknown18 = v);
	}

	public short SecondUnknown19
	{
		get => Model.Unknown2.Unknown19;
		set => SetModelValue(value, m => m.Unknown2.Unknown19, (m, v) => m.Unknown2.Unknown19 = v);
	}

	public short SecondUnknown20
	{
		get => Model.Unknown2.Unknown20;
		set => SetModelValue(value, m => m.Unknown2.Unknown20, (m, v) => m.Unknown2.Unknown20 = v);
	}
}
