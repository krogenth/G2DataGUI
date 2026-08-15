namespace G2DataGUI.UI.Common.ViewModels.Enums;

/// <summary>
/// One checkbox in a BaseFlagsSelectorViewModel — one enum member,
/// independently toggleable. Only BaseFlagsSelectorViewModel constructs
/// these (it owns keeping Flag/Label in sync with the enum and routing
/// IsSelected changes back into the combined SelectedFlags value).
/// </summary>
public sealed class FlagOptionViewModel<T> : BaseViewModel
	where T : struct, Enum
{
	private readonly Action _onChanged;
	private bool _isSelected;

	internal FlagOptionViewModel(T flag, bool isSelected, Action onChanged)
	{
		Flag = flag;
		Label = flag.ToString();
		_isSelected = isSelected;
		_onChanged = onChanged;
	}

	public T Flag { get; }
	public string Label { get; }

	public bool IsSelected
	{
		get => _isSelected;
		set
		{
			if (_isSelected == value)
			{
				return;
			}

			_isSelected = value;
			OnPropertyChanged(nameof(IsSelected));
			_onChanged();
		}
	}

	/// <summary>
	/// Used by BaseFlagsSelectorViewModel.SelectedFlags' setter to
	/// bulk-update every option from a combined value without each
	/// individual toggle re-triggering SelectedFlags' own change
	/// notification N times over.
	/// </summary>
	internal void SetIsSelectedSilently(bool value)
	{
		if (_isSelected == value)
		{
			return;
		}

		_isSelected = value;
		OnPropertyChanged(nameof(IsSelected));
	}
}
