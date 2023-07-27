#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./ManaEggs.h"
#include "./Moves.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void ManaEggs::write() {
	std::ofstream output;
	output.open(_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("TB_MAGIC.BIN not found to be written!");
	}

	for (size_t i = 0; i < _manaeggs.size(); i++) {
		for (size_t j = 0; j < 18; j++) {
			writeRaw<SpellImplementation>(output, _manaeggs.at(i).spells[j]);
		}
	}

	output.close();
}

void ManaEggs::read() {
	std::ifstream input(_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_MAGIC.BIN not found to be read!");
	}

	std::filesystem::path filePath(_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	_manaeggs.resize(fileSize / 72); // entries are 72 bytes long(each spell is 4 bytes long, 18 spells per egg)

	for (size_t i = 0; i < _manaeggs.size(); i++) {
		for (size_t j = 0; j < 18; j++) {
			_manaeggs.at(i).spells[j] = readRaw<SpellImplementation>(input);
		}
	}

	input.close();
}

void ManaEggs::draw() {
	ImGui::Begin("MANAEGGS");

	auto eggDefs = JsonDefinitions::getInstance().getDefinitions("eggs");

	if (ImGui::Button("Save")) {
		write();
	}

	// ManaEgg names are stored inside ITEMS.BIN, so until we find a way to track that, we stick with a hardcoded version
	if (ImGui::BeginCombo("ManaEgg Index", eggDefs.at(_eggIndex).c_str())) {
		for (size_t i = 0; i < _manaeggs.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _eggIndex);
			if (ImGui::Selectable(eggDefs.at(i).c_str(), is_selected)) {
				_eggIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Index", std::format("Index {}", _spellIndex + 1).c_str())) {
		for (size_t i = 0; i < 18; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _spellIndex);
			if (ImGui::Selectable(std::format("Index {}", i + 1).c_str(), is_selected)) {
				_spellIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Spell", Moves::getInstance().getMoves().at(_manaeggs.at(_eggIndex).spells[_spellIndex].spellOffset).name)) {
		for (size_t i = 0; i < Moves::getInstance().getMoves().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _manaeggs.at(_eggIndex).spells[_spellIndex].spellOffset);
			if (ImGui::Selectable(Moves::getInstance().getMoves().at(i).name, is_selected)) {
				_manaeggs.at(_eggIndex).spells[_spellIndex].spellOffset = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Starting Level", &_manaeggs.at(_eggIndex).spells[_spellIndex].startingLevel);
	drawInput("Egg Level Required", &_manaeggs.at(_eggIndex).spells[_spellIndex].eggLevelRequired);
	drawInput("Unknown #1", &_manaeggs.at(_eggIndex).spells[_spellIndex].unknown1);

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

	for (size_t i = 0; i < _manaeggs.size(); i++) {
		output << eggDefs.at(i).c_str();
		for (size_t j = 0; j < 18; j++) {
			output << ',' << Moves::getInstance().getMoves().at(_manaeggs.at(i).spells[j].spellOffset).name
				<< ',' << std::to_string(_manaeggs.at(i).spells[j].startingLevel)
				<< ',' << std::to_string(_manaeggs.at(i).spells[j].eggLevelRequired)
				<< ',' << std::to_string(_manaeggs.at(i).spells[j].unknown1);
		}

		output << '\n';
	}

	output.close();
}

void ManaEggs::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& manaegg : _manaeggs) {
		std::unordered_map<int8_t, int8_t> usedIndices;
		for (size_t i = 0; i < 18; i++) {
			do {
				manaegg.spells[i].spellOffset = (g() % (50 - 1)) + 1;
			} while (std::string(Moves::getInstance().getMoves().at(manaegg.spells[i].spellOffset).name).find_first_not_of(' ') == std::string::npos || usedIndices.find(manaegg.spells[i].spellOffset) != usedIndices.end());
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