using System.Numerics;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

public sealed class MapInstanceViewModel(MapInstance model, IRepository repository) : ItemViewModel<MapInstance>(model, repository)
{
	public ushort Id
	{
		get => Model.Id;
		set => SetModelValue(value, m => m.Id, (m, v) => m.Id = v);
	}

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

	public byte Translation
	{
		get => Model.Translation;
		set => SetModelValue(value, m => m.Translation, (m, v) => m.Translation = v);
	}

	public byte Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
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

	public float AngleX
	{
		get => Model.Angle.X;
		set => SetModelValue(value, m => m.Angle.X, (m, v) => m.Angle = new Vector3(v, m.Angle.Y, m.Angle.Z));
	}

	public float AngleY
	{
		get => Model.Angle.Y;
		set => SetModelValue(value, m => m.Angle.Y, (m, v) => m.Angle = new Vector3(m.Angle.X, v, m.Angle.Z));
	}

	public float AngleZ
	{
		get => Model.Angle.Z;
		set => SetModelValue(value, m => m.Angle.Z, (m, v) => m.Angle = new Vector3(m.Angle.X, m.Angle.Y, v));
	}

	public float CX
	{
		get => Model.C.X;
		set => SetModelValue(value, m => m.C.X, (m, v) => m.C = new Vector3(v, m.C.Y, m.C.Z));
	}

	public float CY
	{
		get => Model.C.Y;
		set => SetModelValue(value, m => m.C.Y, (m, v) => m.C = new Vector3(m.C.X, v, m.C.Z));
	}

	public float CZ
	{
		get => Model.C.Z;
		set => SetModelValue(value, m => m.C.Z, (m, v) => m.C = new Vector3(m.C.X, m.C.Y, v));
	}

	public float Rotation
	{
		get => Model.Rotation;
		set => SetModelValue(value, m => m.Rotation, (m, v) => m.Rotation = v);
	}
}
