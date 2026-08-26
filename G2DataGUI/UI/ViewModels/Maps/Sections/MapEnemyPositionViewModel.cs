using System.Numerics;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>Unknown2-7 are all Vector3, same shape as Position — kept as
/// such (not flattened to bare floats) since that's itself a hint they're
/// probably positions/directions of some kind too, even without knowing
/// which — see MapEnemyPosition's own doc comment.</summary>
public sealed class MapEnemyPositionViewModel(MapEnemyPosition model, IRepository repository) : ItemViewModel<MapEnemyPosition>(model, repository)
{
	public ushort Index
	{
		get => Model.Index;
		set => SetModelValue(value, m => m.Index, (m, v) => m.Index = v);
	}

	public short Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public float MinimumX
	{
		get => Model.Minimum.X;
		set => SetModelValue(value, m => m.Minimum.X, (m, v) => m.Minimum = new Vector2(v, m.Minimum.Y));
	}

	public float MinimumY
	{
		get => Model.Minimum.Y;
		set => SetModelValue(value, m => m.Minimum.Y, (m, v) => m.Minimum = new Vector2(m.Minimum.X, v));
	}

	public float MaximumX
	{
		get => Model.Maximum.X;
		set => SetModelValue(value, m => m.Maximum.X, (m, v) => m.Maximum = new Vector2(v, m.Maximum.Y));
	}

	public float MaximumY
	{
		get => Model.Maximum.Y;
		set => SetModelValue(value, m => m.Maximum.Y, (m, v) => m.Maximum = new Vector2(m.Maximum.X, v));
	}

	public float PositionX
	{
		get => Model.Position.X;
		set => SetModelValue(value, m => m.Position.X, (m, v) => m.Position = new Vector3(v, m.Position.Y, m.Position.Z));
	}

	public float PositionY
	{
		get => Model.Position.Y;
		set => SetModelValue(value, m => m.Position.Y, (m, v) => m.Position = new Vector3(m.Position.X, v, m.Position.Z));
	}

	public float PositionZ
	{
		get => Model.Position.Z;
		set => SetModelValue(value, m => m.Position.Z, (m, v) => m.Position = new Vector3(m.Position.X, m.Position.Y, v));
	}

	public float Unknown2X
	{
		get => Model.Unknown2.X;
		set => SetModelValue(value, m => m.Unknown2.X, (m, v) => m.Unknown2 = new Vector3(v, m.Unknown2.Y, m.Unknown2.Z));
	}

	public float Unknown2Y
	{
		get => Model.Unknown2.Y;
		set => SetModelValue(value, m => m.Unknown2.Y, (m, v) => m.Unknown2 = new Vector3(m.Unknown2.X, v, m.Unknown2.Z));
	}

	public float Unknown2Z
	{
		get => Model.Unknown2.Z;
		set => SetModelValue(value, m => m.Unknown2.Z, (m, v) => m.Unknown2 = new Vector3(m.Unknown2.X, m.Unknown2.Y, v));
	}

	public float Unknown3X
	{
		get => Model.Unknown3.X;
		set => SetModelValue(value, m => m.Unknown3.X, (m, v) => m.Unknown3 = new Vector3(v, m.Unknown3.Y, m.Unknown3.Z));
	}

	public float Unknown3Y
	{
		get => Model.Unknown3.Y;
		set => SetModelValue(value, m => m.Unknown3.Y, (m, v) => m.Unknown3 = new Vector3(m.Unknown3.X, v, m.Unknown3.Z));
	}

	public float Unknown3Z
	{
		get => Model.Unknown3.Z;
		set => SetModelValue(value, m => m.Unknown3.Z, (m, v) => m.Unknown3 = new Vector3(m.Unknown3.X, m.Unknown3.Y, v));
	}

	public float Unknown4X
	{
		get => Model.Unknown4.X;
		set => SetModelValue(value, m => m.Unknown4.X, (m, v) => m.Unknown4 = new Vector3(v, m.Unknown4.Y, m.Unknown4.Z));
	}

	public float Unknown4Y
	{
		get => Model.Unknown4.Y;
		set => SetModelValue(value, m => m.Unknown4.Y, (m, v) => m.Unknown4 = new Vector3(m.Unknown4.X, v, m.Unknown4.Z));
	}

	public float Unknown4Z
	{
		get => Model.Unknown4.Z;
		set => SetModelValue(value, m => m.Unknown4.Z, (m, v) => m.Unknown4 = new Vector3(m.Unknown4.X, m.Unknown4.Y, v));
	}

	public float Unknown5X
	{
		get => Model.Unknown5.X;
		set => SetModelValue(value, m => m.Unknown5.X, (m, v) => m.Unknown5 = new Vector3(v, m.Unknown5.Y, m.Unknown5.Z));
	}

	public float Unknown5Y
	{
		get => Model.Unknown5.Y;
		set => SetModelValue(value, m => m.Unknown5.Y, (m, v) => m.Unknown5 = new Vector3(m.Unknown5.X, v, m.Unknown5.Z));
	}

	public float Unknown5Z
	{
		get => Model.Unknown5.Z;
		set => SetModelValue(value, m => m.Unknown5.Z, (m, v) => m.Unknown5 = new Vector3(m.Unknown5.X, m.Unknown5.Y, v));
	}

	public float Unknown6X
	{
		get => Model.Unknown6.X;
		set => SetModelValue(value, m => m.Unknown6.X, (m, v) => m.Unknown6 = new Vector3(v, m.Unknown6.Y, m.Unknown6.Z));
	}

	public float Unknown6Y
	{
		get => Model.Unknown6.Y;
		set => SetModelValue(value, m => m.Unknown6.Y, (m, v) => m.Unknown6 = new Vector3(m.Unknown6.X, v, m.Unknown6.Z));
	}

	public float Unknown6Z
	{
		get => Model.Unknown6.Z;
		set => SetModelValue(value, m => m.Unknown6.Z, (m, v) => m.Unknown6 = new Vector3(m.Unknown6.X, m.Unknown6.Y, v));
	}

	public float Unknown7X
	{
		get => Model.Unknown7.X;
		set => SetModelValue(value, m => m.Unknown7.X, (m, v) => m.Unknown7 = new Vector3(v, m.Unknown7.Y, m.Unknown7.Z));
	}

	public float Unknown7Y
	{
		get => Model.Unknown7.Y;
		set => SetModelValue(value, m => m.Unknown7.Y, (m, v) => m.Unknown7 = new Vector3(m.Unknown7.X, v, m.Unknown7.Z));
	}

	public float Unknown7Z
	{
		get => Model.Unknown7.Z;
		set => SetModelValue(value, m => m.Unknown7.Z, (m, v) => m.Unknown7 = new Vector3(m.Unknown7.X, m.Unknown7.Y, v));
	}
}
