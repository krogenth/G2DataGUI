using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Enemies;

/// <summary>
/// Shared by EnemyViewModel and BossViewModel — same binary
/// layout on both, see EnemyStats' own doc comment. AvailableItems is the
/// SAME ObservableCollection ItemsViewModel.Items already is — Items is
/// global (GlobalDataViewModel), while Enemy/Boss are version-specific
/// (GameVersionViewModel), so this crosses that boundary: threaded from
/// GameProjectViewModel.SetCurrent, through GameVersionViewModel, through
/// EnemiesViewModel/BossesViewModel, through EnemyViewModel/BossViewModel,
/// down to here — same "shared live collection, not a copy" reasoning as
/// ManaeggSpellViewModel.SelectedMove, just a longer chain since Items
/// lives on the other side of the global/version split.
/// </summary>
public sealed class EnemyStatsViewModel(EnemyStats model, IRepository repository, ObservableCollection<GameItemViewModel> availableItems)
	: ItemViewModel<EnemyStats>(model, repository)
{
	public ObservableCollection<GameItemViewModel> AvailableItems => availableItems;

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

	public string Name
	{
		get => Model.Name;
		set => SetModelValue(value, m => m.Name, (m, v) => m.Name = v);
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

	public byte Type1
	{
		get => Model.Type1;
		set => SetModelValue(value, m => m.Type1, (m, v) => m.Type1 = v);
	}

	public byte Type2
	{
		get => Model.Type2;
		set => SetModelValue(value, m => m.Type2, (m, v) => m.Type2 = v);
	}

	public short Level
	{
		get => Model.Level;
		set => SetModelValue(value, m => m.Level, (m, v) => m.Level = v);
	}

	public int Health
	{
		get => Model.Health;
		set => SetModelValue(value, m => m.Health, (m, v) => m.Health = v);
	}

	public short MP
	{
		get => Model.MP;
		set => SetModelValue(value, m => m.MP, (m, v) => m.MP = v);
	}

	public short SP
	{
		get => Model.SP;
		set => SetModelValue(value, m => m.SP, (m, v) => m.SP = v);
	}

	public short Vitality
	{
		get => Model.Vitality;
		set => SetModelValue(value, m => m.Vitality, (m, v) => m.Vitality = v);
	}

	public short Agility
	{
		get => Model.Agility;
		set => SetModelValue(value, m => m.Agility, (m, v) => m.Agility = v);
	}

	public short Speed
	{
		get => Model.Speed;
		set => SetModelValue(value, m => m.Speed, (m, v) => m.Speed = v);
	}

	public short Mentality
	{
		get => Model.Mentality;
		set => SetModelValue(value, m => m.Mentality, (m, v) => m.Mentality = v);
	}

	public short Stamina
	{
		get => Model.Stamina;
		set => SetModelValue(value, m => m.Stamina, (m, v) => m.Stamina = v);
	}

	public short IpStunDuration
	{
		get => Model.IpStunDuration;
		set => SetModelValue(value, m => m.IpStunDuration, (m, v) => m.IpStunDuration = v);
	}

	public short IpCancelStunDuration
	{
		get => Model.IpCancelStunDuration;
		set => SetModelValue(value, m => m.IpCancelStunDuration, (m, v) => m.IpCancelStunDuration = v);
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

	public sbyte FireResist
	{
		get => Model.FireResist;
		set => SetModelValue(value, m => m.FireResist, (m, v) => m.FireResist = v);
	}

	public sbyte WindResist
	{
		get => Model.WindResist;
		set => SetModelValue(value, m => m.WindResist, (m, v) => m.WindResist = v);
	}

	public sbyte EarthResist
	{
		get => Model.EarthResist;
		set => SetModelValue(value, m => m.EarthResist, (m, v) => m.EarthResist = v);
	}

	public sbyte LightningResist
	{
		get => Model.LightningResist;
		set => SetModelValue(value, m => m.LightningResist, (m, v) => m.LightningResist = v);
	}

	public sbyte BlizzardResist
	{
		get => Model.BlizzardResist;
		set => SetModelValue(value, m => m.BlizzardResist, (m, v) => m.BlizzardResist = v);
	}

	public byte AilmentsBitflag
	{
		get => Model.AilmentsBitflag;
		set => SetModelValue(value, m => m.AilmentsBitflag, (m, v) => m.AilmentsBitflag = v);
	}

	public short KnockbackResist
	{
		get => Model.KnockbackResist;
		set => SetModelValue(value, m => m.KnockbackResist, (m, v) => m.KnockbackResist = v);
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

	public short Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
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

	public short Unknown4
	{
		get => Model.Unknown4;
		set => SetModelValue(value, m => m.Unknown4, (m, v) => m.Unknown4 = v);
	}

	public byte Unknown5
	{
		get => Model.Unknown5;
		set => SetModelValue(value, m => m.Unknown5, (m, v) => m.Unknown5 = v);
	}

	public bool NoRunFlag
	{
		get => Model.NoRunFlag;
		set => SetModelValue(value, m => m.NoRunFlag, (m, v) => m.NoRunFlag = v);
	}

	public short Unknown6
	{
		get => Model.Unknown6;
		set => SetModelValue(value, m => m.Unknown6, (m, v) => m.Unknown6 = v);
	}

	public int Experience
	{
		get => Model.Experience;
		set => SetModelValue(value, m => m.Experience, (m, v) => m.Experience = v);
	}

	public int SkillCoins
	{
		get => Model.SkillCoins;
		set => SetModelValue(value, m => m.SkillCoins, (m, v) => m.SkillCoins = v);
	}

	public int MagicCoins
	{
		get => Model.MagicCoins;
		set => SetModelValue(value, m => m.MagicCoins, (m, v) => m.MagicCoins = v);
	}

	public int GoldCoins
	{
		get => Model.GoldCoins;
		set => SetModelValue(value, m => m.GoldCoins, (m, v) => m.GoldCoins = v);
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

	public short Item2Offset
	{
		get => Model.Item2Offset;
		set
		{
			SetModelValue(value, m => m.Item2Offset, (m, v) => m.Item2Offset = v);
			OnPropertyChanged(nameof(SelectedItem2));
		}
	}

	public byte Item1Chance
	{
		get => Model.Item1Chance;
		set => SetModelValue(value, m => m.Item1Chance, (m, v) => m.Item1Chance = v);
	}

	public byte Item2Chance
	{
		get => Model.Item2Chance;
		set => SetModelValue(value, m => m.Item2Chance, (m, v) => m.Item2Chance = v);
	}

	public short Unknown7
	{
		get => Model.Unknown7;
		set => SetModelValue(value, m => m.Unknown7, (m, v) => m.Unknown7 = v);
	}
}
