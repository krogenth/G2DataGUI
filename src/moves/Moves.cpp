#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include "./Moves.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void Moves::write() {
	std::ofstream output;
	output.open(_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("MS_PARAM.BIN not found to be written!");
	}

	for (size_t i = 0; i < _moves.size(); i++) {
		_moves.at(i).icon++; // icon is offset by 1, adjust first
		_moves.at(i).stats.atkMod %= 6; // stat changes are limited to between -5 and 5
		_moves.at(i).stats.defMod %= 6;
		_moves.at(i).stats.actMod %= 6;
		_moves.at(i).stats.movMod %= 6;

		writeRaw<uint8_t>(output, _moves.at(i).id);
		writeRaw<uint8_t>(output, _moves.at(i).icon);
		output.write((char*)_moves[i].name, 18);
		writeRaw<MoveStats>(output, _moves.at(i).stats);
		output.write((char*)_moves.at(i).description, 40);

		_moves.at(i).icon--;
	}

	output.close();
}

void Moves::read() {
	std::ifstream input(_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("MS_PARAM.BIN not found to be read!");
	}

	std::filesystem::path filePath(_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	_moves.resize(fileSize / 108);		//	entries are 108 bytes long

	for (int i = 0; i < _moves.size(); i++) {
		readRaw<uint8_t>(input);
		_moves.at(i).id = i; // overwrite all IDs, we want them to be equal to the offset of the entry in the file

		_moves.at(i).icon = readRaw<uint8_t>(input);

		// strings are not null terminated, we cannot read them from the file like everything else
		input.read(&_moves.at(i).name[0], 18);
		replaceNulls(_moves.at(i).name, 18);

		_moves[i].stats = readRaw<MoveStats>(input);

		input.read(&_moves.at(i).description[0], 40);
		replaceNulls(_moves.at(i).description, 40);

		if (_moves.at(i).icon != 0) {
			_moves.at(i).icon--;
		}
	}

	input.close();

	// set default starting state
	if (_moves.size()) {
		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = (_moves.at(0).stats.ailmentsBitflag & (1 << i)) > 0;
		}
	}
}

void Moves::draw() {
	ImGui::Begin("MS_PARAM");
	
	auto iconDefs = JsonDefinitions::getInstance().getDefinitions("icons");
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto animationDefs = JsonDefinitions::getInstance().getDefinitions("animations");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");
	auto statusDefs = JsonDefinitions::getInstance().getDefinitions("statuses");

	if (ImGui::Button("Save")) {
		write();
	}

	ImGui::SameLine();
	if (ImGui::Button("Copy")) {
		copyObj(&_moves.at(_moveIndex), "Move");
	}

	ImGui::SameLine();
	if (ImGui::Button("Paste")) {
		if (checkObjType("Move")) {
			_moves.at(_moveIndex) = *((Move*)pasteObj());

			for (size_t i = 0; i < 8; i++) {
				AilmentBitFlags[i] = _moves.at(_moveIndex).stats.ailmentsBitflag & (1 << i);
			}
		}
	}

	if (ImGui::BeginCombo("Move Index", _moves.at(_moveIndex).name)) {
		for (size_t i = 0; i < _moves.size() - 1; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moveIndex);
			if (ImGui::Selectable(_moves.at(i).name, is_selected)) {
				_moveIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = _moves.at(_moveIndex).stats.ailmentsBitflag & (1 << i);
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Icon", iconDefs.at(_moves.at(_moveIndex).icon).c_str())) {
		for (size_t i = 0; i < iconDefs.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moves.at(_moveIndex).icon);
			if (ImGui::Selectable(iconDefs.at(i).c_str(), is_selected)) {
				_moves.at(_moveIndex).icon = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", _moves.at(_moveIndex).name, 19);
	drawInput("Cost", &_moves.at(_moveIndex).stats.cost);

	if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(_moves.at(_moveIndex).stats.targetEffect).c_str())) {
		for (uint8_t i = 0; i < 16; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moves.at(_moveIndex).stats.targetEffect);
			if (ImGui::Selectable(targetEffectDefs.at(i).c_str(), is_selected)) {
				_moves.at(_moveIndex).stats.targetEffect = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(_moves.at(_moveIndex).stats.targetType).c_str())) {
		for (uint8_t i = 0; i < 16; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moves.at(_moveIndex).stats.targetType);
			if (ImGui::Selectable(targetTypeDefs.at(i).c_str(), is_selected)) {
				_moves.at(_moveIndex).stats.targetType = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Strength", &_moves.at(_moveIndex).stats.strength);
	drawInput("Power", &_moves.at(_moveIndex).stats.power);
	drawInput("Range", &_moves.at(_moveIndex).stats.range);
	drawInputN("Cast Time Lv1/Lv5", &_moves.at(_moveIndex).stats.cast1Time, 2);
	drawInput("Recovery", &_moves.at(_moveIndex).stats.recovery);

	if (ImGui::BeginCombo("Animation", animationDefs.at(_moves.at(_moveIndex).stats.animation).c_str())) {
		for (size_t i = 0; i < _moves.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moves.at(_moveIndex).stats.animation);
			if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
				_moves.at(_moveIndex).stats.animation = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Unknown #1", &_moves.at(_moveIndex).stats.unknown1);
	drawInput("Knockdown", &_moves.at(_moveIndex).stats.knockDown);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
	drawInputN("IP Stun/IP Cancel Stun", &_moves.at(_moveIndex).stats.ipStun, 2);
	drawInput("Knockback", &_moves.at(_moveIndex).stats.knockback);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

	if (ImGui::BeginCombo("Element", elementDefs.at(_moves.at(_moveIndex).stats.element).c_str())) {
		for (uint8_t i = 0; i < 5; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _moves.at(_moveIndex).stats.element);
			if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
				_moves.at(_moveIndex).stats.element = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Element Strength", &_moves.at(_moveIndex).stats.elementStrength);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statusDefs.at(i).c_str(), &AilmentBitFlags[i])) {
			_moves.at(_moveIndex).stats.ailmentsBitflag ^= (1 << i);
		}
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	drawInput("Ailments Chance", &_moves.at(_moveIndex).stats.ailmentsChance);
	drawInputN("Atk/Def/Act/Mov Mods", &_moves.at(_moveIndex).stats.atkMod, 4);
	drawInput("Special", &_moves.at(_moveIndex).stats.special);
	drawInput("Coin Cost Lv1", &_moves.at(_moveIndex).stats.coinCost1);
	drawInput("Coin Cost Lv2", &_moves.at(_moveIndex).stats.coinCost2);
	drawInput("Coin Cost Lv3", &_moves.at(_moveIndex).stats.coinCost3);
	drawInput("Coin Cost Lv4", &_moves.at(_moveIndex).stats.coinCost4);
	drawInput("Coin Cost Lv5", &_moves.at(_moveIndex).stats.coinCost5);
	drawInput("Multiplier", &_moves.at(_moveIndex).stats.multiplier);
	ImGui::InputText("Description", _moves.at(_moveIndex).description, 41);

	ImGui::End();
}

void Moves::outputToCSV() {
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");
	std::ofstream output;
	output.open("./csv/MS_PARAM.CSV");

	if (!output.is_open()) {
		return;
	}

	output  << "Index,Icon,Name,Cost,Target Effect,Target Type,Strength,Power,Range,Cast Lv1,Cast Lv5,Recovery,Animation,???,Knockdown,IP Damage,IP Cancel Damage,Knockback,Element,Element Strength,Status Effect Bitflag,"
			<< "Status Effect Chance,ATK Change,DEF Change,ACT Change,MOV Change,Special Effect,Cost Lv1,Cost Lv2,Cost Lv3,Cost Lv4,Cost Lv5,Power Multiplier,Description\n";

	for (const auto& val : _moves) {
		output << std::to_string(val.id) << ','
			<< std::to_string(val.icon) << ','
			<< val.name << ','
			<< std::to_string(val.stats.cost) << ','
			<< targetEffectDefs.at(val.stats.targetEffect) << ','
			<< targetTypeDefs.at(val.stats.targetType) << ','
			<< std::to_string(val.stats.strength) << ','
			<< std::to_string(val.stats.power) << ','
			<< std::to_string(val.stats.range) << ','
			<< std::to_string(val.stats.cast1Time) << ','
			<< std::to_string(val.stats.cast5Time) << ','
			<< std::to_string(val.stats.recovery) << ','
			<< _moves.at(val.stats.animation).name << ','
			<< std::to_string(val.stats.unknown1) << ','
			<< std::to_string(val.stats.knockDown) << ','
			<< std::to_string(val.stats.ipStun) << ','
			<< std::to_string(val.stats.ipCancelStun) << ','
			<< std::to_string(val.stats.knockback) << ','
			<< elementDefs.at(val.stats.element) << ','
			<< std::to_string(val.stats.elementStrength) << ','
			<< std::to_string(val.stats.ailmentsBitflag) << ','
			<< std::to_string(val.stats.ailmentsChance) << ','
			<< std::to_string(val.stats.atkMod) << ','
			<< std::to_string(val.stats.defMod) << ','
			<< std::to_string(val.stats.actMod) << ','
			<< std::to_string(val.stats.movMod) << ','
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