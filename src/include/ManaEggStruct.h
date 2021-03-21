#pragma once
#include <vector>
#include <string>

#include ".\imgui.h"

#include "MoveStruct.h"

struct SpellImplementationStruct{

public:
	ImU8 spellOffset = 0;
	ImU8 startingLevel = 0;
	ImU8 eggLevelRequired = 0;
	ImU8 unknown1 = 0;

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

void writeMAG(std::vector<ManaEggStruct>& eggs, std::string filename = "content/data/afs/xls_data/TB_MAGIC.BIN");
void readMAG(std::vector<ManaEggStruct>& eggs, std::string filename = "content/data/afs/xls_data/TB_MAGIC.BIN");
void drawMAG(std::vector<ManaEggStruct>& eggs, bool* canClose, char** moveIDs, const size_t& numMoves);