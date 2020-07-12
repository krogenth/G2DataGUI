#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SkillBookStruct.h"

void writeSKI(SkillBookStruct* books, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/TB_SKILL.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_SKILL.BIN not found to be written!");

	for (size_t i = 0; i < 0x0A; i++) {

		for (size_t j = 0; j < 6; j++) {

			output.put(books[i].skills[j].skillOffset);
			output.put(books[i].skills[j].startingLevel);
			output.put(books[i].skills[j].bookLevelRequired);
			output.put(books[i].skills[j].unknown1);

		}

	}

	output.close();

}

SkillBookStruct* readSKI(ImU16& count) {

	std::experimental::filesystem::path filePath("content/data/afs/xls_data/TB_SKILL.BIN");
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[1]{};

	std::ifstream input("content/data/afs/xls_data/TB_SKILL.BIN", std::ios::binary);

	count = (ImU16)(fileSize / 24);
	SkillBookStruct* books = new SkillBookStruct[count];	//entries are 24 bytes long(each skill is 4 bytes long, 6 skills per book)

	if (!input.is_open())
		throw new std::exception("TB_SKILL.BIN not found to be read!");

	for (size_t i = 0; i < count; i++) {

		for (size_t j = 0; j < 6; j++) {

			input.read(readByte, 1);
			books[i].skills[j].skillOffset = (ImU8)readByte[0];

			input.read(readByte, 1);
			books[i].skills[j].startingLevel = (ImU8)readByte[0];

			input.read(readByte, 1);
			books[i].skills[j].bookLevelRequired = (ImU8)readByte[0];

			input.read(readByte, 1);
			books[i].skills[j].unknown1 = (ImU8)readByte[0];

		}

	}

	input.close();

	return books;

}

void drawSKI(SkillBookStruct* books, ImU16& numBooks, bool* canClose, char** skillIDs, ImU16& numSkills) {

	const char* bookIDs[] = { "NULL", "Adventure Book", "Book of Wizards", "Book of Warriors", "Book of Priests", "Book of Gales", "Book of Swords", "Book of War", "Book of Sages", "Book of Learning" };
	const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6" };
	static ImU16 bookID = 0;
	static ImU16 skillSlot = 0;

	ImGui::Begin("TB_SKILL");

	ImGui::Combo("Index", &bookID, bookIDs, (int)numBooks); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeSKI(books, numBooks);

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