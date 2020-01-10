#pragma once
#include "imgui.h"

#include "MoveStruct.h"

struct SpellImplementationStruct{

public:
	MoveStruct* Spell = nullptr;
	ImU8 startingLevel = 0;
	ImU8 eggLevelRequired = 0;
	ImU8 unkown = 0;

};

struct ManaEggStruct {

public:
	SpellImplementationStruct spells[18];

};

/*
01 byte Off - Offset of move from MS_PARAM.BIN
01 byte SL - Starting level of move
01 byte EL - Egg level required to unlock move
01 byte ? - Unknown
*/