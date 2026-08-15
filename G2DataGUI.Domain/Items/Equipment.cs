using G2DataGUI.Domain.Common;

namespace G2DataGUI.Domain.Items;

/// <summary>
/// Present on an Item only when its equipment-block pointer was
/// valid (see ItemOffsets) — a null Equipment on an Item means "not
/// equippable", not "unread".
/// </summary>
public sealed class Equipment
{
	public const uint ByteSize = 0x1C;

	public ushort CharacterBitflag { get; set; }
	public short Strength { get; set; }
	public short Vitality { get; set; }
	public short Action { get; set; }
	public short Movement { get; set; }
	public byte EffectiveOn { get; set; }
	public sbyte FireAffinity { get; set; }
	public sbyte WindAffinity { get; set; }
	public sbyte EarthAffinity { get; set; }
	public sbyte LightningAffinity { get; set; }
	public sbyte BlizzardAffinity { get; set; }
	public byte AilmentsBitflag { get; set; }
	public byte AilmentsChance { get; set; }
	public sbyte IncreaseFirePercent { get; set; }
	public sbyte IncreaseWindPercent { get; set; }
	public sbyte IncreaseEarthPercent { get; set; }
	public sbyte IncreaseLightningPercent { get; set; }
	public sbyte IncreaseBlizzardPercent { get; set; }
	public sbyte IncreaseWaterPercent { get; set; }
	public sbyte IncreaseExplosionPercent { get; set; }
	public sbyte IncreaseForestPercent { get; set; }
	public short Special { get; set; }

	public bool CharacterFlag(CharacterTypes character) =>
		(CharacterBitflag & (ushort)character) != 0;

	public void SetCharacterFlag(CharacterTypes character, bool value) =>
		CharacterBitflag = value
			? (ushort)(CharacterBitflag | (ushort)character)
			: (ushort)(CharacterBitflag & ~(ushort)character);

	public bool AilmentFlag(AilmentTypes ailment) =>
		(AilmentsBitflag & (byte)ailment) != 0;

	public void SetAilmentFlag(AilmentTypes ailment, bool value) =>
		AilmentsBitflag = value
			? (byte)(AilmentsBitflag | (byte)ailment)
			: (byte)(AilmentsBitflag & ~(byte)ailment);
}
