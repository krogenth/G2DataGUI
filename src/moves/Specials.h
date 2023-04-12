#pragma once
#include <vector>

#include "Special.h"

#include "../common/version_check.h"
#include "../common/BaseDataInterface.h"

class Specials : public BaseDataInterface {
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

	const std::vector<SpecialMove>& getSpecials() { return _specials; };

private:
	Specials() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/TB_SPCL.BIN" : "./data/afs/xls_data/TB_SPCL.BIN");
		read();
	};

	std::vector<SpecialMove> _specials;
	size_t _specialIndex = 0;
	size_t _moveIndex = 0;
};