#pragma once
#include <vector>
#include <string>

#include ".\imgui.h"

#pragma pack(1)
struct EnemyAIStruct {
	ImU8 aiType = 0;
	ImU8 move1Chance = 0;
	ImU8 move2Chance = 0;
	ImU8 move3Chance = 0;
	ImU8 move4Chance = 0;
	ImU8 move5Chance = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyMoveStatsStruct {

	ImU16 mp = 0;
	ImU16 sp = 0;
	ImU8 unknown = 0;
	ImU8 targetEffect = 0;
	ImU16 str = 0;
	ImU16 pow = 0;
	ImU16 ad = 0;
	ImU8 targetType = 0;
	ImU8 unknown1 = 0;
	ImU16 distance = 0;
	ImU16 accuracy = 0;
	ImU16 range = 0;
	ImU16 castTime = 0;
	ImU16 recovery = 0;
	ImU8 animation = 0;
	ImU8 knockDown = 0;
	ImS16 ipStun = 0;
	ImS16 ipCancelStun = 0;
	ImS16 knockback = 0;
	ImU8 element = 0;
	ImU8 elementStr = 0;
	ImU8 ailmentsBitflag = 0;
	ImU8 ailmentsChance = 0;
	ImS8 atkMod = 0;
	ImS8 defMod = 0;
	ImS8 actMod = 0;
	ImS8 movMod = 0;
	ImU16 special = 0;

};
#pragma pack()

struct EnemyMoveStruct {

	char* name = new char[19]{};
	EnemyMoveStatsStruct stats;

};

#pragma pack(1)
struct EnemyStatsStruct {

	ImU8 unknown = 0;
	ImU8 unknown1 = 0;
	ImU8 type1 = 0;
	ImU8 type2 = 0;
	ImS16 level = 0;
	ImS32 health = 0;
	ImS16 mp = 0;
	ImS16 sp = 0;
	ImS16 vit = 0;
	ImS16 agi = 0;
	ImS16 spd = 0;
	ImS16 men = 0;
	ImS16 stamina = 0;
	ImS16 ipStun = 0;
	ImS16 ipCancelStun = 0;
	ImS8 evasionStillRate = 0;
	ImS8 evasionMovingRate = 0;
	ImS8 fireResist = 0;
	ImS8 windResist = 0;
	ImS8 earthResist = 0;
	ImS8 lightningResist = 0;
	ImS8 blizzardResist = 0;
	ImS8 ailmentsBitflag = 0;
	ImS16 knockbackResist = 0;
	ImS16 T_REC = 0;
	ImS16 T_DMG = 0;
	ImS16 unknown2 = 0;
	ImS16 T_HEAL = 0;
	ImS16 size = 0;
	ImS16 unknown3 = 0;
	ImS8 unknown4 = 0;
	ImS8 noRunFlag = 0;
	ImS16 unknown5 = 0;
	ImS32 exp = 0;
	ImS32 skillCoins = 0;
	ImS32 magicCoins = 0;
	ImS32 goldCoins = 0;
	ImU16 item1 = 0;
	ImU16 item2 = 0;
	ImS8 item1Chance = 0;
	ImS8 item2Chance = 0;
	ImU16 unknown6 = 0;
};
#pragma pack()

struct EnemyStruct {
	char* name = new char[19]{};
	EnemyStatsStruct stats;
	EnemyAIStruct ai[6]{};
	EnemyMoveStruct moves[5]{};

	std::string filename = "";

};

void writeEnemyStats(std::vector<EnemyStruct>& enemies);
void readEnemyStats(std::vector<EnemyStruct>& enemies, std::string filepath = "content/data/afs/");
void drawEnemyStats(std::vector<EnemyStruct>& enemies, char** enemyIDs, bool* canClose, char** moveIDs, const size_t& numMoves, char** itemIDs, const size_t& numItems);