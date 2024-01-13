using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;

namespace G2DataGUI.UI.Selectors;

public class MapOpcodeSelector : ITreeDataTemplate
{
	[Content]
	public Dictionary<string, IDataTemplate> Templates { get; private set; } = new();

	public InstancedBinding ItemsSelector(object item)
	{
		if (item is IMapDialogueOpcode opcode)
		{
			return ((TreeDataTemplate)Templates[opcode.GetType().Name]).ItemsSelector(item);
		}
		else
		{
			return null;
		}
	}

	public IControl Build(object data) =>
		Templates[data.GetType().Name].Build(data);

	public bool Match(object data)
		=> data is IMapDialogueOpcode && Templates.ContainsKey(data.GetType().Name);
}
