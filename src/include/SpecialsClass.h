#ifndef SPECIALS_CLASS_H
#define SPECIALS_CLASS_H

#include <vector>

#include "BaseDataClass.h"

#include "MovesClass.h"

struct MoveImplementationStruct {

public:
	uint8_t moveOffset = 0;
	uint8_t startingLevel = 0;
	uint16_t storyFlag = 0;

};

struct SpecialMoveStruct {

public:
	MoveImplementationStruct moves[6];

};

class SpecialsClass : public BaseDataClass {

public:
	SpecialsClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();
	void randomize();

	void storeMoves(MoveStruct* moves, size_t numMoves) { _moves = moves; _numMoves = numMoves; };
	SpecialMoveStruct* getSpecials() {

		if (_specials.size())
			return &_specials.at(0);
		else
			return nullptr;

	};
	size_t getNumSpecials() { return _specials.size(); };

private:
	std::vector<SpecialMoveStruct> _specials;
	size_t _specialIndex = 0;
	size_t _moveIndex = 0;

	MoveStruct* _moves = nullptr;
	size_t _numMoves = 0;

};

#endif
