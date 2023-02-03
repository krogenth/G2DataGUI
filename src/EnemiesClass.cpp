#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./include/EnemiesClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./include/common/imgui_wrappers.h"
#include "./imgui.h"

void Enemies::writeEnemy(std::fstream& stream, const EnemyStruct& enemy, bool isSecond) {
	stream.write((char*)enemy.name, 18);
	writeRaw<EnemyStatsStruct>(stream, enemy.stats);

	// write AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		writeRaw<EnemyAIStruct>(stream, enemy.ai[aiIndex]);
	}

	if (isSecond) {
		stream.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
	}
	else {
		stream.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
	}

	uint32_t offset = readRaw<uint32_t>(stream);
	stream.seekg(offset, std::ios::beg);

	for (size_t k = 0; k < 5; k++) {
		stream.write((char*)enemy.moveSet.moves[k].name, 18);
		writeRaw<EnemyMoveStatsStruct>(stream, enemy.moveSet.moves[k].stats);
	}
}

void Enemies::write() {
	std::fstream stream;

	for (const auto& enemy : this->_enemies) {
		stream.open(enemy.filename, std::ios::binary | std::ios::in | std::ios::out);
		if (!stream.is_open()) {
			throw new std::exception(enemy.filename.c_str());
		}

		if (enemy.isSecond) {
			stream.seekp(0x44, std::ios::beg); // second copy data offset is always at 0x44
		} else {
			stream.seekp(0x34, std::ios::beg); // first copy data offset is always at 0x34
		}

		uint32_t offset = readRaw<uint32_t>(stream);
		if (offset) {
			stream.seekg(offset, std::ios::beg);
			this->writeEnemy(stream, enemy, enemy.isSecond);
		}

		stream.close();
	}
}

void Enemies::readEnemy(std::ifstream& stream, std::string& filename, bool isSecond) {
	this->_enemies.emplace_back(EnemyStruct());
	stream.read(this->_enemies.back().name, 18);
	replaceNulls(this->_enemies.back().name, 18);
	this->_enemies.back().stats = readRaw<EnemyStatsStruct>(stream);
	this->_enemies.back().filename = filename;

	// read in enemy AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		this->_enemies.back().ai[aiIndex] = readRaw<EnemyAIStruct>(stream);
	}

	if (isSecond) {
		stream.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
	} else {
		stream.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
	}

	uint32_t moveOffset = readRaw<uint32_t>(stream);
	stream.seekg(moveOffset, std::ios::beg);

	// don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
	for (size_t moveIndex = 0; moveIndex < 5; moveIndex++) {
		stream.read(this->_enemies.back().moveSet.moves[moveIndex].name, 18);
		replaceNulls(this->_enemies.back().moveSet.moves[moveIndex].name, 18);
		this->_enemies.back().moveSet.moves[moveIndex].stats = readRaw<EnemyMoveStatsStruct>(stream);
	}
}

void Enemies::read(bool isHardmode) {
	this->isHardmode = isHardmode;
	std::ifstream stream;
	uint32_t offset = 0;
	std::string directory = this->_directory + "enemy" + (isHardmode ? "_hardmode" : "");

	// clear out any existing data, we are re-reading data
	this->_enemies.clear();

	for (const auto& p : std::filesystem::directory_iterator(directory)) {
		std::string filename = p.path().string();
		if (!std::strstr(filename.c_str(), "_0.dat")) {
			continue;
		}

		stream.open(filename, std::ios::binary);
		if (!stream.is_open()) {
			throw new std::exception(filename.c_str());
		}

		stream.seekg(0x34, std::ios::beg); // first copy data offset is always at 0x34
		offset = readRaw<int32_t>(stream);
		if (offset > 0) {
			stream.seekg(offset, std::ios::beg);
			this->readEnemy(stream, filename);
		}

		stream.seekg(0x44, std::ios::beg); // second copy data offset is always at 0x44
		offset = readRaw<int32_t>(stream);
		if (offset > 0) {
			stream.seekg(offset, std::ios::beg);
			this->readEnemy(stream, filename, true);
		}

		stream.close();
	}

	// set default starting state
	if (this->_enemies.size()) {
		for (size_t i = 0; i < 8; i++) {
			this->AilmentBitFlags[i] = (this->_enemies.at(0).stats.ailmentsBitflag & (1 << i)) > 0;
			this->MoveAilmentBitFlags[i] = (this->_enemies.at(0).moveSet.moves[0].stats.ailmentsBitflag & (1 << i)) > 0;
		}
	}
}

void Enemies::draw() {
	ImGui::Begin("ENEMIES");

	auto statusDefs = JsonDefinitions::getInstance().getDefinitions("statuses");
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto animationDefs = JsonDefinitions::getInstance().getDefinitions("animations");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");

	if (ImGui::Button("Save")) {
		this->write();
	}

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

		this->_moveIndex = 0;

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = this->_enemies.at(this->_enemyIndex).stats.ailmentsBitflag & (1 << i);
			MoveAilmentBitFlags[i] = this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", this->_enemies.at(this->_enemyIndex).name, 19);
	drawInput("Type1", &this->_enemies.at(this->_enemyIndex).stats.type1);
	drawInput("Type2", &this->_enemies.at(this->_enemyIndex).stats.type2);
	drawInput("Level", &this->_enemies.at(this->_enemyIndex).stats.level);
	drawInput("Health", &this->_enemies.at(this->_enemyIndex).stats.health);
	drawInput("MP", &this->_enemies.at(this->_enemyIndex).stats.mp);
	drawInput("SP", &this->_enemies.at(this->_enemyIndex).stats.sp);
	drawInput("VIT", &this->_enemies.at(this->_enemyIndex).stats.vit);
	drawInput("AGI", &this->_enemies.at(this->_enemyIndex).stats.agi);
	drawInput("SPD", &this->_enemies.at(this->_enemyIndex).stats.spd);
	drawInput("MEN", &this->_enemies.at(this->_enemyIndex).stats.men);
	drawInput("Stamina", &this->_enemies.at(this->_enemyIndex).stats.stamina);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	drawInputN("IP Stun/IP Cancel Stun", &this->_enemies.at(this->_enemyIndex).stats.ipStun, 2);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	drawInput("Still Evasion Rate", &this->_enemies.at(this->_enemyIndex).stats.evasionStillRate);
	drawInput("Moving Evasion Rate", &this->_enemies.at(this->_enemyIndex).stats.evasionMovingRate);
	drawInput("Fire Resist", &this->_enemies.at(this->_enemyIndex).stats.fireResist);
	drawInput("Wind Resist", &this->_enemies.at(this->_enemyIndex).stats.windResist);
	drawInput("Earth Resist", &this->_enemies.at(this->_enemyIndex).stats.earthResist);
	drawInput("Lightning Resist", &this->_enemies.at(this->_enemyIndex).stats.lightningResist);
	drawInput("Blizzard Resist", &this->_enemies.at(this->_enemyIndex).stats.blizzardResist);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statusDefs.at(i).c_str(), &AilmentBitFlags[i])) {
			this->_enemies.at(this->_enemyIndex).stats.ailmentsBitflag ^= (1 << i);
		}
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	drawInput("Knockback Resist Rate", &this->_enemies.at(this->_enemyIndex).stats.knockbackResist);
	drawInput("Size", &this->_enemies.at(this->_enemyIndex).stats.size);
	drawInput("No Run", &this->_enemies.at(this->_enemyIndex).stats.noRunFlag);
	drawInput("EXP", &this->_enemies.at(this->_enemyIndex).stats.exp);
	drawInput("Skill Coins", &this->_enemies.at(this->_enemyIndex).stats.skillCoins);
	drawInput("Magic Coins", &this->_enemies.at(this->_enemyIndex).stats.magicCoins);
	drawInput("Gold Coins", &this->_enemies.at(this->_enemyIndex).stats.goldCoins);

	if (ImGui::BeginCombo("Item #1", this->_items->at(this->_enemies.at(this->_enemyIndex).stats.item1).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_enemies.at(this->_enemyIndex).stats.item1);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected))
				this->_enemies.at(this->_enemyIndex).stats.item1 = (uint16_t)i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Item #2", this->_items->at(this->_enemies.at(this->_enemyIndex).stats.item2).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_enemies.at(this->_enemyIndex).stats.item2);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_enemies.at(this->_enemyIndex).stats.item2 = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Item #1 Chance", &this->_enemies.at(this->_enemyIndex).stats.item1Chance);
	drawInput("Item #2 Chance", &this->_enemies.at(this->_enemyIndex).stats.item2Chance);

	ImGui::Checkbox("Moves", &this->_showMoves);
	ImGui::SameLine();
	ImGui::Checkbox("AI", &this->_showAI);

	ImGui::End();

	if (this->_showMoves) {
		ImGui::Begin("ENEMY MOVES");
		if (ImGui::BeginCombo("Move Index", this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].name)) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_moveIndex);
				if (ImGui::Selectable(this->_enemies.at(this->_enemyIndex).moveSet.moves[i].name, is_selected)) {
					this->_moveIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

			ImGui::EndCombo();
		}

		ImGui::InputText("Name", this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].name, 19);
		drawInput("MP Cost", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.mp);
		drawInput("SP Cost", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.sp);
		drawInput("Unknown #1", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.unknown);
		
		if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.targetEffect).c_str())) {
			for (size_t i = 0; i < targetEffectDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.targetEffect);
				if (ImGui::Selectable(targetEffectDefs.at(i).c_str())) {
					this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.targetEffect = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}
			ImGui::EndCombo();
		}
		
		drawInput("Strength", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.str);
		drawInput("Power", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.pow);
		drawInput("Damage(?)", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.ad);
		
		if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.targetType).c_str())) {
			for (size_t i = 0; i < targetTypeDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.targetType);
				if (ImGui::Selectable(targetTypeDefs.at(i).c_str())) {
					this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.targetType = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}
			ImGui::EndCombo();
		}
		
		drawInput("Unknown #2", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.normalAttackFlag);
		drawInput("Distance", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.distance);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		drawInput("Accuracy", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.accuracy);
		drawInput("Range", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.range);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		drawInput("Cast Time", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.castTime);
		drawInput("Recovery", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.recovery);

		if (ImGui::BeginCombo("Animation", animationDefs.at(this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.animation).c_str())) {
			for (size_t i = 0; i < this->_moves->size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.animation);
				if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
					this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.animation = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Knockdown", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.knockDown);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
		drawInput("IP Stun/IP Cancel Stun", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.ipStun);
		drawInput("Knockback", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.knockback);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");
		
		if (ImGui::BeginCombo("Element", elementDefs.at(this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.element).c_str())) {
			for (size_t i = 0; i < elementDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.element);
				if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
					this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.element = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}
		
		drawInput("Element Strength", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.elementStr);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statusDefs.at(i).c_str(), &MoveAilmentBitFlags[i])) {
				this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.ailmentsBitflag ^= (1 << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInput("Ailments Chance", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.ailmentsChance);
		drawInputN("Atk/Def/Act/Mov Mods", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.atkMod, 4);
		drawInput("Special", &this->_enemies.at(this->_enemyIndex).moveSet.moves[this->_moveIndex].stats.special);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();
	}

	if (this->_showAI) {
		ImGui::Begin("ENEMY AI");
		if (ImGui::BeginCombo("AI Index", std::format("AI Index {}", this->_aiIndex + 1).c_str())) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_aiIndex);
				if (ImGui::Selectable(std::format("AI Index {}", i + 1).c_str(), is_selected)) {
					this->_aiIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("AI Type", &this->_enemies.at(this->_enemyIndex).ai[this->_aiIndex].aiType);
		drawInput("Move #1 Chance", &this->_enemies.at(this->_enemyIndex).ai[this->_aiIndex].move1Chance);
		drawInput("Move #2 Chance", &this->_enemies.at(this->_enemyIndex).ai[this->_aiIndex].move2Chance);
		drawInput("Move #3 Chance", &this->_enemies.at(this->_enemyIndex).ai[this->_aiIndex].move3Chance);
		drawInput("Move #4 Chance", &this->_enemies.at(this->_enemyIndex).ai[this->_aiIndex].move4Chance);
		drawInput("Move #5 Chance", &this->_enemies.at(this->_enemyIndex).ai[this->_aiIndex].move5Chance);
		
		ImGui::End();
	}
}

void Enemies::outputToCSV() {
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");
	std::ofstream stream;
	stream.open(this->isHardmode ? "./csv/ENEMIES_HARDMODE.CSV" : "./csv/ENEMIES.CSV");

	std::ofstream output2;
	output2.open(this->isHardmode ? "./csv/ENEMY_HARDMODE_MOVES.CSV" : "./csv/ENEMY_MOVES.CSV");

	if (!stream.is_open() || !output2.is_open()) {
		return;
	}

	stream << "Name,Type 1,Type 2,Level,Health,MP,SP,VIT,AGI,SPD,MEN,Stamina,IP Stun,IP Cancel Stun,Evasion Still %,Evasion Moving %,Fire Resist %,Wind Resist %,Earth Resist %,Lightning Resist %,Blizzard Resist %,"
		<< "Status Effect Resist Bitflag,Knockback Resist,Status Recovery Time,T_DMG,T_HEAL,Size,No Run,EXP,Skill Coins,Magic Coins,Gold Coins,Item 1,Item 2,Item 1 Chance,Item 2 Chance\n";

	output2 << "Enemy,Move,MP,SP,???,Target Effect,STR,POW,AD,Target Type,???,Distance,Accuracy,Range,Cast Time,Recovery,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback,Element,Element Strength,Status Effect Bitflag,"
		<< "Status Effect Chance,ATK Change,DEF Change,ACT Change,MOV Change,Special\n";

	for (const auto& val : this->_enemies) {
		stream << val.name << ','
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
			if (std::string(val.moveSet.moves[i].name).find_first_not_of(' ') == std::string::npos) {
				continue;
			}

			output2 << val.name << ','
				<< val.moveSet.moves[i].name << ','
				<< std::to_string(val.moveSet.moves[i].stats.mp) << ','
				<< std::to_string(val.moveSet.moves[i].stats.sp) << ','
				<< std::to_string(val.moveSet.moves[i].stats.unknown) << ','
				<< targetEffectDefs.at(val.moveSet.moves[i].stats.targetEffect) << ','
				<< std::to_string(val.moveSet.moves[i].stats.str) << ','
				<< std::to_string(val.moveSet.moves[i].stats.pow) << ','
				<< std::to_string(val.moveSet.moves[i].stats.ad) << ','
				<< targetTypeDefs.at(val.moveSet.moves[i].stats.targetType) << ','
				<< std::to_string(val.moveSet.moves[i].stats.normalAttackFlag) << ','
				<< std::to_string(val.moveSet.moves[i].stats.distance) << ','
				<< std::to_string(val.moveSet.moves[i].stats.accuracy) << ','
				<< std::to_string(val.moveSet.moves[i].stats.range) << ','
				<< std::to_string(val.moveSet.moves[i].stats.castTime) << ','
				<< std::to_string(val.moveSet.moves[i].stats.recovery) << ','
				<< std::to_string(val.moveSet.moves[i].stats.animation) << ','
				<< std::to_string(val.moveSet.moves[i].stats.knockDown) << ','
				<< std::to_string(val.moveSet.moves[i].stats.ipStun) << ','
				<< std::to_string(val.moveSet.moves[i].stats.ipCancelStun) << ','
				<< std::to_string(val.moveSet.moves[i].stats.knockback) << ','
				<< elementDefs.at(val.moveSet.moves[i].stats.element) << ','
				<< std::to_string(val.moveSet.moves[i].stats.elementStr) << ','
				<< std::to_string(val.moveSet.moves[i].stats.ailmentsBitflag) << ','
				<< std::to_string(val.moveSet.moves[i].stats.ailmentsChance) << ','
				<< std::to_string(val.moveSet.moves[i].stats.atkMod) << ','
				<< std::to_string(val.moveSet.moves[i].stats.defMod) << ','
				<< std::to_string(val.moveSet.moves[i].stats.actMod) << ','
				<< std::to_string(val.moveSet.moves[i].stats.movMod) << ','
				<< std::to_string(val.moveSet.moves[i].stats.special) << '\n';
		}
	}

	stream.close();
	output2.close();
}

void Enemies::randomize() {
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