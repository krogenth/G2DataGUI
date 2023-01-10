#pragma once

#include <vector>

#include "./common/version_check.h"

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

class ManaEggs : public BaseDataClass {
public:
	ManaEggs(const ManaEggs&) = delete;
    ManaEggs(const ManaEggs&&) = delete;

    static ManaEggs& getInstance() {
		static ManaEggs instance;
		return instance;
	};

	
	void write();
	void read();
	void draw();
	void outputToCSV();
	void randomize();

	void storeMoves(const std::vector<MoveStruct>* moves) { _moves = moves; };
	const std::vector<ManaEggStruct>* getManaEggs() { return &_manaeggs; };

private:
	ManaEggs() {
		this->_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/TB_MAGIC.BIN" : "./data/afs/xls_data/TB_MAGIC.BIN");
	};

	std::vector<ManaEggStruct> _manaeggs;
	size_t _eggIndex = 0;
	size_t _spellIndex = 0;

	const std::vector<MoveStruct>* _moves = nullptr;
};