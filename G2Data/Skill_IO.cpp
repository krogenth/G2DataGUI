#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SkillStruct.h"
#include "io_util.h"

void writeSK(std::vector<SkillStruct>& skills) {

	std::ofstream output("content/data/afs/xls_data/SK_PARAM.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("SK_PARAM.BIN not found to be written!");

	for (size_t i = 0; i < skills.size(); i++) {

		output.write((char*)skills[i].name, 18);

		writeRaw<SkillStatsStruct>(output, skills[i].stats);

		output.write((char*)skills[i].description, 40);

	}

	output.close();

}

void readSK(std::vector<SkillStruct>& skills) {

	std::experimental::filesystem::path filePath("content/data/afs/xls_data/SK_PARAM.BIN");
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[2]{};

	std::ifstream input("content/data/afs/xls_data/SK_PARAM.BIN", std::ios::binary);

	skills.resize(fileSize / 104);		//entries are 104 bytes long

	if (!input.is_open())
		throw new std::exception("SK_PARAM.BIN not found to be read!");

	for (size_t i = 0; i < skills.size(); i++) {

		input.read(skills[i].name, 18);

		skills[i].stats = readRaw<SkillStatsStruct>(input);

		input.read(skills[i].description, 40);

	}

	input.close();

}

void drawSK(std::vector<SkillStruct>& skills, char** skillIDs, bool* canClose) {

	static ImU16 skillID = 0;

	ImGui::Begin("SK_PARAM");

	ImGui::Combo("Index", &skillID, skillIDs, (int)skills.size()); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeSK(skills);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::InputText("Name", skills[skillID].name, 19);
	ImGui::InputUByte("Cost Type #1", &skills[skillID].stats.cost1);
	ImGui::InputUByte("Cost Type #2", &skills[skillID].stats.cost2);
	ImGui::InputUShort("Base HP", &skills[skillID].stats.baseHp);
	ImGui::InputUShort("Base MP", &skills[skillID].stats.baseMp);
	ImGui::InputUShort("Base SP", &skills[skillID].stats.baseSp);
	ImGui::InputUShort("Base STR", &skills[skillID].stats.baseStr);
	ImGui::InputUShort("Base VIT", &skills[skillID].stats.baseVit);
	ImGui::InputUShort("Base ACT", &skills[skillID].stats.baseAct);
	ImGui::InputUShort("Base MOV", &skills[skillID].stats.baseMov);
	ImGui::InputUShort("Base MAG", &skills[skillID].stats.baseMag);
	ImGui::InputUShort("Base MEN", &skills[skillID].stats.baseMen);
	ImGui::InputUByte("Unknown #1", &skills[skillID].stats.unknown1);
	ImGui::InputUByte("Unknown #2", &skills[skillID].stats.unknown2);
	ImGui::InputUByte("Unknown #3", &skills[skillID].stats.unknown3);
	ImGui::InputUByte("Unknown #4", &skills[skillID].stats.unknown4);
	ImGui::InputUByte("Unknown #5", &skills[skillID].stats.unknown5);
	ImGui::InputByte("Base Fire %", &skills[skillID].stats.baseFirePercent);
	ImGui::InputByte("Base Wind %", &skills[skillID].stats.baseWindPercent);
	ImGui::InputByte("Base Earth %", &skills[skillID].stats.baseEarthPercent);
	ImGui::InputByte("Base Lightning %", &skills[skillID].stats.baseLightningPercent);
	ImGui::InputByte("Base Blizzard %", &skills[skillID].stats.baseBlizzardPercent);
	ImGui::InputByte("Base Water %", &skills[skillID].stats.baseWaterPercent);
	ImGui::InputByte("Base Explosion %", &skills[skillID].stats.baseExplosionPercent);
	ImGui::InputByte("Base Forest %", &skills[skillID].stats.baseForestPercent);
	ImGui::InputUByte("Special", &skills[skillID].stats.special);
	ImGui::InputUShort("Coin Cost Lv1", &skills[skillID].stats.coinCost1);
	ImGui::InputUShort("Coin Cost Lv2", &skills[skillID].stats.coinCost2);
	ImGui::InputUShort("Coin Cost Lv3", &skills[skillID].stats.coinCost3);
	ImGui::InputUShort("Coin Cost Lv4", &skills[skillID].stats.coinCost4);
	ImGui::InputUShort("Coin Cost Lv5", &skills[skillID].stats.coinCost5);
	ImGui::InputUShort("Multiplier", &skills[skillID].stats.multiplier);
	ImGui::InputText("Description", skills[skillID].description, 41);

	ImGui::End();

}