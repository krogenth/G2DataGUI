#include <fstream>
#include <exception>

#include "StringManip.h"
#include "ManaEggStruct.h"

void writeMAG(ManaEggStruct* eggs, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_MAGIC.BIN not found to be written!");

	for (size_t i = 0; i < 0x0B; i++) {

		for (size_t j = 0; j < 18; j++) {

			output.put(eggs[i].spells[j].spellOffset);
			output.put(eggs[i].spells[j].startingLevel);
			output.put(eggs[i].spells[j].eggLevelRequired);
			output.put(eggs[i].spells[j].unknown1);

		}

	}

	output.close();

}

ManaEggStruct* readMAG(ImU16& count) {

	char* readByte = new char[1]{};

	std::ifstream input("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	count = 0x0B;
	ManaEggStruct* eggs = new ManaEggStruct[count];	//entries are 72 bytes long(each spell is 4 bytes long, 18 spells per egg)

	if (!input.is_open())
		throw new std::exception("TB_MAGIC.BIN not found to be read!");

	for (size_t i = 0; i < count; i++) {

		for (size_t j = 0; j < 18; j++) {

			input.read(readByte, 1);
			eggs[i].spells[j].spellOffset = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].startingLevel = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].eggLevelRequired = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].unknown1 = (ImU8)readByte[0];

		}

	}

	input.close();

	return eggs;

}