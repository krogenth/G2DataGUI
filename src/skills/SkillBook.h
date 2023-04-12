#pragma once
#include <cinttypes>

#pragma pack(1)
struct SkillImplementation {
	uint8_t skillOffset = 0;
	uint8_t startingLevel = 0;
	uint8_t bookLevelRequired = 0;
	uint8_t unknown1 = 0;
};
#pragma pack()

#pragma pack(1)
struct SkillBook {
	SkillImplementation skills[6];
};
#pragma pack()