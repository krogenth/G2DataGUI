using G2DataGUI.Domain.Items;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Items;

public static class ItemStatsSerializer
{
	public static ItemStats Read(Stream stream) => new()
	{
		EntryType = stream.ReadRawByte(),
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte(),
		Icon = stream.ReadRawByte(),
		Unknown4 = stream.ReadRawByte(),
		Price = stream.ReadRawInt()
	};

	public static void Write(Stream stream, ItemStats stats)
	{
		stream.WriteRawByte(stats.EntryType);
		stream.WriteRawByte(stats.Unknown1);
		stream.WriteRawByte(stats.Unknown2);
		stream.WriteRawByte(stats.Unknown3);
		stream.WriteRawByte(stats.Icon);
		stream.WriteRawByte(stats.Unknown4);
		stream.WriteRawInt(stats.Price);
	}
}
