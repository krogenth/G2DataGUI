#pragma once
#include <vector>
#include <fstream>

#include "../common/version_check.h"

#include "../common/BaseDataInterface.h"
#include "./Enemy.h"

class Enemies : public BaseDataInterface {
public:
	Enemies(const Enemies&) = delete;
    Enemies(const Enemies&&) = delete;

	static Enemies& getInstance() {
		static Enemies instance;
		return instance;
	};
	
	void write();
	void read();
	void draw();
	void outputToCSV();
	void randomize();

	const std::vector<Enemy>& getEnemies() { return _enemies; };

private:
	Enemies() {
		_directory = (Version::getInstance().isHDVersion() ? "./content/data/afs/" : "./data/afs/");
		read();
	};

	void writeEnemy(std::fstream& stream, const Enemy& enemy, bool isSecond = false);
	void readEnemy(std::ifstream& stream, std::string& filename, bool isSecond = false);

	std::vector<Enemy> _enemies;
	size_t _enemyIndex = 0;
	bool AilmentBitFlags[8] = {};
	bool MoveAilmentBitFlags[8] = {};
	size_t _moveIndex = 0;
	size_t _aiIndex = 0;
	bool _showMoves = false;
	bool _showAI = false;
};