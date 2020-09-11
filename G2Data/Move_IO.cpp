#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>
#include <algorithm>

#include "StringManip.h"
#include "MoveStruct.h"
#include "io_util.h"
#include "char_constants.h"

extern bool isHDVersion;

void writeMS(std::vector<MoveStruct>& moves, std::string filename) {

	std::ofstream output(filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("MS_PARAM.BIN not found to be written!");

	for (size_t i = 0; i < moves.size(); i++) {

		moves[i].icon++;
		moves[i].stats.atkMod %= 6;
		moves[i].stats.defMod %= 6;
		moves[i].stats.actMod %= 6;
		moves[i].stats.movMod %= 6;

		writeRaw<ImU8>(output, moves[i].id);
		writeRaw<ImU8>(output, moves[i].icon);

		output.write((char*)moves[i].name, 18);

		writeRaw<MoveStatsStruct>(output, moves[i].stats);

		output.write((char*)moves[i].description, 40);

		moves[i].icon--;

	}

	output.close();

}

void readMS(std::vector<MoveStruct>& moves, std::string filename) {

	std::ifstream input(filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("MS_PARAM.BIN not found to be read!");

	std::experimental::filesystem::path filePath(filename);
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[2]{};

	moves.resize(fileSize / 108);		//	entries are 108 bytes long

	for (int i = 0; i < moves.size(); i++) {

		readRaw<ImU8>(input);
		moves[i].id = i;	//	overwrite all IDs, we want them to be equal to the offset of the entry in the file

		moves[i].icon = readRaw<ImU8>(input);

		input.read(&moves[i].name[0], 18);
		replaceNulls(moves[i].name, 18);	//	strings are not null terminated, we cannot read them from the file like everything else

		moves[i].stats = readRaw<MoveStatsStruct>(input);

		input.read(&moves[i].description[0], 40);
		replaceNulls(moves[i].description, 40);

		if (moves[i].icon != 0)
			moves[i].icon--;

	}

	input.close();

}

void drawMS(std::vector<MoveStruct>& moves, bool* canClose, char** moveIDs, const size_t& numMoves) {

	static ImU16 moveID = 0;
	static bool AilmentBitFlags[8] = {};

	ImGui::Begin("MS_PARAM");

	if (ImGui::Combo("Index", &moveID, moveIDs, (int)moves.size()))
		for (size_t i = 0; i < 8; i++)
			AilmentBitFlags[i] = moves[moveID].stats.ailmentsBitflag & (1 << i);

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			if (isHDVersion)
				writeMS(moves);
			else
				writeMS(moves, "data/afs/xls_data/MS_PARAM.BIN");

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::Combo("Icon", &moves[moveID].icon, moveIcons, 17);
	ImGui::InputText("Name", moves[moveID].name, 19);
	ImGui::InputUShort("Cost", &moves[moveID].cost);

	ImGui::Combo("Target Effect", &moves[moveID].stats.targetEffect, targetEffects, 16);
	ImGui::Combo("Target Type", &moves[moveID].stats.targetType, targetTypes, 16);

	ImGui::InputUShort("Strength", &moves[moveID].stats.str);
	ImGui::InputUShort("Power", &moves[moveID].stats.pow);
	ImGui::InputUShort("Range", &moves[moveID].stats.range);

	ImGui::InputUShort2("Cast Time Lv1/Lv5", &moves[moveID].stats.cast1);

	ImGui::InputUShort("Recovery", &moves[moveID].stats.recovery);
	ImGui::Combo("Animation", &moves[moveID].stats.animation, moveIDs, numMoves);
	ImGui::InputUByte("Unknown #1", &moves[moveID].stats.unknown1);
	ImGui::InputUByte("Knockdown", &moves[moveID].stats.knockDown);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");

	ImGui::InputShort2("IP Stun/IP Cancel Stun", &moves[moveID].stats.ipStun);

	ImGui::InputShort("Knockback", &moves[moveID].stats.knockback);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

	ImGui::Combo("Element", &moves[moveID].stats.element, elements, 5);
	ImGui::InputUByte("Element Strength", &moves[moveID].stats.elementStr);

	if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[0] << 0);
	ImGui::SameLine();
	if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[1] << 1);
	ImGui::SameLine();
	if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[2] << 2);
	ImGui::SameLine();
	if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[3] << 3);

	if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[4] << 4);
	ImGui::SameLine();
	if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[5] << 5);
	ImGui::SameLine();
	if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[6] << 6);
	ImGui::SameLine();
	if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
		moves[moveID].stats.ailmentsBitflag ^= (AilmentBitFlags[7] << 7);

	ImGui::InputUByte("Ailments Chance", &moves[moveID].stats.ailmentsChance);
	ImGui::InputByte4("Atk/Def/Act/Mov Mods", &moves[moveID].stats.atkMod);
	ImGui::InputUShort("Special", &moves[moveID].stats.special);
	ImGui::InputUShort("Coin Cost Lv1", &moves[moveID].stats.coinCost1);
	ImGui::InputUShort("Coin Cost Lv2", &moves[moveID].stats.coinCost2);
	ImGui::InputUShort("Coin Cost Lv3", &moves[moveID].stats.coinCost3);
	ImGui::InputUShort("Coin Cost Lv4", &moves[moveID].stats.coinCost4);
	ImGui::InputUShort("Coin Cost Lv5", &moves[moveID].stats.coinCost5);
	ImGui::InputUShort("Multiplier", &moves[moveID].stats.multiplier);
	ImGui::InputText("Description", moves[moveID].description, 41);

	ImGui::End();

}