#include <fstream>
#include <filesystem>
#include <random>

#include "./include/SpecialsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/char_constants.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./imgui.h"

void SpecialsClass::write() {
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

void SpecialsClass::read(std::string filename) {
	this->_filename = filename;
	std::ifstream input(filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_SPCL.BIN not found to be read!");
	}

	std::filesystem::path filePath(filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	this->_specials.resize(fileSize / 24); // entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	for (size_t i = 0; i < this->_specials.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			this->_specials.at(i).moves[j] = readRaw<MoveImplementationStruct>(input);
		}
	}

	input.close();
}

void SpecialsClass::draw() {
	ImGui::Begin("TB_SPCL");

	if (ImGui::BeginCombo("Special Index", specialIDs[this->_specialIndex])) {
		for (size_t i = 0; i < this->_specials.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_specialIndex);
			if (ImGui::Selectable(specialIDs[i], is_selected)) {
				this->_specialIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {
		this->write();
	}

	if (ImGui::BeginCombo("Index", slotIDs[this->_moveIndex])) {
		for (size_t i = 0; i < 6; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moveIndex);
			if (ImGui::Selectable(slotIDs[i], is_selected)) {
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

	ImGui::InputUByte("Starting Level", &this->_specials[this->_specialIndex].moves[this->_moveIndex].startingLevel);
	ImGui::InputUShort("Story Flag", &this->_specials[this->_specialIndex].moves[this->_moveIndex].storyFlag);

	ImGui::End();
}

void SpecialsClass::outputToCSV() {
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
		output << specialIDs[i];
		for (size_t j = 0; j < 6; j++) {
			output << ',' << this->_moves->at(this->_specials.at(i).moves[j].moveOffset).name
				<< ',' << std::to_string(this->_specials.at(i).moves[j].startingLevel)
				<< ',' << std::to_string(this->_specials.at(i).moves[j].storyFlag);
		}

		output << '\n';
	}

	output.close();
}