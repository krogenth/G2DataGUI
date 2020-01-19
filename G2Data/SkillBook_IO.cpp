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

	char* readByte = new char[1]{};

	std::ifstream input("content/data/afs/xls_data/TB_SKILL.BIN", std::ios::binary);

	count = 0x0A;
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