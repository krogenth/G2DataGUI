using System.Numerics;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

public sealed class MapHTAViewModel(MapHTA model, IRepository repository) : ItemViewModel<MapHTA>(model, repository)
{
	public byte Shape
	{
		get => Model.Shape;
		set => SetModelValue(value, m => m.Shape, (m, v) => m.Shape = v);
	}

	public byte Type
	{
		get => Model.Type;
		set => SetModelValue(value, m => m.Type, (m, v) => m.Type = v);
	}

	public byte Trigger
	{
		get => Model.Trigger;
		set => SetModelValue(value, m => m.Trigger, (m, v) => m.Trigger = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public int Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
	}

	public byte Attribute
	{
		get => Model.Attribute;
		set => SetModelValue(value, m => m.Attribute, (m, v) => m.Attribute = v);
	}

	public byte Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}

	public short Unknown4
	{
		get => Model.Unknown4;
		set => SetModelValue(value, m => m.Unknown4, (m, v) => m.Unknown4 = v);
	}

	public float Unknown5
	{
		get => Model.Unknown5;
		set => SetModelValue(value, m => m.Unknown5, (m, v) => m.Unknown5 = v);
	}

	public float MinimumX
	{
		get => Model.Minimum.X;
		set => SetModelValue(value, m => m.Minimum.X, (m, v) => m.Minimum = new Vector3(v, m.Minimum.Y, m.Minimum.Z));
	}

	public float MinimumY
	{
		get => Model.Minimum.Y;
		set => SetModelValue(value, m => m.Minimum.Y, (m, v) => m.Minimum = new Vector3(m.Minimum.X, v, m.Minimum.Z));
	}

	public float MinimumZ
	{
		get => Model.Minimum.Z;
		set => SetModelValue(value, m => m.Minimum.Z, (m, v) => m.Minimum = new Vector3(m.Minimum.X, m.Minimum.Y, v));
	}

	public float MaximumX
	{
		get => Model.Maximum.X;
		set => SetModelValue(value, m => m.Maximum.X, (m, v) => m.Maximum = new Vector3(v, m.Maximum.Y, m.Maximum.Z));
	}

	public float MaximumY
	{
		get => Model.Maximum.Y;
		set => SetModelValue(value, m => m.Maximum.Y, (m, v) => m.Maximum = new Vector3(m.Maximum.X, v, m.Maximum.Z));
	}

	public float MaximumZ
	{
		get => Model.Maximum.Z;
		set => SetModelValue(value, m => m.Maximum.Z, (m, v) => m.Maximum = new Vector3(m.Maximum.X, m.Maximum.Y, v));
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

	public int Unknown7
	{
		get => Model.Unknown7;
		set => SetModelValue(value, m => m.Unknown7, (m, v) => m.Unknown7 = v);
	}

	public float Unknown8
	{
		get => Model.Unknown8;
		set => SetModelValue(value, m => m.Unknown8, (m, v) => m.Unknown8 = v);
	}

	public float Unknown9
	{
		get => Model.Unknown9;
		set => SetModelValue(value, m => m.Unknown9, (m, v) => m.Unknown9 = v);
	}

	public float Unknown10
	{
		get => Model.Unknown10;
		set => SetModelValue(value, m => m.Unknown10, (m, v) => m.Unknown10 = v);
	}

	public float Unknown11
	{
		get => Model.Unknown11;
		set => SetModelValue(value, m => m.Unknown11, (m, v) => m.Unknown11 = v);
	}
}
