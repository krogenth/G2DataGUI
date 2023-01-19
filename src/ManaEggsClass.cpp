#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./include/ManaEggsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./imgui.h"

void ManaEggs::write() {
	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("TB_MAGIC.BIN not found to be written!");
	}

	for (size_t i = 0; i < this->_manaeggs.size(); i++) {
		for (size_t j = 0; j < 18; j++) {
			writeRaw<SpellImplementationStruct>(output, this->_manaeggs.at(i).spells[j]);
		}
	}

	output.close();
}

void ManaEggs::read() {
	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_MAGIC.BIN not found to be read!");
	}

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	this->_manaeggs.resize(fileSize / 72); // entries are 72 bytes long(each spell is 4 bytes long, 18 spells per egg)

	for (size_t i = 0; i < this->_manaeggs.size(); i++) {
		for (size_t j = 0; j < 18; j++) {
			this->_manaeggs.at(i).spells[j] = readRaw<SpellImplementationStruct>(input);
		}
	}

	input.close();
}

void ManaEggs::draw() {
	ImGui::Begin("MANAEGGS");

	auto eggDefs = JsonDefinitions::getInstance().getDefinitions("eggs");

	if (ImGui::Button("Save")) {
		this->write();
	}

	// ManaEgg names are stored inside ITEMS.BIN, so until we find a way to track that, we stick with a hardcoded version
	if (ImGui::BeginCombo("ManaEgg Index", eggDefs.at(this->_eggIndex).c_str())) {
		for (size_t i = 0; i < this->_manaeggs.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_eggIndex);
			if (ImGui::Selectable(eggDefs.at(i).c_str(), is_selected)) {
				this->_eggIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Index", std::format("Index {}", this->_spellIndex + 1).c_str())) {
		for (size_t i = 0; i < 18; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_spellIndex);
			if (ImGui::Selectable(std::format("Index {}", i + 1).c_str(), is_selected)) {
				this->_spellIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Spell", this->_moves->at(this->_manaeggs.at(this->_eggIndex).spells[this->_spellIndex].spellOffset).name)) {
		for (size_t i = 0; i < this->_moves->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_manaeggs.at(this->_eggIndex).spells[this->_spellIndex].spellOffset);
			if (ImGui::Selectable(this->_moves->at(i).name, is_selected)) {
				this->_manaeggs.at(this->_eggIndex).spells[this->_spellIndex].spellOffset = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputUByte("Starting Level", &this->_manaeggs.at(this->_eggIndex).spells[this->_spellIndex].startingLevel);
	ImGui::InputUByte("Egg Level Required", &this->_manaeggs.at(this->_eggIndex).spells[this->_spellIndex].eggLevelRequired);
	ImGui::InputUByte("Unknown #1", &this->_manaeggs.at(this->_eggIndex).spells[this->_spellIndex].unknown1);

	ImGui::End();
}

void ManaEggs::outputToCSV() {
	auto eggDefs = JsonDefinitions::getInstance().getDefinitions("eggs");
	std::ofstream output;
	output.open("./csv/TB_MAGIC.CSV");

	if (!output.is_open()) {
		return;
	}

	output << "Manaegg";
	for(size_t i = 0; i < 18; i++) {
		output << ",Move,Starting Level,Level Required,???";
	}
	output << '\n';

	for (size_t i = 0; i < this->_manaeggs.size(); i++) {
		output << eggDefs.at(i).c_str();
		for (size_t j = 0; j < 18; j++) {
			output << ',' << this->_moves->at(this->_manaeggs.at(i).spells[j].spellOffset).name
				<< ',' << std::to_string(this->_manaeggs.at(i).spells[j].startingLevel)
				<< ',' << std::to_string(this->_manaeggs.at(i).spells[j].eggLevelRequired)
				<< ',' << std::to_string(this->_manaeggs.at(i).spells[j].unknown1);
		}

		output << '\n';
	}

	output.close();
}

void ManaEggs::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& manaegg : this->_manaeggs) {
		std::unordered_map<int8_t, int8_t> usedIndices;
		for (size_t i = 0; i < 18; i++) {
			do {
				manaegg.spells[i].spellOffset = (g() % (50 - 1)) + 1;
			} while (std::string(this->_moves->at(manaegg.spells[i].spellOffset).name).find_first_not_of(' ') == std::string::npos || usedIndices.find(manaegg.spells[i].spellOffset) != usedIndices.end());
			usedIndices.insert(std::make_pair(manaegg.spells[i].spellOffset, manaegg.spells[i].spellOffset));
			manaegg.spells[i].startingLevel = (g() % 2) ? g() % 6 : 0;
			if (manaegg.spells[i].startingLevel == 0) {
				manaegg.spells[i].eggLevelRequired = g() % 41;
			}
			else {
				manaegg.spells[i].eggLevelRequired = 0;
			}
		}
	}
}