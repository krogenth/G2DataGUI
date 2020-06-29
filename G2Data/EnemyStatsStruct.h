#pragma once
#include "ImGui/imgui.h"

struct EnemyStatsStruct {
	char* name = new char[19]{};
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

	std::string filename = "";

};

void writeEnemyStats(EnemyStatsStruct* enemies, const ImU16& count);
EnemyStatsStruct* readEnemyStats(ImU16& count);