#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./include/SkillBooksClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./include/common/imgui_wrappers.h"
#include "./imgui.h"

void SkillBooks::write() {
	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("TB_SKILL.BIN not found to be written!");
	}

	for (size_t i = 0; i < this->_skillbooks.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			writeRaw<SkillImplementationStruct>(output, this->_skillbooks.at(i).skills[j]);
		}
	}

	output.close();
}

void SkillBooks::read() {
	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_SKILL.BIN not found to be read!");
	}

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);

	this->_skillbooks.resize(fileSize / 24); // entries are 24 bytes long(each skill is 4 bytes long, 6 skills per book)

	for (size_t i = 0; i < this->_skillbooks.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			this->_skillbooks.at(i).skills[j] = readRaw<SkillImplementationStruct>(input);
		}
	}

	input.close();
}

void SkillBooks::draw() {
	ImGui::Begin("SKILLBOOKS");
	
	auto bookDefs = JsonDefinitions::getInstance().getDefinitions("books");

	if (ImGui::Button("Save")) {
		this->write();
	}

	if (ImGui::BeginCombo("SkillBook Index", bookDefs.at(this->_bookIndex).c_str())) {
		for (size_t i = 0; i < this->_skillbooks.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_bookIndex);
			if (ImGui::Selectable(bookDefs.at(i).c_str(), is_selected)) {
				this->_bookIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Index", std::format("Index {}", this->_skillIndex + 1).c_str())) {
		for (size_t i = 0; i < 6; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_skillIndex);
			if (ImGui::Selectable(std::format("Index {}", i + 1).c_str(), is_selected)) {
				this->_skillIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Skill", this->_skills->at(this->_skillbooks.at(this->_bookIndex).skills[this->_skillIndex].skillOffset).name)) {
		for (size_t i = 0; i < this->_skills->size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_skillbooks.at(this->_bookIndex).skills[this->_skillIndex].skillOffset);
			if (ImGui::Selectable(this->_skills->at(i).name, is_selected)) {
				this->_skillbooks.at(this->_bookIndex).skills[this->_skillIndex].skillOffset = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Starting Level", &this->_skillbooks[this->_bookIndex].skills[this->_skillIndex].startingLevel);
	drawInput("Book Level Required", &this->_skillbooks[this->_bookIndex].skills[this->_skillIndex].bookLevelRequired);
	drawInput("Unknown #1", &this->_skillbooks[this->_bookIndex].skills[this->_skillIndex].unknown1);

	ImGui::End();
}

void SkillBooks::outputToCSV() {
	auto bookDefs = JsonDefinitions::getInstance().getDefinitions("books");
	std::ofstream output;
	output.open("./csv/TB_SKILL.CSV");

	if (!output.is_open()) {
		return;
	}

	output << "Skillbook";
	for (size_t i = 0; i < 6; i++) {
		output << ",Skill,Starting Level,Level Required,???";
	}
	output << '\n';

	for (size_t i = 0; i < this->_skillbooks.size(); i++) {
		output << bookDefs.at(i);

		for (size_t j = 0; j < 6; j++) {
			output << ',' << this->_skills->at(this->_skillbooks.at(i).skills[j].skillOffset).name
				<< ',' << std::to_string(this->_skillbooks.at(i).skills[j].startingLevel)
				<< ',' << std::to_string(this->_skillbooks.at(i).skills[j].bookLevelRequired)
				<< ',' << std::to_string(this->_skillbooks.at(i).skills[j].unknown1);
		}

		output << '\n';
	}

	output.close();
}

void SkillBooks::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& skillbook : this->_skillbooks) {
		for (size_t i = 0; i < 6; i++) {
			do {
				skillbook.skills[i].skillOffset = (uint8_t)(g() % (this->_skills->size() - 1)) + 1;
			} while (std::string(this->_skills->at(skillbook.skills[i].skillOffset).name).find_first_not_of(' ') == std::string::npos);

			skillbook.skills[i].startingLevel = (g() % 2) ? g() % 6 : 0;
			if (skillbook.skills[i].startingLevel == 0) {
				skillbook.skills[i].bookLevelRequired = g() % 16;
			}
		}
	}
}