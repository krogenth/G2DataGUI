#pragma once

#include <vector>

#include "./common/version_check.h"

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

class Specials : public BaseDataClass {
public:
	Specials(const Specials&) = delete;
    Specials(const Specials&&) = delete;

    static Specials& getInstance() {
		static Specials instance;
		return instance;
	};
	
	void write();
	void read();
	void draw();
	void outputToCSV();

	void storeMoves(const std::vector<MoveStruct>* moves) { _moves = moves; };
	const std::vector<SpecialMoveStruct>* getSpecials() { return &_specials; };
	size_t getNumSpecials() { return _specials.size(); };

private:
	Specials() {
		this->_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/TB_SPCL.BIN" : "./data/afs/xls_data/TB_SPCL.BIN");
	};

	std::vector<SpecialMoveStruct> _specials;
	size_t _specialIndex = 0;
	size_t _moveIndex = 0;

	const std::vector<MoveStruct>* _moves = nullptr;
};