using G2DataGUI.Common.Data.Common;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class Manaegg : BaseContainer
{
    public ManaeggSpell Spell1 { get; set; }
    public ManaeggSpell Spell2 { get; set; }
    public ManaeggSpell Spell3 { get; set; }
    public ManaeggSpell Spell4 { get; set; }
    public ManaeggSpell Spell5 { get; set; }
    public ManaeggSpell Spell6 { get; set; }
    public ManaeggSpell Spell7 { get; set; }
    public ManaeggSpell Spell8 { get; set; }
    public ManaeggSpell Spell9 { get; set; }
    public ManaeggSpell Spell10 { get; set; }
    public ManaeggSpell Spell11 { get; set; }
    public ManaeggSpell Spell12 { get; set; }
    public ManaeggSpell Spell13 { get; set; }
    public ManaeggSpell Spell14 { get; set; }
    public ManaeggSpell Spell15 { get; set; }
    public ManaeggSpell Spell16 { get; set; }
    public ManaeggSpell Spell17 { get; set; }
    public ManaeggSpell Spell18 { get; set; }

    public static Manaegg ReadManaegg(Stream reader)
    {
        Manaegg manaegg = new Manaegg();
        manaegg.Spell1 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell2 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell3 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell4 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell5 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell6 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell7 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell8 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell9 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell10 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell11 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell12 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell13 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell14 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell15 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell16 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell17 = ManaeggSpell.ReadManaeggSpell(reader);
        manaegg.Spell18 = ManaeggSpell.ReadManaeggSpell(reader);

        return manaegg;
    }

    public void WriteManaegg(Stream writer)
    {
        Spell1.WriteManaeggSpell(writer);
        Spell2.WriteManaeggSpell(writer);
        Spell3.WriteManaeggSpell(writer);
        Spell4.WriteManaeggSpell(writer);
        Spell5.WriteManaeggSpell(writer);
        Spell6.WriteManaeggSpell(writer);
        Spell7.WriteManaeggSpell(writer);
        Spell8.WriteManaeggSpell(writer);
        Spell9.WriteManaeggSpell(writer);
        Spell10.WriteManaeggSpell(writer);
        Spell11.WriteManaeggSpell(writer);
        Spell12.WriteManaeggSpell(writer);
        Spell13.WriteManaeggSpell(writer);
        Spell14.WriteManaeggSpell(writer);
        Spell15.WriteManaeggSpell(writer);
        Spell16.WriteManaeggSpell(writer);
        Spell17.WriteManaeggSpell(writer);
        Spell18.WriteManaeggSpell(writer);
    }
}