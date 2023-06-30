using G2DataGUI.Common.Data.Enemies;
using System.Collections.ObjectModel;
using System.Linq;

namespace G2DataGUI.UI.ViewModels;

public class EnemiesViewModel : BaseViewModel
{
    public ObservableCollection<Enemy> GameEnemies { get; private set; } = Enemies.Instance.GetEnemies();
    private int _selectedEnemyIndex = 0;
    private Enemy _selectedEnemyItem;

    public static EnemiesViewModel Instance { get; private set; } = new();

    private EnemiesViewModel()
    {
        _selectedEnemyItem = GameEnemies.First();

    }
}
