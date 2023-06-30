using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Moves;

public sealed class Move : BaseContainer
{
    private byte _id;
    private byte _icon;
    private FixedLengthName _name;
    private MoveStats _stats;
    private FixedLengthDescription _description;

    public static Move ReadMove(Stream reader)
    {
        Move move = new Move();
        move._id = (byte)reader.ReadByte();
        move._icon = (byte)reader.ReadByte();

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name == null) return null;
        move._name = name.Value;

        MoveStats? _stats = reader.ReadStruct<MoveStats>();
        if (_stats == null) return null;
        move._stats = _stats.Value;

        FixedLengthDescription? description = reader.ReadStruct<FixedLengthDescription>();
        if (description == null) return null;
        move._description = description.Value;

        return move;
    }

    public void WriteMove(Stream writer)
    {
        writer.WriteByte(_id);
        writer.WriteByte(_icon);
        writer.WriteStruct(_name);
        writer.WriteStruct(_stats);
        writer.WriteStruct(_description);
    }

    public byte ID { get => _id; set => _id = value; }
    public byte Icon { get => _icon; set => _icon = value; }
    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }
    public short Cost { get => _stats.Cost; set => _stats.Cost = value; }
    public byte TargetEffect { get => _stats.TargetEffect; set => _stats.TargetEffect = value; }
    public byte TargetType { get => _stats.TargetType; set => _stats.TargetType = value; }
    public short Strength { get => _stats.Strength; set => _stats.Strength = value; }
    public short Power { get => _stats.Power; set => _stats.Power = value; }
    public short Range { get => _stats.Range; set => _stats.Range = value; }
    public short Cast1Time { get => _stats.Cast1Time; set => _stats.Cast1Time = value; }
    public short Cast5Time { get => _stats.Cast5Time; set => _stats.Cast5Time = value; }
    public short RecoveryTime { get => _stats.RecoveryTime; set => _stats.RecoveryTime = value; }
    public short Animation { get => _stats.Animation; set => _stats.Animation = value; }
    public byte Unknown1 { get => _stats.Unknown1; set => _stats.Unknown1 = value; }
    public byte Knockdown { get => _stats.Knockdown; set => _stats.Knockdown = value; }
    public short IpDamage { get => _stats.IpDamage; set => _stats.IpDamage = value; }
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
    public short Special { get => _stats.Special; set => _stats.Special = value; }
    public short CoinCost1 { get => _stats.CoinCost1; set => _stats.CoinCost1 = value; }
    public short CoinCost2 { get => _stats.CoinCost2; set => _stats.CoinCost2 = value; }
    public short CoinCost3 { get => _stats.CoinCost3; set => _stats.CoinCost3 = value; }
    public short CoinCost4 { get => _stats.CoinCost4; set => _stats.CoinCost4 = value; }
    public short CoinCost5 { get => _stats.CoinCost5; set => _stats.CoinCost5 = value; }
    public short Multiplier { get => _stats.Multiplier; set => _stats.Multiplier = value; }
    public string Description { get => _description.Description; set => _description.Description = value; }
    public int MaxDescriptionLength { get => _description.MaxLength; }
}