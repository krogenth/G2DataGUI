using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapEnemyGroupFirstUnknown
{
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public short Unknown5 { get; set; }
	public float Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }

	public static MapEnemyGroupFirstUnknown ReadMapEnemyGroupFirstUnknown(Stream reader)
	{
		MapEnemyGroupFirstUnknown unknown = new()
		{
			Unknown1 = reader.ReadRawShort(),
			Unknown2 = reader.ReadRawShort(),
			Unknown3 = reader.ReadRawShort(),
			Unknown4 = reader.ReadRawShort(),
			Unknown5 = reader.ReadRawShort(),
			Unknown6 = reader.ReadRawFloat(),
			Unknown7 = reader.ReadRawShort(),
			Unknown8 = reader.ReadRawShort(),
			Unknown9 = reader.ReadRawShort(),
			Unknown10 = reader.ReadRawShort(),
		};

		return unknown;
	}

	public void WriteMapEnemyGroupFirstUnknown(Stream writer)
	{
		writer.WriteRawShort(Unknown1);
		writer.WriteRawShort(Unknown2);
		writer.WriteRawShort(Unknown3);
		writer.WriteRawShort(Unknown4);
		writer.WriteRawShort(Unknown5);
		writer.WriteRawFloat(Unknown6);
		writer.WriteRawShort(Unknown7);
		writer.WriteRawShort(Unknown8);
		writer.WriteRawShort(Unknown9);
		writer.WriteRawShort(Unknown10);
	}
}
