using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies
{
    public class Enemies
    {
        public static Enemies Instance { get; } = new Enemies();
        private ObservableCollection<Enemy> _enemies = new();
        public event EventHandler CollectionRefreshed;

        private Enemies()
        {
            ReadEnemies();
        }

        public void Save()
        {
            WriteEnemies();
        }

        public void Reload()
        {
            ReadEnemies();
        }

        private void ReadEnemies()
        {
            _enemies.Clear();
            foreach (var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "enemy", "*_0.dat", SearchOption.AllDirectories))
            {
                using (FileStream reader = File.Open(file, FileMode.Open, FileAccess.Read))
                using (MemoryStream memReader = new MemoryStream())
                {
                    reader.CopyTo(memReader);
                    _enemies.Add(Enemy.ReadEnemy(memReader, file));

                    if (Enemy.FileHasSecondEnemy(memReader))
                    {
                        _enemies.Add(Enemy.ReadEnemy(memReader, file, true));
                    }
                }
            }
            CollectionRefreshed?.Invoke(this, EventArgs.Empty);
        }

        private void WriteEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.WriteEnemy();
            }
        }

        public ObservableCollection<Enemy> GetEnemies() { return _enemies; }
    }
}