#pragma once

#include <vector>

#include "BaseDataClass.h"

#include "MovesClass.h"

struct MoveImplementationStruct {
	uint8_t moveOffset = 0;
	uint8_t startingLevel = 0;
	uint16_t storyFlag = 0;
};

struct SpecialMoveStruct {
	MoveImplementationStruct moves[6];
};

class SpecialsClass : public BaseDataClass {
public:
	SpecialsClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();

	void storeMoves(const std::vector<MoveStruct>* moves) { _moves = moves; };
	const std::vector<SpecialMoveStruct>* getSpecials() { return &_specials; };
	size_t getNumSpecials() { return _specials.size(); };

private:
	std::vector<SpecialMoveStruct> _specials;
	size_t _specialIndex = 0;
	size_t _moveIndex = 0;

	const std::vector<MoveStruct>* _moves = nullptr;
};