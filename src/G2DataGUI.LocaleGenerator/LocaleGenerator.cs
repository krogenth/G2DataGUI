using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Text;

namespace G2DataGUI.LocaleGenerator;

[Generator]
public class LocaleGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		/*Debugger.Launch();
		Debugger.Break();*/

		var englishLocaleFile = context.AdditionalTextsProvider.Where(static x => x.Path.EndsWith("en_US.json"));

		IncrementalValuesProvider<string> contents = englishLocaleFile.Select((text, cancellationToken) => text.GetText(cancellationToken)!.ToString());

		context.RegisterSourceOutput(contents, (spc, content) =>
		{
			var lines = content.Split('\n').Where(x => x.Trim().StartsWith("\"") && x.Trim().Contains(":")).Select(x => x.Split(':').First().Trim().Replace("\"", ""));
			StringBuilder enumSourceBuilder = new();
			enumSourceBuilder.AppendLine("namespace G2DataGUI.UI.Common.Locale;");
			enumSourceBuilder.AppendLine("public enum LocaleKeys");
			enumSourceBuilder.AppendLine("{");
			foreach (var line in lines)
			{
				enumSourceBuilder.AppendLine($"    {line},");
			}

			enumSourceBuilder.AppendLine("}");

			spc.AddSource("LocaleKeys", enumSourceBuilder.ToString());
		});
	}
}
