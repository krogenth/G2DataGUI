#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include "./Skills.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void Skills::write() {
	std::ofstream output;
	output.open(_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("SK_PARAM.BIN not found to be written!");
	}

	for (size_t i = 0; i < _skills.size(); i++) {
		output.write((char*)_skills.at(i).name, 18);
		writeRaw<SkillStats>(output, _skills.at(i).stats);
		output.write((char*)_skills.at(i).description, 40);
	}

	output.close();
}

void Skills::read() {
	std::ifstream input(_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("SK_PARAM.BIN not found to be read!");
	}

	std::filesystem::path filePath(_filename);
	size_t fileSize = std::filesystem::file_size(filePath);

	_skills.resize(fileSize / 104);		//entries are 104 bytes long

	for (size_t i = 0; i < _skills.size(); i++) {
		input.read(_skills.at(i).name, 18);
		_skills.at(i).stats = readRaw<SkillStats>(input);
		input.read(_skills.at(i).description, 40);
	}

	input.close();
}

void Skills::draw() {
	ImGui::Begin("SKILLS");

	if (ImGui::Button("Save")) {
		write();
	}

	ImGui::SameLine();
	if (ImGui::Button("Copy")) {
		copyObj(&_skills.at(_skillIndex), "Skill");
	}

	ImGui::SameLine();
	if (ImGui::Button("Paste") && checkObjType("Skill")) {
		_skills.at(_skillIndex) = *((Skill*)pasteObj());
	}

	if (ImGui::BeginCombo("Skill Index", _skills.at(_skillIndex).name)) {
		for (size_t i = 0; i < _skills.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _skillIndex);
			if (ImGui::Selectable(_skills.at(i).name, is_selected)) {
				_skillIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", _skills.at(_skillIndex).name, 19);
	drawInput("Cost Type #1", &_skills.at(_skillIndex).stats.cost1);
	drawInput("Cost Type #2", &_skills.at(_skillIndex).stats.cost2);
	drawInput("Base HP", &_skills.at(_skillIndex).stats.baseHp);
	drawInput("Base MP", &_skills.at(_skillIndex).stats.baseMp);
	drawInput("Base SP", &_skills.at(_skillIndex).stats.baseSp);
	drawInput("Base STR", &_skills.at(_skillIndex).stats.baseStr);
	drawInput("Base VIT", &_skills.at(_skillIndex).stats.baseVit);
	drawInput("Base ACT", &_skills.at(_skillIndex).stats.baseAct);
	drawInput("Base MOV", &_skills.at(_skillIndex).stats.baseMov);
	drawInput("Base MAG", &_skills.at(_skillIndex).stats.baseMag);
	drawInput("Base MEN", &_skills.at(_skillIndex).stats.baseMen);
	drawInput("Unknown #1", &_skills.at(_skillIndex).stats.unknown1);
	drawInput("Unknown #2", &_skills.at(_skillIndex).stats.unknown2);
	drawInput("Unknown #3", &_skills.at(_skillIndex).stats.unknown3);
	drawInput("Unknown #4", &_skills.at(_skillIndex).stats.unknown4);
	drawInput("Unknown #5", &_skills.at(_skillIndex).stats.unknown5);
	drawInput("Increase Fire %", &_skills.at(_skillIndex).stats.increaseFirePercent);
	drawInput("Increase Wind %", &_skills.at(_skillIndex).stats.increaseWindPercent);
	drawInput("Increase Earth %", &_skills.at(_skillIndex).stats.increaseEarthPercent);
	drawInput("Increase Lightning %", &_skills.at(_skillIndex).stats.increaseLightningPercent);
	drawInput("Increase Blizzard %", &_skills.at(_skillIndex).stats.increaseBlizzardPercent);
	drawInput("Increase Water %", &_skills.at(_skillIndex).stats.increaseWaterPercent);
	drawInput("Increase Explosion %", &_skills.at(_skillIndex).stats.increaseExplosionPercent);
	drawInput("Increase Forest %", &_skills.at(_skillIndex).stats.increaseForestPercent);
	drawInput("Special", &_skills.at(_skillIndex).stats.special);
	drawInput("Coin Cost Lv1", &_skills.at(_skillIndex).stats.coinCost1);
	drawInput("Coin Cost Lv2", &_skills.at(_skillIndex).stats.coinCost2);
	drawInput("Coin Cost Lv3", &_skills.at(_skillIndex).stats.coinCost3);
	drawInput("Coin Cost Lv4", &_skills.at(_skillIndex).stats.coinCost4);
	drawInput("Coin Cost Lv5", &_skills.at(_skillIndex).stats.coinCost5);
	drawInput("Multiplier", &_skills.at(_skillIndex).stats.multiplier);
	ImGui::InputText("Description", _skills.at(_skillIndex).description, 41);

	ImGui::End();
}

void Skills::outputToCSV() {
	std::ofstream output;
	output.open("./csv/SK_PARAM.CSV");

	if (!output.is_open()) {
		return;
	}

	output << "Name,Cost Type,Cost Type,Base HP Inc,Base MP Inc,Base SP Inc,Base STR Inc,Base VIT Inc,Base ACT Inc,Base MOV Inc,Base MAG Inc,Base MEN Inc,???,???,???,???,???,"
		<< "Fire Pow Inc %,Wind Pow Inc %,Earth Pow Inc %,Blizzard Pow Inc %,Water Pow Inc %,Explosion Pow Inc %,Forest Pow Inc %,Special,Cost Lv1,Cost Lv2,Cost Lv3,Cost Lv4,Cost Lv5,Multiplier,Description\n";

	for (const auto& val : _skills) {
		output << val.name << ','
			<< std::to_string(val.stats.cost1) << ','
			<< std::to_string(val.stats.cost2) << ','
			<< std::to_string(val.stats.baseHp) << ','
			<< std::to_string(val.stats.baseMp) << ','
			<< std::to_string(val.stats.baseSp) << ','
			<< std::to_string(val.stats.baseStr) << ','
			<< std::to_string(val.stats.baseVit) << ','
			<< std::to_string(val.stats.baseAct) << ','
			<< std::to_string(val.stats.baseMov) << ','
			<< std::to_string(val.stats.baseMag) << ','
			<< std::to_string(val.stats.baseMen) << ','
			<< std::to_string(val.stats.unknown1) << ','
			<< std::to_string(val.stats.unknown2) << ','
			<< std::to_string(val.stats.unknown3) << ','
			<< std::to_string(val.stats.unknown4) << ','
			<< std::to_string(val.stats.unknown5) << ','
			<< std::to_string(val.stats.increaseFirePercent) << ','
			<< std::to_string(val.stats.increaseWindPercent) << ','
			<< std::to_string(val.stats.increaseEarthPercent) << ','
			<< std::to_string(val.stats.increaseBlizzardPercent) << ','
			<< std::to_string(val.stats.increaseWaterPercent) << ','
			<< std::to_string(val.stats.increaseExplosionPercent) << ','
			<< std::to_string(val.stats.increaseForestPercent) << ','
			<< std::to_string(val.stats.special) << ','
			<< std::to_string(val.stats.coinCost1) << ','
			<< std::to_string(val.stats.coinCost2) << ','
			<< std::to_string(val.stats.coinCost3) << ','
			<< std::to_string(val.stats.coinCost4) << ','
			<< std::to_string(val.stats.coinCost5) << ','
			<< std::to_string(val.stats.multiplier) << ','
			<< val.description << '\n';
	}

	output.close();
}