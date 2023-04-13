#pragma once

#include <vector>

#include "../common/version_check.h"

#include "../common/BaseDataInterface.h"
#include "./CharacterStartStats.h"

class StartStats : public BaseDataInterface {
public:
	StartStats(const StartStats&) = delete;
    StartStats(const StartStats&&) = delete;

    static StartStats& getInstance() {
		static StartStats instance;
		return instance;
	};

	void write();
	void read();
	void draw();
	void outputToCSV();
	void randomize();

	const std::vector<CharacterStartStats>& getStartStats() { return _startStats; };

private:
	StartStats() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/PC_INIT.BIN" : "./data/afs/xls_data/PC_INIT.BIN");
		read();
	};

	std::vector<CharacterStartStats> _startStats;
	size_t _statIndex = 0;
};