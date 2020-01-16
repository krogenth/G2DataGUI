#pragma once
#include "imgui.h"

struct EquipmentStruct {

public:
	ImU16 characterBitflag = 0b00000000;
	ImS16 str = 0;
	ImS16 vit = 0;
	ImS16 act = 0;
	ImS16 mov = 0;
	ImU8 effectiveOn = 0;
	ImS8 fireAffinity = 0;
	ImS8 windAffinity = 0;
	ImS8 earthAffinity = 0;
	ImS8 lightningAffinity = 0;
	ImS8 blizzardAffinity = 0;
	ImU8 ailmentsBitflag = 0b00000000;
	ImU8 ailmentsChance = 0;
	ImU8 unknown1 = 0;
	ImU8 unknown2 = 0;
	ImU8 unknown3 = 0;
	ImU8 unknown4 = 0;
	ImU8 unknown5 = 0;
	ImU8 unknown6 = 0;
	ImU8 unknown7 = 0;
	ImU8 unknown8 = 0;
	ImU16 special = 0;

};

/*
02 byte Character Bitflag(Add together for multiple characters)
02 byte Strength
02 byte Defense
02 byte Action
02 byte Movement
01 byte Effective against monster type
01 byte Fire
01 byte Wind
01 byte Earth
01 byte Lightning
01 byte Blizzard
01 byte Status Effect(Add together for multiple status effects)
01 byte % Chance of Effect
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
02 byte Special Effect
*/

struct UsableStruct {

public:
	ImU8 targetEffect = 0;
	ImU8 targetType = 0;
	ImU16 power = 0;
	ImU16 range = 0;
	ImU16 castTime = 0;
	ImU16 recoveryTime = 0;
	ImU16 animation = 0;
	ImU8 effectiveOn = 0;
	ImU8 unknown1 = 0;
	ImS16 ipDamage = 0;
	ImS16 ipCancelDamage = 0;
	ImS16 knockback = 0;
	ImU8 element = 0;
	ImU8 elementStr = 0;
	ImU8 ailmentsBitflag = 0b00000000;
	ImU8 ailmentsChance = 0;
	ImS8 atkMod = 0;
	ImS8 defMod = 0;
	ImS8 actMod = 0;
	ImS8 movMod = 0;
	ImU8 breakChance = 0;
	ImU8 special = 0;
	ImU8 unknown2 = 0;
	ImU8 unknown3 = 0;

};

/*
01 byte Target Effect
01 byte Target Type
02 byte Power
02 byte Range
02 byte Cast Time
02 byte Recovery
02 byte Animation
01 byte Effective against monster type
01 byte Unknown
02 byte IP Damage
02 byte IP Cancel Damage
02 byte Knockback
01 byte Element Type
01 byte Element Strength
01 byte S - status effect bitflag(Add together for multiple effects)
01 byte % Chance of Effect
01 byte Attack
01 byte Defense
01 byte Action
01 byte Movement
01 byte % Chance to Break(?)
01 byte Special Effect(?)(Not the same as Moves/Spells/Weapon Special Effects)
01 byte Unknown
01 byte Unknown
*/

struct ItemStruct {

public:
	char* name = new char[19]{};
	char* description = new char[41]{};
	ImU8 entryType = 0;
	ImU8 unknown1 = 0;
	ImU8 unknown2 = 0;
	ImU8 unknown3 = 0;
	ImU8 icon = 0;
	ImU8 unknown5 = 0;
	ImU32 price = 0;
	EquipmentStruct* equipmentOffset = nullptr;// 0xFFFFFFFF means null
	UsableStruct* usableOffset = nullptr;// 0xFFFFFFFF means null
	ImU32 id = 0;// overwrite this to array index, used for overworld items and enemy drops

};

/*
18 byte Name
40 byte Description
01 byte Entry Type(Determines which item catagory it is placed in in your inventory)
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
01 byte Unknown
04 byte Purchase Price(Sell Price = Purchase Price / 2)
04 byte Equipment Offset(from start of file)
04 byte Item Offset(from start of file)
04 byte Unknown(Previously believed to be an ID, but that was wrong. Items are referenced by their entry offset from the start of ITEM.BIN, so these 4 bytes were changed for easier reference.)
*/

void writeITE(ItemStruct* items, const ImU16& count);
ItemStruct* readITE(ImU16& count);