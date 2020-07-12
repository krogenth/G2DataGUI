#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SpecialMoveStruct.h"

void writeSPC(SpecialMoveStruct* specials, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/TB_SPCL.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_SPCL.BIN not found to be written!");

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

	std::experimental::filesystem::path filePath("content/data/afs/xls_data/TB_SPCL.BIN");
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[2]{};

	std::ifstream input("content/data/afs/xls_data/TB_SPCL.BIN", std::ios::binary);

	count = (ImU16)(fileSize / 24);
	SpecialMoveStruct* specials = new SpecialMoveStruct[count];	//entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	if (!input.is_open())
		throw new std::exception("TB_SPCL.BIN not found to be read!");

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

void drawSPC(SpecialMoveStruct* specials, ImU16& numSpecials, bool* canClose, char** moveIDs, ImU16& numMoves) {

	const char* specialIDs[] = { "NULL", "Ryduo", "Elena", "Millenia", "Roan", "Tio", "Mareg", "Prince Roan", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" };
	const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6" };
	static ImU16 specialID = 0;
	static ImU16 moveSlot = 0;

	ImGui::Begin("TB_SPCL");

	ImGui::Combo("Index", &specialID, specialIDs, (int)numSpecials); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeSPC(specials, numSpecials);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::Combo("Slot", &moveSlot, slotIDs, 6); ImGui::NewLine();

	ImGui::Combo("Move", &specials[specialID].moves[moveSlot].moveOffset, moveIDs, (int)numMoves);
	ImGui::InputUByte("Starting Level", &specials[specialID].moves[moveSlot].startingLevel);
	ImGui::InputUShort("Story Flag", &specials[specialID].moves[moveSlot].storyFlag);

	ImGui::End();

}