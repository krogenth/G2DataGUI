using Microsoft.CodeAnalysis;

namespace G2DataGUI.LocaleGenerator;

[Generator]
public class LocaleGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var englishLocaleFile = context.AdditionalTextsProvider.Where(x => x.Path.EndsWith("en_US.json"));

        IncrementalValuesProvider<string> contents = englishLocaleFile.Select((text, cancellationToken) => text.GetText(cancellationToken)!.ToString());

        context.RegisterSourceOutput(contents, (spc, content) =>
        {
            var lines = content.Split('\n').Where(x => x.Trim().StartsWith("\"") && x.Trim().Contains(":")).Select(x => x.Split(':').First().Trim().Replace("\"", ""));
            string enumSource = "namespace G2DataGUI.Locale;\n\n";
            enumSource += "internal enum LocaleKeys\n{\n";
            foreach (var line in lines)
            {
                enumSource += $"    {line},\n";
            }
            enumSource += "}";

            spc.AddSource("LocaleKeys", enumSource);
        });
    }
}