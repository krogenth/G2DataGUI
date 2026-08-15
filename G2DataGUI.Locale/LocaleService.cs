using G2DataGUI.IO;
using G2DataGUI.IO.Json;

namespace G2DataGUI.Locale;

/// <summary>
/// Plain data service — deliberately does NOT implement INotifyPropertyChanged
/// or derive from any ViewModel base. The old LocaleService derived from
/// BaseViewModel (Avalonia-adjacent) purely so LocaleValue could observe
/// property-change notifications; that concern now lives in LocaleValue
/// itself (in UI.Common), which subscribes to LanguageChanged below and
/// raises its own notification. This keeps a CSV exporter able to depend
/// on ILocalizationService without pulling in anything UI-related.
/// </summary>
public sealed class LocaleService : ILocalizationService
{
	private readonly Dictionary<LocaleKeys, List<string>> _strings = [];

	public string CurrentLanguage { get; private set; } = "en_US";

	public event EventHandler? LanguageChanged;

	public LocaleService()
	{
		LoadLanguage(CurrentLanguage);
	}

	public List<string> this[LocaleKeys key] =>
		_strings.TryGetValue(key, out List<string>? value) ? value : [key.ToString()];

	public void SetLanguage(string language)
	{
		if (string.Equals(CurrentLanguage, language, StringComparison.OrdinalIgnoreCase))
		{
			return;
		}

		LoadLanguage(language);
		CurrentLanguage = language;

		LanguageChanged?.Invoke(this, EventArgs.Empty);
	}

	private void LoadLanguage(string languageCode)
	{
		_strings.Clear();

		string resourceName = $"G2DataGUI.Locale/Assets/Locales/{languageCode}.json";
		string json = EmbeddedResources.ReadAllText(resourceName);
		Dictionary<string, List<string>>? parsed =
			JsonHelper.Deserialize(json, JsonContext.Default.StringListDictionary);

		if (parsed is null)
		{
			return;
		}

		foreach (KeyValuePair<string, List<string>> item in parsed)
		{
			if (Enum.TryParse(item.Key, out LocaleKeys key))
			{
				_strings[key] = item.Value;
			}
		}
	}
}
