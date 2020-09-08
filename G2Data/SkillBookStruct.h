#pragma once
#include <vector>

#include "ImGui/imgui.h"

#include "MoveStruct.h"

struct SkillImplementationStruct {

public:
	ImU8 skillOffset = 0;
	ImU8 startingLevel = 0;
	ImU8 bookLevelRequired = 0;
	ImU8 unknown1 = 0;

};

struct SkillBookStruct {

public:
	SkillImplementationStruct skills[6];

};

/*
01 byte Off - Offset of move from SK_PARAM.BIN
01 byte SL - Starting level of skill
01 byte EL - Skillbook level required to unlock skill
01 byte ? - Unknown
*/

void writeSKI(std::vector<SkillBookStruct>& books);
void readSKI(std::vector<SkillBookStruct>& books);
void drawSKI(std::vector<SkillBookStruct>& books, bool* canClose, char** skillIDs, const size_t& numSkills);