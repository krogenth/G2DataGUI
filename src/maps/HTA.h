#pragma once
#include <cinttypes>

#include "../common/structs/Vector3.h"

#pragma pack(1)
struct HTA {
	uint8_t shape = 0;
	uint8_t type = 0;
	uint8_t trigger = 0;
	uint8_t unknown1 = 0;
	uint32_t unknown2 = 0;
	uint8_t attribute = 0;
	uint8_t unknown3 = 0;
	uint16_t unknown4 = 0;
	uint32_t unknown5 = 0;
	Vector3 minimum;
    Vector3 maximum;
    Vector3 unknown6;
	uint32_t unknown7 = 0;
	uint32_t unknown8 = 0;
	uint32_t unknown9 = 0;
	uint32_t unknown10 = 0;
	uint32_t unknown11 = 0;
};
#pragma pack()