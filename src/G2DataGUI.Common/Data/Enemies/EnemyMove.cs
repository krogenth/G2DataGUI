using System.IO;
using G2DataGUI.Common.Data.Common;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMove : BaseContainer
{
    private FixedLengthName _name;
    public EnemyMoveStats Stats { get; private set; }

    public string Name
    {
        get => _name.Name;
        set
        {
            _name.Name = value;
            NotifyPropertyChanged(nameof(Name));
        }
    }
	public int MaxNameLength { get => FixedLengthName.MaxLength; }

    public static EnemyMove ReadEnemyMove(Stream reader)
    {
        EnemyMove move = new()
        {
            _name = FixedLengthName.ReadFixedLengthName(reader),
            Stats = EnemyMoveStats.ReadEnemyMoveStats(reader),
        };
        return move;
    }

    public void WriteEnemyMove(Stream writer)
    {
        _name.WriteFixedLengthName(writer);
        Stats.WriteEnemyMoveStats(writer);
    }
}
