#pragma once
#include "imgui.h"

#include "MoveStruct.h"

struct SkillImplementationStruct {

public:
	MoveStruct* SkillOffset = nullptr;
	ImU8 startingLevel = 0;
	ImU8 bookLevelRequired = 0;
	ImU8 unkown = 0;

};

struct SkillBookStruct {

public:
	SkillImplementationStruct spells[18];

};

/*
01 byte Off - Offset of move from SK_PARAM.BIN
01 byte SL - Starting level of skill
01 byte EL - Skillbook level required to unlock skill
01 byte ? - Unknown
*/