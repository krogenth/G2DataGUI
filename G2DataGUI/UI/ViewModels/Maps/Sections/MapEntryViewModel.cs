using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>
/// Position (and every other Vector3/Vector2 field across the Maps
/// sections) is exposed as separate X/Y/Z float properties rather than
/// bound directly — System.Numerics.Vector3 is a struct, so a property
/// getter like Model.Position returns a COPY; binding straight to
/// "Position.X" would silently never write back to the actual model.
/// Every Vector-typed field in this section (and the rest of Maps) uses
/// this same reconstruct-the-whole-vector pattern.
///
/// Id doubles as a reference to another map — its 4-digit lowercase hex
/// representation is that map's folder name (Id 4096 is 0x1000, i.e. map
/// "1000"; confirmed against real data, not a guess the way MapScript's
/// MapID field was before this was confirmed to work the same way).
/// SelectedMap resolves/sets that by matching against AvailableMaps
/// (Map.Name), same reference-by-name shape as every other picker in this
/// project, just resolving against Maps instead of Moves/Items/etc.
/// </summary>
public sealed class MapEntryViewModel(MapEntry model, IRepository repository, ObservableCollection<MapViewModel> availableMaps)
	: ItemViewModel<MapEntry>(model, repository)
{
	public ObservableCollection<MapViewModel> AvailableMaps => availableMaps;

	public MapViewModel? SelectedMap
	{
		get => AvailableMaps.FirstOrDefault(map =>
			string.Equals(map.Name, Id.ToString("x4"), StringComparison.OrdinalIgnoreCase));
		set
		{
			if (value is null || !uint.TryParse(value.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint parsed))
			{
				return;
			}

			SetModelValue(parsed, m => m.Id, (m, v) => m.Id = v, nameof(Id));
			OnPropertyChanged(nameof(SelectedMap));
		}
	}

	public uint Id
	{
		get => Model.Id;
		set
		{
			SetModelValue(value, m => m.Id, (m, v) => m.Id = v);
			OnPropertyChanged(nameof(SelectedMap));
		}
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

	public float Direction
	{
		get => Model.Direction;
		set => SetModelValue(value, m => m.Direction, (m, v) => m.Direction = v);
	}

	public float Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public int Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
	}

	public int Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}
}
