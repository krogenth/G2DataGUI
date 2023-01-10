#pragma once

#include <vector>

#include "./common/version_check.h"
#include "BaseDataClass.h"

#pragma pack(1)
struct MoveStatsStruct {
	uint16_t cost = 0;
	uint8_t targetEffect = 0;
	uint8_t targetType = 0;
	uint16_t str = 0;
	uint16_t pow = 0;
	uint16_t range = 0;
	uint16_t cast1 = 0;
	uint16_t cast5 = 0;
	uint16_t recovery = 0;
	uint16_t animation = 0;
	uint8_t unknown1 = 0;
	uint8_t knockDown = 0;
	int16_t ipStun = 0;
	int16_t ipCancelStun = 0;
	int16_t knockback = 0;
	uint8_t element = 0;
	uint8_t elementStr = 0;
	uint8_t ailmentsBitflag = 0;
	uint8_t ailmentsChance = 0;
	int8_t atkMod = 0;
	int8_t defMod = 0;
	int8_t actMod = 0;
	int8_t movMod = 0;
	uint16_t special = 0;
	uint16_t coinCost1 = 0;
	uint16_t coinCost2 = 0;
	uint16_t coinCost3 = 0;
	uint16_t coinCost4 = 0;
	uint16_t coinCost5 = 0;
	uint16_t multiplier = 0;
};
#pragma pack()

struct MoveStruct {
	uint8_t id = 0;	// overwrite this with array index, used to reference which move manaeggs and specials reference
	uint8_t icon = 0;
	char name[19];
	MoveStatsStruct stats;
	char description[41];
};

class Moves : public BaseDataClass{
public:
	Moves(const Moves&) = delete;
    Moves(const Moves&&) = delete;

    static Moves& getInstance() {
		static Moves instance;
		return instance;
	};

	void write();
	void read();
	void draw();
	void outputToCSV();

	const std::vector<MoveStruct>* getMoves() { return &_moves; };

private:
	Moves() {
		this->_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/MS_PARAM.BIN" : "./data/afs/xls_data/MS_PARAM.BIN");
	};

	std::vector<MoveStruct> _moves;
	size_t _moveIndex = 0;
	bool AilmentBitFlags[8] = {};
};