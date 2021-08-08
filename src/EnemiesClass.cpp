#include <fstream>
#include <filesystem>

#include ".\include\EnemiesClass.h"

#include ".\include\common\io_util.h"
#include ".\include\common\char_constants.h"
#include ".\include\common\string_manip.h"
#include ".\include\common\copypaste_obj.h"

#include ".\imgui.h"

void EnemiesClass::write() {

	std::fstream output;
	char* readByte = new char[4]{};
	uint32_t offset = 0;

	for (size_t i = 0; i < this->_enemies.size(); i++) {

		output.open(this->_enemies.at(i).filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open())
			throw new std::exception(this->_enemies.at(i).filename.c_str());

		for (size_t j = 0; j < 2; j++) {

			if (!j)
				output.seekp(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
			else
				output.seekp(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
			output.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			if (offset) {

				output.seekg(offset, std::ios::beg);

				output.write((char*)this->_enemies.at(i).name, 18);
				writeRaw<EnemyStatsStruct>(output, this->_enemies.at(i).stats);

				//	write AI data
				for (size_t k = 0; k < 5; k++)
					writeRaw<EnemyAIStruct>(output, this->_enemies.at(i).ai[k]);

				if (!j)
					output.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					output.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				output.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				output.seekg(offset, std::ios::beg);

				for (size_t k = 0; k < 5; k++) {

					output.write((char*)this->_enemies.at(i).moves[k].name, 18);

					writeRaw<EnemyMoveStatsStruct>(output, this->_enemies.at(i).moves[k].stats);

				}

			}

		}

		output.close();

	}

}

void EnemiesClass::read(std::string filename) {

	char* readByte = new char[4]{};
	std::ifstream input;

	std::string actualFilename = "";
	uint32_t offset = 0;

	for (const auto& p : std::filesystem::directory_iterator(filename + "enemy")) {

		actualFilename = p.path().u8string();

		if (!std::strstr(actualFilename.c_str(), "_0.dat"))
			continue;

		input.open(actualFilename, std::ios::binary);

		if (!input.is_open())
			throw new std::exception(actualFilename.c_str());

		for (size_t i = 0; i < 2; i++) {

			if (!i)
				input.seekg(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
			else
				input.seekg(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
			input.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			if (offset) {

				this->_enemies.emplace_back(EnemyStruct());
				input.seekg(offset, std::ios::beg);

				input.read(this->_enemies.back().name, 18);
				replaceNulls(this->_enemies.back().name, 18);

				this->_enemies.back().stats = readRaw<EnemyStatsStruct>(input);

				//	read in enemy AI data
				for (size_t j = 0; j < 5; j++)
					this->_enemies.back().ai[j] = readRaw<EnemyAIStruct>(input);

				this->_enemies.back().filename = p.path().u8string();

				if (!i)
					input.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					input.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				input.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				input.seekg(offset, std::ios::beg);

				//don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
				for (size_t j = 0; j < 5; j++) {

					input.read(this->_enemies.back().moves[j].name, 18);
					replaceNulls(this->_enemies.back().moves[j].name, 18);

					this->_enemies.back().moves[j].stats = readRaw<EnemyMoveStatsStruct>(input);

				}

			}

		}

		input.close();

	}

	for (const auto& p : std::filesystem::directory_iterator(filename + "boss")) {

		actualFilename = p.path().u8string();

		if (!std::strstr(actualFilename.c_str(), "_0.dat"))
			continue;

		input.open(p.path().u8string(), std::ios::binary);

		if (!input.is_open())
			throw new std::exception(actualFilename.c_str());

		for (size_t i = 0; i < 2; i++) {

			if (!i)
				input.seekg(0x34, std::ios::beg);	//	first copy data offset is always at 0x34
			else
				input.seekg(0x44, std::ios::beg);	//	second copy data offset is always at 0x44
			input.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			if (offset) {

				this->_enemies.emplace_back(EnemyStruct());
				input.seekg(offset, std::ios::beg);

				input.read(this->_enemies.back().name, 18);
				replaceNulls(this->_enemies.back().name, 18);

				this->_enemies.back().stats = readRaw<EnemyStatsStruct>(input);

				this->_enemies.back().filename = p.path().u8string();	//	we need the filename for writing data

				if (!i)
					input.seekg(0x3C, std::ios::beg);	//	first copy move data offset is always at 0x3C
				else
					input.seekg(0x4C, std::ios::beg);	//	second copy move data offset is always at 0x4C
				input.read(readByte, 4);
				offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));
				input.seekg(offset, std::ios::beg);

				//	don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
				for (size_t j = 0; j < 5; j++) {

					input.read(this->_enemies.back().moves[j].name, 18);
					replaceNulls(this->_enemies.back().moves[j].name, 18);

					this->_enemies.back().moves[j].stats = readRaw<EnemyMoveStatsStruct>(input);

				}

			}

		}

		input.close();

	}

}

void EnemiesClass::draw() {

	ImGui::Begin("ENEMIES");

	ImGui::Text("%s", &this->_filename[0]);

	if (ImGui::BeginCombo("Enemy Index", this->_enemies.at(this->_enemyIndex).name)) {

		for (size_t i = 0; i < this->_enemies.size(); i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_enemyIndex);
			if (ImGui::Selectable(this->_enemies.at(i).name, is_selected))
				this->_enemyIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		for (size_t i = 0; i < 8; i++)
			AilmentBitFlags[i] = this->_enemies.at(this->_enemyIndex).stats.ailmentsBitflag & (1 << i);

		ImGui::EndCombo();

	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		this->write();

	}

	ImGui::InputText("Name", this->_enemies[this->_enemyIndex].name, 19);

	ImGui::InputUByte("Type1", &this->_enemies[this->_enemyIndex].stats.type1);
	ImGui::InputUByte("Type2", &this->_enemies[this->_enemyIndex].stats.type2);

	ImGui::InputShort("Level", &this->_enemies[this->_enemyIndex].stats.level);
	ImGui::InputInt("Health", &this->_enemies[this->_enemyIndex].stats.health);
	ImGui::InputShort("MP", &this->_enemies[this->_enemyIndex].stats.mp);
	ImGui::InputShort("SP", &this->_enemies[this->_enemyIndex].stats.sp);
	ImGui::InputShort("VIT", &this->_enemies[this->_enemyIndex].stats.vit);
	ImGui::InputShort("AGI", &this->_enemies[this->_enemyIndex].stats.agi);
	ImGui::InputShort("SPD", &this->_enemies[this->_enemyIndex].stats.spd);
	ImGui::InputShort("MEN", &this->_enemies[this->_enemyIndex].stats.men);
	ImGui::InputShort("Stamina", &this->_enemies[this->_enemyIndex].stats.stamina);                                    if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_enemies[this->_enemyIndex].stats.ipStun);                     if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	ImGui::InputByte("Still Evasion Rate", &this->_enemies[this->_enemyIndex].stats.evasionStillRate);
	ImGui::InputByte("Moving Evasion Rate", &this->_enemies[this->_enemyIndex].stats.evasionMovingRate);
	ImGui::InputByte("Fire Resist", &this->_enemies[this->_enemyIndex].stats.fireResist);
	ImGui::InputByte("Wind Resist", &this->_enemies[this->_enemyIndex].stats.windResist);
	ImGui::InputByte("Earth Resist", &this->_enemies[this->_enemyIndex].stats.earthResist);
	ImGui::InputByte("Lightning Resist", &this->_enemies[this->_enemyIndex].stats.lightningResist);
	ImGui::InputByte("Blizzard Resist", &this->_enemies[this->_enemyIndex].stats.blizzardResist);

	if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[0] << 0);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[1] << 1);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[2] << 2);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[3] << 3);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");

	if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[4] << 4);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[5] << 5);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[6] << 6);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
	ImGui::SameLine();
	if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
		this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[7] << 7);                          if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");

	ImGui::InputShort("Knockback Resist Rate", &this->_enemies[this->_enemyIndex].stats.knockbackResist);
	ImGui::InputShort("Size", &this->_enemies[this->_enemyIndex].stats.size);
	ImGui::InputByte("No Run", &this->_enemies[this->_enemyIndex].stats.noRunFlag);
	ImGui::InputInt("EXP", &this->_enemies[this->_enemyIndex].stats.exp);
	ImGui::InputInt("Skill Coins", &this->_enemies[this->_enemyIndex].stats.skillCoins);
	ImGui::InputInt("Magic Coins", &this->_enemies[this->_enemyIndex].stats.magicCoins);
	ImGui::InputInt("Gold Coins", &this->_enemies[this->_enemyIndex].stats.goldCoins);

	if (ImGui::BeginCombo("Item #1", this->_items[this->_enemies[this->_enemyIndex].stats.item1].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_enemies[this->_enemyIndex].stats.item1);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_enemies[this->_enemyIndex].stats.item1 = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Item #2", this->_items[this->_enemies[this->_enemyIndex].stats.item2].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_enemies[this->_enemyIndex].stats.item2);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_enemies[this->_enemyIndex].stats.item2 = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::InputByte("Item #1 Chance", &this->_enemies[this->_enemyIndex].stats.item1Chance);
	ImGui::InputByte("Item #2 Chance", &this->_enemies[this->_enemyIndex].stats.item2Chance);

	//ImGui::LabelText("Filename", enemies[enemyID].filename.c_str());      //Used for testing to verify files are correct

	ImGui::Checkbox("Show Moves", &this->_showMoves);	ImGui::SameLine();
	ImGui::Checkbox("Show AI", &this->_showAI);

	ImGui::End();

	if (this->_showMoves) {

		ImGui::Begin("ENEMY MOVES");

		if (ImGui::BeginCombo("Move Index", this->_enemies.at(this->_enemyIndex).moves[this->_moveIndex].name)) {

			for (size_t i = 0; i < 5; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_moveIndex);
				if (ImGui::Selectable(this->_enemies.at(this->_enemyIndex).moves[i].name, is_selected))
					this->_moveIndex = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			for (size_t i = 0; i < 8; i++)
				MoveAilmentBitFlags[i] = this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);

			ImGui::EndCombo();

		}

		ImGui::InputText("Name", this->_enemies[this->_enemyIndex].moves[this->_moveIndex].name, 19);
		ImGui::InputUShort("MP Cost", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.mp);
		ImGui::InputUShort("SP Cost", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.sp);
		ImGui::InputUByte("Unknown #1", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.unknown);

		ImGui::Combo("Target Effect", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.targetEffect, targetEffects, 16);

		ImGui::InputUShort("Strength", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.str);
		ImGui::InputUShort("Power", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.pow);
		ImGui::InputUShort("Damage(?)", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ad);

		ImGui::Combo("Target Type", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.targetType, targetTypes, 16);

		ImGui::InputUByte("Unknown #2", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.unknown1);
		ImGui::InputUShort("Distance", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.distance);             if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		ImGui::InputUShort("Accuracy", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.accuracy);
		ImGui::InputUShort("Range", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.range);                   if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		ImGui::InputUShort("Cast Time", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.castTime);
		ImGui::InputUShort("Recovery", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.recovery);

		if (ImGui::BeginCombo("Animation", this->_moves[this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.animation].name)) {

			for (size_t i = 0; i < this->_numMoves; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.animation);
				if (ImGui::Selectable(this->_moves[i].name, is_selected))
					this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.animation = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			for (size_t i = 0; i < 8; i++)
				MoveAilmentBitFlags[i] = this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);

			ImGui::EndCombo();

		}

		ImGui::InputUByte("Knockdown", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.knockDown);            if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");

		ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ipStun);
		ImGui::InputShort("Knockback", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.knockback);            if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

		ImGui::Combo("Element", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.element, elements, 5);
		ImGui::InputUByte("Element Strength", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.elementStr);    if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		if (ImGui::Checkbox("Poison", &MoveAilmentBitFlags[0]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &MoveAilmentBitFlags[1]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &MoveAilmentBitFlags[2]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &MoveAilmentBitFlags[3]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &MoveAilmentBitFlags[4]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &MoveAilmentBitFlags[5]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &MoveAilmentBitFlags[6]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &MoveAilmentBitFlags[7]))
			this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[7] << 7);

		ImGui::InputUByte("Ailments Chance", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsChance);

		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.atkMod);

		ImGui::InputUShort("Special", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.special);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();

	}

	if (this->_showAI) {

		ImGui::Begin("ENEMY AI");

		if (ImGui::BeginCombo("AI Index", slotIDs[this->_aiIndex])) {

			for (size_t i = 0; i < 5; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_aiIndex);
				if (ImGui::Selectable(slotIDs[i], is_selected))
					this->_aiIndex = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			for (size_t i = 0; i < 8; i++)
				MoveAilmentBitFlags[i] = this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);

			ImGui::EndCombo();

		}

		ImGui::InputUByte("AI Type", &this->_enemies[this->_enemyIndex].ai[this->_aiIndex].aiType);
		ImGui::InputUByte("Move #1 Chance", &this->_enemies[this->_enemyIndex].ai[this->_aiIndex].move1Chance);
		ImGui::InputUByte("Move #2 Chance", &this->_enemies[this->_enemyIndex].ai[this->_aiIndex].move2Chance);
		ImGui::InputUByte("Move #3 Chance", &this->_enemies[this->_enemyIndex].ai[this->_aiIndex].move3Chance);
		ImGui::InputUByte("Move #4 Chance", &this->_enemies[this->_enemyIndex].ai[this->_aiIndex].move4Chance);
		ImGui::InputUByte("Move #5 Chance", &this->_enemies[this->_enemyIndex].ai[this->_aiIndex].move5Chance);

		ImGui::End();

	}

}

void EnemiesClass::outputToCSV() {



}