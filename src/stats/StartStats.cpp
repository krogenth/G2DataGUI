#include <fstream>
#include <filesystem>
#include <random>

#include "./StartStats.h"
#include "../items/Items.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void StartStats::write() {
	std::ofstream output;
	output.open(_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("PC_INIT.BIN not found to be written!");
	}

	for (size_t i = 0; i < _startStats.size(); i++) {
		writeRaw<CharacterStartStats>(output, _startStats.at(i));
	}

	output.close();
}

void StartStats::read() {
	// Upon starting a new game, all characters gain EXP together.
	// The EXP value displayed is what each character starts the game at.
	std::ifstream input(_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("PC_INIT.BIN not found to be read!");
	}

	std::filesystem::path filePath(_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	_startStats.resize(fileSize / 80); // entries are 80 bytes long

	for (size_t i = 0; i < _startStats.size(); i++) {
		_startStats.at(i) = readRaw<CharacterStartStats>(input);
	}

	input.close();
}

void StartStats::draw() {
	ImGui::Begin("STARTING STATS");
	
	auto characterDefs = JsonDefinitions::getInstance().getDefinitions("characters");

	if (ImGui::Button("Save")) {
		write();
	}

	if (ImGui::BeginCombo("Start Stat Index", characterDefs.at(_statIndex).c_str())) {
		for (size_t i = 0; i < _startStats.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _statIndex);
			if (ImGui::Selectable(characterDefs.at(i).c_str(), is_selected))
				_statIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("EXP", &_startStats[_statIndex].exp);

	if (ImGui::BeginCombo("Weapon", Items::getInstance().getItems().at(_startStats.at(_statIndex).weapon).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _startStats.at(_statIndex).weapon);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_startStats.at(_statIndex).weapon = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Armour", Items::getInstance().getItems().at(_startStats.at(_statIndex).armour).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _startStats.at(_statIndex).armour);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_startStats.at(_statIndex).armour = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Headgear", Items::getInstance().getItems().at(_startStats.at(_statIndex).headgear).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _startStats.at(_statIndex).headgear);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_startStats.at(_statIndex).headgear = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Footwear", Items::getInstance().getItems().at(_startStats.at(_statIndex).footwear).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _startStats.at(_statIndex).footwear);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_startStats.at(_statIndex).footwear = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Accessory", Items::getInstance().getItems().at(_startStats.at(_statIndex).accessory).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _startStats.at(_statIndex).accessory);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_startStats.at(_statIndex).accessory = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Mana Egg", Items::getInstance().getItems().at(_startStats.at(_statIndex).manaEgg).name)) {
		for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _startStats.at(_statIndex).manaEgg);
			if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
				_startStats.at(_statIndex).manaEgg = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Stamina", &_startStats[_statIndex].stamina);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("How long can they move without tiring?");
	}

	drawInput("Unknown #1", &_startStats[_statIndex].unknown1);
	drawInput("Unknown #2", &_startStats[_statIndex].unknown2);
	drawInput("Unknown #3", &_startStats[_statIndex].unknown3);
	drawInput("Unknown #4", &_startStats[_statIndex].unknown4);
	drawInput("Unknown #5", &_startStats[_statIndex].unknown5);
	drawInput("Unknown #6", &_startStats[_statIndex].unknown6);
	drawInput("Unknown #7", &_startStats[_statIndex].unknown7);
	drawInput("Unknown #8", &_startStats[_statIndex].unknown8);
	drawInput("Unknown #9", &_startStats[_statIndex].unknown9);
	drawInput("Unknown #10", &_startStats[_statIndex].unknown10);

	drawInputN("IP Stun/IP Cancel Stun", &_startStats[_statIndex].ipStun, 2);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	}

	drawInput("Combo SP Regen", &_startStats[_statIndex].comboSpRegen);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("SP regen from combo hit.");
	}

	drawInput("Critical SP Regen", &_startStats[_statIndex].critSpRegen);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("SP regen from critical hit.");
	}

	drawInput("Unknown #11", &_startStats[_statIndex].unknown11);

	drawInput("Damaged SP Regen", &_startStats[_statIndex].hitSpRegen);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("SP regen from taking damage.");
	}

	drawInput("Unknown #12", &_startStats[_statIndex].unknown12);

	drawInput("Still Evasion Rate", &_startStats[_statIndex].evasionStillRate);
	drawInput("Move Evasion Rate", &_startStats[_statIndex].evasionMovingRate);
	drawInput("Knockback Resist Rate", &_startStats[_statIndex].ResistKnockback);

	drawInput("Unknown #13", &_startStats[_statIndex].unknown13);

	drawInput("T_REC", &_startStats[_statIndex].TREC);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("Time to recover from status effects(lower is better).");
	}

	drawInput("T_DMG", &_startStats[_statIndex].TDMG);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("Time to recover from being hit(lower is better)?");
	}

	drawInput("Unknown #14", &_startStats[_statIndex].unknown14);

	drawInput("T_HEAL", &_startStats[_statIndex].THEAL);

	drawInput("Size", &_startStats[_statIndex].size);

	drawInput("Unknown #15", &_startStats[_statIndex].unknown15);
	drawInput("Unknown #16", &_startStats[_statIndex].unknown16);
	drawInput("Unknown #17", &_startStats[_statIndex].unknown17);
	drawInput("Unknown #18", &_startStats[_statIndex].unknown18);
	drawInput("Unknown #19", &_startStats[_statIndex].unknown19);
	drawInput("Unknown #20", &_startStats[_statIndex].unknown20);
	drawInput("Unknown #21", &_startStats[_statIndex].unknown21);
	drawInput("Unknown #22", &_startStats[_statIndex].unknown22);
	drawInput("Unknown #23", &_startStats[_statIndex].unknown23);

	ImGui::End();
}

void StartStats::outputToCSV() {
	std::ofstream output;
	output.open("./csv/PC_INIT.CSV");

	if (!output.is_open()) {
		return;
	}

	output << "EXP,weapon,armor,headgear,footwear,accessory,manaegg,stamina,???,???,???,???,???,???,???,???,???,???,hit IP Stun,cancel IP Stun,combo SP regen,crit SP regen,???,"
		<< "hit SP regen,???,evasion,moveing evasion,R_KB,???,T_REC,T_DMG,???,T_HEAL,Size,???,???,???,???,???,???,???,???,???\n";

	for (const auto& val : _startStats) {
		output << std::to_string(val.exp) << ','
			<< Items::getInstance().getItems().at(val.weapon).name << ','
			<< Items::getInstance().getItems().at(val.armour).name << ','
			<< Items::getInstance().getItems().at(val.headgear).name << ','
			<< Items::getInstance().getItems().at(val.footwear).name << ','
			<< Items::getInstance().getItems().at(val.accessory).name << ','
			<< Items::getInstance().getItems().at(val.manaEgg).name << ','
			<< std::to_string(val.stamina) << ','
			<< std::to_string(val.unknown1) << ','
			<< std::to_string(val.unknown2) << ','
			<< std::to_string(val.unknown3) << ','
			<< std::to_string(val.unknown4) << ','
			<< std::to_string(val.unknown5) << ','
			<< std::to_string(val.unknown6) << ','
			<< std::to_string(val.unknown7) << ','
			<< std::to_string(val.unknown8) << ','
			<< std::to_string(val.unknown9) << ','
			<< std::to_string(val.unknown10) << ','
			<< std::to_string(val.ipStun) << ','
			<< std::to_string(val.ipCancelStun) << ','
			<< std::to_string(val.comboSpRegen) << ','
			<< std::to_string(val.critSpRegen) << ','
			<< std::to_string(val.unknown11) << ','
			<< std::to_string(val.hitSpRegen) << ','
			<< std::to_string(val.unknown12) << ','
			<< std::to_string(val.evasionStillRate) << ','
			<< std::to_string(val.evasionMovingRate) << ','
			<< std::to_string(val.ResistKnockback) << ','
			<< std::to_string(val.unknown13) << ','
			<< std::to_string(val.TREC) << ','
			<< std::to_string(val.TDMG) << ','
			<< std::to_string(val.unknown14) << ','
			<< std::to_string(val.THEAL) << ','
			<< std::to_string(val.size) << ','
			<< std::to_string(val.unknown15) << ','
			<< std::to_string(val.unknown16) << ','
			<< std::to_string(val.unknown17) << ','
			<< std::to_string(val.unknown18) << ','
			<< std::to_string(val.unknown19) << ','
			<< std::to_string(val.unknown20) << ','
			<< std::to_string(val.unknown21) << ','
			<< std::to_string(val.unknown22) << ','
			<< std::to_string(val.unknown23) << '\n';
	}

	output.close();
}

void StartStats::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());
	size_t charIndex = 0;

	for (auto& startStat : _startStats) {
		do {
			startStat.weapon = (uint16_t)(300 + (30 * charIndex) + (g() % 30));
		} while (std::string(Items::getInstance().getItems().at(startStat.weapon).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.armour = (uint16_t)(500 + (g() % 70));
		} while (std::string(Items::getInstance().getItems().at(startStat.armour).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.headgear = (uint16_t)(570 + (g() % 70));
		} while (std::string(Items::getInstance().getItems().at(startStat.headgear).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.footwear = (uint16_t)(650 + (g() % 50));
		} while (std::string(Items::getInstance().getItems().at(startStat.footwear).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.accessory = (uint16_t)(700 + (g() % 99));
		} while (std::string(Items::getInstance().getItems().at(startStat.accessory).name).find_first_not_of(' ') == std::string::npos || std::string(Items::getInstance().getItems().at(startStat.accessory).name).find("Egg") != std::string::npos);

		if (g() % 3 != 0)
			startStat.manaEgg = 0;
		else {
			do {
				startStat.manaEgg = (uint16_t)(700 + g() % 99);
			} while (std::string(Items::getInstance().getItems().at(startStat.manaEgg).name).find("Egg") == std::string::npos);
		}

		++charIndex;
	}
}