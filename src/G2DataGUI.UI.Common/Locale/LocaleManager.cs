﻿using G2DataGUI.IO.Json;
using G2DataGUI.IO.Resources;
using G2DataGUI.UI.Common.ViewModels;
using System.Globalization;

namespace G2DataGUI.UI.Common.Locale;

public class LocaleManager : BaseViewModel
{
	private const string DefaultLanguageCode = "en_US";

	private readonly Dictionary<LocaleKeys, List<string>> _localeStringLists = new();
	private Dictionary<LocaleKeys, List<string>> _defaultLocaleStringLists = new();

	public static LocaleManager Instance { get; } = new();

	private LocaleManager()
	{
		Load();
	}

	public void Load()
	{
		string systemLanguageCode = CultureInfo.CurrentCulture.Name.Replace("-", "_");
		LoadLanguage(systemLanguageCode);
		LoadDefaultLanguage();
	}

	public void LoadLanguage(string languageCode)
	{
		foreach (var item in LoadJsonLanguage(languageCode))
		{
			this[item.Key] = item.Value;
		}
	}

	private void LoadDefaultLanguage() => _defaultLocaleStringLists = LoadJsonLanguage();

	private Dictionary<LocaleKeys, List<string>> LoadJsonLanguage(string languageCode = DefaultLanguageCode)
	{
		Dictionary<LocaleKeys, List<string>> localeStringLists = new();
		string jsonData = EmbeddedResources.ReadAllText($"G2DataGUI.UI.Common/Assets/Locales/{languageCode}.json");
		var parsed = JsonHelper.Deserialize(jsonData, JsonContext.Default.StringListDictionary);

		if (parsed != null)
		{
			foreach (var item in parsed)
			{
				if (Enum.TryParse<LocaleKeys>(item.Key, out var key))
				{
					localeStringLists[key] = item.Value;
				}
			}
		}

		return localeStringLists;
	}

	public List<string> this[LocaleKeys key]
	{
		get
		{
			if (_localeStringLists.ContainsKey(key))
			{
				return _localeStringLists[key];
			}

			if (_defaultLocaleStringLists.ContainsKey(key))
			{
				return _defaultLocaleStringLists[key];
			}

			return [key.ToString()];
		}
		set
		{
			_localeStringLists[key] = value;
			OnPropertyChanged();
		}
	}
}
