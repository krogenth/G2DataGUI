#pragma once

#include <vector>

#include "../common/version_check.h"
#include "../common/BaseDataInterface.h"
#include "./Move.h"

class Moves : public BaseDataInterface {
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

	const std::vector<Move>& getMoves() { return _moves; };

private:
	Moves() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/MS_PARAM.BIN" : "./data/afs/xls_data/MS_PARAM.BIN");
		read();
	};

	std::vector<Move> _moves;
	size_t _moveIndex = 0;
	bool AilmentBitFlags[8] = {};
};