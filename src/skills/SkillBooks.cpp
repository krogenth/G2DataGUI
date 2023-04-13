#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./SkillBooks.h"
#include "./Skills.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void SkillBooks::write() {
	std::ofstream output;
	output.open(_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("TB_SKILL.BIN not found to be written!");
	}

	for (size_t i = 0; i < _skillbooks.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			writeRaw<SkillImplementation>(output, _skillbooks.at(i).skills[j]);
		}
	}

	output.close();
}

void SkillBooks::read() {
	std::ifstream input(_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("TB_SKILL.BIN not found to be read!");
	}

	std::filesystem::path filePath(_filename);
	size_t fileSize = std::filesystem::file_size(filePath);

	_skillbooks.resize(fileSize / 24); // entries are 24 bytes long(each skill is 4 bytes long, 6 skills per book)

	for (size_t i = 0; i < _skillbooks.size(); i++) {
		for (size_t j = 0; j < 6; j++) {
			_skillbooks.at(i).skills[j] = readRaw<SkillImplementation>(input);
		}
	}

	input.close();
}

void SkillBooks::draw() {
	ImGui::Begin("SKILLBOOKS");
	
	auto bookDefs = JsonDefinitions::getInstance().getDefinitions("books");

	if (ImGui::Button("Save")) {
		write();
	}

	if (ImGui::BeginCombo("SkillBook Index", bookDefs.at(_bookIndex).c_str())) {
		for (size_t i = 0; i < _skillbooks.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _bookIndex);
			if (ImGui::Selectable(bookDefs.at(i).c_str(), is_selected)) {
				_bookIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Index", std::format("Index {}", _skillIndex + 1).c_str())) {
		for (size_t i = 0; i < 6; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _skillIndex);
			if (ImGui::Selectable(std::format("Index {}", i + 1).c_str(), is_selected)) {
				_skillIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Skill", Skills::getInstance().getSkills().at(_skillbooks.at(_bookIndex).skills[_skillIndex].skillOffset).name)) {
		for (size_t i = 0; i < Skills::getInstance().getSkills().size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _skillbooks.at(_bookIndex).skills[_skillIndex].skillOffset);
			if (ImGui::Selectable(Skills::getInstance().getSkills().at(i).name, is_selected)) {
				_skillbooks.at(_bookIndex).skills[_skillIndex].skillOffset = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Starting Level", &_skillbooks[_bookIndex].skills[_skillIndex].startingLevel);
	drawInput("Book Level Required", &_skillbooks[_bookIndex].skills[_skillIndex].bookLevelRequired);
	drawInput("Unknown #1", &_skillbooks[_bookIndex].skills[_skillIndex].unknown1);

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

	for (size_t i = 0; i < _skillbooks.size(); i++) {
		output << bookDefs.at(i);

		for (size_t j = 0; j < 6; j++) {
			output << ',' << Skills::getInstance().getSkills().at(_skillbooks.at(i).skills[j].skillOffset).name
				<< ',' << std::to_string(_skillbooks.at(i).skills[j].startingLevel)
				<< ',' << std::to_string(_skillbooks.at(i).skills[j].bookLevelRequired)
				<< ',' << std::to_string(_skillbooks.at(i).skills[j].unknown1);
		}

		output << '\n';
	}

	output.close();
}

void SkillBooks::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& skillbook : _skillbooks) {
		for (size_t i = 0; i < 6; i++) {
			do {
				skillbook.skills[i].skillOffset = (uint8_t)(g() % (Skills::getInstance().getSkills().size() - 1)) + 1;
			} while (std::string(Skills::getInstance().getSkills().at(skillbook.skills[i].skillOffset).name).find_first_not_of(' ') == std::string::npos);

			skillbook.skills[i].startingLevel = (g() % 2) ? g() % 6 : 0;
			if (skillbook.skills[i].startingLevel == 0) {
				skillbook.skills[i].bookLevelRequired = g() % 16;
			}
		}
	}
}