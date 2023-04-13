#pragma once
#include <cinttypes>

#pragma pack(1)
struct MoveImplementation {
	uint8_t moveOffset = 0;
	uint8_t startingLevel = 0;
	uint16_t storyFlag = 0;
};
#pragma pack()

#pragma pack(1)
struct SpecialMove {
	MoveImplementation moves[6];
};
#pragma pack()