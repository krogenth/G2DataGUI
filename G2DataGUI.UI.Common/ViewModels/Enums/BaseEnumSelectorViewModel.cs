using G2DataGUI.Common.Extensions;

namespace G2DataGUI.UI.Common.ViewModels.Enums;

/// <summary>
/// Backs a "pick one value of this enum" control (a ComboBox, typically).
///
/// NOT what AilmentTypes/CharacterTypes actually need — both are always
/// multi-select in practice (an item usable by several characters, a move
/// inflicting several ailments at once), so those two now use
/// BaseFlagsSelectorViewModel instead — see its doc comment, including
/// why the same enum/bits serve both "a move inflicts this" and "an enemy
/// resists this" without needing two separate types. This single-select
/// base is kept as general infrastructure for a genuine "pick exactly one"
/// enum need, if one comes up — not currently used by anything.
/// </summary>
public abstract class BaseEnumSelectorViewModel<T> : BaseViewModel
	where T : struct, Enum
{
	public event EventHandler<EnumChangedEventArgs>? EnumChanged;

	public IList<T> Items { get; set; }
	public string Title { get; }

	protected BaseEnumSelectorViewModel(string title, T initialValue)
	{
		Items = EnumExtensions.GetEnumList<T>();
		Title = title;

		SelectedValueIndex = Items.IndexOf(initialValue);
	}

	public int SelectedValueIndex
	{
		get;
		set
		{
			if (Items.Count == 0)
			{
				return;
			}

			if (value >= Items.Count)
			{
				value = Items.Count - 1;
			}

			if (value < 0)
			{
				value = 0;
			}

			field = value;
			OnPropertyChanged(nameof(SelectedValueIndex));
			EnumChanged?.Invoke(this, new EnumChangedEventArgs(Items[value]));
		}
	}

	public T SelectedValue
	{
		get
		{
			if (SelectedValueIndex < Items.Count && SelectedValueIndex >= 0)
			{
				return Items[SelectedValueIndex];
			}

			return default;
		}
	}

	public sealed class EnumChangedEventArgs(T value) : EventArgs
	{
		public T Value { get; set; } = value;
	}
}
