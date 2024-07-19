using G2DataGUI.Common.Data.Moves;
using G2DataGUI.UI.Common.ViewModels;
using System.Collections.ObjectModel;

namespace G2DataGUI.UI.ViewModels;

public sealed class ManaeggsViewModel : BaseViewModel
{
	public ObservableCollection<Manaegg> GameManaeggs { get; set; } = Manaeggs.Instance.GameManaeggs;
	public ObservableCollection<Move> GameMoves { get; set; } = Moves.Instance.GameMoves;
	private int _selectedManaeggIndex = 0;
	public Manaegg? SelectedManaeggItem { get; set; }

	public ManaeggsViewModel()
	{
		SelectedManaeggIndex = 0;
		Manaeggs.Instance.CollectionRefreshed += ManaeggCollectionRefreshed;
	}

	private void ManaeggCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedManaeggIndex = SelectedManaeggIndex;

	public int SelectedManaeggIndex
	{
		get => _selectedManaeggIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			else if (value >= GameManaeggs.Count)
			{
				value = GameManaeggs.Count - 1;
			}

			if (GameManaeggs.Count == 0)
			{
				return;
			}

			_selectedManaeggIndex = value;
			SelectedManaeggItem = GameManaeggs[value];
			OnPropertyChanged(nameof(SelectedManaeggIndex));
			OnPropertyChanged(nameof(SelectedManaeggItem));
		}
	}
}
