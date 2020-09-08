#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "ManaEggStruct.h"
#include "io_util.h"
#include "char_constants.h"

void writeMAG(std::vector<ManaEggStruct>& eggs) {

	std::ofstream output("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_MAGIC.BIN not found to be written!");

	for (size_t i = 0; i < eggs.size(); i++)
		for (size_t j = 0; j < 18; j++)
			writeRaw<SpellImplementationStruct>(output, eggs[i].spells[j]);

	output.close();

}

void readMAG(std::vector<ManaEggStruct>& eggs) {

	std::experimental::filesystem::path filePath("content/data/afs/xls_data/TB_MAGIC.BIN");
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[1]{};

	std::ifstream input("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	eggs.resize(fileSize / 72);		//entries are 72 bytes long(each spell is 4 bytes long, 18 spells per egg)

	if (!input.is_open())
		throw new std::exception("TB_MAGIC.BIN not found to be read!");

	for (size_t i = 0; i < eggs.size(); i++) {

		for (size_t j = 0; j < 18; j++)
			eggs[i].spells[j] = readRaw<SpellImplementationStruct>(input);

	}

	input.close();

}

void drawMAG(std::vector<ManaEggStruct>& eggs, bool* canClose, char** moveIDs, const size_t& numMoves) {

	static ImU16 eggID = 0;
	static ImU16 spellSlot = 0;

	ImGui::Begin("TB_MAGIC");

	ImGui::Combo("Index", &eggID, eggIDs, (int)eggs.size()); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeMAG(eggs);

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