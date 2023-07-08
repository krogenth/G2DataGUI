using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Items;

public class ItemStats
{
    public byte EntryType { get; set; }
    public byte Unknown1 { get; set; }
    public byte Unknown2 { get; set; }
    public byte Unknown3 { get; set; }
    public byte Icon { get; set; }
    public byte Unknown4 { get; set; }
    public int Price { get; set; }

    public static ItemStats ReadItemStats(Stream reader)
    {
		ItemStats stats = new()
		{
			EntryType = reader.ReadRawByte(),
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Icon = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
			Price = reader.ReadRawInt()
		};

		return stats;
    }

    public void WriteItemStats(Stream writer)
    {
        writer.WriteRawByte(EntryType);
        writer.WriteRawByte(Unknown1);
        writer.WriteRawByte(Unknown2);
        writer.WriteRawByte(Unknown3);
        writer.WriteRawByte(Icon);
        writer.WriteRawByte(Unknown4);
        writer.WriteRawInt(Price);
    }
}