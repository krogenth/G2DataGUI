using G2DataGUI.Common.Extensions;

namespace G2DataGUI.UI.Common.ViewModels.Enums;

/// <summary>
/// Backs a "toggle any number of these" control — a list of checkboxes,
/// one per [Flags] enum member — for genuinely multi-select bitmask data
/// like AilmentTypes/CharacterTypes. There's no legitimate single-value
/// case for either: an item can be usable by several characters at once,
/// a move can inflict several ailments at once, so this replaces
/// BaseEnumSelectorViewModel for both rather than sitting alongside it as
/// an alternative.
///
/// The same enum and the same bit values serve two different MEANINGS
/// depending on where they're used — a move's AilmentTypes selector means
/// "inflicts this," an enemy's means "resists this" — without needing two
/// separate enums or an inversion flag. Title (already a constructor
/// parameter, same as the single-select base) is what communicates that:
/// construct one instance with Title="Ailments Inflicted" for a move,
/// another with Title="Ailment Resistance" for an enemy, both over the
/// same AilmentTypes enum.
/// </summary>
public abstract class BaseFlagsSelectorViewModel<T> : BaseViewModel
	where T : struct, Enum
{
	public event EventHandler<FlagsChangedEventArgs>? FlagsChanged;

	public string Title { get; }
	public IReadOnlyList<FlagOptionViewModel<T>> Options { get; }

	protected BaseFlagsSelectorViewModel(string title, T initialValue)
	{
		Title = title;

		Options = [.. EnumExtensions.GetEnumList<T>().Select(flag => new FlagOptionViewModel<T>(flag, HasFlag(initialValue, flag), OnOptionChanged))];
	}

	/// <summary>
	/// Every currently-checked option, OR'd together into one
	/// value of T. Setting it checks/unchecks every option to match —
	/// useful for loading a model's existing bitmask into the UI, or for
	/// a "select all"/"clear all" button bound straight to this.
	/// </summary>
	public T SelectedFlags
	{
		get
		{
			ulong combined = 0;

			foreach (FlagOptionViewModel<T> option in Options)
			{
				if (option.IsSelected)
				{
					combined |= Convert.ToUInt64(option.Flag);
				}
			}

			return (T)Enum.ToObject(typeof(T), combined);
		}
		set
		{
			foreach (FlagOptionViewModel<T> option in Options)
			{
				option.SetIsSelectedSilently(HasFlag(value, option.Flag));
			}

			OnPropertyChanged(nameof(SelectedFlags));
		}
	}

	private void OnOptionChanged()
	{
		OnPropertyChanged(nameof(SelectedFlags));
		FlagsChanged?.Invoke(this, new FlagsChangedEventArgs(SelectedFlags));
	}

	private static bool HasFlag(T value, T flag)
	{
		ulong flagBits = Convert.ToUInt64(flag);

		if (flagBits == 0)
		{
			return Convert.ToUInt64(value) == 0;
		}

		return (Convert.ToUInt64(value) & flagBits) == flagBits;
	}

	public sealed class FlagsChangedEventArgs(T value) : EventArgs
	{
		public T Value { get; } = value;
	}
}
