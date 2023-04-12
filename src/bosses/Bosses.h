#pragma once

#include "../common/version_check.h"

#include "../common/BaseDataInterface.h"
#include "./Boss.h"

class Bosses : public BaseDataInterface {
public:
    Bosses(const Bosses&) = delete;
    Bosses(const Bosses&&) = delete;

    static Bosses& getInstance() {
		static Bosses instance;
		return instance;
	};
    
    void write();
    void read();
    void draw();
    void outputToCSV();
    void randomize();
	const std::vector<Boss>& getBosses() { return _bosses; };

private:
    Bosses() {
		_directory = (Version::getInstance().isHDVersion() ? "./content/data/afs/" : "./data/afs/");
        read();
    };

    void writeBoss(std::fstream& stream, const Boss& boss, bool isSecond = false);
    void readBoss(std::ifstream& stream, std::string& filename, bool isSecond = false);

	std::vector<Boss> _bosses;
	size_t _bossIndex = 0;
	bool AilmentBitFlags[8] = {};
	bool MoveAilmentBitFlags[8] = {};
    size_t _moveSetIndex = 0;
	size_t _moveIndex = 0;
	size_t _aiIndex = 0;
	bool _showMoves = false;
	bool _showAI = false;
};