#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <fstream>
#include <exception>
#include <experimental/filesystem>
#include <string>

#include "StringManip.h"
#include "EnemyStatsStruct.h"

/*
enemy.csv = 5 slot offset
boss.csv = 20 slot offset
*/

void writeEnemyStats(EnemyStatsStruct* enemies, const ImU16& count) {

	std::fstream output;
	char* readByte = new char[4]{};
	uint32_t offset = 0;

	for (size_t i = 0; i < count; i++) {

		output.open(enemies[i].filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open())
			throw new std::exception(enemies[i].filename.c_str());

		output.seekg(0x34, std::ios::beg);
		output.read(readByte, 4);
		offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

		output.seekg(offset, std::ios::beg);

		output.write((char*)enemies[i].name, 18);

		output.put(enemies[i].unknown);
		output.put(enemies[i].unknown1);

		output.put(enemies[i].type1);
		output.put(enemies[i].type2);

		output.put(enemies[i].level);
		output.put(enemies[i].level >> 8);

		output.put(enemies[i].health);
		output.put(enemies[i].health >> 8);
		output.put(enemies[i].health >> 16);
		output.put(enemies[i].health >> 24);

		output.put(enemies[i].mp);
		output.put(enemies[i].mp >> 8);

		output.put(enemies[i].sp);
		output.put(enemies[i].sp >> 8);

		output.put(enemies[i].vit);
		output.put(enemies[i].vit >> 8);

		output.put(enemies[i].agi);
		output.put(enemies[i].agi >> 8);

		output.put(enemies[i].spd);
		output.put(enemies[i].spd >> 8);

		output.put(enemies[i].men);
		output.put(enemies[i].men >> 8);

		output.put(enemies[i].stamina);
		output.put(enemies[i].stamina >> 8);

		output.put(enemies[i].ipStun);
		output.put(enemies[i].ipStun >> 8);

		output.put(enemies[i].ipCancelStun);
		output.put(enemies[i].ipCancelStun >> 8);

		output.put(enemies[i].evasionStillRate);
		output.put(enemies[i].evasionMovingRate);

		output.put(enemies[i].fireResist % 11);
		output.put(enemies[i].windResist % 11);
		output.put(enemies[i].earthResist % 11);
		output.put(enemies[i].lightningResist % 11);
		output.put(enemies[i].blizzardResist % 11);

		output.put(enemies[i].ailmentsBitflag);

		output.put(enemies[i].knockbackResist);
		output.put(enemies[i].knockbackResist >> 8);

		output.put(enemies[i].T_REC);
		output.put(enemies[i].T_REC >> 8);

		output.put(enemies[i].T_DMG);
		output.put(enemies[i].T_DMG >> 8);

		output.put(enemies[i].unknown2);
		output.put(enemies[i].unknown2 >> 8);

		output.put(enemies[i].T_HEAL);
		output.put(enemies[i].T_HEAL >> 8);

		output.put(enemies[i].size);
		output.put(enemies[i].size >> 8);

		output.put(enemies[i].unknown3);
		output.put(enemies[i].unknown3 >> 8);

		output.put(enemies[i].unknown4);

		output.put(enemies[i].noRunFlag);

		output.put(enemies[i].unknown5);
		output.put(enemies[i].unknown5 >> 8);

		output.put(enemies[i].exp);
		output.put(enemies[i].exp >> 8);
		output.put(enemies[i].exp >> 16);
		output.put(enemies[i].exp >> 24);

		output.put(enemies[i].skillCoins);
		output.put(enemies[i].skillCoins >> 8);
		output.put(enemies[i].skillCoins >> 16);
		output.put(enemies[i].skillCoins >> 24);

		output.put(enemies[i].magicCoins);
		output.put(enemies[i].magicCoins >> 8);
		output.put(enemies[i].magicCoins >> 16);
		output.put(enemies[i].magicCoins >> 24);

		output.put(enemies[i].goldCoins);
		output.put(enemies[i].goldCoins >> 8);
		output.put(enemies[i].goldCoins >> 16);
		output.put(enemies[i].goldCoins >> 24);

		output.put(enemies[i].item1);
		output.put(enemies[i].item1 >> 8);

		output.put(enemies[i].item2);
		output.put(enemies[i].item2 >> 8);

		output.put(enemies[i].item1Chance);

		output.put(enemies[i].item2Chance);

		output.close();

	}

}

EnemyStatsStruct* readEnemyStats(ImU16& count) {

	char* readByte = new char[4]{};
	std::ifstream input;

	std::string filename = "";
	uint32_t index = 0;
	uint32_t offset = 0;

	for (const auto& p : std::experimental::filesystem::directory_iterator("content/data/afs/enemy")) {

		filename = p.path().u8string();

		if (std::strstr(filename.c_str(), "_0.dat"))
			count++;

	}

	for (const auto& p : std::experimental::filesystem::directory_iterator("content/data/afs/boss")) {

		filename = p.path().u8string();

		if (std::strstr(filename.c_str(), "_0.dat"))
			count++;

	}

	EnemyStatsStruct* enemies = new EnemyStatsStruct[count];

	for (const auto& p : std::experimental::filesystem::directory_iterator("content/data/afs/enemy")) {

		filename = p.path().u8string();

		if (std::strstr(filename.c_str(), "_0.dat")) {

			input.open(p.path().u8string(), std::ios::binary);

			if (!input.is_open())
				throw new std::exception(filename.c_str());

			input.seekg(0x34, std::ios::beg);
			input.read(readByte, 4);

			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			input.seekg(offset, std::ios::beg);

			input.read(enemies[index].name, 18);
			replaceNulls(enemies[index].name, 18);

			input.read(readByte, 1);
			enemies[index].unknown = (ImU8)readByte[0];

			input.read(readByte, 1);
			enemies[index].unknown1 = (ImU8)readByte[0];

			input.read(readByte, 1);
			enemies[index].type1 = (ImU8)readByte[0];

			input.read(readByte, 1);
			enemies[index].type2 = (ImU8)readByte[0];

			input.read(readByte, 2);
			enemies[index].level = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].health = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].mp = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].sp = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].vit = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].agi = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].spd = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].men = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].stamina = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].ipStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].ipCancelStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			enemies[index].evasionStillRate = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].evasionMovingRate = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].fireResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].windResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].earthResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].lightningResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].blizzardResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].ailmentsBitflag = (ImS8)readByte[0];

			input.read(readByte, 2);
			enemies[index].knockbackResist = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].T_REC = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].T_DMG = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].unknown2 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].T_HEAL = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].size = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].unknown3 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			enemies[index].unknown4 = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].noRunFlag = (ImS8)readByte[0];

			input.read(readByte, 2);
			enemies[index].unknown5 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].exp = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].skillCoins = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].magicCoins = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].goldCoins = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].item1 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].item2 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			enemies[index].item1Chance = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].item2Chance = (ImS8)readByte[0];

			enemies[index].filename = p.path().u8string();

			input.close();

			index++;

		}

	}

	for (const auto& p : std::experimental::filesystem::directory_iterator("content/data/afs/boss")) {

		filename = p.path().u8string();

		if (std::strstr(filename.c_str(), "_0.dat")) {

			input.open(p.path().u8string(), std::ios::binary);

			if (!input.is_open())
				throw new std::exception(filename.c_str());

			input.seekg(0x34, std::ios::beg);
			input.read(readByte, 4);

			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			input.seekg(offset, std::ios::beg);

			input.read(enemies[index].name, 18);
			replaceNulls(enemies[index].name, 18);

			input.read(readByte, 1);
			enemies[index].unknown = (ImU8)readByte[0];

			input.read(readByte, 1);
			enemies[index].unknown1 = (ImU8)readByte[0];

			input.read(readByte, 1);
			enemies[index].type1 = (ImU8)readByte[0];

			input.read(readByte, 1);
			enemies[index].type2 = (ImU8)readByte[0];

			input.read(readByte, 2);
			enemies[index].level = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].health = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].mp = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].sp = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].vit = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].agi = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].spd = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].men = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].stamina = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].ipStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].ipCancelStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			enemies[index].evasionStillRate = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].evasionMovingRate = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].fireResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].windResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].earthResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].lightningResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].blizzardResist = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].ailmentsBitflag = (ImS8)readByte[0];

			input.read(readByte, 2);
			enemies[index].knockbackResist = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].T_REC = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].T_DMG = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].unknown2 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].T_HEAL = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].size = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].unknown3 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			enemies[index].unknown4 = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].noRunFlag = (ImS8)readByte[0];

			input.read(readByte, 2);
			enemies[index].unknown5 = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].exp = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].skillCoins = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].magicCoins = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 4);
			enemies[index].goldCoins = ((ImS16)((ImU8)(readByte[3])) << 24) + ((ImS16)((ImU8)(readByte[2])) << 16) + ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].item1 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			enemies[index].item2 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			enemies[index].item1Chance = (ImS8)readByte[0];

			input.read(readByte, 1);
			enemies[index].item2Chance = (ImS8)readByte[0];

			enemies[index].filename = p.path().u8string();

			input.close();

			index++;

		}

	}

	return enemies;

}