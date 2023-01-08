#include <fstream>
#include <filesystem>
#include <random>

#include "./include/EnemiesClass.h"

#include "./include/common/io_util.h"
#include "./include/common/char_constants.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./imgui.h"

void EnemiesClass::write() {
	std::fstream output;
	char* readByte = new char[4]{};
	uint32_t offset = 0;

	for (size_t i = 0; i < this->_enemies.size(); i++) {
		output.open(this->_enemies.at(i).filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open()) {
			throw new std::exception(this->_enemies.at(i).filename.c_str());
		}

		for (size_t j = 0; j < 2; j++) {
			if (!j) {
				output.seekp(0x34, std::ios::beg); // first copy data offset is always at 0x34
			}
			else {
				output.seekp(0x44, std::ios::beg); // second copy data offset is always at 0x44
			}
			output.read(readByte, 4);
			offset = ((ImU32)((ImU8)(readByte[3])) << 24) + ((ImU32)((ImU8)(readByte[2])) << 16) + ((ImU32)((ImU8)(readByte[1])) << 8) + (ImU32)((ImU8)(readByte[0]));

			if (offset) {
				if (j) {
					i++;
				}

				output.seekg(offset, std::ios::beg);
				output.write((char*)this->_enemies.at(i).name, 18);
				writeRaw<EnemyStatsStruct>(output, this->_enemies.at(i).stats);

				// write AI data
				for (size_t k = 0; k < 5; k++) {
					writeRaw<EnemyAIStruct>(output, this->_enemies.at(i).ai[k]);
				}

				if (!j) {
					output.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
				}
				else {
					output.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
				}
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
	uint32_t offset = 0;

	for (const auto& p : std::filesystem::directory_iterator(filename + "enemy")) {
		std::string actualFilename = p.path().string();

		if (!std::strstr(actualFilename.c_str(), "_0.dat")) {
			continue;
		}

		input.open(actualFilename, std::ios::binary);

		if (!input.is_open()) {
			throw new std::exception(actualFilename.c_str());
		}

		for (size_t i = 0; i < 2; i++) {
			if (!i) {
				input.seekg(0x34, std::ios::beg); // first copy data offset is always at 0x34
			}
			else {
				input.seekg(0x44, std::ios::beg); // second copy data offset is always at 0x44
			}
			input.read(readByte, 4);
			offset = ((uint32_t)((uint8_t)(readByte[3])) << 24) + ((uint32_t)((uint8_t)(readByte[2])) << 16) + ((uint32_t)((uint8_t)(readByte[1])) << 8) + (uint32_t)((uint8_t)(readByte[0]));

			if (offset) {
				this->_enemies.emplace_back(EnemyStruct());
				input.seekg(offset, std::ios::beg);
				input.read(this->_enemies.back().name, 18);
				replaceNulls(this->_enemies.back().name, 18);
				this->_enemies.back().stats = readRaw<EnemyStatsStruct>(input);

				// read in enemy AI data
				for (size_t j = 0; j < 5; j++) {
					this->_enemies.back().ai[j] = readRaw<EnemyAIStruct>(input);
				}

				this->_enemies.back().filename = actualFilename;

				if (!i) {
					input.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
				}
				else {
					input.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
				}
				input.read(readByte, 4);
				offset = ((uint32_t)((uint8_t)(readByte[3])) << 24) + ((uint32_t)((uint8_t)(readByte[2])) << 16) + ((uint32_t)((uint8_t)(readByte[1])) << 8) + (uint32_t)((uint8_t)(readByte[0]));
				input.seekg(offset, std::ios::beg);

				// don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
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
		std::string actualFilename = p.path().string();

		if (!std::strstr(actualFilename.c_str(), "_0.dat")) {
			continue;
		}

		input.open(p.path().u8string(), std::ios::binary);

		if (!input.is_open()) {
			throw new std::exception(actualFilename.c_str());
		}

		for (size_t i = 0; i < 2; i++) {
			if (!i) {
				input.seekg(0x34, std::ios::beg); // first copy data offset is always at 0x34
			}
			else {
				input.seekg(0x44, std::ios::beg); // second copy data offset is always at 0x44
			}
			input.read(readByte, 4);
			offset = ((uint32_t)((uint8_t)(readByte[3])) << 24) + ((uint32_t)((uint8_t)(readByte[2])) << 16) + ((uint32_t)((uint8_t)(readByte[1])) << 8) + (uint32_t)((uint8_t)(readByte[0]));

			if (offset) {
				this->_enemies.emplace_back(EnemyStruct());
				input.seekg(offset, std::ios::beg);
				input.read(this->_enemies.back().name, 18);
				replaceNulls(this->_enemies.back().name, 18);
				this->_enemies.back().stats = readRaw<EnemyStatsStruct>(input);
				this->_enemies.back().filename = actualFilename; // we need the filename for writing data

				if (!i) {
					input.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
				}
				else {
					input.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
				}
				input.read(readByte, 4);
				offset = ((uint32_t)((uint8_t)(readByte[3])) << 24) + ((uint32_t)((uint8_t)(readByte[2])) << 16) + ((uint32_t)((uint8_t)(readByte[1])) << 8) + (uint32_t)((uint8_t)(readByte[0]));
				input.seekg(offset, std::ios::beg);

				// don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
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

	if (ImGui::BeginCombo("Enemy Index", this->_enemies.at(this->_enemyIndex).name)) {
		for (size_t i = 0; i < this->_enemies.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_enemyIndex);
			if (ImGui::Selectable(this->_enemies.at(i).name, is_selected)) {
				this->_enemyIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = this->_enemies.at(this->_enemyIndex).stats.ailmentsBitflag & (1 << i);
		}

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
	ImGui::InputShort("Stamina", &this->_enemies[this->_enemyIndex].stats.stamina);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_enemies[this->_enemyIndex].stats.ipStun);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	ImGui::InputByte("Still Evasion Rate", &this->_enemies[this->_enemyIndex].stats.evasionStillRate);
	ImGui::InputByte("Moving Evasion Rate", &this->_enemies[this->_enemyIndex].stats.evasionMovingRate);
	ImGui::InputByte("Fire Resist", &this->_enemies[this->_enemyIndex].stats.fireResist);
	ImGui::InputByte("Wind Resist", &this->_enemies[this->_enemyIndex].stats.windResist);
	ImGui::InputByte("Earth Resist", &this->_enemies[this->_enemyIndex].stats.earthResist);
	ImGui::InputByte("Lightning Resist", &this->_enemies[this->_enemyIndex].stats.lightningResist);
	ImGui::InputByte("Blizzard Resist", &this->_enemies[this->_enemyIndex].stats.blizzardResist);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statuses[i], &AilmentBitFlags[0])) {
			this->_enemies[this->_enemyIndex].stats.ailmentsBitflag ^= (AilmentBitFlags[i] << i);
		}
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	ImGui::InputShort("Knockback Resist Rate", &this->_enemies[this->_enemyIndex].stats.knockbackResist);
	ImGui::InputShort("Size", &this->_enemies[this->_enemyIndex].stats.size);
	ImGui::InputByte("No Run", &this->_enemies[this->_enemyIndex].stats.noRunFlag);
	ImGui::InputInt("EXP", &this->_enemies[this->_enemyIndex].stats.exp);
	ImGui::InputInt("Skill Coins", &this->_enemies[this->_enemyIndex].stats.skillCoins);
	ImGui::InputInt("Magic Coins", &this->_enemies[this->_enemyIndex].stats.magicCoins);
	ImGui::InputInt("Gold Coins", &this->_enemies[this->_enemyIndex].stats.goldCoins);

	if (ImGui::BeginCombo("Item #1", this->_items->at(this->_enemies[this->_enemyIndex].stats.item1).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_enemies[this->_enemyIndex].stats.item1);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected))
				this->_enemies[this->_enemyIndex].stats.item1 = (uint16_t)i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Item #2", this->_items->at(this->_enemies[this->_enemyIndex].stats.item2).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_enemies[this->_enemyIndex].stats.item2);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_enemies[this->_enemyIndex].stats.item2 = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputByte("Item #1 Chance", &this->_enemies[this->_enemyIndex].stats.item1Chance);
	ImGui::InputByte("Item #2 Chance", &this->_enemies[this->_enemyIndex].stats.item2Chance);
	ImGui::Checkbox("Show Moves", &this->_showMoves);	ImGui::SameLine();
	ImGui::Checkbox("Show AI", &this->_showAI);
	ImGui::End();

	if (this->_showMoves) {
		ImGui::Begin("ENEMY MOVES");
		if (ImGui::BeginCombo("Move Index", this->_enemies.at(this->_enemyIndex).moves[this->_moveIndex].name)) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_moveIndex);
				if (ImGui::Selectable(this->_enemies.at(this->_enemyIndex).moves[i].name, is_selected)) {
					this->_moveIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

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
		ImGui::InputUByte("Unknown #2", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.normalAttackFlag);
		ImGui::InputUShort("Distance", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.distance);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		ImGui::InputUShort("Accuracy", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.accuracy);
		ImGui::InputUShort("Range", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.range);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		ImGui::InputUShort("Cast Time", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.castTime);
		ImGui::InputUShort("Recovery", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.recovery);

		if (ImGui::BeginCombo("Animation", animationIDs[this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.animation])) {
			for (size_t i = 0; i < this->_moves->size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.animation);
				if (ImGui::Selectable(animationIDs[i], is_selected)) {
					this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.animation = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		ImGui::InputUByte("Knockdown", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.knockDown);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
		ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ipStun);
		ImGui::InputShort("Knockback", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.knockback);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");
		ImGui::Combo("Element", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.element, elements, 5);
		ImGui::InputUByte("Element Strength", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.elementStr);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statuses[i], &AilmentBitFlags[0])) {
				this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[i] << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		ImGui::InputUByte("Ailments Chance", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsChance);
		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.atkMod);
		ImGui::InputUShort("Special", &this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.special);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();
	}

	if (this->_showAI) {
		ImGui::Begin("ENEMY AI");
		if (ImGui::BeginCombo("AI Index", slotIDs[this->_aiIndex])) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_aiIndex);
				if (ImGui::Selectable(slotIDs[i], is_selected)) {
					this->_aiIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = this->_enemies[this->_enemyIndex].moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

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
	std::ofstream output;
	output.open("./csv/ENEMIES.CSV");

	std::ofstream output2;
	output2.open("./csv/ENEMY_MOVES.CSV");

	if (!output.is_open() || !output2.is_open()) {
		return;
	}

	output << "Name,Type 1,Type 2,Level,Health,MP,SP,VIT,AGI,SPD,MEN,Stamina,IP Stun,IP Cancel Stun,Evasion Still %,Evasion Moving %,Fire Resist %,Wind Resist %,Earth Resist %,Lightning Resist %,Blizzard Resist %,"
		<< "Status Effect Resist Bitflag,Knockback Resist,Status Recovery Time,T_DMG,T_HEAL,Size,No Run,EXP,Skill Coins,Magic Coins,Gold Coins,Item 1,Item 2,Item 1 Chance,Item 2 Chance\n";

	output2 << "Enemy,Move,MP,SP,???,Target Effect,STR,POW,AD,Target Type,???,Distance,Accuracy,Range,Cast Time,Recovery,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback,Element,Element Strength,Status Effect Bitflag,"
		<< "Status Effect Chance,ATK Change,DEF Change,ACT Change,MOV Change,Special\n";

	for (const auto& val : this->_enemies) {
		output << val.name << ','
			<< std::to_string(val.stats.type1) << ','
			<< std::to_string(val.stats.type2) << ','
			<< std::to_string(val.stats.level) << ','
			<< std::to_string(val.stats.health) << ','
			<< std::to_string(val.stats.mp) << ','
			<< std::to_string(val.stats.sp) << ','
			<< std::to_string(val.stats.vit) << ','
			<< std::to_string(val.stats.agi) << ','
			<< std::to_string(val.stats.spd) << ','
			<< std::to_string(val.stats.men) << ','
			<< std::to_string(val.stats.stamina) << ','
			<< std::to_string(val.stats.ipStun) << ','
			<< std::to_string(val.stats.ipCancelStun) << ','
			<< std::to_string(val.stats.evasionStillRate) << ','
			<< std::to_string(val.stats.evasionMovingRate) << ','
			<< std::to_string(val.stats.fireResist) << ','
			<< std::to_string(val.stats.windResist) << ','
			<< std::to_string(val.stats.earthResist) << ','
			<< std::to_string(val.stats.lightningResist) << ','
			<< std::to_string(val.stats.blizzardResist) << ','
			<< std::to_string(val.stats.ailmentsBitflag) << ','
			<< std::to_string(val.stats.knockbackResist) << ','
			<< std::to_string(val.stats.T_REC) << ','
			<< std::to_string(val.stats.T_DMG) << ','
			<< std::to_string(val.stats.T_HEAL) << ','
			<< std::to_string(val.stats.size) << ','
			<< std::to_string(val.stats.noRunFlag) << ','
			<< std::to_string(val.stats.exp) << ','
			<< std::to_string(val.stats.skillCoins) << ','
			<< std::to_string(val.stats.magicCoins) << ','
			<< std::to_string(val.stats.goldCoins) << ','
			<< this->_items->at(val.stats.item1).name << ','
			<< this->_items->at(val.stats.item2).name << ','
			<< std::to_string(val.stats.item1Chance) << ','
			<< std::to_string(val.stats.item2Chance) << '\n';

		for (size_t i = 0; i < 5; i++) {
			if (std::string(val.moves[i].name).find_first_not_of(' ') == std::string::npos) {
				continue;
			}

			output2 << val.name << ','
				<< val.moves[i].name << ','
				<< std::to_string(val.moves[i].stats.mp) << ','
				<< std::to_string(val.moves[i].stats.sp) << ','
				<< std::to_string(val.moves[i].stats.unknown) << ','
				<< targetEffects[val.moves[i].stats.targetEffect] << ','
				<< std::to_string(val.moves[i].stats.str) << ','
				<< std::to_string(val.moves[i].stats.pow) << ','
				<< std::to_string(val.moves[i].stats.ad) << ','
				<< targetTypes[val.moves[i].stats.targetType] << ','
				<< std::to_string(val.moves[i].stats.normalAttackFlag) << ','
				<< std::to_string(val.moves[i].stats.distance) << ','
				<< std::to_string(val.moves[i].stats.accuracy) << ','
				<< std::to_string(val.moves[i].stats.range) << ','
				<< std::to_string(val.moves[i].stats.castTime) << ','
				<< std::to_string(val.moves[i].stats.recovery) << ','
				<< std::to_string(val.moves[i].stats.animation) << ','
				<< std::to_string(val.moves[i].stats.knockDown) << ','
				<< std::to_string(val.moves[i].stats.ipStun) << ','
				<< std::to_string(val.moves[i].stats.ipCancelStun) << ','
				<< std::to_string(val.moves[i].stats.knockback) << ','
				<< elements[val.moves[i].stats.element] << ','
				<< std::to_string(val.moves[i].stats.elementStr) << ','
				<< std::to_string(val.moves[i].stats.ailmentsBitflag) << ','
				<< std::to_string(val.moves[i].stats.ailmentsChance) << ','
				<< std::to_string(val.moves[i].stats.atkMod) << ','
				<< std::to_string(val.moves[i].stats.defMod) << ','
				<< std::to_string(val.moves[i].stats.actMod) << ','
				<< std::to_string(val.moves[i].stats.movMod) << ','
				<< std::to_string(val.moves[i].stats.special) << '\n';
		}
	}

	output.close();
	output2.close();
}

void EnemiesClass::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& enemy : this->_enemies) {
		// keep going until an item is hit
		do {
			enemy.stats.item1 = (uint16_t)((size_t)g() % this->_items->size());
		} while (std::string(this->_items->at(enemy.stats.item1).name).find_first_not_of(' ') == std::string::npos);

		// random chance for the item to be removed
		if (g() % 10 == 0)
			enemy.stats.item1 = 0;

		do {
			enemy.stats.item2 = (uint16_t)((size_t)g() % this->_items->size());
		} while (std::string(this->_items->at(enemy.stats.item2).name).find_first_not_of(' ') == std::string::npos);

		// random chance for the item to be removed
		if (g() % 10 == 0) {
			enemy.stats.item2 = 0;
		}

		// keep getting numbers until the total is less than 100%
		do {
			enemy.stats.item1Chance = (enemy.stats.item1) ? g() % 101 : 0;
			enemy.stats.item2Chance = (enemy.stats.item2) ? g() % 101 : 0;
		} while ((size_t)enemy.stats.item1Chance + (size_t)enemy.stats.item2Chance > 100);
	}
}