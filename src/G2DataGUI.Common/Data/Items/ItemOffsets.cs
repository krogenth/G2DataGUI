using System.IO;

using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Items;

internal class ItemOffsets
{
    public int EquipmentOffset;
    public int UsableOffset;

    public static ItemOffsets ReadItemOffsets(Stream reader)
    {
        ItemOffsets offsets = new()
        {
             EquipmentOffset = reader.ReadRawInt(),
             UsableOffset = reader.ReadRawInt(),
        };
        return offsets;
    }
}