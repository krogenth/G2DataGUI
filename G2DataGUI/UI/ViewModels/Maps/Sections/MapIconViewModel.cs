using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>Item1/2/3Offset reference Items by name — same
/// SelectedItemN/AvailableItems pattern as EnemyStatsViewModel's
/// Item1/Item2, just three slots instead of two.</summary>
public sealed class MapIconViewModel(MapIcon model, IRepository repository, ObservableCollection<GameItemViewModel> availableItems)
	: ItemViewModel<MapIcon>(model, repository)
{
	public ObservableCollection<GameItemViewModel> AvailableItems => availableItems;

	public byte Id
	{
		get => Model.Id;
		set => SetModelValue(value, m => m.Id, (m, v) => m.Id = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public short Unknown2
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

	public float Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}

	public float YAngle
	{
		get => Model.YAngle;
		set => SetModelValue(value, m => m.YAngle, (m, v) => m.YAngle = v);
	}

	public GameItemViewModel? SelectedItem1
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Item1Offset);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Item1Offset, (m, v) => m.Item1Offset = v, nameof(Item1Offset));
			OnPropertyChanged(nameof(SelectedItem1));
		}
	}

	public short Item1Offset
	{
		get => Model.Item1Offset;
		set
		{
			SetModelValue(value, m => m.Item1Offset, (m, v) => m.Item1Offset = v);
			OnPropertyChanged(nameof(SelectedItem1));
		}
	}

	public GameItemViewModel? SelectedItem2
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Item2Offset);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Item2Offset, (m, v) => m.Item2Offset = v, nameof(Item2Offset));
			OnPropertyChanged(nameof(SelectedItem2));
		}
	}

	public short Item2Offset
	{
		get => Model.Item2Offset;
		set
		{
			SetModelValue(value, m => m.Item2Offset, (m, v) => m.Item2Offset = v);
			OnPropertyChanged(nameof(SelectedItem2));
		}
	}

	public GameItemViewModel? SelectedItem3
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Item3Offset);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Item3Offset, (m, v) => m.Item3Offset = v, nameof(Item3Offset));
			OnPropertyChanged(nameof(SelectedItem3));
		}
	}

	public short Item3Offset
	{
		get => Model.Item3Offset;
		set
		{
			SetModelValue(value, m => m.Item3Offset, (m, v) => m.Item3Offset = v);
			OnPropertyChanged(nameof(SelectedItem3));
		}
	}

	public short Flag
	{
		get => Model.Flag;
		set => SetModelValue(value, m => m.Flag, (m, v) => m.Flag = v);
	}
}
