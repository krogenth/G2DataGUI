using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.Common.Data.Bosses;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class MapsViewModel : BaseViewModel
{
    public ObservableCollection<Map> MdtMaps { get; } = Maps.Instance.GetMaps();
    private int _selectedMapIndex = 0;

    public ObservableCollection<Boss> GameBosses { get; } = Bosses.Instance.GameBosses;
    public ObservableCollection<Enemy> GameEnemies { get; } = Enemies.Instance.GameEnemies;

	public static MapsViewModel Instance { get; private set; } = new();

    private MapsViewModel() { }

    public int MapSelectedIndex
    {
        get => _selectedMapIndex;
        set
        {
            _selectedMapIndex = value;
            OnPropertyChanged(nameof(MapSelectedIndex));
        }
    }
}
