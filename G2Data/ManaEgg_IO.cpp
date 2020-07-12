#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "ManaEggStruct.h"

void writeMAG(ManaEggStruct* eggs, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_MAGIC.BIN not found to be written!");

	for (size_t i = 0; i < 0x0B; i++) {

		for (size_t j = 0; j < 18; j++) {

			output.put(eggs[i].spells[j].spellOffset);
			output.put(eggs[i].spells[j].startingLevel);
			output.put(eggs[i].spells[j].eggLevelRequired);
			output.put(eggs[i].spells[j].unknown1);

		}

	}

	output.close();

}

ManaEggStruct* readMAG(ImU16& count) {

	std::experimental::filesystem::path filePath("content/data/afs/xls_data/TB_MAGIC.BIN");
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[1]{};

	std::ifstream input("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	count = fileSize / 72;
	ManaEggStruct* eggs = new ManaEggStruct[count];	//entries are 72 bytes long(each spell is 4 bytes long, 18 spells per egg)

	if (!input.is_open())
		throw new std::exception("TB_MAGIC.BIN not found to be read!");

	for (size_t i = 0; i < count; i++) {

		for (size_t j = 0; j < 18; j++) {

			input.read(readByte, 1);
			eggs[i].spells[j].spellOffset = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].startingLevel = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].eggLevelRequired = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].unknown1 = (ImU8)readByte[0];

		}

	}

	input.close();

	return eggs;

}

void drawMAG(ManaEggStruct* eggs, ImU16& numEggs, bool* canClose, char** moveIDs, ImU16& numMoves) {

	const char* eggIDs[] = { "NULL", "Holy Egg", "Chaos Egg", "Mist Egg", "Gravity Egg", "Soul Egg", "Star Egg", "Tutor Egg", "Change Egg", "Fairy Egg", "Dragon Egg" };
	const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6", "Slot 7", "Slot 8", "Slot 9", "Slot 10", "Slot 11", "Slot 12", "Slot 13", "Slot 14", "Slot 15", "Slot 16", "Slot 17", "Slot 18" };
	static ImU16 eggID = 0;
	static ImU16 spellSlot = 0;

	ImGui::Begin("TB_MAGIC");

	ImGui::Combo("Index", &eggID, eggIDs, (int)numEggs); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeMAG(eggs, numEggs);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::Combo("Slot", &spellSlot, slotIDs, 18); ImGui::NewLine();

	ImGui::Combo("Spell", &eggs[eggID].spells[spellSlot].spellOffset, moveIDs, (int)numMoves);
	ImGui::InputUByte("Starting Level", &eggs[eggID].spells[spellSlot].startingLevel);
	ImGui::InputUByte("Egg Level Required", &eggs[eggID].spells[spellSlot].eggLevelRequired);
	ImGui::InputUByte("Unknown #1", &eggs[eggID].spells[spellSlot].unknown1); ImGui::NewLine();

	ImGui::End();

}