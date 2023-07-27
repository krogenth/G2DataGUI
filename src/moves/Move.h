#pragma once
#include <cinttypes>

#pragma pack(1)
struct MoveStats {
	uint16_t cost = 0;
	uint8_t targetEffect = 0;
	uint8_t targetType = 0;
	uint16_t strength = 0;
	uint16_t power = 0;
	uint16_t range = 0;
	uint16_t cast1Time = 0;
	uint16_t cast5Time = 0;
	uint16_t recovery = 0;
	uint16_t animation = 0;
	uint8_t unknown1 = 0;
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
	uint16_t coinCost1 = 0;
	uint16_t coinCost2 = 0;
	uint16_t coinCost3 = 0;
	uint16_t coinCost4 = 0;
	uint16_t coinCost5 = 0;
	uint16_t multiplier = 0;
};
#pragma pack()

struct Move {
	uint8_t id = 0;	// overwrite this with array index, used to reference which move manaeggs and specials reference
	uint8_t icon = 0;
	char name[19];
	MoveStats stats;
	char description[41];
};