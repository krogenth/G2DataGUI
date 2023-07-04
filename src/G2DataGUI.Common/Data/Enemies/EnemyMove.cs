using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMove : BaseContainer
{
    private FixedLengthName _name;
    public EnemyMoveStats Stats { get; private set; }

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }

    public static EnemyMove ReadEnemyMove(Stream reader)
    {
        EnemyMove move = new EnemyMove();
        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name != null) move._name = name.Value;

        move.Stats = EnemyMoveStats.ReadEnemyMoveStats(reader);

        return move;
    }

    public void WriteEnemyMove(Stream writer)
    {
        writer.WriteStruct(_name);
        Stats.WriteEnemyMoveStats(writer);
    }
}