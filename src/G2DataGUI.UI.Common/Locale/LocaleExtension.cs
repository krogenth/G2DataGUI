using Avalonia.Data.Core;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;

namespace G2DataGUI.UI.Common.Locale;

public class LocaleExtension : MarkupExtension
{
	public LocaleExtension(LocaleKeys key)
	{
		Key = key;
	}

	public LocaleKeys Key { get; }

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		LocaleKeys keyToUse = Key;
		var builder = new CompiledBindingPathBuilder();

		builder.SetRawSource(LocaleManager.Instance)
			.Property(
				new ClrPropertyInfo(
					"Item",
					obj => LocaleManager.Instance[keyToUse],
					null,
					typeof(string)
				),
				PropertyInfoAccessorFactory.CreateInpcPropertyAccessor
			);

		var path = builder.Build();
		var binding = new CompiledBindingExtension(path);
		return binding.ProvideValue(serviceProvider);
	}
}
