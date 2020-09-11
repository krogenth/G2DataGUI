#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SpecialMoveStruct.h"
#include "io_util.h"
#include "char_constants.h"

extern bool isHDVersion;

void writeSPC(std::vector<SpecialMoveStruct>& specials, std::string filename) {

	std::ofstream output(filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("TB_SPCL.BIN not found to be written!");

	for (size_t i = 0; i < specials.size(); i++)
		for (size_t j = 0; j < 6; j++)
			writeRaw<MoveImplementationStruct>(output, specials[i].moves[j]);

	output.close();

}

void readSPC(std::vector<SpecialMoveStruct>& specials, std::string filename) {

	std::ifstream input(filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("TB_SPCL.BIN not found to be read!");

	std::experimental::filesystem::path filePath(filename);
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	specials.resize(fileSize / 24);		//entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	char* readByte = new char[2]{};

	for (size_t i = 0; i < specials.size(); i++)
		for (size_t j = 0; j < 6; j++)
			specials[i].moves[j] = readRaw<MoveImplementationStruct>(input);

	input.close();

}

void drawSPC(std::vector<SpecialMoveStruct>& specials, bool* canClose, char** moveIDs, const size_t& numMoves) {

	static ImU16 specialID = 0;
	static ImU16 moveSlot = 0;

	ImGui::Begin("TB_SPCL");

	ImGui::Combo("Index", &specialID, specialIDs, (int)specials.size()); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			if (isHDVersion)
				writeSPC(specials);
			else
				writeSPC(specials, "data/afs/xls_data/TB_SPCL.BIN");

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