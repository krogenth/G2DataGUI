namespace G2DataGUI.Locale;

/// <summary>
/// Everything an exporter or a ViewModel needs to turn a LocaleKeys +
/// index into display text, without depending on Avalonia. LocaleExtension
/// (in UI.Common) is the only piece that needs actual data-binding, and it
/// wraps this rather than replacing it.
/// </summary>
public interface ILocalizationService
{
	string CurrentLanguage { get; }

	event EventHandler? LanguageChanged;

	List<string> this[LocaleKeys key] { get; }

	void SetLanguage(string language);
}
