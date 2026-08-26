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
/// Unknown1 (40 bytes) and Unknown18 (15 bytes) are shown as read-only hex
/// strings, not per-byte editable fields — neither has any established
/// internal structure, so exposing them as 40/15 separate byte properties
/// would just be noise with no way to know what any individual byte means.
/// MapID is the actual transition target (see MapScript's own doc
/// comment) — still a raw short here, not resolved to another map's Name,
/// since Map.Id (assigned by folder sort order — see MapRepository) isn't
/// confirmed to match whatever numbering MapID uses on disk.
/// </summary>
public sealed class MapScriptViewModel(MapScript model, IRepository repository, ObservableCollection<MapViewModel> availableMaps)
	: ItemViewModel<MapScript>(model, repository)
{
	public int Index => Model.Index;

	public string Unknown1Hex => Convert.ToHexString(Model.Unknown1);

	public float OffsetX
	{
		get => Model.Offset.X;
		set => SetModelValue(value, m => m.Offset.X, (m, v) => m.Offset = new Vector3(v, m.Offset.Y, m.Offset.Z));
	}

	public float OffsetY
	{
		get => Model.Offset.Y;
		set => SetModelValue(value, m => m.Offset.Y, (m, v) => m.Offset = new Vector3(m.Offset.X, v, m.Offset.Z));
	}

	public float OffsetZ
	{
		get => Model.Offset.Z;
		set => SetModelValue(value, m => m.Offset.Z, (m, v) => m.Offset = new Vector3(m.Offset.X, m.Offset.Y, v));
	}

	public float Direction
	{
		get => Model.Direction;
		set => SetModelValue(value, m => m.Direction, (m, v) => m.Direction = v);
	}

	public byte TransitionEffect
	{
		get => Model.TransitionEffect;
		set => SetModelValue(value, m => m.TransitionEffect, (m, v) => m.TransitionEffect = v);
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

	public ObservableCollection<MapViewModel> AvailableMaps => availableMaps;

	/// <summary>Same reference-by-hex-name resolution as
	/// MapEntryViewModel.SelectedMap — MapID is signed (short) but the
	/// hex representation that matches a map's folder name is its raw
	/// 16-bit pattern, not its signed decimal value, so this casts through
	/// ushort before formatting/parsing rather than hex-formatting a
	/// negative short directly.</summary>
	public MapViewModel? SelectedMap
	{
		get => AvailableMaps.FirstOrDefault(map =>
			string.Equals(map.Name, ((ushort)MapID).ToString("x4"), StringComparison.OrdinalIgnoreCase));
		set
		{
			if (value is null || !ushort.TryParse(value.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort parsed))
			{
				return;
			}

			SetModelValue(unchecked((short)parsed), m => m.MapID, (m, v) => m.MapID = v, nameof(MapID));
			OnPropertyChanged(nameof(SelectedMap));
		}
	}

	public short MapID
	{
		get => Model.MapID;
		set
		{
			SetModelValue(value, m => m.MapID, (m, v) => m.MapID = v);
			OnPropertyChanged(nameof(SelectedMap));
		}
	}

	public short Unknown5
	{
		get => Model.Unknown5;
		set => SetModelValue(value, m => m.Unknown5, (m, v) => m.Unknown5 = v);
	}

	public byte Unknown6
	{
		get => Model.Unknown6;
		set => SetModelValue(value, m => m.Unknown6, (m, v) => m.Unknown6 = v);
	}

	public byte Unknown7
	{
		get => Model.Unknown7;
		set => SetModelValue(value, m => m.Unknown7, (m, v) => m.Unknown7 = v);
	}

	public short Unknown8
	{
		get => Model.Unknown8;
		set => SetModelValue(value, m => m.Unknown8, (m, v) => m.Unknown8 = v);
	}

	public byte Unknown9
	{
		get => Model.Unknown9;
		set => SetModelValue(value, m => m.Unknown9, (m, v) => m.Unknown9 = v);
	}

	public byte Unknown10
	{
		get => Model.Unknown10;
		set => SetModelValue(value, m => m.Unknown10, (m, v) => m.Unknown10 = v);
	}

	public byte Unknown11
	{
		get => Model.Unknown11;
		set => SetModelValue(value, m => m.Unknown11, (m, v) => m.Unknown11 = v);
	}

	public byte Unknown12
	{
		get => Model.Unknown12;
		set => SetModelValue(value, m => m.Unknown12, (m, v) => m.Unknown12 = v);
	}

	public byte Unknown13
	{
		get => Model.Unknown13;
		set => SetModelValue(value, m => m.Unknown13, (m, v) => m.Unknown13 = v);
	}

	public byte Unknown14
	{
		get => Model.Unknown14;
		set => SetModelValue(value, m => m.Unknown14, (m, v) => m.Unknown14 = v);
	}

	public byte Unknown15
	{
		get => Model.Unknown15;
		set => SetModelValue(value, m => m.Unknown15, (m, v) => m.Unknown15 = v);
	}

	public byte Unknown16
	{
		get => Model.Unknown16;
		set => SetModelValue(value, m => m.Unknown16, (m, v) => m.Unknown16 = v);
	}

	public byte Unknown17
	{
		get => Model.Unknown17;
		set => SetModelValue(value, m => m.Unknown17, (m, v) => m.Unknown17 = v);
	}

	public string Unknown18Hex => Convert.ToHexString(Model.Unknown18);
}
