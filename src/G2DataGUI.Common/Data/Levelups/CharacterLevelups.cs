using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Levelups;

[StructLayout(LayoutKind.Explicit, Size = 0x882)]
public class CharacterLevelups
{
    [FieldOffset(0x00)] public LevelupStats[] levelups = new LevelupStats[99];
}