#pragma once
#include <cinttypes>

#pragma pack(1)
struct ItemUsable {
	uint8_t targetEffect = 0;
	uint8_t targetType = 0;
	uint16_t power = 0;
	uint16_t range = 0;
	uint16_t castTime = 0;
	uint16_t recoveryTime = 0;
	uint16_t animation = 0;
	uint8_t effectiveOn = 0;
	uint8_t unknown1 = 0;
	int16_t ipDamage = 0;
	int16_t ipCancelDamage = 0;
	int16_t knockback = 0;
	uint8_t element = 0;
	uint8_t elementStr = 0;
	uint8_t ailmentsBitflag = 0b00000000;
	uint8_t ailmentsChance = 0;
	int8_t atkMod = 0;
	int8_t defMod = 0;
	int8_t actMod = 0;
	int8_t movMod = 0;
	uint8_t breakChance = 0;
	uint8_t special = 0;
	uint8_t unknown2 = 0;
	uint8_t unknown3 = 0;
};
#pragma pack()