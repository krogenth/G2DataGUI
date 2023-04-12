#pragma once
#include <cinttypes>

#include "../common/structs/Vector3.h"

#pragma pack(1)
struct MapEntry {
    uint32_t ID = 0; //	first is always 0x1000
    Vector3 position;
	float direction = 0.0f;
	float unknown1 = 0.0f;
	uint32_t unknown2 = 0;
	uint32_t unknown3 = 0;
};
#pragma pack()