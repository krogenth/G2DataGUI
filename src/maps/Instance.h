#pragma once
#include <cinttypes>

#include "../common/structs/Vector3.h"

#pragma pack(1)
struct Instance {
    uint16_t ID = 0;
	uint16_t index = 0;
	uint16_t unknown1 = 0;
	uint8_t translation = 0;
    uint8_t unknown2 = 0;
    Vector3 position;
    Vector3 angle;
    Vector3 c;
};
#pragma pack()