#pragma once
#include <cinttypes>

#include "../common/structs/Vector3.h"

#pragma pack(1)
struct MOS {
    uint16_t ID = 0;
	uint16_t index = 0;
	uint32_t unknown1 = 0;
    Vector3 position;
	uint32_t unknown2 = 0;
	uint32_t unknown3 = 0;
	uint32_t unknown4 = 0;
	uint32_t unknown5 = 0;
	uint16_t unknown6 = 0;
};
#pragma pack()