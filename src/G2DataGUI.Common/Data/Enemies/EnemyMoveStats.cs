using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMoveStats
{
    public short MP { get; set; }
    public short SP { get; set; }
    public byte Unknown1 { get; set; }
    public byte TargetEffect { get; set; }
    public short Strength { get; set; }
    public short Power { get; set; }
    public short AD { get; set; }
    public byte TargetType { get; set; }
    public bool NormalAttackFlag { get; set; }
    public short Distance { get; set; }
    public short Accuracy { get; set; }
    public short Range { get; set; }
    public short CastTime { get; set; }
    public short RecoveryTime { get; set; }
    public byte Animation { get; set; }
    public byte Knockdown { get; set; }
    public short IpStun { get; set; }
    public short IpCancelStun { get; set; }
    public short Knockback { get; set; }
    public byte Element { get; set; }
    public byte ElementStrength { get; set; }
    public byte AilmentsBitflag { get; set; }
    public byte AilmentsChance { get; set; }
    public byte AttackModifier { get; set; }
    public byte DefenseModifier { get; set; }
    public byte ActionModifier { get; set; }
    public byte MovementModifier { get; set; }
    public short Special { get; set; }

    public static EnemyMoveStats ReadEnemyMoveStats(Stream reader)
    {
        EnemyMoveStats stats = new EnemyMoveStats();
        stats.MP = reader.ReadRawShort();
        stats.SP = reader.ReadRawShort();
        stats.Unknown1 = reader.ReadRawByte();
        stats.TargetEffect = reader.ReadRawByte();
        stats.Strength = reader.ReadRawShort();
        stats.Power = reader.ReadRawShort();
        stats.AD = reader.ReadRawShort();
        stats.TargetType = reader.ReadRawByte();
        stats.NormalAttackFlag = reader.ReadRawBool();
        stats.Distance = reader.ReadRawShort();
        stats.Accuracy = reader.ReadRawShort();
        stats.Range = reader.ReadRawShort();
        stats.CastTime = reader.ReadRawShort();
        stats.RecoveryTime = reader.ReadRawShort();
        stats.Animation = reader.ReadRawByte();
        stats.Knockdown = reader.ReadRawByte();
        stats.IpStun = reader.ReadRawShort();
        stats.IpCancelStun = reader.ReadRawShort();
        stats.Knockback = reader.ReadRawShort();
        stats.Element = reader.ReadRawByte();
        stats.ElementStrength = reader.ReadRawByte();
        stats.AilmentsBitflag = reader.ReadRawByte();
        stats.AilmentsChance = reader.ReadRawByte();
        stats.AttackModifier = reader.ReadRawByte();
        stats.DefenseModifier = reader.ReadRawByte();
        stats.ActionModifier = reader.ReadRawByte();
        stats.MovementModifier = reader.ReadRawByte();
        stats.Special = reader.ReadRawShort();

        return stats;
    }

    public void WriteEnemyMoveStats(Stream writer)
    {
        writer.WriteRawShort(MP);
        writer.WriteRawShort(SP);
        writer.WriteRawByte(Unknown1);
        writer.WriteRawByte(TargetEffect);
        writer.WriteRawShort(Strength);
        writer.WriteRawShort(Power);
        writer.WriteRawShort(AD);
        writer.WriteRawByte(TargetType);
        writer.WriteRawBool(NormalAttackFlag);
        writer.WriteRawShort(Distance);
        writer.WriteRawShort(Accuracy);
        writer.WriteRawShort(Range);
        writer.WriteRawShort(CastTime);
        writer.WriteRawShort(RecoveryTime);
        writer.WriteRawByte(Animation);
        writer.WriteRawByte(Knockdown);
        writer.WriteRawShort(IpStun);
        writer.WriteRawShort(IpCancelStun);
        writer.WriteRawShort(Knockback);
        writer.WriteRawByte(Element);
        writer.WriteRawByte(ElementStrength);
        writer.WriteRawByte(AttackModifier);
        writer.WriteRawByte(DefenseModifier);
        writer.WriteRawByte(ActionModifier);
        writer.WriteRawByte(MovementModifier);
        writer.WriteRawShort(Special);
    }
}