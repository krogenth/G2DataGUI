using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common;

namespace G2DataGUI.Common.Data.Bosses;

public class Bosses
{
    private static readonly Bosses singleton = new Bosses();
    private ObservableCollection<Boss> bosses = new();

    private Bosses()
    {
        ReadBosses();
    }

    public static Bosses Instance { get { return singleton; } }

    private void ReadBosses()
    {
        bosses.Clear();

        foreach (var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "boss", "*_0.dat", SearchOption.AllDirectories))
        {
            using (FileStream reader = File.Open(file, FileMode.Open))
            using (BinaryReader binReader = new BinaryReader(reader))
            {
                bosses.Add(Boss.ReadBoss(binReader, file));

                if (Boss.FileHasSecondBoss(binReader))
                {
                    bosses.Add(Boss.ReadBoss(binReader, file, true));
                }
            }
                
        }
    }

    public ObservableCollection<Boss> GetBosses() { return bosses; }
}