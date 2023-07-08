using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Bosses;

public class Bosses
{
    private readonly ObservableCollection<Boss> _bosses = new();

    private Bosses()
    {
        ReadBosses();
    }

    public static Bosses Instance { get; } = new();

    private void ReadBosses()
    {
        _bosses.Clear();

        foreach (var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "boss", "*_0.dat", SearchOption.AllDirectories))
        {
            using FileStream reader = File.Open(file, FileMode.Open);
            using BinaryReader binReader = new(reader);
            _bosses.Add(Boss.ReadBoss(binReader, file));

            if (Boss.FileHasSecondBoss(binReader))
            {
                _bosses.Add(Boss.ReadBoss(binReader, file, true));
            }
        }
    }

    public ObservableCollection<Boss> GetBosses() { return _bosses; }
}