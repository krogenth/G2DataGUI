using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Maps
{
    public class Maps
    {
        public static Maps Instance { get; private set; } = new Maps();
        private ObservableCollection<Map> _maps = new();
        public event EventHandler CollectionRefreshed;

        private Maps()
        {
            ReadMaps();
        }

        private void ReadMaps()
        {
            _maps.Clear();
            foreach(var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "map", "*.mdt", SearchOption.AllDirectories))
            {
                FileStream reader = File.Open(file, FileMode.Open);
                Map map = Map.ReadMap(reader, file);
                if (map != null) _maps.Add(map);
            }
            CollectionRefreshed?.Invoke(this, EventArgs.Empty);
        }

        public ObservableCollection<Map> GetMaps() { return _maps; }
    }
}