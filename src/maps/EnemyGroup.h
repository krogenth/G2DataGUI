#pragma once
#include <cinttypes>

#pragma pack(1)
struct EnemyFirstUnknown {
	uint16_t unknown = 0;
	uint16_t unknown1 = 0;
	uint16_t unknown2 = 0;
	uint16_t unknown3 = 0;
	uint16_t unknown4 = 0;
	float unknown5 = 0.0f;
	uint16_t unknown6 = 0;
	uint16_t unknown7 = 0;
	uint16_t unknown8 = 0;
	uint16_t unknown9 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyType {
	uint16_t index = 0; // unsure about this
	uint16_t numEnemy = 0;
	uint16_t enemyOffset = 0;
	uint16_t unknown = 0;
	uint16_t unknown1 = 0;
	uint16_t unknown2 = 0;
	uint16_t unknown3 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemySecondUnknown {
	uint16_t unknown = 0;
	uint16_t unknown1 = 0;
	uint16_t unknown2 = 0;
	uint16_t unknown3 = 0;
	uint16_t unknown4 = 0;
	uint16_t unknown5 = 0;
	uint16_t unknown6 = 0;
	uint16_t unknown7 = 0;
	uint16_t unknown8 = 0;
	uint16_t unknown9 = 0;
	uint16_t unknown10 = 0;
	uint16_t unknown11 = 0;
	uint16_t unknown12 = 0;
	uint16_t unknown13 = 0;
	uint16_t unknown14 = 0;
	uint16_t unknown15 = 0;
	uint16_t unknown16 = 0;
	uint16_t unknown17 = 0;
	uint16_t unknown18 = 0;
	uint16_t unknown19 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyGroup {
	uint16_t index = 0;
	EnemyFirstUnknown unknown;
	EnemyType enemies[4];
	EnemySecondUnknown unknown1;
};
#pragma pack()