#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SkillBookStruct.h"
#include "io_util.h"
#include "char_constants.h"

extern bool isHDVersion;

void writeSKI(std::vector<SkillBookStruct>& books, std::string filename) {

	std::ofstream output(filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_SKILL.BIN not found to be written!");

	for (size_t i = 0; i < books.size(); i++)
		for (size_t j = 0; j < 6; j++)
			writeRaw<SkillImplementationStruct>(output, books[i].skills[j]);

	output.close();

}

void readSKI(std::vector<SkillBookStruct>& books, std::string filename) {

	std::ifstream input(filename, std::ios::binary);	

	if (!input.is_open())
		throw new std::exception("TB_SKILL.BIN not found to be read!");

	std::experimental::filesystem::path filePath(filename);
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	books.resize(fileSize / 24);		//	entries are 24 bytes long(each skill is 4 bytes long, 6 skills per book)

	char* readByte = new char[1]{};

	for (size_t i = 0; i < books.size(); i++)
		for (size_t j = 0; j < 6; j++)
			books[i].skills[j] = readRaw<SkillImplementationStruct>(input);

	input.close();

}

void drawSKI(std::vector<SkillBookStruct>& books, bool* canClose, char** skillIDs, const size_t& numSkills) {

	static ImU16 bookID = 0;
	static ImU16 skillSlot = 0;

	ImGui::Begin("TB_SKILL");

	ImGui::Combo("Index", &bookID, bookIDs, (int)books.size()); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			if (isHDVersion)
				writeSKI(books);
			else
				writeSKI(books, "data/afs/xls_data/TB_SKILL.BIN");

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}
	}

	ImGui::Combo("Slot", &skillSlot, slotIDs, 6); ImGui::NewLine();

	ImGui::Combo("Skill", &books[bookID].skills[skillSlot].skillOffset, skillIDs, (int)numSkills);
	ImGui::InputUByte("Starting Level", &books[bookID].skills[skillSlot].startingLevel);
	ImGui::InputUByte("Book Level Required", &books[bookID].skills[skillSlot].bookLevelRequired);
	ImGui::InputUByte("Unknown #1", &books[bookID].skills[skillSlot].unknown1); ImGui::NewLine();

	ImGui::End();

}