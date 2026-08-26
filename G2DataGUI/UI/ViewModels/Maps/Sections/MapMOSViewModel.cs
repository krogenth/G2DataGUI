using System.Numerics;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

public sealed class MapMOSViewModel(MapMOS model, IRepository repository) : ItemViewModel<MapMOS>(model, repository)
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

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public byte Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
	}

	public byte Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}

	public byte Unknown4
	{
		get => Model.Unknown4;
		set => SetModelValue(value, m => m.Unknown4, (m, v) => m.Unknown4 = v);
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

	public short Unknown5
	{
		get => Model.Unknown5;
		set => SetModelValue(value, m => m.Unknown5, (m, v) => m.Unknown5 = v);
	}

	public short Unknown6
	{
		get => Model.Unknown6;
		set => SetModelValue(value, m => m.Unknown6, (m, v) => m.Unknown6 = v);
	}

	public short Unknown7
	{
		get => Model.Unknown7;
		set => SetModelValue(value, m => m.Unknown7, (m, v) => m.Unknown7 = v);
	}

	public short Unknown8
	{
		get => Model.Unknown8;
		set => SetModelValue(value, m => m.Unknown8, (m, v) => m.Unknown8 = v);
	}

	public short Unknown9
	{
		get => Model.Unknown9;
		set => SetModelValue(value, m => m.Unknown9, (m, v) => m.Unknown9 = v);
	}

	public short Unknown10
	{
		get => Model.Unknown10;
		set => SetModelValue(value, m => m.Unknown10, (m, v) => m.Unknown10 = v);
	}
}
