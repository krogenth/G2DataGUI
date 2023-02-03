#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./include/SpecialsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./include/common/imgui_wrappers.h"
#include "./imgui.h"

void Specials::write() {
	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("TB_SPCL.BIN not found to be written!");
	}

	for (size_t i = 0; i < this->_specials.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			writeRaw<MoveImplementationStruct>(output, this->_specials.at(i).moves[j]);
		}
	}

	output.close();
}

void Specials::read() {
	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_SPCL.BIN not found to be read!");
	}

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	this->_specials.resize(fileSize / 24); // entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	for (size_t i = 0; i < this->_specials.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			this->_specials.at(i).moves[j] = readRaw<MoveImplementationStruct>(input);
		}
	}

	input.close();
}

void Specials::draw() {
	ImGui::Begin("SPECIALS");
	
	auto specialDefs = JsonDefinitions::getInstance().getDefinitions("specials");

	if (ImGui::Button("Save")) {
		this->write();
	}

	if (ImGui::BeginCombo("Special Index", specialDefs.at(this->_specialIndex).c_str())) {
		for (size_t i = 0; i < this->_specials.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_specialIndex);
			if (ImGui::Selectable(specialDefs.at(i).c_str(), is_selected)) {
				this->_specialIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Index", std::format("Index {}", this->_moveIndex + 1).c_str())) {
		for (size_t i = 0; i < 6; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moveIndex);
			if (ImGui::Selectable(std::format("Index {}", i + 1).c_str(), is_selected)) {
				this->_moveIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Move", this->_moves->at(this->_specials.at(this->_specialIndex).moves[this->_moveIndex].moveOffset).name)) {
		for (size_t i = 0; i < this->_moves->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_specials.at(this->_specialIndex).moves[this->_moveIndex].moveOffset);
			if (ImGui::Selectable(this->_moves->at(i).name, is_selected)) {
				this->_specials.at(this->_specialIndex).moves[this->_moveIndex].moveOffset = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Starting Level", &this->_specials[this->_specialIndex].moves[this->_moveIndex].startingLevel);
	drawInput("Story Flag", &this->_specials[this->_specialIndex].moves[this->_moveIndex].storyFlag);

	ImGui::End();
}

void Specials::outputToCSV() {
	auto specialDefs = JsonDefinitions::getInstance().getDefinitions("specials");
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

	for (size_t i = 0; i < this->_specials.size(); i++) {
		output << specialDefs.at(i);
		for (size_t j = 0; j < 6; j++) {
			output << ',' << this->_moves->at(this->_specials.at(i).moves[j].moveOffset).name
				<< ',' << std::to_string(this->_specials.at(i).moves[j].startingLevel)
				<< ',' << std::to_string(this->_specials.at(i).moves[j].storyFlag);
		}

		output << '\n';
	}

	output.close();
}