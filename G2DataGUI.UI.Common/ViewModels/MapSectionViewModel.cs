using System.Linq;

namespace G2DataGUI.UI.Common.ViewModels;

/// <summary>
/// One tab/page within Maps' sub-navigation (Entries, Instances, HTAs,
/// ...) — a fixed list of sub-objects belonging to ONE selected map, plus
/// which one is currently shown in the detail panel. Deliberately NOT
/// CollectionViewModel<TModel,TViewModel> — that base tracks a
/// repository-backed ObservableCollection with its own add/remove/reload
/// lifecycle (Items/StateChanged), which doesn't fit here: a map's list of
/// HTAs (for example) isn't itself a repository, it's a fixed sub-list
/// that comes and goes whole with whichever Map is selected. Add/remove
/// of individual entries isn't supported in this pass, same as the
/// original this was ported from — only editing existing entries.
/// </summary>
public class MapSectionViewModel<TItemViewModel>(IEnumerable<TItemViewModel> items) : BaseViewModel
	where TItemViewModel : class
{
	public IReadOnlyList<TItemViewModel> Items { get; } = items.ToList();

	public TItemViewModel? SelectedItem
	{
		get;
		set
		{
			if (ReferenceEquals(field, value))
			{
				return;
			}

			field = value;
			OnPropertyChanged(nameof(SelectedItem));
		}
	}
}
