﻿using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapEnemyType
{
	public short Index { get; set; }
	public short EnemyCount { get; set; }
	public short EnemyOffset { get; set; }
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }

	public static MapEnemyType ReadMapEnemyType(Stream reader)
	{
		MapEnemyType enemyType = new()
		{
			Index = reader.ReadRawShort(),
			EnemyCount = reader.ReadRawShort(),
			EnemyOffset = reader.ReadRawShort(),
			Unknown1 = reader.ReadRawShort(),
			Unknown2 = reader.ReadRawShort(),
			Unknown3 = reader.ReadRawShort(),
			Unknown4 = reader.ReadRawShort(),
		};

		return enemyType;
	}

	public void WriteMapEnemyType(Stream writer)
	{
		writer.WriteRawShort(Index);
		writer.WriteRawShort(EnemyCount);
		writer.WriteRawShort(EnemyOffset);
		writer.WriteRawShort(Unknown1);
		writer.WriteRawShort(Unknown2);
		writer.WriteRawShort(Unknown3);
		writer.WriteRawShort(Unknown4);
	}
}

