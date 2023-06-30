using G2DataGUI.Common.Data.Moves;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace G2DataGUI.UI.ViewModels;

public sealed class ManaeggsViewModel : BaseViewModel
{
    public ObservableCollection<Manaegg> GameManaeggs { get; private set; } = Manaeggs.Instance.GetManaeggs();
    public ObservableCollection<Move> GameMoves { get; private set; } = Moves.Instance.GetMoves();
    private int _selectedManaeggIndex = 0;
    private Manaegg _selectedManaeggItem;

    public static ManaeggsViewModel Instance { get; private set; } = new();

    private ManaeggsViewModel()
    {
        _selectedManaeggItem = GameManaeggs.First();
        Manaeggs.Instance.CollectionRefreshed += ManaeggCollectionRefreshed;
    }

    private void ManaeggCollectionRefreshed(object sender, EventArgs _)
    {
        SelectedManaeggItem = GameManaeggs.ElementAt(SelectedManaeggIndex);
    }

    public int SelectedManaeggIndex
    {
        get => _selectedManaeggIndex;
        set
        {
            if (value < 0) value = 0;
            if (value >= GameManaeggs.Count) return;
            _selectedManaeggIndex = value;
            SelectedManaeggItem = GameManaeggs.ElementAt(value);
            OnPropertyChanged(nameof(SelectedManaeggIndex));
        }
    }

    public Manaegg SelectedManaeggItem
    {
        get => _selectedManaeggItem;
        set
        {
            _selectedManaeggItem = value;
            OnPropertyChanged(nameof(SelectedManaeggItem));
        }
    }
}