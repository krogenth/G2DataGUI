#pragma once
#include <vector>

#include "./ManaEgg.h"

#include "../common/version_check.h"
#include "../common/BaseDataInterface.h"

class ManaEggs : public BaseDataInterface {
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

	const std::vector<ManaEgg>& getManaEggs() { return _manaeggs; };

private:
	ManaEggs() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/TB_MAGIC.BIN" : "./data/afs/xls_data/TB_MAGIC.BIN");
		read();
	};

	std::vector<ManaEgg> _manaeggs;
	size_t _eggIndex = 0;
	size_t _spellIndex = 0;
};