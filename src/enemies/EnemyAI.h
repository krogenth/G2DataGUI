#pragma once
#include <cinttypes>

#pragma pack(1)
struct EnemyAI {
	uint8_t aiType = 0;
	uint8_t move1Chance = 0;
	uint8_t move2Chance = 0;
	uint8_t move3Chance = 0;
	uint8_t move4Chance = 0;
	uint8_t move5Chance = 0;
};
#pragma pack()