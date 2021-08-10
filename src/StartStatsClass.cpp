#include <fstream>
#include <filesystem>

#include ".\include\StartStatsClass.h"

#include ".\include\common\io_util.h"
#include ".\include\common\char_constants.h"
#include ".\include\common\string_manip.h"
#include ".\include\common\copypaste_obj.h"

#include ".\imgui.h"

void StartStatsClass::write() {

	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("PC_INIT.BIN not found to be written!");

	for (size_t i = 0; i < this->_startStats.size(); i++)
		writeRaw<StartStatsStruct>(output, this->_startStats.at(i));

	output.close();

}

void StartStatsClass::read(std::string filename) {

	//	Upon starting a new game, all characters gain EXP together. The EXP value displayed is what each character starts the game at

	this->_filename = filename;

	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("PC_INIT.BIN not found to be read!");

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);

	this->_startStats.resize(fileSize / 80);		//	entries are 80 bytes long

	for (size_t i = 0; i < this->_startStats.size(); i++)
		this->_startStats.at(i) = readRaw<StartStatsStruct>(input);

	input.close();

}

void StartStatsClass::draw() {

	ImGui::Begin("PC_INIT");

	if (ImGui::BeginCombo("Start Stat Index", statIDs[this->_statIndex])) {

		for (size_t i = 0; i < this->_startStats.size(); i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_statIndex);
			if (ImGui::Selectable(statIDs[i], is_selected))
				this->_statIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		this->write();

	}

	ImGui::InputUInt("EXP", &this->_startStats[this->_statIndex].exp);

	if (ImGui::BeginCombo("Weapon", this->_items[this->_startStats.at(this->_statIndex).weapon].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).weapon);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_startStats.at(this->_statIndex).weapon = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Armour", this->_items[this->_startStats.at(this->_statIndex).armour].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).armour);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_startStats.at(this->_statIndex).armour = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Headgear", this->_items[this->_startStats.at(this->_statIndex).headgear].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).headgear);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_startStats.at(this->_statIndex).headgear = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Footwear", this->_items[this->_startStats.at(this->_statIndex).footwear].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).footwear);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_startStats.at(this->_statIndex).footwear = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Accessory", this->_items[this->_startStats.at(this->_statIndex).accessory].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).accessory);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_startStats.at(this->_statIndex).accessory = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Mana Egg", this->_items[this->_startStats.at(this->_statIndex).manaEgg].name)) {

		for (size_t i = 0; i < this->_numItems; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_startStats.at(this->_statIndex).manaEgg);
			if (ImGui::Selectable(this->_items[i].name, is_selected))
				this->_startStats.at(this->_statIndex).manaEgg = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}


	ImGui::InputShort("Stamina", &this->_startStats[this->_statIndex].stamina);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("How long can they move without tiring?");

	ImGui::InputUShort("Unknown #1", &this->_startStats[this->_statIndex].unknown1);
	ImGui::InputUShort("Unknown #2", &this->_startStats[this->_statIndex].unknown2);
	ImGui::InputUShort("Unknown #3", &this->_startStats[this->_statIndex].unknown3);
	ImGui::InputUShort("Unknown #4", &this->_startStats[this->_statIndex].unknown4);
	ImGui::InputUShort("Unknown #5", &this->_startStats[this->_statIndex].unknown5);
	ImGui::InputUShort("Unknown #6", &this->_startStats[this->_statIndex].unknown6);
	ImGui::InputUShort("Unknown #7", &this->_startStats[this->_statIndex].unknown7);
	ImGui::InputUShort("Unknown #8", &this->_startStats[this->_statIndex].unknown8);
	ImGui::InputUShort("Unknown #9", &this->_startStats[this->_statIndex].unknown9);
	ImGui::InputUShort("Unknown #10", &this->_startStats[this->_statIndex].unknown10);

	ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_startStats[this->_statIndex].ipStun);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");

	ImGui::InputUByte("Combo SP Regen", &this->_startStats[this->_statIndex].comboSpRegen);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("SP regen from combo hit.");

	ImGui::InputUByte("Critical SP Regen", &this->_startStats[this->_statIndex].critSpRegen);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("SP regen from critical hit.");

	ImGui::InputUByte("Unknown #11", &this->_startStats[this->_statIndex].unknown11);

	ImGui::InputUByte("Damaged SP Regen", &this->_startStats[this->_statIndex].hitSpRegen);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("SP regen from taking damage.");

	ImGui::InputUByte("Unknown #12", &this->_startStats[this->_statIndex].unknown12);

	ImGui::InputByte("Still Evasion Rate", &this->_startStats[this->_statIndex].evasionStillRate);
	ImGui::InputByte("Move Evasion Rate", &this->_startStats[this->_statIndex].evasionMovingRate);
	ImGui::InputByte("Knockback Resist Rate", &this->_startStats[this->_statIndex].ResistKnockback);

	ImGui::InputUShort("Unknown #13", &this->_startStats[this->_statIndex].unknown13);

	ImGui::InputShort("T_REC", &this->_startStats[this->_statIndex].TREC);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("Time to recover from status effects(lower is better).");

	ImGui::InputShort("T_DMG", &this->_startStats[this->_statIndex].TDMG);
		if (ImGui::IsItemHovered())
			ImGui::SetTooltip("Time to recover from being hit(lower is better)?");

	ImGui::InputUShort("Unknown #14", &this->_startStats[this->_statIndex].unknown14);

	ImGui::InputShort("T_HEAL", &this->_startStats[this->_statIndex].THEAL);

	ImGui::InputShort("Size", &this->_startStats[this->_statIndex].size);

	ImGui::InputUShort("Unknown #15", &this->_startStats[this->_statIndex].unknown15);
	ImGui::InputUShort("Unknown #16", &this->_startStats[this->_statIndex].unknown16);
	ImGui::InputUShort("Unknown #17", &this->_startStats[this->_statIndex].unknown17);
	ImGui::InputUShort("Unknown #18", &this->_startStats[this->_statIndex].unknown18);
	ImGui::InputUShort("Unknown #19", &this->_startStats[this->_statIndex].unknown19);
	ImGui::InputUShort("Unknown #20", &this->_startStats[this->_statIndex].unknown20);
	ImGui::InputUShort("Unknown #21", &this->_startStats[this->_statIndex].unknown21);
	ImGui::InputUShort("Unknown #22", &this->_startStats[this->_statIndex].unknown22);
	ImGui::InputUShort("Unknown #23", &this->_startStats[this->_statIndex].unknown23);

	ImGui::End();

}

void StartStatsClass::outputToCSV() {

	std::ofstream output;
	output.open(".\\csv\\PC_INIT.CSV");

	if (!output.is_open())
		return;

	output << "EXP,weapon,armor,headgear,footwear,accessory,manaegg,stamina,???,???,???,???,???,???,???,???,???,???,hit IP Stun,cancel IP Stun,combo SP regen,crit SP regen,???,"
		<< "hit SP regen,???,evasion,moveing evasion,R_KB,???,T_REC,T_DMG,???,T_HEAL,Size,???,???,???,???,???,???,???,???,???\n";

	for (const auto& val : this->_startStats) {

		output << std::to_string(val.exp) << ','
			<< this->_items[val.weapon].name << ','
			<< this->_items[val.armour].name << ','
			<< this->_items[val.headgear].name << ','
			<< this->_items[val.footwear].name << ','
			<< this->_items[val.accessory].name << ','
			<< this->_items[val.manaEgg].name << ','
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