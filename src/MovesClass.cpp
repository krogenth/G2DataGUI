#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include "./include/MovesClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./imgui.h"

void Moves::write() {
	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open()) {
		throw new std::exception("MS_PARAM.BIN not found to be written!");
	}

	for (size_t i = 0; i < this->_moves.size(); i++) {
		this->_moves.at(i).icon++; // icon is offset by 1, adjust first
		this->_moves.at(i).stats.atkMod %= 6; // stat changes are limited to between -5 and 5
		this->_moves.at(i).stats.defMod %= 6;
		this->_moves.at(i).stats.actMod %= 6;
		this->_moves.at(i).stats.movMod %= 6;

		writeRaw<uint8_t>(output, this->_moves.at(i).id);
		writeRaw<uint8_t>(output, this->_moves.at(i).icon);
		output.write((char*)this->_moves[i].name, 18);
		writeRaw<MoveStatsStruct>(output, this->_moves.at(i).stats);
		output.write((char*)this->_moves.at(i).description, 40);

		this->_moves.at(i).icon--;
	}

	output.close();
}

void Moves::read() {
	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open()) {
		throw new std::exception("MS_PARAM.BIN not found to be read!");
	}

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);
	this->_moves.resize(fileSize / 108);		//	entries are 108 bytes long

	for (int i = 0; i < this->_moves.size(); i++) {
		readRaw<uint8_t>(input);
		this->_moves.at(i).id = i; // overwrite all IDs, we want them to be equal to the offset of the entry in the file

		this->_moves.at(i).icon = readRaw<uint8_t>(input);

		// strings are not null terminated, we cannot read them from the file like everything else
		input.read(&this->_moves.at(i).name[0], 18);
		replaceNulls(this->_moves.at(i).name, 18);

		this->_moves[i].stats = readRaw<MoveStatsStruct>(input);

		input.read(&this->_moves.at(i).description[0], 40);
		replaceNulls(this->_moves.at(i).description, 40);

		if (this->_moves.at(i).icon != 0) {
			this->_moves.at(i).icon--;
		}
	}

	input.close();

	// set default starting state
	if (this->_moves.size()) {
		for (size_t i = 0; i < 8; i++) {
			this->AilmentBitFlags[i] = (this->_moves.at(0).stats.ailmentsBitflag & (1 << i)) > 0;
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
		this->write();
	}

	ImGui::SameLine();
	if (ImGui::Button("Copy")) {
		copyObj(&this->_moves.at(this->_moveIndex), "Move");
	}

	ImGui::SameLine();
	if (ImGui::Button("Paste")) {
		if (checkObjType("Move")) {
			this->_moves.at(this->_moveIndex) = *((MoveStruct*)pasteObj());

			for (size_t i = 0; i < 8; i++) {
				AilmentBitFlags[i] = this->_moves.at(this->_moveIndex).stats.ailmentsBitflag & (1 << i);
			}
		}
	}

	if (ImGui::BeginCombo("Move Index", this->_moves.at(this->_moveIndex).name)) {
		for (size_t i = 0; i < this->_moves.size() - 1; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moveIndex);
			if (ImGui::Selectable(this->_moves.at(i).name, is_selected)) {
				this->_moveIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		for (size_t i = 0; i < 8; i++) {
			AilmentBitFlags[i] = this->_moves.at(this->_moveIndex).stats.ailmentsBitflag & (1 << i);
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Icon", iconDefs.at(this->_moves.at(this->_moveIndex).icon).c_str())) {
		for (size_t i = 0; i < iconDefs.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).icon);
			if (ImGui::Selectable(iconDefs.at(i).c_str(), is_selected)) {
				this->_moves.at(this->_moveIndex).icon = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", this->_moves.at(this->_moveIndex).name, 19);
	ImGui::InputUShort("Cost", &this->_moves.at(this->_moveIndex).stats.cost);

	if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(this->_moves.at(this->_moveIndex).stats.targetEffect).c_str())) {
		for (uint8_t i = 0; i < 16; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.targetEffect);
			if (ImGui::Selectable(targetEffectDefs.at(i).c_str(), is_selected)) {
				this->_moves.at(this->_moveIndex).stats.targetEffect = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(this->_moves.at(this->_moveIndex).stats.targetType).c_str())) {
		for (uint8_t i = 0; i < 16; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.targetType);
			if (ImGui::Selectable(targetTypeDefs.at(i).c_str(), is_selected)) {
				this->_moves.at(this->_moveIndex).stats.targetType = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputUShort("Strength", &this->_moves.at(this->_moveIndex).stats.str);
	ImGui::InputUShort("Power", &this->_moves.at(this->_moveIndex).stats.pow);
	ImGui::InputUShort("Range", &this->_moves.at(this->_moveIndex).stats.range);
	ImGui::InputUShort2("Cast Time Lv1/Lv5", &this->_moves.at(this->_moveIndex).stats.cast1);
	ImGui::InputUShort("Recovery", &this->_moves.at(this->_moveIndex).stats.recovery);

	if (ImGui::BeginCombo("Animation", animationDefs.at(this->_moves.at(this->_moveIndex).stats.animation).c_str())) {
		for (size_t i = 0; i < this->_moves.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.animation);
			if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
				this->_moves.at(this->_moveIndex).stats.animation = (uint16_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputUByte("Unknown #1", &this->_moves.at(this->_moveIndex).stats.unknown1);
	ImGui::InputUByte("Knockdown", &this->_moves.at(this->_moveIndex).stats.knockDown);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");
	ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_moves.at(this->_moveIndex).stats.ipStun);
	ImGui::InputShort("Knockback", &this->_moves.at(this->_moveIndex).stats.knockback);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

	if (ImGui::BeginCombo("Element", elementDefs.at(this->_moves.at(this->_moveIndex).stats.element).c_str())) {
		for (uint8_t i = 0; i < 5; i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.element);
			if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
				this->_moves.at(this->_moveIndex).stats.element = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	ImGui::InputUByte("Element Strength", &this->_moves.at(this->_moveIndex).stats.elementStr);

	for (size_t i = 0; i < 8; i++) {
		if (ImGui::Checkbox(statusDefs.at(i).c_str(), &AilmentBitFlags[i])) {
			this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (1 << i);
		}
		if ((i+1) % 4) {
			ImGui::SameLine();
		}
	}

	ImGui::InputUByte("Ailments Chance", &this->_moves.at(this->_moveIndex).stats.ailmentsChance);
	ImGui::InputByte4("Atk/Def/Act/Mov Mods", &this->_moves.at(this->_moveIndex).stats.atkMod);
	ImGui::InputUShort("Special", &this->_moves.at(this->_moveIndex).stats.special);
	ImGui::InputUShort("Coin Cost Lv1", &this->_moves.at(this->_moveIndex).stats.coinCost1);
	ImGui::InputUShort("Coin Cost Lv2", &this->_moves.at(this->_moveIndex).stats.coinCost2);
	ImGui::InputUShort("Coin Cost Lv3", &this->_moves.at(this->_moveIndex).stats.coinCost3);
	ImGui::InputUShort("Coin Cost Lv4", &this->_moves.at(this->_moveIndex).stats.coinCost4);
	ImGui::InputUShort("Coin Cost Lv5", &this->_moves.at(this->_moveIndex).stats.coinCost5);
	ImGui::InputUShort("Multiplier", &this->_moves.at(this->_moveIndex).stats.multiplier);
	ImGui::InputText("Description", this->_moves.at(this->_moveIndex).description, 41);

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

	for (const auto& val : this->_moves) {
		output << std::to_string(val.id) << ','
			<< std::to_string(val.icon) << ','
			<< val.name << ','
			<< std::to_string(val.stats.cost) << ','
			<< targetEffectDefs.at(val.stats.targetEffect) << ','
			<< targetTypeDefs.at(val.stats.targetType) << ','
			<< std::to_string(val.stats.str) << ','
			<< std::to_string(val.stats.pow) << ','
			<< std::to_string(val.stats.range) << ','
			<< std::to_string(val.stats.cast1) << ','
			<< std::to_string(val.stats.cast5) << ','
			<< std::to_string(val.stats.recovery) << ','
			<< this->_moves.at(val.stats.animation).name << ','
			<< std::to_string(val.stats.unknown1) << ','
			<< std::to_string(val.stats.knockDown) << ','
			<< std::to_string(val.stats.ipStun) << ','
			<< std::to_string(val.stats.ipCancelStun) << ','
			<< std::to_string(val.stats.knockback) << ','
			<< elementDefs.at(val.stats.element) << ','
			<< std::to_string(val.stats.elementStr) << ','
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