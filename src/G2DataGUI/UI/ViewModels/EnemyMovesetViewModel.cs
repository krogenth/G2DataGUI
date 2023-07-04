using G2DataGUI.Common.Data.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DataGUI.UI.ViewModels;

public class EnemyMovesetViewModel : BaseViewModel
{
    private EnemyMoveset _selectedEnemyMoveset = new EnemyMoveset();

    public static EnemyMovesetViewModel Instance { get; } = new();

    public EnemyMoveset SelectedEnemyMoveset
    {
        get => _selectedEnemyMoveset;
        set
        {
            if (value == null) _selectedEnemyMoveset = new EnemyMoveset();
            else _selectedEnemyMoveset = value;
            OnPropertyChanged(nameof(SelectedEnemyMoveset));
        }
    }
}
