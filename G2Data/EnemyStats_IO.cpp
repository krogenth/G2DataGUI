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

		for (size_t j = 0; j < 2; j++) {

			if (!j)
				output.seekg(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
			else
				output.seekg(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
			output.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			if (offset) {
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

				if (!j)
					output.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					output.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				output.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				output.seekg(offset, std::ios::beg);

				for (size_t k = 0; k < 5; k++) {

					output.write((char*)enemies[i].moves[k].name, 18);

					output.put(enemies[i].moves[k].mp);
					output.put(enemies[i].moves[k].mp >> 8);

					output.put(enemies[i].moves[k].sp);
					output.put(enemies[i].moves[k].sp >> 8);

					output.put(enemies[i].moves[k].unknown);

					output.put(enemies[i].moves[k].targetEffect);

					output.put(enemies[i].moves[k].str);
					output.put(enemies[i].moves[k].str >> 8);

					output.put(enemies[i].moves[k].pow);
					output.put(enemies[i].moves[k].pow >> 8);

					output.put(enemies[i].moves[k].ad);
					output.put(enemies[i].moves[k].ad >> 8);

					output.put(enemies[i].moves[k].targetType);

					output.put(enemies[i].moves[k].unknown1);

					output.put(enemies[i].moves[k].distance);
					output.put(enemies[i].moves[k].distance >> 8);

					output.put(enemies[i].moves[k].accuracy);
					output.put(enemies[i].moves[k].accuracy >> 8);

					output.put(enemies[i].moves[k].range);
					output.put(enemies[i].moves[k].range >> 8);

					output.put(enemies[i].moves[k].castTime);
					output.put(enemies[i].moves[k].castTime >> 8);

					output.put(enemies[i].moves[k].recovery);
					output.put(enemies[i].moves[k].recovery >> 8);

					output.put(enemies[i].moves[k].animation);

					output.put(enemies[i].moves[k].knockDown);

					output.put(enemies[i].moves[k].ipStun);
					output.put(enemies[i].moves[k].ipStun >> 8);

					output.put(enemies[i].moves[k].ipCancelStun);
					output.put(enemies[i].moves[k].ipCancelStun >> 8);

					output.put(enemies[i].moves[k].knockback);
					output.put(enemies[i].moves[k].knockback >> 8);

					output.put(enemies[i].moves[k].element);

					output.put(enemies[i].moves[k].elementStr);

					output.put(enemies[i].moves[k].ailmentsBitflag);

					output.put(enemies[i].moves[k].ailmentsChance);

					output.put(enemies[i].moves[k].atkMod);

					output.put(enemies[i].moves[k].defMod);

					output.put(enemies[i].moves[k].actMod);

					output.put(enemies[i].moves[k].movMod);

					output.put(enemies[i].moves[k].special);
					output.put(enemies[i].moves[k].special >> 8);

				}

			}

		}

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

		if (std::strstr(filename.c_str(), "_0.dat")) {

			input.open(p.path().u8string(), std::ios::binary);

			if (!input.is_open())
				throw new std::exception(filename.c_str());

			input.seekg(0x34, std::ios::beg);
			input.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
			if(offset)
				count++;

			input.seekg(0x44, std::ios::beg);
			input.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
			if (offset)
				count++;

			input.close();

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
			if (offset)
				count++;

			input.seekg(0x44, std::ios::beg);
			input.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
			if (offset)
				count++;

			input.close();

		}

	}

	EnemyStatsStruct* enemies = new EnemyStatsStruct[count];

	for (const auto& p : std::experimental::filesystem::directory_iterator("content/data/afs/enemy")) {

		filename = p.path().u8string();

		if (std::strstr(filename.c_str(), "_0.dat")) {

			input.open(p.path().u8string(), std::ios::binary);

			if (!input.is_open())
				throw new std::exception(filename.c_str());

			for (size_t i = 0; i < 2; i++) {

				if (!i)
					input.seekg(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
				else
					input.seekg(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
				input.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

				if (offset) {

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

					if (!i)
						input.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
					else
						input.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
					input.read(readByte, 4);
					offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
					input.seekg(offset, std::ios::beg);

					//don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
					for (size_t j = 0; j < 5; j++) {

						input.read(enemies[index].moves[j].name, 18);
						replaceNulls(enemies[index].moves[j].name, 18);

						input.read(readByte, 2);
						enemies[index].moves[j].mp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].sp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].unknown = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].targetEffect = (ImU8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].str = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].pow = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].ad = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].targetType = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].unknown1 = (ImU8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].distance = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].accuracy = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].range = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].castTime = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].recovery = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].animation = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].knockDown = (ImU8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].ipStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].ipCancelStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].knockback = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].element = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].elementStr = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].ailmentsBitflag = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].ailmentsChance = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].atkMod = (ImS8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].defMod = (ImS8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].actMod = (ImS8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].movMod = (ImS8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].special = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

					}

					index++;

				}

			}

			input.close();

		}

	}

	for (const auto& p : std::experimental::filesystem::directory_iterator("content/data/afs/boss")) {

		filename = p.path().u8string();

		if (std::strstr(filename.c_str(), "_0.dat")) {

			input.open(p.path().u8string(), std::ios::binary);

			if (!input.is_open())
				throw new std::exception(filename.c_str());

			for (size_t i = 0; i < 2; i++) {

				if (!i)
					input.seekg(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
				else
					input.seekg(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
				input.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

				if (offset) {

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

					if (!i)
						input.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
					else
						input.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
					input.read(readByte, 4);
					offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
					input.seekg(offset, std::ios::beg);

					//don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
					for (size_t j = 0; j < 5; j++) {

						input.read(enemies[index].moves[j].name, 18);
						replaceNulls(enemies[index].moves[j].name, 18);

						input.read(readByte, 2);
						enemies[index].moves[j].mp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].sp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].unknown = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].targetEffect = (ImU8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].str = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].pow = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].ad = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].targetType = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].unknown1 = (ImU8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].distance = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].accuracy = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].range = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].castTime = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].recovery = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].animation = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].knockDown = (ImU8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].ipStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].ipCancelStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

						input.read(readByte, 2);
						enemies[index].moves[j].knockback = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

						input.read(readByte, 1);
						enemies[index].moves[j].element = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].elementStr = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].ailmentsBitflag = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].ailmentsChance = (ImU8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].atkMod = (ImS8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].defMod = (ImS8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].actMod = (ImS8)readByte[0];

						input.read(readByte, 1);
						enemies[index].moves[j].movMod = (ImS8)readByte[0];

						input.read(readByte, 2);
						enemies[index].moves[j].special = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

					}

					index++;

				}

			}

			input.close();

		}

	}

	return enemies;

}