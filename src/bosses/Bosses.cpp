#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./Bosses.h"
#include "../items/Items.h"
#include "../moves/Moves.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"
#include "../common/difficulty.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void Bosses::writeBoss(std::fstream& stream, const Boss& boss, bool isSecond) {
	stream.write((char*)boss.name, 18);
	writeRaw<EnemyStats>(stream, boss.stats);

	// write AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		writeRaw<EnemyAI>(stream, boss.ai[aiIndex]);
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
		stream.write((char*)boss.moveSets.front().moves[k].name, 18);
		writeRaw<EnemyMoveStats>(stream, boss.moveSets.front().moves[k].stats);
	}
}

void Bosses::write() {
    std::fstream stream;
	uint32_t offset = 0;

    for (const auto& boss : _bosses) {
        stream.open(boss.filename, std::ios::binary | std::ios::in | std::ios::out);
        if (!stream.is_open()) {
            throw new std::exception(boss.filename.c_str());
        }

        if (boss.isSecond) {
            stream.seekg(0x44, std::ios::beg); // second copy move data offset is always at 0x44
        } else {
            stream.seekg(0x34, std::ios::beg); // first copy move data offset is always at 0x34
        }

        uint32_t offset = readRaw<uint32_t>(stream);
        if (offset) {
            stream.seekg(offset, std::ios::beg);
            writeBoss(stream, boss, boss.isSecond);
        }
		stream.close();
    }
}

void Bosses::readBoss(std::ifstream& stream, std::string& filename, bool isSecond) {
    _bosses.emplace_back(Boss());
    stream.read(_bosses.back().name, 18);
    replaceNulls(_bosses.back().name, 18);
    _bosses.back().stats = readRaw<EnemyStats>(stream);
    _bosses.back().filename = filename; // we need the filename for writing data
	_bosses.back().isSecond = isSecond;

	// read in enemy AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		_bosses.back().ai[aiIndex] = readRaw<EnemyAI>(stream);
	}

    if (isSecond) {
        stream.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
    }
    else {
        stream.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
    }

    uint32_t movesOffset = readRaw<uint32_t>(stream);
    stream.seekg(movesOffset, std::ios::beg);

    // don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
    _bosses.back().moveSets.emplace_back(EnemyMoves());
    for (size_t moveIndex = 0; moveIndex < 5; moveIndex++) {
        stream.read(_bosses.back().moveSets.back().moves[moveIndex].name, 18);
        replaceNulls(_bosses.back().moveSets.back().moves[moveIndex].name, 18);
        _bosses.back().moveSets.back().moves[moveIndex].stats = readRaw<EnemyMoveStats>(stream);
    }
}

void Bosses::read() {
	std::ifstream stream;
	uint32_t offset = 0;
    std::string directory = _directory + "boss" + (Difficulty::getInstance().isHardMode() ? "_hardmode" : "");

    // clear out any existing data, we are re-reading data
    _bosses.clear();

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
			readBoss(stream, filename);
		}

		stream.seekg(0x44, std::ios::beg); // second copy data offset is always at 0x44
		offset = readRaw<int32_t>(stream);
		if (offset > 0) {
			stream.seekg(offset, std::ios::beg);
			readBoss(stream, filename, true);
		}

		stream.close();
    }

	// set default starting state
	if (_bosses.size()) {
		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = (_bosses.at(0).stats.ailmentsBitflag & (1 << i)) > 0;
			MoveAilmentBitFlags[i] = (_bosses.at(0).moveSets.at(0).moves[0].stats.ailmentsBitflag & (1 << i)) > 0;
		}
	}
}

void Bosses::draw() {
	ImGui::Begin("BOSSES");

	auto statusDefs = JsonDefinitions::getInstance().getDefinitions("statuses");
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto animationDefs = JsonDefinitions::getInstance().getDefinitions("animations");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");

	if (ImGui::Button("Save")) {
		write();
	}

	if (ImGui::BeginCombo("Enemy Index", _bosses.at(_bossIndex).name)) {
		for (size_t i = 0; i < _bosses.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _bossIndex);
			if (ImGui::Selectable(_bosses.at(i).name, is_selected)) {
				_bossIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		_moveSetIndex = 0;
		_moveIndex = 0;

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = _bosses.at(_bossIndex).stats.ailmentsBitflag & (1 << i);
			MoveAilmentBitFlags[i] = _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ailmentsBitflag & (1 << i);
		}

		ImGui::EndCombo();
	}

	

	ImGui::InputText("Name", _bosses.at(_bossIndex).name, 19);
	drawInput("Type1", &_bosses.at(_bossIndex).stats.type1);
	drawInput("Type2", &_bosses.at(_bossIndex).stats.type2);
	drawInput("Level", &_bosses.at(_bossIndex).stats.level);
	drawInput("Health", &_bosses.at(_bossIndex).stats.health);
	drawInput("MP", &_bosses.at(_bossIndex).stats.mp);
	drawInput("SP", &_bosses.at(_bossIndex).stats.sp);
	drawInput("VIT", &_bosses.at(_bossIndex).stats.vit);
	drawInput("AGI", &_bosses.at(_bossIndex).stats.agi);
	drawInput("SPD", &_bosses.at(_bossIndex).stats.spd);
	drawInput("MEN", &_bosses.at(_bossIndex).stats.men);
	drawInput("Stamina", &_bosses.at(_bossIndex).stats.stamina);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	drawInputN("IP Stun/IP Cancel Stun", &_bosses.at(_bossIndex).stats.ipStun, 2);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	drawInput("Still Evasion Rate", &_bosses.at(_bossIndex).stats.evasionStillRate);
	drawInput("Moving Evasion Rate", &_bosses.at(_bossIndex).stats.evasionMovingRate);
	drawInput("Fire Resist", &_bosses.at(_bossIndex).stats.fireResist);
	drawInput("Wind Resist", &_bosses.at(_bossIndex).stats.windResist);
	drawInput("Earth Resist", &_bosses.at(_bossIndex).stats.earthResist);
	drawInput("Lightning Resist", &_bosses.at(_bossIndex).stats.lightningResist);
	drawInput("Blizzard Resist", &_bosses.at(_bossIndex).stats.blizzardResist);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statusDefs.at(i).c_str(), &AilmentBitFlags[i])) {
			_bosses.at(_bossIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[i] << i);
		}
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	drawInput("Knockback Resist Rate", &_bosses.at(_bossIndex).stats.knockbackResist);
	drawInput("Size", &_bosses.at(_bossIndex).stats.size);
	drawInput("No Run", &_bosses.at(_bossIndex).stats.noRunFlag);
	drawInput("EXP", &_bosses.at(_bossIndex).stats.exp);
	drawInput("Skill Coins", &_bosses.at(_bossIndex).stats.skillCoins);
	drawInput("Magic Coins", &_bosses.at(_bossIndex).stats.magicCoins);
	drawInput("Gold Coins", &_bosses.at(_bossIndex).stats.goldCoins);

	if (ImGui::BeginCombo("Item #1", Items::getInstance().getItems().at(_bosses.at(_bossIndex).stats.item1).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _bosses.at(_bossIndex).stats.item1);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected))
				_bosses.at(_bossIndex).stats.item1 = (uint16_t)i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Item #2", Items::getInstance().getItems().at(_bosses.at(_bossIndex).stats.item2).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _bosses.at(_bossIndex).stats.item2);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_bosses.at(_bossIndex).stats.item2 = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Item #1 Chance", &_bosses.at(_bossIndex).stats.item1Chance);
	drawInput("Item #2 Chance", &_bosses.at(_bossIndex).stats.item2Chance);

	ImGui::Checkbox("Moves", &_showMoves);
	ImGui::SameLine();
	ImGui::Checkbox("AI", &_showAI);
	
	ImGui::End();

	if (_showMoves) {
		ImGui::Begin("BOSS MOVES");

        if (ImGui::BeginCombo("Moveset Index", std::format("Moveset Index {}", _moveSetIndex + 1).c_str())) {
            for (size_t i = 0; i < _bosses.at(_bossIndex).moveSets.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moveSetIndex);
			if (ImGui::Selectable(std::format("Moveset Index {}", i + 1).c_str(), is_selected)) {
				_moveSetIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
        }

		if (ImGui::BeginCombo("Move Index", _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].name)) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _moveIndex);
				if (ImGui::Selectable(_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[i].name, is_selected)) {
					_moveIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

			ImGui::EndCombo();
		}

		ImGui::InputText("Name", _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].name, 19);
		drawInput("MP Cost", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.mp);
		drawInput("SP Cost", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.sp);
		drawInput("Unknown #1", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.unknown);
		
		if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.targetEffect).c_str())) {
			for (size_t i = 0; i < targetEffectDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.targetEffect);
				if (ImGui::Selectable(targetEffectDefs.at(i).c_str())) {
					_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.targetEffect = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}
			ImGui::EndCombo();
		}
		
		drawInput("Strength", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.strength);
		drawInput("Power", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.power);
		drawInput("Damage(?)", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ad);
		
		if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.targetType).c_str())) {
			for (size_t i = 0; i < targetTypeDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.targetType);
				if (ImGui::Selectable(targetTypeDefs.at(i).c_str())) {
					_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.targetType = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}
			ImGui::EndCombo();
		}
		
		drawInput("Unknown #2", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.normalAttackFlag);
		drawInput("Distance", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.distance);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		drawInput("Accuracy", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.accuracy);
		drawInput("Range", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.range);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		drawInput("Cast Time", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.castTime);
		drawInput("Recovery", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.recovery);

		if (ImGui::BeginCombo("Animation", animationDefs.at(_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.animation).c_str())) {
			for (size_t i = 0; i < Moves::getInstance().getMoves().size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.animation);
				if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
					_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.animation = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Knockdown", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.knockDown);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
		drawInputN("IP Stun/IP Cancel Stun", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ipStun, 2);
		drawInput("Knockback", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.knockback);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

		if (ImGui::BeginCombo("Element", elementDefs.at(_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.element).c_str())) {
			for (size_t i = 0; i < elementDefs.size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.element);
				if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
					_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.element = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Element Strength", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.elementStrength);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		for (size_t i = 0; i < 8; i++) {
			if (auto ret = ImGui::Checkbox(statusDefs.at(i).c_str(), &MoveAilmentBitFlags[i])) {
				_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ailmentsBitflag ^= (1 << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInput("Ailments Chance", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ailmentsChance);
		drawInputN("Atk/Def/Act/Mov Mods", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.atkMod, 4);
		drawInput("Special", &_bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.special);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");

		ImGui::End();
	}

	if (_showAI) {
		ImGui::Begin("BOSS AI");
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

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = _bosses.at(_bossIndex).moveSets.at(_moveSetIndex).moves[_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

			ImGui::EndCombo();
		}

		drawInput("AI Type", &_bosses.at(_bossIndex).ai[_aiIndex].aiType);
		drawInput("Move #1 Chance", &_bosses.at(_bossIndex).ai[_aiIndex].move1Chance);
		drawInput("Move #2 Chance", &_bosses.at(_bossIndex).ai[_aiIndex].move2Chance);
		drawInput("Move #3 Chance", &_bosses.at(_bossIndex).ai[_aiIndex].move3Chance);
		drawInput("Move #4 Chance", &_bosses.at(_bossIndex).ai[_aiIndex].move4Chance);
		drawInput("Move #5 Chance", &_bosses.at(_bossIndex).ai[_aiIndex].move5Chance);

		ImGui::End();
	}
}

void Bosses::outputToCSV() {
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");
	std::ofstream stream;
	stream.open(Difficulty::getInstance().isHardMode() ? "./csv/BOSS_HARDMODE.CSV" : "./csv/BOSS.CSV");

	std::ofstream stream2;
	stream2.open(Difficulty::getInstance().isHardMode() ? "./csv/BOSS_HARDMODE_MOVES.CSV" : "./csv/BOSS_MOVES.CSV");

	if (!stream.is_open() || !stream2.is_open()) {
		return;
	}

	stream << "Name,Type 1,Type 2,Level,Health,MP,SP,VIT,AGI,SPD,MEN,Stamina,IP Stun,IP Cancel Stun,Evasion Still %,Evasion Moving %,Fire Resist %,Wind Resist %,Earth Resist %,Lightning Resist %,Blizzard Resist %,"
		<< "Status Effect Resist Bitflag,Knockback Resist,Status Recovery Time,T_DMG,T_HEAL,Size,No Run,EXP,Skill Coins,Magic Coins,Gold Coins,Item 1,Item 2,Item 1 Chance,Item 2 Chance\n";

	stream2 << "Enemy,Move,MP,SP,???,Target Effect,STR,POW,AD,Target Type,???,Distance,Accuracy,Range,Cast Time,Recovery,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback,Element,Element Strength,Status Effect Bitflag,"
		<< "Status Effect Chance,ATK Change,DEF Change,ACT Change,MOV Change,Special\n";

	for (const auto& val : _bosses) {
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
			<< Items::getInstance().getItems().at(val.stats.item1).name << ','
			<< Items::getInstance().getItems().at(val.stats.item2).name << ','
			<< std::to_string(val.stats.item1Chance) << ','
			<< std::to_string(val.stats.item2Chance) << '\n';

        for (const auto& moveSet : val.moveSets) {
            for (size_t moveIndex = 0; moveIndex < 5; moveIndex++) {
                if (std::string(moveSet.moves[moveIndex].name).find_first_not_of(' ') == std::string::npos) {
                    continue;
                }

                stream2 << val.name << ','
                    << moveSet.moves[moveIndex].name << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.mp) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.sp) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.unknown) << ','
                    << targetEffectDefs.at(moveSet.moves[moveIndex].stats.targetEffect) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.strength) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.power) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.ad) << ','
                    << targetTypeDefs.at(moveSet.moves[moveIndex].stats.targetType) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.normalAttackFlag) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.distance) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.accuracy) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.range) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.castTime) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.recovery) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.animation) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.knockDown) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.ipStun) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.ipCancelStun) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.knockback) << ','
                    << elementDefs.at(moveSet.moves[moveIndex].stats.element) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.elementStrength) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.ailmentsBitflag) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.ailmentsChance) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.atkMod) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.defMod) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.actMod) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.movMod) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.special) << '\n';
            }
        }
	}

	stream.close();
	stream2.close();
}

void Bosses::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& enemy : _bosses) {
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