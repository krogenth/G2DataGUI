#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./Enemies.h"
#include "../items/Items.h"
#include "../moves/Moves.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"
#include "../common/difficulty.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void Enemies::writeEnemy(std::fstream& stream, const Enemy& enemy, bool isSecond) {
	stream.write((char*)enemy.name, 18);
	writeRaw<EnemyStats>(stream, enemy.stats);

	// write AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		writeRaw<EnemyAI>(stream, enemy.ai[aiIndex]);
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
		writeRaw<EnemyMoveStats>(stream, enemy.moveSet.moves[k].stats);
	}
}

void Enemies::write() {
	std::fstream stream;

	for (const auto& enemy : _enemies) {
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
			writeEnemy(stream, enemy, enemy.isSecond);
		}

		stream.close();
	}
}

void Enemies::readEnemy(std::ifstream& stream, std::string& filename, bool isSecond) {
	_enemies.emplace_back(Enemy());
	stream.read(_enemies.back().name, 18);
	replaceNulls(_enemies.back().name, 18);
	_enemies.back().stats = readRaw<EnemyStats>(stream);
	_enemies.back().filename = filename;

	// read in enemy AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		_enemies.back().ai[aiIndex] = readRaw<EnemyAI>(stream);
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
		stream.read(_enemies.back().moveSet.moves[moveIndex].name, 18);
		replaceNulls(_enemies.back().moveSet.moves[moveIndex].name, 18);
		_enemies.back().moveSet.moves[moveIndex].stats = readRaw<EnemyMoveStats>(stream);
	}
}

void Enemies::read() {
	std::ifstream stream;
	uint32_t offset = 0;
	std::string directory = _directory + "enemy" + (Difficulty::getInstance().isHardMode() ? "_hardmode" : "");

	// clear out any existing data, we are re-reading data
	_enemies.clear();

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
			readEnemy(stream, filename);
		}

		stream.seekg(0x44, std::ios::beg); // second copy data offset is always at 0x44
		offset = readRaw<int32_t>(stream);
		if (offset > 0) {
			stream.seekg(offset, std::ios::beg);
			readEnemy(stream, filename, true);
		}

		stream.close();
	}

	// set default starting state
	if (_enemies.size()) {
		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = (_enemies.at(0).stats.ailmentsBitflag & (1 << i)) > 0;
			MoveAilmentBitFlags[i] = (_enemies.at(0).moveSet.moves[0].stats.ailmentsBitflag & (1 << i)) > 0;
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
		write();
	}

	if (ImGui::BeginCombo("Enemy Index", _enemies.at(_enemyIndex).name)) {
		for (size_t i = 0; i < _enemies.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _enemyIndex);
			if (ImGui::Selectable(_enemies.at(i).name, is_selected)) {
				_enemyIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		_moveIndex = 0;

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = _enemies.at(_enemyIndex).stats.ailmentsBitflag & (1 << i);
			MoveAilmentBitFlags[i] = _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.ailmentsBitflag & (1 << i);
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", _enemies.at(_enemyIndex).name, 19);
	drawInput("Unknown #1", &_enemies.at(_enemyIndex).stats.unknown1);
	drawInput("Unknown #2", &_enemies.at(_enemyIndex).stats.unknown2);
	drawInput("Type1", &_enemies.at(_enemyIndex).stats.type1);
	drawInput("Type2", &_enemies.at(_enemyIndex).stats.type2);
	drawInput("Level", &_enemies.at(_enemyIndex).stats.level);
	drawInput("Health", &_enemies.at(_enemyIndex).stats.health);
	drawInput("MP", &_enemies.at(_enemyIndex).stats.mp);
	drawInput("SP", &_enemies.at(_enemyIndex).stats.sp);
	drawInput("VIT", &_enemies.at(_enemyIndex).stats.vit);
	drawInput("AGI", &_enemies.at(_enemyIndex).stats.agi);
	drawInput("SPD", &_enemies.at(_enemyIndex).stats.spd);
	drawInput("MEN", &_enemies.at(_enemyIndex).stats.men);
	drawInput("Stamina", &_enemies.at(_enemyIndex).stats.stamina);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	drawInputN("IP Stun/IP Cancel Stun", &_enemies.at(_enemyIndex).stats.ipStun, 2);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	drawInput("Still Evasion Rate", &_enemies.at(_enemyIndex).stats.evasionStillRate);
	drawInput("Moving Evasion Rate", &_enemies.at(_enemyIndex).stats.evasionMovingRate);
	drawInput("Fire Resist", &_enemies.at(_enemyIndex).stats.fireResist);
	drawInput("Wind Resist", &_enemies.at(_enemyIndex).stats.windResist);
	drawInput("Earth Resist", &_enemies.at(_enemyIndex).stats.earthResist);
	drawInput("Lightning Resist", &_enemies.at(_enemyIndex).stats.lightningResist);
	drawInput("Blizzard Resist", &_enemies.at(_enemyIndex).stats.blizzardResist);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statusDefs.at(i).c_str(), &AilmentBitFlags[i])) {
			_enemies.at(_enemyIndex).stats.ailmentsBitflag ^= (1 << i);
		}
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	drawInput("Knockback Resist Rate", &_enemies.at(_enemyIndex).stats.knockbackResist);
	drawInput("T_REC", &_enemies.at(_enemyIndex).stats.T_REC);
	drawInput("T_DMG", &_enemies.at(_enemyIndex).stats.T_DMG);
	drawInput("Unknown #3", &_enemies.at(_enemyIndex).stats.unknown3);
	drawInput("T_HEAL", &_enemies.at(_enemyIndex).stats.T_HEAL);
	drawInput("Size", &_enemies.at(_enemyIndex).stats.size);
	drawInput("Unknwon #4", &_enemies.at(_enemyIndex).stats.unknown4);
	drawInput("Unknown #5", &_enemies.at(_enemyIndex).stats.unknown5);
	drawInput("No Run", &_enemies.at(_enemyIndex).stats.noRunFlag);
	drawInput("Unknown #6", &_enemies.at(_enemyIndex).stats.unknown6);
	drawInput("EXP", &_enemies.at(_enemyIndex).stats.exp);
	drawInput("Skill Coins", &_enemies.at(_enemyIndex).stats.skillCoins);
	drawInput("Magic Coins", &_enemies.at(_enemyIndex).stats.magicCoins);
	drawInput("Gold Coins", &_enemies.at(_enemyIndex).stats.goldCoins);

	if (ImGui::BeginCombo("Item #1", Items::getInstance().getItems().at(_enemies.at(_enemyIndex).stats.item1).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _enemies.at(_enemyIndex).stats.item1);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected))
				_enemies.at(_enemyIndex).stats.item1 = (uint16_t)i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Item #2", Items::getInstance().getItems().at(_enemies.at(_enemyIndex).stats.item2).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _enemies.at(_enemyIndex).stats.item2);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_enemies.at(_enemyIndex).stats.item2 = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Item #1 Chance", &_enemies.at(_enemyIndex).stats.item1Chance);
	drawInput("Item #2 Chance", &_enemies.at(_enemyIndex).stats.item2Chance);
	drawInput("Unknown #7", &_enemies.at(_enemyIndex).stats.unknown7);

	ImGui::Checkbox("Moves", &_showMoves);
	ImGui::SameLine();
	ImGui::Checkbox("AI", &_showAI);

	ImGui::End();

	if (_showMoves) {
		ImGui::Begin("ENEMY MOVES");
		if (ImGui::BeginCombo("Move Index", _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].name)) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _moveIndex);
				if (ImGui::Selectable(_enemies.at(_enemyIndex).moveSet.moves[i].name, is_selected)) {
					_moveIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

			ImGui::EndCombo();
		}

		ImGui::InputText("Name", _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].name, 19);
		drawInput("MP Cost", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.mp);
		drawInput("SP Cost", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.sp);
		drawInput("Unknown #1", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.unknown1);
		
		if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.targetEffect).c_str())) {
			for (size_t i = 0; i < targetEffectDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.targetEffect);
				if (ImGui::Selectable(targetEffectDefs.at(i).c_str())) {
					_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.targetEffect = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}
			ImGui::EndCombo();
		}
		
		drawInput("Strength", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.strength);
		drawInput("Power", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.power);
		drawInput("Damage(?)", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.ad);
		
		if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.targetType).c_str())) {
			for (size_t i = 0; i < targetTypeDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.targetType);
				if (ImGui::Selectable(targetTypeDefs.at(i).c_str())) {
					_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.targetType = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}
			ImGui::EndCombo();
		}
		
		drawInput("Is Normal Attack", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.normalAttackFlag);
		drawInput("Distance", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.distance);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		drawInput("Accuracy", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.accuracy);
		drawInput("Range", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.range);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		drawInput("Cast Time", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.castTime);
		drawInput("Recovery", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.recovery);

		if (ImGui::BeginCombo("Animation", animationDefs.at(_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.animation).c_str())) {
			for (size_t i = 0; i < Moves::getInstance().getMoves().size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.animation);
				if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
					_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.animation = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Knockdown", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.knockDown);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
		drawInput("IP Stun/IP Cancel Stun", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.ipStun);
		drawInput("Knockback", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.knockback);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");
		
		if (ImGui::BeginCombo("Element", elementDefs.at(_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.element).c_str())) {
			for (size_t i = 0; i < elementDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.element);
				if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
					_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.element = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}
		
		drawInput("Element Strength", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.elementStrength);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statusDefs.at(i).c_str(), &MoveAilmentBitFlags[i])) {
				_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.ailmentsBitflag ^= (1 << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInput("Ailments Chance", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.ailmentsChance);
		drawInputN("Atk/Def/Act/Mov Mods", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.atkMod, 4);
		drawInput("Special", &_enemies.at(_enemyIndex).moveSet.moves[_moveIndex].stats.special);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();
	}

	if (_showAI) {
		ImGui::Begin("ENEMY AI");
		if (ImGui::BeginCombo("AI Index", std::format("AI Index {}", _aiIndex + 1).c_str())) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _aiIndex);
				if (ImGui::Selectable(std::format("AI Index {}", i + 1).c_str(), is_selected)) {
					_aiIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("AI Type", &_enemies.at(_enemyIndex).ai[_aiIndex].aiType);
		drawInput("Move #1 Chance", &_enemies.at(_enemyIndex).ai[_aiIndex].move1Chance);
		drawInput("Move #2 Chance", &_enemies.at(_enemyIndex).ai[_aiIndex].move2Chance);
		drawInput("Move #3 Chance", &_enemies.at(_enemyIndex).ai[_aiIndex].move3Chance);
		drawInput("Move #4 Chance", &_enemies.at(_enemyIndex).ai[_aiIndex].move4Chance);
		drawInput("Move #5 Chance", &_enemies.at(_enemyIndex).ai[_aiIndex].move5Chance);
		
		ImGui::End();
	}
}

void Enemies::outputToCSV() {
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");
	std::ofstream stream;
	stream.open(Difficulty::getInstance().isHardMode() ? "./csv/ENEMIES_HARDMODE.CSV" : "./csv/ENEMIES.CSV");

	std::ofstream output2;
	output2.open(Difficulty::getInstance().isHardMode() ? "./csv/ENEMY_HARDMODE_MOVES.CSV" : "./csv/ENEMY_MOVES.CSV");

	if (!stream.is_open() || !output2.is_open()) {
		return;
	}

	stream << "Name,Unknown #1,Unknown #2,Type 1,Type 2,Level,Health,MP,SP,VIT,AGI,SPD,MEN,Stamina,IP Stun,IP Cancel Stun,Evasion Still %,Evasion Moving %,Fire Resist %,Wind Resist %,Earth Resist %,Lightning Resist %,Blizzard Resist %,"
		<< "Status Effect Resist Bitflag,Knockback Resist,Status Recovery Time,T_DMG,Unknown #3,T_HEAL,Size,Unknown #4,Unknown #5,No Run,Unknown #6,EXP,Skill Coins,Magic Coins,Gold Coins,Item 1,Item 2,Item 1 Chance,Item 2 Chance,Unknown #7\n";

	output2 << "Enemy,Move,MP,SP,???,Target Effect,STR,POW,AD,Target Type,???,Distance,Accuracy,Range,Cast Time,Recovery,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback,Element,Element Strength,Status Effect Bitflag,"
		<< "Status Effect Chance,ATK Change,DEF Change,ACT Change,MOV Change,Special\n";

	for (const auto& val : _enemies) {
		stream << val.name << ','
			<< std::to_string(val.stats.unknown1) << ','
			<< std::to_string(val.stats.unknown2) << ','
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
			<< std::to_string(val.stats.unknown3) << ','
			<< std::to_string(val.stats.T_HEAL) << ','
			<< std::to_string(val.stats.size) << ','
			<< std::to_string(val.stats.unknown4) << ','
			<< std::to_string(val.stats.unknown5) << ','
			<< std::to_string(val.stats.noRunFlag) << ','
			<< std::to_string(val.stats.unknown6) << ','
			<< std::to_string(val.stats.exp) << ','
			<< std::to_string(val.stats.skillCoins) << ','
			<< std::to_string(val.stats.magicCoins) << ','
			<< std::to_string(val.stats.goldCoins) << ','
			<< Items::getInstance().getItems().at(val.stats.item1).name << ','
			<< Items::getInstance().getItems().at(val.stats.item2).name << ','
			<< std::to_string(val.stats.item1Chance) << ','
			<< std::to_string(val.stats.item2Chance) << ','
			<< std::to_string(val.stats.unknown7) << '\n';

		for (size_t i = 0; i < 5; i++) {
			if (std::string(val.moveSet.moves[i].name).find_first_not_of(' ') == std::string::npos) {
				continue;
			}

			output2 << val.name << ','
				<< val.moveSet.moves[i].name << ','
				<< std::to_string(val.moveSet.moves[i].stats.mp) << ','
				<< std::to_string(val.moveSet.moves[i].stats.sp) << ','
				<< std::to_string(val.moveSet.moves[i].stats.unknown1) << ','
				<< targetEffectDefs.at(val.moveSet.moves[i].stats.targetEffect) << ','
				<< std::to_string(val.moveSet.moves[i].stats.strength) << ','
				<< std::to_string(val.moveSet.moves[i].stats.power) << ','
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
				<< std::to_string(val.moveSet.moves[i].stats.elementStrength) << ','
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

	for (auto& enemy : _enemies) {
		// keep going until an item is hit
		do {
			enemy.stats.item1 = (uint16_t)((size_t)g() % Items::getInstance().getItems().size());
		} while (std::string(Items::getInstance().getItems().at(enemy.stats.item1).name).find_first_not_of(' ') == std::string::npos);

		// random chance for the item to be removed
		if (g() % 10 == 0)
			enemy.stats.item1 = 0;

		do {
			enemy.stats.item2 = (uint16_t)((size_t)g() % Items::getInstance().getItems().size());
		} while (std::string(Items::getInstance().getItems().at(enemy.stats.item2).name).find_first_not_of(' ') == std::string::npos);

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