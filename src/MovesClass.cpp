#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include ".\include\MovesClass.h"

#include ".\include\common\io_util.h"
#include ".\include\common\char_constants.h"
#include ".\include\common\string_manip.h"
#include ".\include\common\copypaste_obj.h"

#include ".\imgui.h"

void MovesClass::write() {

	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("MS_PARAM.BIN not found to be written!");

	for (size_t i = 0; i < this->_moves.size(); i++) {

		this->_moves.at(i).icon++;				//	icon is offset by 1, adjust first
		this->_moves.at(i).stats.atkMod %= 6;	//	stat changes are limited to between -5 and 5
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

void MovesClass::read(std::string filename) {

	this->_filename = filename;

	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("MS_PARAM.BIN not found to be read!");

	std::filesystem::path filePath(this->_filename);
	size_t fileSize = std::filesystem::file_size(filePath);

	this->_moves.resize(fileSize / 108);		//	entries are 108 bytes long

	for (int i = 0; i < this->_moves.size(); i++) {

		readRaw<uint8_t>(input);
		this->_moves.at(i).id = i;	//	overwrite all IDs, we want them to be equal to the offset of the entry in the file

		this->_moves.at(i).icon = readRaw<uint8_t>(input);

		input.read(&this->_moves.at(i).name[0], 18);
		replaceNulls(this->_moves.at(i).name, 18);	//	strings are not null terminated, we cannot read them from the file like everything else

		this->_moves[i].stats = readRaw<MoveStatsStruct>(input);

		input.read(&this->_moves.at(i).description[0], 40);
		replaceNulls(this->_moves.at(i).description, 40);

		if (this->_moves.at(i).icon != 0)
			this->_moves.at(i).icon--;

	}

	input.close();

}

void MovesClass::draw() {

	ImGui::Begin("MS_PARAM");

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

			for (size_t i = 0; i < 8; i++)
				AilmentBitFlags[i] = this->_moves.at(this->_moveIndex).stats.ailmentsBitflag & (1 << i);

		}

	}

	if (ImGui::BeginCombo("Move Index", this->_moves.at(this->_moveIndex).name)) {

		for (size_t i = 0; i < this->_moves.size() - 1; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_moveIndex);
			if (ImGui::Selectable(this->_moves.at(i).name, is_selected))
				this->_moveIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		for (size_t i = 0; i < 8; i++)
			AilmentBitFlags[i] = this->_moves.at(this->_moveIndex).stats.ailmentsBitflag & (1 << i);

		ImGui::EndCombo();

	}

	ImGui::Combo("Icon", &this->_moves.at(this->_moveIndex).icon, moveIcons, 17);
	ImGui::InputText("Name", this->_moves.at(this->_moveIndex).name, 19);
	ImGui::InputUShort("Cost", &this->_moves.at(this->_moveIndex).stats.cost);

	if (ImGui::BeginCombo("Target Effect", targetEffects[this->_moves.at(this->_moveIndex).stats.targetEffect])) {
		for (size_t i = 0; i < 16; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.targetEffect);
			if (ImGui::Selectable(targetEffects[i], is_selected))
				this->_moves.at(this->_moveIndex).stats.targetEffect = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	if (ImGui::BeginCombo("Target Type", targetTypes[this->_moves.at(this->_moveIndex).stats.targetType])) {
		for (size_t i = 0; i < 16; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.targetType);
			if (ImGui::Selectable(targetTypes[i], is_selected))
				this->_moves.at(this->_moveIndex).stats.targetType = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::InputUShort("Strength", &this->_moves.at(this->_moveIndex).stats.str);
	ImGui::InputUShort("Power", &this->_moves.at(this->_moveIndex).stats.pow);
	ImGui::InputUShort("Range", &this->_moves.at(this->_moveIndex).stats.range);

	ImGui::InputUShort2("Cast Time Lv1/Lv5", &this->_moves.at(this->_moveIndex).stats.cast1);

	ImGui::InputUShort("Recovery", &this->_moves.at(this->_moveIndex).stats.recovery);

	if (ImGui::BeginCombo("Animation", animationIDs[this->_moves.at(this->_moveIndex).stats.animation])) {

		for (size_t i = 0; i < this->_moves.size(); i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.animation);
			if (ImGui::Selectable(animationIDs[i], is_selected))
				this->_moves.at(this->_moveIndex).stats.animation = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::InputUByte("Unknown #1", &this->_moves.at(this->_moveIndex).stats.unknown1);
	ImGui::InputUByte("Knockdown", &this->_moves.at(this->_moveIndex).stats.knockDown);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");

	ImGui::InputShort2("IP Stun/IP Cancel Stun", &this->_moves.at(this->_moveIndex).stats.ipStun);

	ImGui::InputShort("Knockback", &this->_moves.at(this->_moveIndex).stats.knockback);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

	if (ImGui::BeginCombo("Element", elements[this->_moves.at(this->_moveIndex).stats.element])) {

		for (size_t i = 0; i < 5; i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_moves.at(this->_moveIndex).stats.element);
			if (ImGui::Selectable(elements[i], is_selected))
				this->_moves.at(this->_moveIndex).stats.element = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::InputUByte("Element Strength", &this->_moves.at(this->_moveIndex).stats.elementStr);

	if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[0] << 0);
	ImGui::SameLine();
	if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[1] << 1);
	ImGui::SameLine();
	if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[2] << 2);
	ImGui::SameLine();
	if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[3] << 3);

	if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[4] << 4);
	ImGui::SameLine();
	if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[5] << 5);
	ImGui::SameLine();
	if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[6] << 6);
	ImGui::SameLine();
	if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
		this->_moves.at(this->_moveIndex).stats.ailmentsBitflag ^= (AilmentBitFlags[7] << 7);

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

void MovesClass::outputToCSV() {

	std::ofstream output;
	output.open(".\\csv\\MS_PARAM.CSV");

	if (!output.is_open())
		return;

	output  << "index,icon,name,cost,target effect,target type,strength,power,range,cast Lv1,castLv5,Recovery,Animation,???,knockdown,IP Damage,IP Cancel Damage,knockback,element,element strength,status effect bitflag,"
			<< "status effect chance, ATK change, DEF change, ACT change, MOV change, special effect, cost Lv1, cost Lv2, cost Lv3, cost Lv4, cost Lv5, power multiplier, description\n";

	for (const auto& val : this->_moves) {

		output << std::to_string(val.id) << ','
			<< std::to_string(val.icon) << ','
			<< val.name << ','
			<< std::to_string(val.stats.cost) << ','
			<< targetEffects[val.stats.targetEffect] << ','
			<< targetTypes[val.stats.targetType] << ','
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
			<< elements[val.stats.element] << ','
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