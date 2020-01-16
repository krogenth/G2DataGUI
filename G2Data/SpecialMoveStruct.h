#pragma once
#include "imgui.h"

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

void writeSPC(SpecialMoveStruct* specials, const ImU16& count);
SpecialMoveStruct* readSPC(ImU16& count);