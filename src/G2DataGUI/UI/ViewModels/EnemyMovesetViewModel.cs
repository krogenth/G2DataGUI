using G2DataGUI.Common.Data.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class EnemyMovesetViewModel : BaseViewModel
{
    private EnemyMoveset _selectedEnemyMoveset = new();

    public static EnemyMovesetViewModel Instance { get; } = new();

    public EnemyMoveset SelectedEnemyMoveset
    {
        get => _selectedEnemyMoveset;
        set
        {
            _selectedEnemyMoveset = value == null ? new EnemyMoveset() : value;
			OnPropertyChanged(nameof(SelectedEnemyMoveset));
        }
    }
}
