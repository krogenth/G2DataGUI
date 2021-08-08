#ifndef MANA_EGGS_CLASS_H
#define MANA_EGGS_CLASS_H

#include <vector>

#include "BaseDataClass.h"
#include "MovesClass.h"

struct SpellImplementationStruct {

	uint8_t spellOffset = 0;
	uint8_t startingLevel = 0;
	uint8_t eggLevelRequired = 0;
	uint8_t unknown1 = 0;

};

struct ManaEggStruct {

	SpellImplementationStruct spells[18];

};

class ManaEggsClass : public BaseDataClass {

public:
	ManaEggsClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();

	void storeMoves(MoveStruct* moves, size_t numMoves) { _moves = moves; _numMoves = numMoves; };
	ManaEggStruct* getManaEggs() {

		if (_manaeggs.size())
			return &_manaeggs.at(0);
		else
			return nullptr;

	};
	size_t getNumManaEggs() { return _manaeggs.size(); };

private:
	std::vector<ManaEggStruct> _manaeggs;
	size_t _eggIndex = 0;
	size_t _spellIndex = 0;

	MoveStruct* _moves = nullptr;
	size_t _numMoves = 0;

};

#endif
