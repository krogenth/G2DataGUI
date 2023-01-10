#include <fstream>
#include <filesystem>
#include <random>

#include "./include/BossesClass.h"

#include "./include/common/io_util.h"
#include "./include/common/char_constants.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./imgui.h"

void Bosses::writeBoss(std::fstream& stream, const BossStruct& boss, bool isSecond) {
	stream.write((char*)boss.name, 18);
	writeRaw<EnemyStatsStruct>(stream, boss.stats);

	// write AI data
	for (size_t aiIndex = 0; aiIndex < 5; aiIndex++) {
		writeRaw<EnemyAIStruct>(stream, boss.ai[aiIndex]);
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
		writeRaw<EnemyMoveStatsStruct>(stream, boss.moveSets.front().moves[k].stats);
	}
}

void Bosses::write() {
    std::fstream stream;
	uint32_t offset = 0;

    for (const auto& boss : this->_bosses) {
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
            this->writeBoss(stream, boss, boss.isSecond);
        }
    }
}

void Bosses::readBoss(std::ifstream& stream, std::string& filename, bool isSecond) {
    this->_bosses.emplace_back(BossStruct());
    stream.read(this->_bosses.back().name, 18);
    replaceNulls(this->_bosses.back().name, 18);
    this->_bosses.back().stats = readRaw<EnemyStatsStruct>(stream);
    this->_bosses.back().filename = filename; // we need the filename for writing data

    if (isSecond) {
        stream.seekg(0x4C, std::ios::beg); // second copy move data offset is always at 0x4C
    }
    else {
        stream.seekg(0x3C, std::ios::beg); // first copy move data offset is always at 0x3C
    }

    uint32_t movesOffset = readRaw<uint32_t>(stream);
    stream.seekg(movesOffset, std::ios::beg);

    // don't bother checking if the move data segment is non-null, if the enemy data exists, the move data should too
    this->_bosses.back().moveSets.emplace_back(EnemyMovesStruct());
    for (size_t moveIndex = 0; moveIndex < 5; moveIndex++) {
        stream.read(this->_bosses.back().moveSets.back().moves[moveIndex].name, 18);
        replaceNulls(this->_bosses.back().moveSets.back().moves[moveIndex].name, 18);
        this->_bosses.back().moveSets.back().moves[moveIndex].stats = readRaw<EnemyMoveStatsStruct>(stream);
    }
}

void Bosses::read(bool isHardmode) {
    this->isHardmode = isHardmode;
	std::ifstream stream;
	uint32_t offset = 0;
    std::string directory = this->_directory + "boss" + (isHardmode ? "_hardmode" : "");

    // clear out any existing data, we are re-reading data
    this->_bosses.clear();

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
			this->readBoss(stream, filename);
		}

		stream.seekg(0x44, std::ios::beg); // second copy data offset is always at 0x44
		offset = readRaw<int32_t>(stream);
		if (offset > 0) {
			stream.seekg(offset, std::ios::beg);
			this->readBoss(stream, filename, true);
		}

		stream.close();
    }
}

void Bosses::draw() {
	ImGui::Begin("BOSSES");

	if (ImGui::BeginCombo("Enemy Index", this->_bosses.at(this->_bossIndex).name)) {
		for (size_t i = 0; i < this->_bosses.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_bossIndex);
			if (ImGui::Selectable(this->_bosses.at(i).name, is_selected)) {
				this->_bossIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = this->_bosses.at(this->_bossIndex).stats.ailmentsBitflag & (1 << i);
		}

		ImGui::EndCombo();
	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {
		this->write();
	}

	ImGui::InputText("Name", this->_bosses.at(this->_bossIndex).name, 19);
	ImGui::InputUByte("Type1", &this->_bosses.at(this->_bossIndex).stats.type1);
	ImGui::InputUByte("Type2", &this->_bosses.at(this->_bossIndex).stats.type2);
	ImGui::InputShort("Level", &this->_bosses.at(this->_bossIndex).stats.level);
	ImGui::InputInt("Health", &this->_bosses.at(this->_bossIndex).stats.health);
	ImGui::InputShort("MP", &this->_bosses.at(this->_bossIndex).stats.mp);
	ImGui::InputShort("SP", &this->_bosses.at(this->_bossIndex).stats.sp);
	ImGui::InputShort("VIT", &this->_bosses.at(this->_bossIndex).stats.vit);
	ImGui::InputShort("AGI", &this->_bosses.at(this->_bossIndex).stats.agi);
	ImGui::InputShort("SPD", &this->_bosses.at(this->_bossIndex).stats.spd);
	ImGui::InputShort("MEN", &this->_bosses.at(this->_bossIndex).stats.men);
	ImGui::InputShort("Stamina", &this->_bosses.at(this->_bossIndex).stats.stamina);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");
	ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_bosses.at(this->_bossIndex).stats.ipStun);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	ImGui::InputByte("Still Evasion Rate", &this->_bosses.at(this->_bossIndex).stats.evasionStillRate);
	ImGui::InputByte("Moving Evasion Rate", &this->_bosses.at(this->_bossIndex).stats.evasionMovingRate);
	ImGui::InputByte("Fire Resist", &this->_bosses.at(this->_bossIndex).stats.fireResist);
	ImGui::InputByte("Wind Resist", &this->_bosses.at(this->_bossIndex).stats.windResist);
	ImGui::InputByte("Earth Resist", &this->_bosses.at(this->_bossIndex).stats.earthResist);
	ImGui::InputByte("Lightning Resist", &this->_bosses.at(this->_bossIndex).stats.lightningResist);
	ImGui::InputByte("Blizzard Resist", &this->_bosses.at(this->_bossIndex).stats.blizzardResist);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statuses[i], &AilmentBitFlags[0])) {
			this->_bosses.at(this->_bossIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[i] << i);
		}
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Do they resist this ailment?");
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	ImGui::InputShort("Knockback Resist Rate", &this->_bosses.at(this->_bossIndex).stats.knockbackResist);
	ImGui::InputShort("Size", &this->_bosses.at(this->_bossIndex).stats.size);
	ImGui::InputByte("No Run", &this->_bosses.at(this->_bossIndex).stats.noRunFlag);
	ImGui::InputInt("EXP", &this->_bosses.at(this->_bossIndex).stats.exp);
	ImGui::InputInt("Skill Coins", &this->_bosses.at(this->_bossIndex).stats.skillCoins);
	ImGui::InputInt("Magic Coins", &this->_bosses.at(this->_bossIndex).stats.magicCoins);
	ImGui::InputInt("Gold Coins", &this->_bosses.at(this->_bossIndex).stats.goldCoins);

	if (ImGui::BeginCombo("Item #1", this->_items->at(this->_bosses.at(this->_bossIndex).stats.item1).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_bosses.at(this->_bossIndex).stats.item1);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected))
				this->_bosses.at(this->_bossIndex).stats.item1 = (uint16_t)i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Item #2", this->_items->at(this->_bosses.at(this->_bossIndex).stats.item2).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_bosses.at(this->_bossIndex).stats.item2);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_bosses.at(this->_bossIndex).stats.item2 = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputByte("Item #1 Chance", &this->_bosses.at(this->_bossIndex).stats.item1Chance);
	ImGui::InputByte("Item #2 Chance", &this->_bosses.at(this->_bossIndex).stats.item2Chance);

	if (ImGui::CollapsingHeader("Boss Moves")) {
        if (ImGui::BeginCombo("Moveset Index", slotIDs[this->_moveSetIndex])) {
            for (size_t i = 0; i < this->_bosses.at(this->_bossIndex).moveSets.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moveSetIndex);
			if (ImGui::Selectable(slotIDs[i], is_selected)) {
				this->_moveSetIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
        }

		if (ImGui::BeginCombo("Move Index", this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].name)) {
			for (size_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_moveIndex);
				if (ImGui::Selectable(this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[i].name, is_selected)) {
					this->_moveIndex = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			for (size_t i = 0; i < 8; i++) {
				MoveAilmentBitFlags[i] = this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

			ImGui::EndCombo();
		}

		ImGui::InputText("Name", this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].name, 19);
		ImGui::InputUShort("MP Cost", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.mp);
		ImGui::InputUShort("SP Cost", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.sp);
		ImGui::InputUByte("Unknown #1", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.unknown);
		ImGui::Combo("Target Effect", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.targetEffect, targetEffects, 16);
		ImGui::InputUShort("Strength", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.str);
		ImGui::InputUShort("Power", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.pow);
		ImGui::InputUShort("Damage(?)", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.ad);
		ImGui::Combo("Target Type", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.targetType, targetTypes, 16);
		ImGui::InputUByte("Unknown #2", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.normalAttackFlag);
		ImGui::InputUShort("Distance", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.distance);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How far away to use move?");
		ImGui::InputUShort("Accuracy", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.accuracy);
		ImGui::InputUShort("Range", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.range);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How big is the move area?");
		ImGui::InputUShort("Cast Time", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.castTime);
		ImGui::InputUShort("Recovery", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.recovery);

		if (ImGui::BeginCombo("Animation", animationIDs[this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.animation])) {
			for (size_t i = 0; i < this->_moves->size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.animation);
				if (ImGui::Selectable(animationIDs[i], is_selected)) {
					this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.animation = (uint8_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		ImGui::InputUByte("Knockdown", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.knockDown);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
		ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.ipStun);
		ImGui::InputShort("Knockback", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.knockback);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");
		ImGui::Combo("Element", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.element, elements, 5);
		ImGui::InputUByte("Element Strength", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.elementStr);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("* 10 percent of damage is this element.");

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statuses[i], &AilmentBitFlags[0])) {
				this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.ailmentsBitflag ^= (MoveAilmentBitFlags[i] << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		ImGui::InputUByte("Ailments Chance", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.ailmentsChance);
		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.atkMod);
		ImGui::InputUShort("Special", &this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.special);
		if (ImGui::IsItemHovered()) ImGui::SetTooltip("In Beta, use at your own peril.");
	}

	if (ImGui::CollapsingHeader("Enemy AI")) {
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
				MoveAilmentBitFlags[i] = this->_bosses.at(this->_bossIndex).moveSets.at(_moveSetIndex).moves[this->_moveIndex].stats.ailmentsBitflag & (1 << i);
			}

			ImGui::EndCombo();
		}

		ImGui::InputUByte("AI Type", &this->_bosses.at(this->_bossIndex).ai[this->_aiIndex].aiType);
		ImGui::InputUByte("Move #1 Chance", &this->_bosses.at(this->_bossIndex).ai[this->_aiIndex].move1Chance);
		ImGui::InputUByte("Move #2 Chance", &this->_bosses.at(this->_bossIndex).ai[this->_aiIndex].move2Chance);
		ImGui::InputUByte("Move #3 Chance", &this->_bosses.at(this->_bossIndex).ai[this->_aiIndex].move3Chance);
		ImGui::InputUByte("Move #4 Chance", &this->_bosses.at(this->_bossIndex).ai[this->_aiIndex].move4Chance);
		ImGui::InputUByte("Move #5 Chance", &this->_bosses.at(this->_bossIndex).ai[this->_aiIndex].move5Chance);
	}

	ImGui::End();
}

void Bosses::outputToCSV() {
	std::ofstream stream;
	stream.open(this->isHardmode ? "./csv/BOSS_HARDMODE.CSV" : "./csv/BOSS.CSV");

	std::ofstream stream2;
	stream2.open(this->isHardmode ? "./csv/BOSS_HARDMODE_MOVES.CSV" : "./csv/BOSS_MOVES.CSV");

	if (!stream.is_open() || !stream2.is_open()) {
		return;
	}

	stream << "Name,Type 1,Type 2,Level,Health,MP,SP,VIT,AGI,SPD,MEN,Stamina,IP Stun,IP Cancel Stun,Evasion Still %,Evasion Moving %,Fire Resist %,Wind Resist %,Earth Resist %,Lightning Resist %,Blizzard Resist %,"
		<< "Status Effect Resist Bitflag,Knockback Resist,Status Recovery Time,T_DMG,T_HEAL,Size,No Run,EXP,Skill Coins,Magic Coins,Gold Coins,Item 1,Item 2,Item 1 Chance,Item 2 Chance\n";

	stream2 << "Enemy,Move,MP,SP,???,Target Effect,STR,POW,AD,Target Type,???,Distance,Accuracy,Range,Cast Time,Recovery,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback,Element,Element Strength,Status Effect Bitflag,"
		<< "Status Effect Chance,ATK Change,DEF Change,ACT Change,MOV Change,Special\n";

	for (const auto& val : this->_bosses) {
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
                    << targetEffects[moveSet.moves[moveIndex].stats.targetEffect] << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.str) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.pow) << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.ad) << ','
                    << targetTypes[moveSet.moves[moveIndex].stats.targetType] << ','
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
                    << elements[moveSet.moves[moveIndex].stats.element] << ','
                    << std::to_string(moveSet.moves[moveIndex].stats.elementStr) << ','
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

	for (auto& enemy : this->_bosses) {
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