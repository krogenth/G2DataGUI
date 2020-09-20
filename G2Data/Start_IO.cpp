#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StartStats.h"
#include "io_util.h"
#include "char_constants.h"

extern bool isHDVersion;

void writePC(std::vector<StartStatsStruct>& stats, std::string filename) {

	std::ofstream output(filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("PC_INIT.BIN not found to be written!");

	for (size_t i = 0; i < stats.size(); i++)
		writeRaw<StartStatsStruct>(output, stats[i]);

	output.close();

}

void readPC(std::vector<StartStatsStruct>& stats, std::string filename) {
	//Upon starting a new game, all characters gain EXP together. The EXP value displayed is what each character starts the game at

	std::ifstream input(filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("PC_INIT.BIN not found to be read!");

	std::experimental::filesystem::path filePath(filename);
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	stats.resize(fileSize / 80);		//entries are 80 bytes long

	for (size_t i = 0; i < stats.size(); i++)
		stats[i] = readRaw<StartStatsStruct>(input);

	input.close();

}

void drawPC(std::vector<StartStatsStruct>& stats, bool* canClose, char** itemIDs, const size_t& numItems) {

	static ImU16 statID = 0;

	ImGui::Begin("PC_INIT");

	ImGui::Combo("Index", &statID, statIDs, (int)stats.size()); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			if (isHDVersion)
				writePC(stats);
			else
				writePC(stats, "data/afs/xls_data/PC_INIT.BIN");

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::InputUInt("EXP", &stats[statID].exp);

	ImGui::Combo("Weapon", &stats[statID].weapon, itemIDs, (int)numItems);
	ImGui::Combo("Armour", &stats[statID].armour, itemIDs, (int)numItems);
	ImGui::Combo("Headgear", &stats[statID].headgear, itemIDs, (int)numItems);
	ImGui::Combo("Footwear", &stats[statID].footwear, itemIDs, (int)numItems);
	ImGui::Combo("Accessory", &stats[statID].accessory, itemIDs, (int)numItems);
	ImGui::Combo("Mana Egg", &stats[statID].manaEgg, itemIDs, (int)numItems);

	ImGui::InputShort("Stamina", &stats[statID].stamina);                                           if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");

	ImGui::InputUShort("Unknown #1", &stats[statID].unknown1);
	ImGui::InputUShort("Unknown #2", &stats[statID].unknown2);
	ImGui::InputUShort("Unknown #3", &stats[statID].unknown3);
	ImGui::InputUShort("Unknown #4", &stats[statID].unknown4);
	ImGui::InputUShort("Unknown #5", &stats[statID].unknown5);
	ImGui::InputUShort("Unknown #6", &stats[statID].unknown6);
	ImGui::InputUShort("Unknown #7", &stats[statID].unknown7);
	ImGui::InputUShort("Unknown #8", &stats[statID].unknown8);
	ImGui::InputUShort("Unknown #9", &stats[statID].unknown9);
	ImGui::InputUShort("Unknown #10", &stats[statID].unknown10);

	ImGui::InputShort2("IP Stun/IP Cancel Stun", &stats[statID].ipStun);                            if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");

	ImGui::InputUByte("Combo SP Regen", &stats[statID].comboSpRegen);                               if (ImGui::IsItemHovered()) ImGui::SetTooltip("SP regen from combo hit.");
	ImGui::InputUByte("Critical SP Regen", &stats[statID].critSpRegen);                             if (ImGui::IsItemHovered()) ImGui::SetTooltip("SP regen from critical hit.");

	ImGui::InputUByte("Unknown #11", &stats[statID].unknown11);

	ImGui::InputUByte("Damaged SP Regen", &stats[statID].hitSpRegen);                               if (ImGui::IsItemHovered()) ImGui::SetTooltip("SP regen from taking damage.");

	ImGui::InputUByte("Unknown #12", &stats[statID].unknown12);

	ImGui::InputByte("Still Evasion Rate", &stats[statID].evasionStillRate);
	ImGui::InputByte("Move Evasion Rate", &stats[statID].evasionMovingRate);
	ImGui::InputByte("Knockback Resist Rate", &stats[statID].ResistKnockback);

	ImGui::InputUShort("Unknown #13", &stats[statID].unknown13);

	ImGui::InputShort("TREC", &stats[statID].TREC);
	ImGui::InputShort("TDMG", &stats[statID].TDMG);

	ImGui::InputUShort("Unknown #14", &stats[statID].unknown14);

	ImGui::InputShort("THEAL", &stats[statID].THEAL);

	ImGui::InputShort("Size", &stats[statID].size);

	ImGui::InputUShort("Unknown #15", &stats[statID].unknown15);
	ImGui::InputUShort("Unknown #16", &stats[statID].unknown16);
	ImGui::InputUShort("Unknown #17", &stats[statID].unknown17);
	ImGui::InputUShort("Unknown #18", &stats[statID].unknown18);
	ImGui::InputUShort("Unknown #19", &stats[statID].unknown19);
	ImGui::InputUShort("Unknown #20", &stats[statID].unknown20);
	ImGui::InputUShort("Unknown #21", &stats[statID].unknown21);
	ImGui::InputUShort("Unknown #22", &stats[statID].unknown22);
	ImGui::InputUShort("Unknown #23", &stats[statID].unknown23);

	ImGui::End();

}