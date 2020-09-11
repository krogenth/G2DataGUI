#pragma once
#include <vector>
#include <string>

#include "ImGui/imgui.h"

struct MoveImplementationStruct {

public:
	ImU8 moveOffset = 0;
	ImU8 startingLevel = 0;
	ImU16 storyFlag = 0;

};

struct SpecialMoveStruct {

public:
	MoveImplementationStruct moves[6];

};

/*
01 byte Off - Offset of move from MS_PARAM.BIN
01 byte SL - Starting level of move
02 byte SF - Story progress flag required to unlock move(?)
*/

void writeSPC(std::vector<SpecialMoveStruct>& specials, std::string filename = "content/data/afs/xls_data/TB_SPCL.BIN");
void readSPC(std::vector<SpecialMoveStruct>& specials, std::string filename = "content/data/afs/xls_data/TB_SPCL.BIN");
void drawSPC(std::vector<SpecialMoveStruct>& specials, bool* canClose, char** moveIDs, const size_t& numMoves);