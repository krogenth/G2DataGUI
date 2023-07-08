using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using G2DataGUI.UI.Common.Locale;

namespace G2DataGUI.Common.Data.Moves;

public sealed class Move : BaseContainer
{
    public byte ID { get; set; }
    public byte Icon { get; set; }
    private FixedLengthName _name;
    public MoveStats Stats { get; private set; }
    private FixedLengthDescription _description;

    public string Name {
        get => _name.Name;
        set {
            _name.Name = value;
            NotifyPropertyChanged(nameof(Name));
        }
    }
    public int MaxNameLength { get => FixedLengthName.MaxLength; }
    public string Description { get => _description.Description; set => _description.Description = value; }
    public int MaxDescriptionLength { get => FixedLengthDescription.MaxLength; }

    public static string CSVHeader =>
        $"ID,Icon,Name,{MoveStats.CSVHeader},Description";

    public static Move ReadMove(Stream reader)
    {
        Move move = new()
        {
            ID = (byte)reader.ReadByte(),
            Icon = (byte)reader.ReadByte(),
            _name = FixedLengthName.ReadFixedLengthName(reader),
            Stats = MoveStats.ReadMoveStats(reader),
            _description = FixedLengthDescription.ReadFixedLengthDescription(reader),
        };

        return move;
    }

    public void WriteMove(Stream writer)
    {
        writer.WriteRawByte(ID);
        writer.WriteRawByte(Icon);
        _name.WriteFixedLengthName(writer);
        Stats.WriteMoveStats(writer);
        _description.WriteFixedLengthDescription(writer);
    }

    public void GenerateCSV(StreamWriter writer)
    {
        writer.Write(
            $"{ID}," +
            $"{LocaleManager.Instance[LocaleKeys.Icons][Icon]}," +
            $"{Name},");
        Stats.GenerateCSV(writer);
        writer.WriteLine($",{Description}");
    }
}