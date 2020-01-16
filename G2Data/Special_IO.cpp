#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SpecialMoveStruct.h"

void writeSPC(SpecialMoveStruct* specials, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/TB_SPCL.BIN", std::ios::binary);

	if (!output.is_open())
		return;

	for (size_t i = 0; i < 0x0E; i++) {

		for (size_t j = 0; j < 6; j++) {

			output.put(specials[i].moves[j].moveOffset);
			output.put(specials[i].moves[j].startingLevel);

			output.put(specials[i].moves[j].storyFlag);
			output.put((specials[i].moves[j].storyFlag >> 8));

		}

	}

	output.close();

}

SpecialMoveStruct* readSPC(ImU16& count) {

	char* readByte = new char[2]{};

	std::ifstream input("content/data/afs/xls_data/TB_SPCL.BIN", std::ios::binary);

	count = 0x0E;
	SpecialMoveStruct* specials = new SpecialMoveStruct[count];	//entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	if (!input.is_open())
		throw new std::exception();

	for (size_t i = 0; i < count; i++) {

		for (size_t j = 0; j < 6; j++) {

			input.read(readByte, 1);
			specials[i].moves[j].moveOffset = (ImU8)readByte[0];

			input.read(readByte, 1);
			specials[i].moves[j].startingLevel = (ImU8)readByte[0];

			input.read(readByte, 2);
			specials[i].moves[j].storyFlag = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		}

	}

	input.close();

	return specials;

}