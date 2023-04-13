#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./Specials.h"
#include "./Moves.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "./imgui.h"
#include "../ui/imgui_wrappers.h"

#include "./Moves.h"

void Specials::write() {
	std::ofstream output;
	output.open(_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("TB_SPCL.BIN not found to be written!");
	}

	for (size_t i = 0; i < _specials.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			writeRaw<MoveImplementation>(output, _specials.at(i).moves[j]);
		}
	}

	output.close();
}

void Specials::read() {
	std::ifstream input(_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_SPCL.BIN not found to be read!");
	}

	std::filesystem::path filePath(_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	_specials.resize(fileSize / 24); // entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	for (size_t i = 0; i < _specials.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			_specials.at(i).moves[j] = readRaw<MoveImplementation>(input);
		}
	}

	input.close();
}

void Specials::draw() {
	ImGui::Begin("SPECIALS");
	
	auto specialDefs = JsonDefinitions::getInstance().getDefinitions("specialChars");

	if (ImGui::Button("Save")) {
		write();
	}

	if (ImGui::BeginCombo("Special Index", specialDefs.at(_specialIndex).c_str())) {
		for (size_t i = 0; i < _specials.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _specialIndex);
			if (ImGui::Selectable(specialDefs.at(i).c_str(), is_selected)) {
				_specialIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Index", std::format("Index {}", _moveIndex + 1).c_str())) {
		for (size_t i = 0; i < 6; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moveIndex);
			if (ImGui::Selectable(std::format("Index {}", i + 1).c_str(), is_selected)) {
				_moveIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Move", Moves::getInstance().getMoves().at(_specials.at(_specialIndex).moves[_moveIndex].moveOffset).name)) {
		for (size_t i = 0; i < Moves::getInstance().getMoves().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _specials.at(_specialIndex).moves[_moveIndex].moveOffset);
			if (ImGui::Selectable(Moves::getInstance().getMoves().at(i).name, is_selected)) {
				_specials.at(_specialIndex).moves[_moveIndex].moveOffset = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Starting Level", &_specials[_specialIndex].moves[_moveIndex].startingLevel);
	drawInput("Story Flag", &_specials[_specialIndex].moves[_moveIndex].storyFlag);

	ImGui::End();
}

void Specials::outputToCSV() {
	auto specialDefs = JsonDefinitions::getInstance().getDefinitions("specialChars");
	std::ofstream output;
	output.open("./csv/TB_SPCL.CSV");

	if (!output.is_open()) {
		return;
	}

	output << "Character";
	for (size_t i = 0; i < 6; i++) {
		output << ",Move,Starting Level,Story Flag";
	}
	output << '\n';

	for (size_t i = 0; i < _specials.size(); i++) {
		output << specialDefs.at(i);
		for (size_t j = 0; j < 6; j++) {
			output << ',' << Moves::getInstance().getMoves().at(_specials.at(i).moves[j].moveOffset).name
				<< ',' << std::to_string(_specials.at(i).moves[j].startingLevel)
				<< ',' << std::to_string(_specials.at(i).moves[j].storyFlag);
		}

		output << '\n';
	}

	output.close();
}