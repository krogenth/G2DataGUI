#pragma once

#include "./common/version_check.h"

#include "./BaseDataClass.h"
#include "./EnemiesClass.h"

struct BossStruct {
    char* name = new char[19]{};
    EnemyStatsStruct stats;
    EnemyAIStruct ai[6]{};
    std::vector<EnemyMovesStruct> moveSets{};
    bool isSecond = false;

    std::string filename = "";
};

class Bosses : public BaseDataClass {
public:
    Bosses(const Bosses&) = delete;
    Bosses(const Bosses&&) = delete;

    static Bosses& getInstance() {
		static Bosses instance;
		return instance;
	};
    
    void write();
    void read(bool isHardmode = false);
    void draw();
    void outputToCSV();
    void randomize();
    void storeMoves(const std::vector<MoveStruct>* moves) { _moves = moves; };
	void storeItems(const std::vector<ItemStruct>* items) { _items = items; };
	const std::vector<BossStruct>* getBosses() { return &_bosses; };

private:
    Bosses() {
		this->_directory = (Version::getInstance().isHDVersion() ? "./content/data/afs/" : "./data/afs/");
    };

    void writeBoss(std::fstream& stream, const BossStruct& boss, bool isSecond = false);
    void readBoss(std::ifstream& stream, std::string& filename, bool isSecond = false);

	std::vector<BossStruct> _bosses;
	size_t _bossIndex = 0;
	bool AilmentBitFlags[8] = {};
	bool _showMoves = false;
	bool _showAI = false;
	bool MoveAilmentBitFlags[8] = {};
    size_t _moveSetIndex = 0;
	size_t _moveIndex = 0;
	size_t _aiIndex = 0;
    bool isHardmode = false;

	const std::vector<MoveStruct>* _moves = nullptr;
	const std::vector<ItemStruct>* _items = nullptr;
};