#pragma once
#include <cinttypes>

#pragma pack(1)
struct SpellImplementation {
	uint8_t spellOffset = 0;
	uint8_t startingLevel = 0;
	uint8_t eggLevelRequired = 0;
	uint8_t unknown1 = 0;
};
#pragma pack()

#pragma pack(1)
struct ManaEgg {
	SpellImplementation spells[18];
};
#pragma pack()