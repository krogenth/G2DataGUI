#pragma once

#include <vector>

#include "../common/version_check.h"

#include "../common/BaseDataInterface.h"
#include "./CharacterLevelups.h"

static const size_t MAX_LEVEL = 99;

class Levelups : public BaseDataInterface {
public:
    Levelups(const Levelups&) = delete;
    Levelups(const Levelups&&) = delete;

    static Levelups& getInstance() {
		static Levelups instance;
		return instance;
	};
    
    void write();
    void read();
    void draw();
    void outputToCSV();

    const std::vector<CharacterLevelups> getLevelUps() { return _levelups; };

private:
    Levelups() {
        _filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/TB_LVUP.BIN" : "./data/afs/xls_data/TB_LVUP.BIN");
        read();
    };

    std::vector<CharacterLevelups> _levelups;
    size_t _characterIndex = 0;
    size_t _characterLevelupIndex = 0;
};