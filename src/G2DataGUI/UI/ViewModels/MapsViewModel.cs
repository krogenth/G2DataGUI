using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.Common.Data.Bosses;
using G2DataGUI.Common.Data.Enemies;

namespace G2DataGUI.UI.ViewModels
{
    public sealed class MapsViewModel : BaseViewModel
    {
        public ObservableCollection<Map> MdtMaps { get; } = Maps.Instance.GetMaps();
        private int _selectedMapIndex = 0;

        public ObservableCollection<Boss> bosses { get; } = Bosses.Instance.GetBosses();
        public ObservableCollection<Enemy> enemies { get; } = Enemies.Instance.GetEnemies();

        public MapsViewModel() { }

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
}