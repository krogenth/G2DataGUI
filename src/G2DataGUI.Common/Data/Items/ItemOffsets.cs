using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace G2DataGUI.Common.Data.Items;

[StructLayout(LayoutKind.Explicit, Size = 0x08)]
internal struct ItemOffsets
{
    [FieldOffset(0x00)] public int EquipmentOffset;
    [FieldOffset(0x04)] public int UsableOffset;
}
