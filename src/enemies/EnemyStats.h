#pragma once
#include <cinttypes>

#pragma pack(1)
struct EnemyStats {
	uint8_t unknown = 0;
	uint8_t unknown1 = 0;
	uint8_t type1 = 0;
	uint8_t type2 = 0;
	int16_t level = 0;
	int32_t health = 0;
	int16_t mp = 0;
	int16_t sp = 0;
	int16_t vit = 0;
	int16_t agi = 0;
	int16_t spd = 0;
	int16_t men = 0;
	int16_t stamina = 0;
	int16_t ipStun = 0;
	int16_t ipCancelStun = 0;
	int8_t evasionStillRate = 0;
	int8_t evasionMovingRate = 0;
	int8_t fireResist = 0;
	int8_t windResist = 0;
	int8_t earthResist = 0;
	int8_t lightningResist = 0;
	int8_t blizzardResist = 0;
	uint8_t ailmentsBitflag = 0;
	int16_t knockbackResist = 0;
	int16_t T_REC = 0;
	int16_t T_DMG = 0;
	int16_t unknown2 = 0;
	int16_t T_HEAL = 0;
	int16_t size = 0;
	int16_t unknown3 = 0;
	int8_t unknown4 = 0;
	int8_t noRunFlag = 0;
	int16_t unknown5 = 0;
	int32_t exp = 0;
	int32_t skillCoins = 0;
	int32_t magicCoins = 0;
	int32_t goldCoins = 0;
	uint16_t item1 = 0;
	uint16_t item2 = 0;
	int8_t item1Chance = 0;
	int8_t item2Chance = 0;
	uint16_t unknown6 = 0;
};
#pragma pack()