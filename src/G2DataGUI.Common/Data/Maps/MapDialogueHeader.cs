using System.Collections.Generic;
using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapDialogueHeader
{
    public uint HeaderLength { get; set; }
    public List<MapDialogueOffset> Offsets { get; set; } = new();

    public static MapDialogueHeader ReadMapDialogueHeader(Stream reader)
    {
        MapDialogueHeader header = new()
        {
            HeaderLength = reader.ReadRawUInt(),
        };
        for (var index = 0; index < (header.HeaderLength - 4) / 4; index++)
        {
            header.Offsets.Add(MapDialogueOffset.ReadMapDialogueOffset(reader));
        }

        return header;
    }
}

public class MapDialogueOffset
{
    public ushort Unknown1 { get; set; }
    public ushort Offset { get; set; }

    public static MapDialogueOffset ReadMapDialogueOffset(Stream reader)
    {
        MapDialogueOffset offset = new()
        {
            Unknown1 = reader.ReadRawUShort(),
            Offset = reader.ReadRawUShort(),
        };
        return offset;
    }
}
