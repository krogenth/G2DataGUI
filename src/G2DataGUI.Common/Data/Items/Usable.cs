using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Items;

public class Usable : BaseContainer
{
    private UsableStats _stats = new UsableStats();

    public static Usable ReadUsable(Stream reader)
    {
        Usable usable = new Usable();
        UsableStats? stats = reader.ReadStruct<UsableStats>();
        if (stats == null) return null;
        usable._stats = stats.Value;
        return usable;
    }

    public void WriteUsable(Stream writer)
    {
        writer.WriteStruct(_stats);
    }
    public byte TargetEffect { get => _stats.TargetEffect; set => _stats.TargetEffect = value; }
    public byte TargetType { get => _stats.TargetType; set => _stats.TargetType = value; }
    public short Power { get => _stats.Power; set => _stats.Power = value; }
    public short Range { get => _stats.Range; set => _stats.Range = value; }
    public short CastTime { get => _stats.CastTime; set => _stats.CastTime = value; }
    public short RecoveryTime { get => _stats.RecoveryTime; set => _stats.RecoveryTime = value; }
    public ushort Animation { get => _stats.Animation; set => _stats.Animation = value; }
    public byte EffectiveOn { get => _stats.EffectiveOn; set => _stats.EffectiveOn = value; }
    public byte Unknown1 { get => _stats.Unknown1; set => _stats.Unknown1 = value; }
    public short IpDamage { get => _stats.IpDamage;  set => _stats.IpDamage = value; }
    public short IpCancelDamage { get => _stats.IpCancelDamage; set => _stats.IpCancelDamage = value; }
    public short Knockback { get => _stats.Knockback; set => _stats.Knockback = value; }
    public byte Element { get => _stats.Element; set => _stats.Element = value; }
    public byte ElementStrength { get => _stats.ElementStrength; set => _stats.ElementStrength = value; }
    public bool PoisonBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Poison) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Poison;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Poison;
        }
    }
    public bool SleepBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Sleep) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Sleep;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Sleep;
        }
    }
    public bool ParalysisBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Paralysis) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Paralysis;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Paralysis;
        }
    }
    public bool ConfusionBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Confusion) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Confusion;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Confusion;
        }
    }
    public bool PlagueBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Plague) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Plague;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Plague;
        }
    }
    public bool Magic_BlockBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Magic_Block) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Magic_Block;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Magic_Block;
        }
    }
    public bool Move_BlockBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Move_Block) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Move_Block;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Move_Block;
        }
    }
    public bool DeathBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Death) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Death;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Death;
        }
    }
    public byte AilmentsChance { get => _stats.AilmentsChance; set => _stats.AilmentsChance = value; }
    public sbyte AttackModifier { get => _stats.AttackModifier; set => _stats.AttackModifier = value; }
    public sbyte DefenseModifier { get => _stats.DefenseModifier; set => _stats.DefenseModifier = value; }
    public sbyte ActionModifier { get => _stats.ActionModifier; set => _stats.ActionModifier = value; }
    public sbyte MovementModifier { get => _stats.MovementModifier; set => _stats.MovementModifier = value; }
    public byte BreakChance { get => _stats.BreakChance; set => _stats.BreakChance = value; }
    public byte Special { get => _stats.Special; set => _stats.Special = value; }
    public byte Unknown2 { get => _stats.Unknown2; set => _stats.Unknown2 = value; }
    public byte Unknown3 { get => _stats.Unknown3; set => _stats.Unknown3 = value; }
}