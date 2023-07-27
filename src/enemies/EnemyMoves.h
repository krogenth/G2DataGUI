#pragma once
#include <cinttypes>

#pragma pack(1)
struct EnemyMoveStats {
	uint16_t mp = 0;
	uint16_t sp = 0;
	uint8_t unknown1 = 0;
	uint8_t targetEffect = 0;
	uint16_t strength = 0;
	uint16_t power = 0;
	uint16_t ad = 0;
	uint8_t targetType = 0;
	uint8_t normalAttackFlag = 0;
	uint16_t distance = 0;
	uint16_t accuracy = 0;
	uint16_t range = 0;
	uint16_t castTime = 0;
	uint16_t recovery = 0;
	uint8_t animation = 0;
	uint8_t knockDown = 0;
	int16_t ipStun = 0;
	int16_t ipCancelStun = 0;
	int16_t knockback = 0;
	uint8_t element = 0;
	uint8_t elementStrength = 0;
	uint8_t ailmentsBitflag = 0;
	uint8_t ailmentsChance = 0;
	int8_t atkMod = 0;
	int8_t defMod = 0;
	int8_t actMod = 0;
	int8_t movMod = 0;
	uint16_t special = 0;
};
#pragma pack()

struct EnemyMove {
	char* name = new char[19]{};
	EnemyMoveStats stats;
};

struct EnemyMoves {
	EnemyMove moves[5]{};
};