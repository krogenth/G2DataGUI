#include <fstream>
#include <filesystem>
#include <random>

#include "./include/StartStatsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./include/common/imgui_wrappers.h"
#include "./imgui.h"

void StartStats::write() {
	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("PC_INIT.BIN not found to be written!");
	}

	for (size_t i = 0; i < this->_startStats.size(); i++) {
		writeRaw<StartStatsStruct>(output, this->_startStats.at(i));
	}

	output.close();
}

void StartStats::read() {
	// Upon starting a new game, all characters gain EXP together.
	// The EXP value displayed is what each character starts the game at.
	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("PC_INIT.BIN not found to be read!");
	}

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	this->_startStats.resize(fileSize / 80); // entries are 80 bytes long

	for (size_t i = 0; i < this->_startStats.size(); i++) {
		this->_startStats.at(i) = readRaw<StartStatsStruct>(input);
	}

	input.close();
}

void StartStats::draw() {
	ImGui::Begin("STARTING STATS");
	
	auto characterDefs = JsonDefinitions::getInstance().getDefinitions("characters");

	if (ImGui::Button("Save")) {
		this->write();
	}

	if (ImGui::BeginCombo("Start Stat Index", characterDefs.at(this->_statIndex).c_str())) {
		for (size_t i = 0; i < this->_startStats.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_statIndex);
			if (ImGui::Selectable(characterDefs.at(i).c_str(), is_selected))
				this->_statIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("EXP", &this->_startStats[this->_statIndex].exp);

	if (ImGui::BeginCombo("Weapon", this->_items->at(this->_startStats.at(this->_statIndex).weapon).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).weapon);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_startStats.at(this->_statIndex).weapon = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Armour", this->_items->at(this->_startStats.at(this->_statIndex).armour).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).armour);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_startStats.at(this->_statIndex).armour = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Headgear", this->_items->at(this->_startStats.at(this->_statIndex).headgear).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).headgear);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_startStats.at(this->_statIndex).headgear = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Footwear", this->_items->at(this->_startStats.at(this->_statIndex).footwear).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).footwear);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_startStats.at(this->_statIndex).footwear = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Accessory", this->_items->at(this->_startStats.at(this->_statIndex).accessory).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).accessory);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_startStats.at(this->_statIndex).accessory = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Mana Egg", this->_items->at(this->_startStats.at(this->_statIndex).manaEgg).name)) {
		for (size_t i = 0; i < this->_items->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).manaEgg);
			if (ImGui::Selectable(this->_items->at(i).name, is_selected)) {
				this->_startStats.at(this->_statIndex).manaEgg = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Stamina", &this->_startStats[this->_statIndex].stamina);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("How long can they move without tiring?");
	}

	drawInput("Unknown #1", &this->_startStats[this->_statIndex].unknown1);
	drawInput("Unknown #2", &this->_startStats[this->_statIndex].unknown2);
	drawInput("Unknown #3", &this->_startStats[this->_statIndex].unknown3);
	drawInput("Unknown #4", &this->_startStats[this->_statIndex].unknown4);
	drawInput("Unknown #5", &this->_startStats[this->_statIndex].unknown5);
	drawInput("Unknown #6", &this->_startStats[this->_statIndex].unknown6);
	drawInput("Unknown #7", &this->_startStats[this->_statIndex].unknown7);
	drawInput("Unknown #8", &this->_startStats[this->_statIndex].unknown8);
	drawInput("Unknown #9", &this->_startStats[this->_statIndex].unknown9);
	drawInput("Unknown #10", &this->_startStats[this->_statIndex].unknown10);

	drawInputN("IP Stun/IP Cancel Stun", &this->_startStats[this->_statIndex].ipStun, 2);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");
	}

	drawInput("Combo SP Regen", &this->_startStats[this->_statIndex].comboSpRegen);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("SP regen from combo hit.");
	}

	drawInput("Critical SP Regen", &this->_startStats[this->_statIndex].critSpRegen);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("SP regen from critical hit.");
	}

	drawInput("Unknown #11", &this->_startStats[this->_statIndex].unknown11);

	drawInput("Damaged SP Regen", &this->_startStats[this->_statIndex].hitSpRegen);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("SP regen from taking damage.");
	}

	drawInput("Unknown #12", &this->_startStats[this->_statIndex].unknown12);

	drawInput("Still Evasion Rate", &this->_startStats[this->_statIndex].evasionStillRate);
	drawInput("Move Evasion Rate", &this->_startStats[this->_statIndex].evasionMovingRate);
	drawInput("Knockback Resist Rate", &this->_startStats[this->_statIndex].ResistKnockback);

	drawInput("Unknown #13", &this->_startStats[this->_statIndex].unknown13);

	drawInput("T_REC", &this->_startStats[this->_statIndex].TREC);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("Time to recover from status effects(lower is better).");
	}

	drawInput("T_DMG", &this->_startStats[this->_statIndex].TDMG);
	if (ImGui::IsItemHovered()) {
		ImGui::SetTooltip("Time to recover from being hit(lower is better)?");
	}

	drawInput("Unknown #14", &this->_startStats[this->_statIndex].unknown14);

	drawInput("T_HEAL", &this->_startStats[this->_statIndex].THEAL);

	drawInput("Size", &this->_startStats[this->_statIndex].size);

	drawInput("Unknown #15", &this->_startStats[this->_statIndex].unknown15);
	drawInput("Unknown #16", &this->_startStats[this->_statIndex].unknown16);
	drawInput("Unknown #17", &this->_startStats[this->_statIndex].unknown17);
	drawInput("Unknown #18", &this->_startStats[this->_statIndex].unknown18);
	drawInput("Unknown #19", &this->_startStats[this->_statIndex].unknown19);
	drawInput("Unknown #20", &this->_startStats[this->_statIndex].unknown20);
	drawInput("Unknown #21", &this->_startStats[this->_statIndex].unknown21);
	drawInput("Unknown #22", &this->_startStats[this->_statIndex].unknown22);
	drawInput("Unknown #23", &this->_startStats[this->_statIndex].unknown23);

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

	for (const auto& val : this->_startStats) {
		output << std::to_string(val.exp) << ','
			<< this->_items->at(val.weapon).name << ','
			<< this->_items->at(val.armour).name << ','
			<< this->_items->at(val.headgear).name << ','
			<< this->_items->at(val.footwear).name << ','
			<< this->_items->at(val.accessory).name << ','
			<< this->_items->at(val.manaEgg).name << ','
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

	for (auto& startStat : this->_startStats) {
		do {
			startStat.weapon = (uint16_t)(300 + (30 * charIndex) + (g() % 30));
		} while (std::string(this->_items->at(startStat.weapon).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.armour = (uint16_t)(500 + (g() % 70));
		} while (std::string(this->_items->at(startStat.armour).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.headgear = (uint16_t)(570 + (g() % 70));
		} while (std::string(this->_items->at(startStat.headgear).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.footwear = (uint16_t)(650 + (g() % 50));
		} while (std::string(this->_items->at(startStat.footwear).name).find_first_not_of(' ') == std::string::npos);

		do {
			startStat.accessory = (uint16_t)(700 + (g() % 99));
		} while (std::string(this->_items->at(startStat.accessory).name).find_first_not_of(' ') == std::string::npos || std::string(this->_items->at(startStat.accessory).name).find("Egg") != std::string::npos);

		if (g() % 3 != 0)
			startStat.manaEgg = 0;
		else {
			do {
				startStat.manaEgg = (uint16_t)(700 + g() % 99);
			} while (std::string(this->_items->at(startStat.manaEgg).name).find("Egg") == std::string::npos);
		}

		++charIndex;
	}
}