#pragma once
#include <cinttypes>

#pragma pack(1)
struct ItemStats {
	uint8_t entryType = 0;
	uint8_t unknown1 = 0;
	uint8_t unknown2 = 0;
	uint8_t unknown3 = 0;
	uint8_t icon = 0;
	uint8_t unknown4 = 0;
	uint32_t price = 0;
};
#pragma pack()