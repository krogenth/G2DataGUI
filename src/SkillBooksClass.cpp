#include <fstream>
#include <filesystem>

#include ".\include\SkillBooksClass.h"

#include ".\include\common\io_util.h"
#include ".\include\common\char_constants.h"
#include ".\include\common\string_manip.h"
#include ".\include\common\copypaste_obj.h"

#include ".\imgui.h"

void SkillBooksClass::write() {

	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_SKILL.BIN not found to be written!");

	for (size_t i = 0; i < this->_skillbooks.size(); i++)
		for (size_t j = 0; j < 6; j++)
			writeRaw<SkillImplementationStruct>(output, this->_skillbooks.at(i).skills[j]);

	output.close();

}

void SkillBooksClass::read(std::string filename) {

	this->_filename = filename;

	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("TB_SKILL.BIN not found to be read!");

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);

	this->_skillbooks.resize(fileSize / 24);		//	entries are 24 bytes long(each skill is 4 bytes long, 6 skills per book)

	for (size_t i = 0; i < this->_skillbooks.size(); i++)
		for (size_t j = 0; j < 6; j++)
			this->_skillbooks.at(i).skills[j] = readRaw<SkillImplementationStruct>(input);

	input.close();

}

void SkillBooksClass::draw() {

	ImGui::Begin("TB_SKILL");

	ImGui::Text("%s", &this->_filename[0]);

	if (ImGui::BeginCombo("SkillBook Index", bookIDs[this->_bookIndex])) {

		for (size_t i = 0; i < this->_skillbooks.size(); i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_bookIndex);
			if (ImGui::Selectable(bookIDs[i], is_selected))
				this->_bookIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		this->write();
	}

	if (ImGui::BeginCombo("Index", slotIDs[this->_skillIndex])) {

		for (size_t i = 0; i < 6; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_skillIndex);
			if (ImGui::Selectable(slotIDs[i], is_selected))
				this->_skillIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Skill", this->_skills[this->_skillbooks.at(this->_bookIndex).skills[this->_skillIndex].skillOffset].name)) {

		for (size_t i = 0; i < this->_numSkills; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_skillbooks.at(this->_bookIndex).skills[this->_skillIndex].skillOffset);
			if (ImGui::Selectable(this->_skills[i].name, is_selected))
				this->_skillbooks.at(this->_bookIndex).skills[this->_skillIndex].skillOffset = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::InputUByte("Starting Level", &this->_skillbooks[this->_bookIndex].skills[this->_skillIndex].startingLevel);
	ImGui::InputUByte("Book Level Required", &this->_skillbooks[this->_bookIndex].skills[this->_skillIndex].bookLevelRequired);
	ImGui::InputUByte("Unknown #1", &this->_skillbooks[this->_bookIndex].skills[this->_skillIndex].unknown1);

	ImGui::End();

}

void SkillBooksClass::outputToCSV() {



}