#ifndef ENEMIES_CLASS_H
#define ENEMIES_CLASS_H

#include <vector>

#include "./BaseDataClass.h"

#include "./MovesClass.h"
#include "./ItemsClass.h"

#pragma pack(1)
struct EnemyAIStruct {
	uint8_t aiType = 0;
	uint8_t move1Chance = 0;
	uint8_t move2Chance = 0;
	uint8_t move3Chance = 0;
	uint8_t move4Chance = 0;
	uint8_t move5Chance = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyMoveStatsStruct {

	uint16_t mp = 0;
	uint16_t sp = 0;
	uint8_t unknown = 0;
	uint8_t targetEffect = 0;
	uint16_t str = 0;
	uint16_t pow = 0;
	uint16_t ad = 0;
	uint8_t targetType = 0;
	uint8_t normalAttackFlag = 0;
	uint16_t distance = 0;
	uint16_t accuracy = 0;
	uint16_t range = 0;
	uint16_t castTime = 0;
	uint16_t recovery = 0;
	uint8_t animation = 0;
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

};
#pragma pack()

struct EnemyMoveStruct {

	char* name = new char[19]{};
	EnemyMoveStatsStruct stats;

};

#pragma pack(1)
struct EnemyStatsStruct {

	uint8_t unknown = 0;
	uint8_t unknown1 = 0;
	uint8_t type1 = 0;
	uint8_t type2 = 0;
	int16_t level = 0;
	int32_t health = 0;
	int16_t mp = 0;
	int16_t sp = 0;
	int16_t vit = 0;
	int16_t agi = 0;
	int16_t spd = 0;
	int16_t men = 0;
	int16_t stamina = 0;
	int16_t ipStun = 0;
	int16_t ipCancelStun = 0;
	int8_t evasionStillRate = 0;
	int8_t evasionMovingRate = 0;
	int8_t fireResist = 0;
	int8_t windResist = 0;
	int8_t earthResist = 0;
	int8_t lightningResist = 0;
	int8_t blizzardResist = 0;
	uint8_t ailmentsBitflag = 0;
	int16_t knockbackResist = 0;
	int16_t T_REC = 0;
	int16_t T_DMG = 0;
	int16_t unknown2 = 0;
	int16_t T_HEAL = 0;
	int16_t size = 0;
	int16_t unknown3 = 0;
	int8_t unknown4 = 0;
	int8_t noRunFlag = 0;
	int16_t unknown5 = 0;
	int32_t exp = 0;
	int32_t skillCoins = 0;
	int32_t magicCoins = 0;
	int32_t goldCoins = 0;
	uint16_t item1 = 0;
	uint16_t item2 = 0;
	int8_t item1Chance = 0;
	int8_t item2Chance = 0;
	uint16_t unknown6 = 0;
};
#pragma pack()

struct EnemyStruct {
	char* name = new char[19]{};
	EnemyStatsStruct stats;
	EnemyAIStruct ai[6]{};
	EnemyMoveStruct moves[5]{};

	std::string filename = "";

};

class EnemiesClass : public BaseDataClass {

public:
	EnemiesClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();
	void randomize();

	void storeMoves(MoveStruct* moves, size_t numMoves) { _moves = moves; _numMoves = numMoves; };
	void storeItems(ItemStruct* items, size_t numItems) { _items = items; _numItems = numItems; };
	EnemyStruct* getEnemies() {

		if (_enemies.size())
			return &_enemies.at(0);
		else
			return nullptr;

	};
	size_t getNumMoves() { return _enemies.size(); };

private:
	std::vector<EnemyStruct> _enemies;
	size_t _enemyIndex = 0;
	bool AilmentBitFlags[8] = {};
	bool _showMoves = false;
	bool _showAI = false;
	bool MoveAilmentBitFlags[8] = {};
	size_t _moveIndex = 0;
	size_t _aiIndex = 0;

	MoveStruct* _moves = nullptr;
	size_t _numMoves = 0;

	ItemStruct* _items = nullptr;
	size_t _numItems = 0;

};

#endif