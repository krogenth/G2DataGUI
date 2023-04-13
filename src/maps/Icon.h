#pragma once
#include <cinttypes>

#include "../common/structs/Vector3.h"

#pragma pack(1)
struct Icon {
    uint8_t ID = 0;
	uint16_t unknown1 = 0;
	uint8_t unknown2 = 0;
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;
	float unknown3 = 0;
	float yAngle = 0;
	uint16_t item1 = 0;
	uint16_t item2 = 0;
	uint16_t item3 = 0;
	uint16_t flag = 0;
};
#pragma pack()