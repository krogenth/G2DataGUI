namespace G2DataGUI.UI.Common.Locale;

/// <summary>
/// Process-wide instance of the headless locale service, for XAML
/// consumers (LocaleExtension) that need a static entry point the way the
/// old singleton did. ViewModels that can take a constructor dependency
/// should prefer injecting G2DataGUI.Locale.ILocalizationService directly
/// instead of reaching through this.
/// </summary>
public static class LocaleService
{
	public static G2DataGUI.Locale.LocaleService Instance { get; } = new();
}
