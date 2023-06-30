using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class ManaeggSpell
{
    public byte SpellOffset { get; set; }
    public byte StartingLevel { get; set; }
    public byte RequiredLevel { get; set; }
    public byte Unknown1 { get; set; }

    public static ManaeggSpell ReadManaeggSpell(Stream reader)
    {
        ManaeggSpell spell = new ManaeggSpell();
        spell.SpellOffset = reader.ReadRawByte();
        spell.StartingLevel = reader.ReadRawByte();
        spell.RequiredLevel = reader.ReadRawByte();
        spell.Unknown1 = reader.ReadRawByte();

        return spell;
    }

    public void WriteManaeggSpell(Stream writer)
    {
        writer.WriteRawByte(SpellOffset);
        writer.WriteRawByte(StartingLevel);
        writer.WriteRawByte(RequiredLevel);
        writer.WriteRawByte(Unknown1);
    }
}