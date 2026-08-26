using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Starting;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Starting;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;
using G2DataGUI.UI.ViewModels.Manaeggs;

namespace G2DataGUI.UI.ViewModels.Starting;

/// <summary>
/// CharacterName is read-only, resolved via LocaleKeys.Characters[Id] —
/// same convention as CharacterLevelupsViewModel. AvailableItems/
/// AvailableManaeggs are the same ObservableCollections ItemsViewModel/
/// ManaeggsViewModel.Items already are — same "shared live collection, not
/// a copy" reasoning as ManaeggSpellViewModel.SelectedMove.
/// Weapon/Armor/Headgear/Footwear/Accessory all reference Items;
/// Manaegg references Manaeggs specifically, not Items — two different
/// pools, so two different Selected*/Available* pairs rather than one
/// shared AvailableItems doing double duty.
/// </summary>
public sealed class CharacterStartingStatsViewModel(
	CharacterStartingStats model,
	CharacterStartingStatsRepository repository,
	ILocalizationService locale,
	ObservableCollection<GameItemViewModel> availableItems,
	ObservableCollection<ManaeggViewModel> availableManaeggs)
	: ItemViewModel<CharacterStartingStats>(model, repository)
{
	public int Id => Model.Id;

	public string CharacterName { get; } = locale[LocaleKeys.Characters][model.Id];

	public ObservableCollection<GameItemViewModel> AvailableItems => availableItems;

	public ObservableCollection<ManaeggViewModel> AvailableManaeggs => availableManaeggs;

	public uint Experience
	{
		get => Model.Experience;
		set => SetModelValue(value, m => m.Experience, (m, v) => m.Experience = v);
	}

	public GameItemViewModel? SelectedWeapon
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Weapon);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Weapon, (m, v) => m.Weapon = v, nameof(Weapon));
			OnPropertyChanged(nameof(SelectedWeapon));
		}
	}

	public short Weapon
	{
		get => Model.Weapon;
		set
		{
			SetModelValue(value, m => m.Weapon, (m, v) => m.Weapon = v);
			OnPropertyChanged(nameof(SelectedWeapon));
		}
	}

	public GameItemViewModel? SelectedArmor
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Armor);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Armor, (m, v) => m.Armor = v, nameof(Armor));
			OnPropertyChanged(nameof(SelectedArmor));
		}
	}

	public short Armor
	{
		get => Model.Armor;
		set
		{
			SetModelValue(value, m => m.Armor, (m, v) => m.Armor = v);
			OnPropertyChanged(nameof(SelectedArmor));
		}
	}

	public GameItemViewModel? SelectedHeadgear
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Headgear);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Headgear, (m, v) => m.Headgear = v, nameof(Headgear));
			OnPropertyChanged(nameof(SelectedHeadgear));
		}
	}

	public short Headgear
	{
		get => Model.Headgear;
		set
		{
			SetModelValue(value, m => m.Headgear, (m, v) => m.Headgear = v);
			OnPropertyChanged(nameof(SelectedHeadgear));
		}
	}

	public GameItemViewModel? SelectedFootwear
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Footwear);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Footwear, (m, v) => m.Footwear = v, nameof(Footwear));
			OnPropertyChanged(nameof(SelectedFootwear));
		}
	}

	public short Footwear
	{
		get => Model.Footwear;
		set
		{
			SetModelValue(value, m => m.Footwear, (m, v) => m.Footwear = v);
			OnPropertyChanged(nameof(SelectedFootwear));
		}
	}

	public GameItemViewModel? SelectedAccessory
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == Accessory);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Accessory, (m, v) => m.Accessory = v, nameof(Accessory));
			OnPropertyChanged(nameof(SelectedAccessory));
		}
	}

	public short Accessory
	{
		get => Model.Accessory;
		set
		{
			SetModelValue(value, m => m.Accessory, (m, v) => m.Accessory = v);
			OnPropertyChanged(nameof(SelectedAccessory));
		}
	}

	public ManaeggViewModel? SelectedManaegg
	{
		get => AvailableManaeggs.FirstOrDefault(manaegg => manaegg.Id == Manaegg);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((short)value.Id, m => m.Manaegg, (m, v) => m.Manaegg = v, nameof(Manaegg));
			OnPropertyChanged(nameof(SelectedManaegg));
		}
	}

	public short Manaegg
	{
		get => Model.Manaegg;
		set
		{
			SetModelValue(value, m => m.Manaegg, (m, v) => m.Manaegg = v);
			OnPropertyChanged(nameof(SelectedManaegg));
		}
	}

	public short Stamina
	{
		get => Model.Stamina;
		set => SetModelValue(value, m => m.Stamina, (m, v) => m.Stamina = v);
	}

	public short Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public short Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
	}

	public short Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}

	public short Unknown4
	{
		get => Model.Unknown4;
		set => SetModelValue(value, m => m.Unknown4, (m, v) => m.Unknown4 = v);
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

	public short IpStun
	{
		get => Model.IpStun;
		set => SetModelValue(value, m => m.IpStun, (m, v) => m.IpStun = v);
	}

	public short IpCancelStun
	{
		get => Model.IpCancelStun;
		set => SetModelValue(value, m => m.IpCancelStun, (m, v) => m.IpCancelStun = v);
	}

	public byte ComboSpRegen
	{
		get => Model.ComboSpRegen;
		set => SetModelValue(value, m => m.ComboSpRegen, (m, v) => m.ComboSpRegen = v);
	}

	public byte CritSpRegen
	{
		get => Model.CritSpRegen;
		set => SetModelValue(value, m => m.CritSpRegen, (m, v) => m.CritSpRegen = v);
	}

	public byte Unknown11
	{
		get => Model.Unknown11;
		set => SetModelValue(value, m => m.Unknown11, (m, v) => m.Unknown11 = v);
	}

	public byte HitSpRegen
	{
		get => Model.HitSpRegen;
		set => SetModelValue(value, m => m.HitSpRegen, (m, v) => m.HitSpRegen = v);
	}

	public byte Unknown12
	{
		get => Model.Unknown12;
		set => SetModelValue(value, m => m.Unknown12, (m, v) => m.Unknown12 = v);
	}

	public byte EvasionStillRate
	{
		get => Model.EvasionStillRate;
		set => SetModelValue(value, m => m.EvasionStillRate, (m, v) => m.EvasionStillRate = v);
	}

	public byte EvasionMovingRate
	{
		get => Model.EvasionMovingRate;
		set => SetModelValue(value, m => m.EvasionMovingRate, (m, v) => m.EvasionMovingRate = v);
	}

	public byte KnockbackResist
	{
		get => Model.KnockbackResist;
		set => SetModelValue(value, m => m.KnockbackResist, (m, v) => m.KnockbackResist = v);
	}

	public short Unknown13
	{
		get => Model.Unknown13;
		set => SetModelValue(value, m => m.Unknown13, (m, v) => m.Unknown13 = v);
	}

	public short StatusRecoveryTime
	{
		get => Model.StatusRecoveryTime;
		set => SetModelValue(value, m => m.StatusRecoveryTime, (m, v) => m.StatusRecoveryTime = v);
	}

	public short TDMG
	{
		get => Model.TDMG;
		set => SetModelValue(value, m => m.TDMG, (m, v) => m.TDMG = v);
	}

	public short Unknown14
	{
		get => Model.Unknown14;
		set => SetModelValue(value, m => m.Unknown14, (m, v) => m.Unknown14 = v);
	}

	public short THEAL
	{
		get => Model.THEAL;
		set => SetModelValue(value, m => m.THEAL, (m, v) => m.THEAL = v);
	}

	public short Size
	{
		get => Model.Size;
		set => SetModelValue(value, m => m.Size, (m, v) => m.Size = v);
	}

	public short Unknown15
	{
		get => Model.Unknown15;
		set => SetModelValue(value, m => m.Unknown15, (m, v) => m.Unknown15 = v);
	}

	public short Unknown16
	{
		get => Model.Unknown16;
		set => SetModelValue(value, m => m.Unknown16, (m, v) => m.Unknown16 = v);
	}

	public short Unknown17
	{
		get => Model.Unknown17;
		set => SetModelValue(value, m => m.Unknown17, (m, v) => m.Unknown17 = v);
	}

	public short Unknown18
	{
		get => Model.Unknown18;
		set => SetModelValue(value, m => m.Unknown18, (m, v) => m.Unknown18 = v);
	}

	public short Unknown19
	{
		get => Model.Unknown19;
		set => SetModelValue(value, m => m.Unknown19, (m, v) => m.Unknown19 = v);
	}

	public short Unknown20
	{
		get => Model.Unknown20;
		set => SetModelValue(value, m => m.Unknown20, (m, v) => m.Unknown20 = v);
	}

	public short Unknown21
	{
		get => Model.Unknown21;
		set => SetModelValue(value, m => m.Unknown21, (m, v) => m.Unknown21 = v);
	}

	public short Unknown22
	{
		get => Model.Unknown22;
		set => SetModelValue(value, m => m.Unknown22, (m, v) => m.Unknown22 = v);
	}

	public short Unknown23
	{
		get => Model.Unknown23;
		set => SetModelValue(value, m => m.Unknown23, (m, v) => m.Unknown23 = v);
	}
}
