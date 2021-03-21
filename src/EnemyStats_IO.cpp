#include <fstream>
#include <exception>
#include <filesystem>

#include ".\include\StringManip.h"
#include ".\include\EnemyStatsStruct.h"
#include ".\include\io_util.h"
#include ".\include\char_constants.h"

/*
enemy.csv = 5 slot offset(field enemies 8 slot offset)
boss.csv = 20 slot offset
*/

void writeEnemyStats(std::vector<EnemyStruct>& enemies) {

	std::fstream output;
	char* readByte = new char[4]{};
	uint32_t offset = 0;

	for (size_t i = 0; i < enemies.size(); i++) {

		output.open(enemies[i].filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open())
			throw new std::exception(enemies[i].filename.c_str());

		for (size_t j = 0; j < 2; j++) {

			if (!j)
				output.seekp(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
			else
				output.seekp(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
			output.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			if (offset) {

				output.seekg(offset, std::ios::beg);

				output.write((char*)enemies[i].name, 18);
				writeRaw<EnemyStatsStruct>(output, enemies[i].stats);

				if (!j)
					output.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					output.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				output.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				output.seekg(offset, std::ios::beg);

				for (size_t k = 0; k < 5; k++) {

					output.write((char*)enemies[i].moves[k].name, 18);

					writeRaw<EnemyMoveStatsStruct>(output, enemies[i].moves[k].stats);

				}

			}

		}

		output.close();

	}

}

void readEnemyStats(std::vector<EnemyStruct>& enemies, std::string filepath) {

	char* readByte = new char[4]{};
	std::ifstream input;

	std::string filename = "";
	uint32_t offset = 0;

	for (const auto& p : std::filesystem::directory_iterator(filepath + "enemy")) {

		filename = p.path().u8string();

		if (!std::strstr(filename.c_str(), "_0.dat"))
			continue;

		input.open(filename, std::ios::binary);

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

				enemies.emplace_back(EnemyStruct());
				input.seekg(offset, std::ios::beg);

				input.read(enemies.back().name, 18);
				replaceNulls(enemies.back().name, 18);

				enemies.back().stats = readRaw<EnemyStatsStruct>(input);

				enemies.back().filename = p.path().u8string();

				if (!i)
					input.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					input.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				input.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				input.seekg(offset, std::ios::beg);

				//don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
				for (size_t j = 0; j < 5; j++) {

					input.read(enemies.back().moves[j].name, 18);
					replaceNulls(enemies.back().moves[j].name, 18);

					enemies.back().moves[j].stats = readRaw<EnemyMoveStatsStruct>(input);

				}

			}

		}

		input.close();

	}

	for (const auto& p : std::filesystem::directory_iterator(filepath + "boss")) {

		filename = p.path().u8string();

		if (!std::strstr(filename.c_str(), "_0.dat"))
			continue;

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

				enemies.emplace_back(EnemyStruct());
				input.seekg(offset, std::ios::beg);

				input.read(enemies.back().name, 18);
				replaceNulls(enemies.back().name, 18);

				enemies.back().stats = readRaw<EnemyStatsStruct>(input);

				enemies.back().filename = p.path().u8string();	//	we need the filename for writing data

				if (!i)
					input.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					input.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				input.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				input.seekg(offset, std::ios::beg);

				//	don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
				for (size_t j = 0; j < 5; j++) {

					input.read(enemies.back().moves[j].name, 18);
					replaceNulls(enemies.back().moves[j].name, 18);

					enemies.back().moves[j].stats = readRaw<EnemyMoveStatsStruct>(input);

				}

			}

		}

		input.close();

	}

}

void drawEnemyStats(std::vector<EnemyStruct>& enemies, char** enemyIDs, bool* canClose, char** moveIDs, const size_t& numMoves, char** itemIDs, const size_t& numItems) {

	static ImU16 enemyID = 0;
	static bool AilmentBitFlags[8] = {};
	static bool showMoves = false;
	static bool MoveAilmentBitFlags[8] = {};
	static ImU16 moveSlot = 0;

	ImGui::Begin("ENEMIES");

	if (ImGui::Combo("Index", &enemyID, enemyIDs, (int)enemies.size()), 100)
		for (size_t i = 0; i < 8; i++)
			AilmentBitFlags[i] = enemies[enemyID].stats.ailmentsBitflag & (1 << i);

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeEnemyStats(enemies);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::InputText("Name", enemies[enemyID].name, 19);

	ImGui::InputUByte("Type1", &enemies[enemyID].stats.type1);
	ImGui::InputUByte("Type2", &enemies[enemyID].stats.type2);

	ImGui::InputShort("Level", &enemies[enemyID].stats.level);
	ImGui::InputInt("Health", &enemies[enemyID].stats.health);
	ImGui::InputShort("MP", &enemies[enemyID].stats.mp);
	ImGui::InputShort("SP", &enemies[enemyID].stats.sp);
	ImGui::InputShort("VIT", &enemies[enemyID].stats.vit);
	ImGui::InputShort("AGI", &enemies[enemyID].stats.agi);
	ImGui::InputShort("SPD", &enemies[enemyID].stats.spd);
	ImGui::InputShort("MEN", &enemies[enemyID].stats.men);
	ImGui::InputShort("Stamina", &enemies[enemyID].stats.stamina);                                    if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	ImGui::InputShort2("IP Stun/IP Cancel Stun", &enemies[enemyID].stats.ipStun);                     if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	ImGui::InputByte("Still Evasion Rate", &enemies[enemyID].stats.evasionStillRate);
	ImGui::InputByte("Moving Evasion Rate", &enemies[enemyID].stats.evasionMovingRate);
	ImGui::InputByte("Fire Resist", &enemies[enemyID].stats.fireResist);
	ImGui::InputByte("Wind Resist", &enemies[enemyID].stats.windResist);
	ImGui::InputByte("Earth Resist", &enemies[enemyID].stats.earthResist);
	ImGui::InputByte("Lightning Resist", &enemies[enemyID].stats.lightningResist);
	ImGui::InputByte("Blizzard Resist", &enemies[enemyID].stats.blizzardResist);

	if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[0] << 0);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[1] << 1);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[2] << 2);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[3] << 3);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");

	if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[4] << 4);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[5] << 5);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[6] << 6);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
		enemies[enemyID].stats.ailmentsBitflag ^= (AilmentBitFlags[7] << 7);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");

	ImGui::InputShort("Knockback Resist Rate", &enemies[enemyID].stats.knockbackResist);
	ImGui::InputShort("Size", &enemies[enemyID].stats.size);
	ImGui::InputByte("No Run", &enemies[enemyID].stats.noRunFlag);
	ImGui::InputInt("EXP", &enemies[enemyID].stats.exp);
	ImGui::InputInt("Skill Coins", &enemies[enemyID].stats.skillCoins);
	ImGui::InputInt("Magic Coins", &enemies[enemyID].stats.magicCoins);
	ImGui::InputInt("Gold Coins", &enemies[enemyID].stats.goldCoins);

	ImGui::Combo("Item #1", &enemies[enemyID].stats.item1, itemIDs, (int)numItems);
	ImGui::Combo("Item #2", &enemies[enemyID].stats.item2, itemIDs, (int)numItems);

	ImGui::InputByte("Item #1 Chance", &enemies[enemyID].stats.item1Chance);
	ImGui::InputByte("Item #2 Chance", &enemies[enemyID].stats.item2Chance);

	//ImGui::LabelText("Filename", enemies[enemyID].filename.c_str());      //Used for testing to verify files are correct

	ImGui::Checkbox("Show Moves", &showMoves);

	if (showMoves) {

		ImGui::Begin("ENEMY MOVES");

		if (ImGui::Combo("Slot", &moveSlot, slotIDs, 5))
			for (size_t i = 0; i < 8; i++)
				MoveAilmentBitFlags[i] = enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag & (1 << i);

		ImGui::InputText("Name", enemies[enemyID].moves[moveSlot].name, 19);
		ImGui::InputUShort("MP Cost", &enemies[enemyID].moves[moveSlot].stats.mp);
		ImGui::InputUShort("SP Cost", &enemies[enemyID].moves[moveSlot].stats.sp);
		ImGui::InputUByte("Unknown #1", &enemies[enemyID].moves[moveSlot].stats.unknown);

		ImGui::Combo("Target Effect", &enemies[enemyID].moves[moveSlot].stats.targetEffect, targetEffects, 16);

		ImGui::InputUShort("Strength", &enemies[enemyID].moves[moveSlot].stats.str);
		ImGui::InputUShort("Power", &enemies[enemyID].moves[moveSlot].stats.pow);
		ImGui::InputUShort("Damage(?)", &enemies[enemyID].moves[moveSlot].stats.ad);

		ImGui::Combo("Target Type", &enemies[enemyID].moves[moveSlot].stats.targetType, targetTypes, 16);

		ImGui::InputUByte("Unknown #2", &enemies[enemyID].moves[moveSlot].stats.unknown1);
		ImGui::InputUShort("Distance", &enemies[enemyID].moves[moveSlot].stats.distance);             if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		ImGui::InputUShort("Accuracy", &enemies[enemyID].moves[moveSlot].stats.accuracy);
		ImGui::InputUShort("Range", &enemies[enemyID].moves[moveSlot].stats.range);                   if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		ImGui::InputUShort("Cast Time", &enemies[enemyID].moves[moveSlot].stats.castTime);
		ImGui::InputUShort("Recovery", &enemies[enemyID].moves[moveSlot].stats.recovery);

		//ImGui::InputUByte("Animation", &enemies[enemyID].moves[moveSlot].animation);
		ImGui::Combo("Animation", &enemies[enemyID].moves[moveSlot].stats.animation, moveIDs, numMoves);

		ImGui::InputUByte("Knockdown", &enemies[enemyID].moves[moveSlot].stats.knockDown);            if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");

		ImGui::InputShort2("IP Stun/IP Cancel Stun", &enemies[enemyID].moves[moveSlot].stats.ipStun);
		ImGui::InputShort("Knockback", &enemies[enemyID].moves[moveSlot].stats.knockback);            if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

		ImGui::Combo("Element", &enemies[enemyID].moves[moveSlot].stats.element, elements, 5);
		ImGui::InputUByte("Element Strength", &enemies[enemyID].moves[moveSlot].stats.elementStr);    if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		if (ImGui::Checkbox("Poison", &MoveAilmentBitFlags[0]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &MoveAilmentBitFlags[1]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &MoveAilmentBitFlags[2]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &MoveAilmentBitFlags[3]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &MoveAilmentBitFlags[4]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &MoveAilmentBitFlags[5]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &MoveAilmentBitFlags[6]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &MoveAilmentBitFlags[7]))
			enemies[enemyID].moves[moveSlot].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[7] << 7);

		ImGui::InputUByte("Ailments Chance", &enemies[enemyID].moves[moveSlot].stats.ailmentsChance);

		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &enemies[enemyID].moves[moveSlot].stats.atkMod);

		ImGui::InputUShort("Special", &enemies[enemyID].moves[moveSlot].stats.special);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();

	}

	ImGui::End();

}