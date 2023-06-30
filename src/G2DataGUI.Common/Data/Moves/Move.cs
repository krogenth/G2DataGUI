using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Moves;

public sealed class Move : BaseContainer
{
    public byte ID { get; set; }
    public byte Icon { get; set; }
    private FixedLengthName _name;
    public MoveStats Stats { get; private set; }
    private FixedLengthDescription _description;

    public static Move ReadMove(Stream reader)
    {
        Move move = new Move();
        move.ID = (byte)reader.ReadByte();
        move.Icon = (byte)reader.ReadByte();

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name == null) return null;
        move._name = name.Value;

        move.Stats = MoveStats.ReadMoveStats(reader);

        FixedLengthDescription? description = reader.ReadStruct<FixedLengthDescription>();
        if (description == null) return null;
        move._description = description.Value;

        return move;
    }

    public void WriteMove(Stream writer)
    {
        writer.WriteRawByte(ID);
        writer.WriteRawByte(Icon);
        writer.WriteStruct(_name);
        Stats.WriteMoveStats(writer);
        writer.WriteStruct(_description);
    }

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }
    public string Description { get => _description.Description; set => _description.Description = value; }
    public int MaxDescriptionLength { get => _description.MaxLength; }
}