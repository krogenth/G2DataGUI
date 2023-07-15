using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapEnemyGroupSecondUnknown
{
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public short Unknown5 { get; set; }
	public short Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }
	public short Unknown11 { get; set; }
	public short Unknown12 { get; set; }
	public short Unknown13 { get; set; }
	public short Unknown14 { get; set; }
	public short Unknown15 { get; set; }
	public short Unknown16 { get; set; }
	public short Unknown17 { get; set; }
	public short Unknown18 { get; set; }
	public short Unknown19 { get; set; }
	public short Unknown20 { get; set; }

	public static MapEnemyGroupSecondUnknown ReadMapEnemyGroupSecondUnknown(Stream reader)
	{
		MapEnemyGroupSecondUnknown unknown = new()
		{
			Unknown1 = reader.ReadRawShort(),
			Unknown2 = reader.ReadRawShort(),
			Unknown3 = reader.ReadRawShort(),
			Unknown4 = reader.ReadRawShort(),
			Unknown5 = reader.ReadRawShort(),
			Unknown6 = reader.ReadRawShort(),
			Unknown7 = reader.ReadRawShort(),
			Unknown8 = reader.ReadRawShort(),
			Unknown9 = reader.ReadRawShort(),
			Unknown10 = reader.ReadRawShort(),
			Unknown11 = reader.ReadRawShort(),
			Unknown12 = reader.ReadRawShort(),
			Unknown13 = reader.ReadRawShort(),
			Unknown14 = reader.ReadRawShort(),
			Unknown15 = reader.ReadRawShort(),
			Unknown16 = reader.ReadRawShort(),
			Unknown17 = reader.ReadRawShort(),
			Unknown18 = reader.ReadRawShort(),
			Unknown19 = reader.ReadRawShort(),
			Unknown20 = reader.ReadRawShort(),
		};

		return unknown;
	}

	public void WriteMapEnemyGroupSecondUnknown(Stream writer)
	{
		writer.WriteRawShort(Unknown1);
		writer.WriteRawShort(Unknown2);
		writer.WriteRawShort(Unknown3);
		writer.WriteRawShort(Unknown4);
		writer.WriteRawShort(Unknown5);
		writer.WriteRawShort(Unknown6);
		writer.WriteRawShort(Unknown7);
		writer.WriteRawShort(Unknown8);
		writer.WriteRawShort(Unknown9);
		writer.WriteRawShort(Unknown10);
		writer.WriteRawShort(Unknown11);
		writer.WriteRawShort(Unknown12);
		writer.WriteRawShort(Unknown13);
		writer.WriteRawShort(Unknown14);
		writer.WriteRawShort(Unknown15);
		writer.WriteRawShort(Unknown16);
		writer.WriteRawShort(Unknown17);
		writer.WriteRawShort(Unknown18);
		writer.WriteRawShort(Unknown19);
		writer.WriteRawShort(Unknown20);
	}
}
