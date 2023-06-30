using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.ComponentModel;
using System.IO;

namespace G2DataGUI.Common.Data.Items;

public class Equipment : BaseContainer
{
    private EquipmentStats _stats = new EquipmentStats();

    public Equipment()
    {
        NotifyPropertyChanged(nameof(Equipment));
    }

    public static Equipment ReadEquipment(Stream reader)
    {
        Equipment equipment = new Equipment();
        EquipmentStats? stats = reader.ReadStruct<EquipmentStats>();
        if (stats == null) return null;
        equipment._stats = stats.Value;
        return equipment;
    }

    public void WriteEquipment(Stream writer)
    {
        writer.WriteStruct(_stats);
    }

    public bool RyudoBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.Ryudo) > 0;
        set
        {
                if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.Ryudo;
                else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.Ryudo;
        }
    }
    public bool ElenaBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.Elena) > 0;
        set
        {
            if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.Elena;
            else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.Elena;
        }
    }
    public bool MilleniaBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.Millenia) > 0;
        set
        {
            if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.Millenia;
            else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.Millenia;
        }
    }
    public bool RoanBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.Roan) > 0;
        set
        {
            if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.Roan;
            else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.Roan;
        }
    }
    public bool TioBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.Tio) > 0;
        set
        {
            if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.Tio;
            else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.Tio;
        }
    }
    public bool MaregBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.Mareg) > 0;
        set
        {
            if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.Mareg;
            else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.Mareg;
        }
    }
    public bool PrinceRoanBitflag
    {
        get => (_stats.CharacterBitflag & (ushort)Characters.CharacterTypes.PrinceRoan) > 0;
        set
        {
            if (value) _stats.CharacterBitflag |= (ushort)Characters.CharacterTypes.PrinceRoan;
            else _stats.CharacterBitflag &= (ushort)~Characters.CharacterTypes.PrinceRoan;
        }
    }
    public short Strength { get => _stats.Strength; set => _stats.Strength = value; }
    public short Vitality { get => _stats.Vitality; set => _stats.Vitality = value; }
    public short Action { get => _stats.Action; set => _stats.Action = value; }
    public short Movement { get => _stats.Movement; set => _stats.Movement = value; }
    public byte EffectiveOn { get => _stats.EffectiveOn; set => _stats.EffectiveOn = value; }
    public sbyte FireAffinity { get => _stats.FireAffinity; set => _stats.FireAffinity = value; }
    public sbyte WindAffinity { get => _stats.WindAffinity; set => _stats.WindAffinity = value; }
    public sbyte EarthAffinity { get => _stats.EarthAffinity; set => _stats.EarthAffinity = value; }
    public sbyte LightningAffinity { get => _stats.LightningAffinity; set => _stats.LightningAffinity = value; }
    public sbyte BlizzardAffinity { get => _stats.BlizzardAffinity; set => _stats.BlizzardAffinity = value; }
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
    public sbyte IncreaseFirePercent { get => _stats.IncreaseFirePercent; set => _stats.IncreaseFirePercent = value; }
    public sbyte IncreaseWindPercent { get => _stats.IncreaseWindPercent; set => _stats.IncreaseWindPercent = value; }
    public sbyte IncreaseEarthPercent { get => _stats.IncreaseEarthPercent; set =>_stats.IncreaseEarthPercent = value; }
    public sbyte IncreaseLightningPercent { get => _stats.IncreaseLightningPercent; set => _stats.IncreaseLightningPercent = value; }
    public sbyte IncreaseBlizzardPercent { get => _stats.IncreaseBlizzardPercent; set => _stats.IncreaseBlizzardPercent = value; }
    public sbyte IncreaseWaterPercent { get => _stats.IncreaseWaterPercent; set => _stats.IncreaseWaterPercent = value; }
    public sbyte IncreaseExplosionPercent { get => _stats.IncreaseExplosionPercent; set => _stats.IncreaseExplosionPercent = value; }
    public sbyte IncreaseForestPercent { get => _stats.IncreaseForestPercent; set => _stats.IncreaseForestPercent = value; }
    public short Special { get => _stats.Special; set => _stats.Special = value; }
}