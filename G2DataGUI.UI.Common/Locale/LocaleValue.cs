using G2DataGUI.Locale;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.Common.Locale;

/// <summary>
/// Bridges the headless ILocalizationService (no INotifyPropertyChanged)
/// to something Avalonia bindings can observe — this is the one piece
/// that still needs a ViewModel base, which is exactly why it stayed in
/// UI.Common instead of moving to G2DataGUI.Locale with the rest.
/// </summary>
public sealed class LocaleValue : BaseViewModel
{
	private readonly ILocalizationService _localization;

	public LocaleKeys Key { get; }

	public List<string> Value => _localization[Key];

	public LocaleValue(ILocalizationService localization, LocaleKeys key)
	{
		_localization = localization;
		Key = key;

		_localization.LanguageChanged += OnLanguageChanged;
	}

	private void OnLanguageChanged(object? sender, EventArgs e) => OnPropertyChanged(nameof(Value));

	public void Dispose() => _localization.LanguageChanged -= OnLanguageChanged;
}
