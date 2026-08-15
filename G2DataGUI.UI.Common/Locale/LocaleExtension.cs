using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using G2DataGUI.Locale;

namespace G2DataGUI.UI.Common.Locale;

public class LocaleExtension(LocaleKeys key) : MarkupExtension
{
	public LocaleKeys Key { get; } = key;

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		LocaleValue value = new(LocaleService.Instance, Key);

		return new ReflectionBindingExtension()
		{
			Source = value,
			Path = nameof(LocaleValue.Value),
			Mode = BindingMode.OneWay,
		}.ProvideValue(serviceProvider);
	}
}
