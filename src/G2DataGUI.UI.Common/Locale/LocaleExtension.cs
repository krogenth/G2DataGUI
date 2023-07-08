using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System;

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
        ReflectionBindingExtension binding = new($"[{keyToUse}]")
        {
            Mode = Avalonia.Data.BindingMode.OneWay,
            Source = LocaleManager.Instance,
        };

        return binding.ProvideValue(serviceProvider);
    }
}