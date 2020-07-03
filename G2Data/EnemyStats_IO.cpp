#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <fstream>
#include <exception>
#include <experimental/filesystem>
#include <string>

#include "StringManip.h"
#include "EnemyStatsStruct.h"

/*
enemy.csv = 5 slot offset(field enemies 8 slot offset)
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

void readEnemyStats(std::promise<EnemyStatsStruct*>&& ftr, ImU16& count) {

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

	ftr.set_value(enemies);

}

void drawEnemyStats(EnemyStatsStruct* enemies, char** enemyIDs, ImU16& numEnemies, bool* canClose, char** moveIDs, ImU16& numMoves, char** itemIDs, ImU16& numItems) {

	const char* targetEffects[] = { "NULL", "Restore HP(MEN)", "Restore MP", "Restore SP", "Ally Buff/Debuff", "Physical Damage(STR)", "Magical Damage(MAG)", "Enemy Buff/Debuff", "Status Change", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Special" };
	const char* targetTypes[] = { "NULL", "One Ally", "Area Allies", "All Allies", "One Enemy", "Area Enemies", "All Enemies", "Enemy Line", "Self", "Unknown", "Area Around Self", "Unknown", "Unknown", "Area Around Self", "Unknown", "Unknown" };
	const char* elements[] = { "Fire", "Wind", "Earth", "Lightning", "Blizzard" };
	const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5" };
	static ImU16 enemyID = 0;
	static bool AilmentBitFlags[8] = {};
	static bool showMoves = false;
	static bool MoveAilmentBitFlags[8] = {};
	static ImU16 moveSlot = 0;

	ImGui::Begin("ENEMIES");

	if (ImGui::Combo("Index", &enemyID, enemyIDs, (int)numEnemies), 100)
		for (size_t i = 0; i < 8; i++)
			AilmentBitFlags[i] = enemies[enemyID].ailmentsBitflag & (1 << i);

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeEnemyStats(enemies, numEnemies);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::InputText("Name", enemies[enemyID].name, 19);

	ImGui::InputUByte("Type1", &enemies[enemyID].type1);
	ImGui::InputUByte("Type2", &enemies[enemyID].type2);

	ImGui::InputShort("Level", &enemies[enemyID].level);
	ImGui::InputInt("Health", &enemies[enemyID].health);
	ImGui::InputShort("MP", &enemies[enemyID].mp);
	ImGui::InputShort("SP", &enemies[enemyID].sp);
	ImGui::InputShort("VIT", &enemies[enemyID].vit);
	ImGui::InputShort("AGI", &enemies[enemyID].agi);
	ImGui::InputShort("SPD", &enemies[enemyID].spd);
	ImGui::InputShort("MEN", &enemies[enemyID].men);
	ImGui::InputShort("Stamina", &enemies[enemyID].stamina);                                    if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	ImGui::InputShort2("IP Stun/IP Cancel Stun", &enemies[enemyID].ipStun);                     if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	ImGui::InputByte("Still Evasion Rate", &enemies[enemyID].evasionStillRate);
	ImGui::InputByte("Moving Evasion Rate", &enemies[enemyID].evasionMovingRate);
	ImGui::InputByte("Fire Resist", &enemies[enemyID].fireResist);
	ImGui::InputByte("Wind Resist", &enemies[enemyID].windResist);
	ImGui::InputByte("Earth Resist", &enemies[enemyID].earthResist);
	ImGui::InputByte("Lightning Resist", &enemies[enemyID].lightningResist);
	ImGui::InputByte("Blizzard Resist", &enemies[enemyID].blizzardResist);

	if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[0] << 0);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[1] << 1);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[2] << 2);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[3] << 3);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");

	if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[4] << 4);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[5] << 5);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[6] << 6);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
		enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[7] << 7);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");

	ImGui::InputShort("Knockback Resist Rate", &enemies[enemyID].knockbackResist);
	ImGui::InputShort("Size", &enemies[enemyID].size);
	ImGui::InputByte("No Run", &enemies[enemyID].noRunFlag);
	ImGui::InputInt("EXP", &enemies[enemyID].exp);
	ImGui::InputInt("Skill Coins", &enemies[enemyID].skillCoins);
	ImGui::InputInt("Magic Coins", &enemies[enemyID].magicCoins);
	ImGui::InputInt("Gold Coins", &enemies[enemyID].goldCoins);

	ImGui::Combo("Item1", &enemies[enemyID].item1, itemIDs, (int)numItems);
	ImGui::Combo("Item2", &enemies[enemyID].item2, itemIDs, (int)numItems);

	ImGui::InputByte("Item1 Chance", &enemies[enemyID].item1Chance);
	ImGui::InputByte("Item2 Chance", &enemies[enemyID].item2Chance);

	//ImGui::LabelText("Filename", enemies[enemyID].filename.c_str());      //Used for testing to verify files are correct

	ImGui::Checkbox("Show Moves", &showMoves);

	if (showMoves) {

		ImGui::Begin("ENEMY MOVES");

		if (ImGui::Combo("Slot", &moveSlot, slotIDs, 5))
			for (size_t i = 0; i < 8; i++)
				MoveAilmentBitFlags[i] = enemies[enemyID].moves[moveSlot].ailmentsBitflag & (1 << i);

		ImGui::InputText("Name", enemies[enemyID].moves[moveSlot].name, 19);
		ImGui::InputUShort("MP Cost", &enemies[enemyID].moves[moveSlot].mp);
		ImGui::InputUShort("SP Cost", &enemies[enemyID].moves[moveSlot].sp);
		ImGui::InputUByte("Unknown #1", &enemies[enemyID].moves[moveSlot].unknown);

		ImGui::Combo("Target Effect", &enemies[enemyID].moves[moveSlot].targetEffect, targetEffects, 16);

		ImGui::InputUShort("Strength", &enemies[enemyID].moves[moveSlot].str);
		ImGui::InputUShort("Power", &enemies[enemyID].moves[moveSlot].pow);
		ImGui::InputUShort("Damage(?)", &enemies[enemyID].moves[moveSlot].ad);

		ImGui::Combo("Target Type", &enemies[enemyID].moves[moveSlot].targetType, targetTypes, 16);

		ImGui::InputUByte("Unknown #2", &enemies[enemyID].moves[moveSlot].unknown1);
		ImGui::InputUShort("Distance", &enemies[enemyID].moves[moveSlot].distance);             if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		ImGui::InputUShort("Accuracy", &enemies[enemyID].moves[moveSlot].accuracy);
		ImGui::InputUShort("Range", &enemies[enemyID].moves[moveSlot].range);                   if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		ImGui::InputUShort("Cast Time", &enemies[enemyID].moves[moveSlot].castTime);
		ImGui::InputUShort("Recovery", &enemies[enemyID].moves[moveSlot].recovery);

		//ImGui::InputUByte("Animation", &enemies[enemyID].moves[moveSlot].animation);
		ImGui::Combo("Animation", &enemies[enemyID].moves[moveSlot].animation, moveIDs, numMoves);

		ImGui::InputUByte("Knockdown", &enemies[enemyID].moves[moveSlot].knockDown);            if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");

		ImGui::InputShort2("IP Stun/IP Cancel Stun", &enemies[enemyID].moves[moveSlot].ipStun);
		ImGui::InputShort("Knockback", &enemies[enemyID].moves[moveSlot].knockback);            if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

		ImGui::Combo("Element", &enemies[enemyID].moves[moveSlot].element, elements, 5);
		ImGui::InputUByte("Element Strength", &enemies[enemyID].moves[moveSlot].elementStr);    if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		if (ImGui::Checkbox("Poison", &MoveAilmentBitFlags[0]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &MoveAilmentBitFlags[1]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &MoveAilmentBitFlags[2]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &MoveAilmentBitFlags[3]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &MoveAilmentBitFlags[4]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &MoveAilmentBitFlags[5]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &MoveAilmentBitFlags[6]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &MoveAilmentBitFlags[7]))
			enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[7] << 7);

		ImGui::InputUByte("Ailments Chance", &enemies[enemyID].moves[moveSlot].ailmentsChance);

		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &enemies[enemyID].moves[moveSlot].atkMod);

		ImGui::InputUShort("Special", &enemies[enemyID].moves[moveSlot].special);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();

	}

	ImGui::End();

}