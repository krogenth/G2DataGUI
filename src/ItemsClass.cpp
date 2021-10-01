#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include ".\include\ItemsClass.h"

#include ".\include\common\io_util.h"
#include ".\include\common\char_constants.h"
#include ".\include\common\string_manip.h"
#include ".\include\common\copypaste_obj.h"

#include ".\imgui.h"

void ItemsClass::write() {

	ImU32 dataOffset = 0x0000F9B0;	//80 * 799
	ImU32 badOffset = 0xFFFFFFFF;

	std::ofstream output;
	output.open(this->_filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("ITEM.BIN not found to be written!");

	for (size_t i = 0; i < this->_items.size(); i++) {

		output.write(this->_items.at(i).name, 18);
		output.write(this->_items.at(i).description, 40);

		writeRaw<ItemStatsStruct>(output, this->_items.at(i).stats);

		if (!this->_items.at(i).equipmentOffset)
			writeRaw<ImU32>(output, badOffset);
		else {

			writeRaw<ImU32>(output, dataOffset);
			dataOffset += sizeof(EquipmentStruct);

		}

		if (!this->_items.at(i).usableOffset)
			writeRaw<ImU32>(output, badOffset);
		else {

			writeRaw<ImU32>(output, dataOffset);
			dataOffset += sizeof(UsableStruct);

		}

		writeRaw<ImU32>(output, this->_items.at(i).id);

	}

	for (size_t i = 0; i < this->_items.size(); i++) {

		if (this->_items.at(i).equipmentOffset)
			writeRaw<EquipmentStruct>(output, *(this->_items.at(i).equipmentOffset));

		if (this->_items.at(i).usableOffset)
			writeRaw<UsableStruct>(output, *(this->_items.at(i).usableOffset));

	}

	output.close();

}

void ItemsClass::read(std::string filename) {

	this->_filename = filename;

	std::ifstream input(this->_filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("ITEM.BIN not found to be read!");

	char* readByte = new char[4]{};
	std::streampos pos = 0;

	this->_items.resize(0x31F);		//entries are broken down into 3 possible parts(first is always there, 80 bytes long; second is equipment, 28 bytes long; third is usables, 32 bytes long)

	for (size_t i = 0; i < this->_items.size(); i++) {

		input.read(this->_items.at(i).name, 18);
		replaceNulls(this->_items.at(i).name, 18);

		input.read(this->_items.at(i).description, 40);
		replaceNulls(this->_items.at(i).description, 40);

		this->_items.at(i).stats = readRaw<ItemStatsStruct>(input);

		input.read(readByte, 4);
		pos = input.tellg();

		//check if equipment offset is available
		if (((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])) > 0) {

			this->_items.at(i).equipmentOffset = new EquipmentStruct;
			input.seekg(((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])), std::ios::beg);

			*(this->_items.at(i).equipmentOffset) = readRaw<EquipmentStruct>(input);

		}

		input.seekg(pos, std::ios::beg);

		input.read(readByte, 4);
		pos = input.tellg();

		if (((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])) > 0) {

			this->_items.at(i).usableOffset = new UsableStruct;
			input.seekg(((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])), std::ios::beg);

			*(this->_items.at(i).usableOffset) = readRaw<UsableStruct>(input);

		}

		input.seekg(pos, std::ios::beg);

		input.read(readByte, 4);
		this->_items.at(i).id = i;

	}

	input.close();

}

void ItemsClass::draw() {

	ImGui::Begin("ITEM");

	if (ImGui::Button("Save")) {

		this->write();

	}

	ImGui::SameLine();
	if (ImGui::Button("Copy")) {

		copyObj(&this->_items.at(this->_itemIndex), "Item");

	}

	ImGui::SameLine();
	if (ImGui::Button("Paste")) {

		if (checkObjType("Item")) {

			ItemStruct* tempObj = ((ItemStruct*)pasteObj());

			this->_items.at(this->_itemIndex) = *tempObj;

			if (tempObj->equipmentOffset) {

				if (!this->_items.at(this->_itemIndex).equipmentOffset)
					this->_items.at(this->_itemIndex).equipmentOffset = new EquipmentStruct;

				*(this->_items.at(this->_itemIndex).equipmentOffset) = *(tempObj->equipmentOffset);

				for (size_t i = 0; i < 7; i++)
					EquipmentCharacterBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag & (1 << i);

				for (size_t i = 0; i < 8; i++)
					EquipmentAilmentBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag & (1 << i);

				this->_hasEquip = true;

			}
			else if (this->_items.at(this->_itemIndex).equipmentOffset) {

				this->_hasEquip = false;
				delete this->_items.at(this->_itemIndex).equipmentOffset;

			}

			if (tempObj->usableOffset) {

				if (!this->_items.at(this->_itemIndex).usableOffset)
					this->_items.at(this->_itemIndex).usableOffset = new UsableStruct;

				*(this->_items.at(this->_itemIndex).usableOffset) = *(tempObj->usableOffset);

				for (size_t i = 0; i < 8; i++)
					UsableAilmentBitFlags[i] = this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag & (1 << i);

				this->_hasUsable = true;

			}
			else if (this->_items.at(this->_itemIndex).usableOffset) {

				delete this->_items.at(this->_itemIndex).usableOffset;
				this->_hasUsable = false;

			}

		}

	}

	if (ImGui::BeginCombo("Item Index", this->_items.at(this->_itemIndex).name)) {

		for (size_t i = 0; i < this->_items.size(); i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_itemIndex);
			if (ImGui::Selectable(this->_items.at(i).name, is_selected))
				this->_itemIndex = i;
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		(this->_items.at(this->_itemIndex).equipmentOffset) ? this->_hasEquip = true : this->_hasEquip = false;
		(this->_items.at(this->_itemIndex).usableOffset) ? this->_hasUsable = true : this->_hasUsable = false;

		if (this->_hasEquip) {

			for (size_t i = 0; i < 7; i++)
				EquipmentCharacterBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag & (1 << i);

			for (size_t i = 0; i < 8; i++)
				EquipmentAilmentBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag & (1 << i);

		}
		else {

			for (size_t i = 0; i < 7; i++)
				EquipmentCharacterBitFlags[i] = 0;

			for (size_t i = 0; i < 8; i++)
				EquipmentAilmentBitFlags[i] = 0;

		}

		if (this->_hasUsable)
			for (size_t i = 0; i < 8; i++)
				UsableAilmentBitFlags[i] = this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag & (1 << i);
		else
			for (size_t i = 0; i < 8; i++)
				UsableAilmentBitFlags[i] = 0;

		ImGui::EndCombo();

	}

	//ImGui::LabelText("ID", std::to_string(items[itemID].id).c_str());   //it does come last in the file, but better to allow quick finding of the ID

	ImGui::InputText("Name", this->_items.at(this->_itemIndex).name, 19);
	ImGui::InputText("Description", this->_items.at(this->_itemIndex).description, 41);

	ImGui::Combo("Entry Type", &this->_items.at(this->_itemIndex).stats.entryType, entryIDs, 16);

	ImGui::InputUByte("Unknown #1", &this->_items.at(this->_itemIndex).stats.unknown1);
	ImGui::InputUByte("Unknown #2", &this->_items.at(this->_itemIndex).stats.unknown2);
	ImGui::InputUByte("Unknown #3", &this->_items.at(this->_itemIndex).stats.unknown3);
	ImGui::InputUByte("Icon", &this->_items.at(this->_itemIndex).stats.icon);
	ImGui::InputUByte("Unknown #5", &this->_items.at(this->_itemIndex).stats.unknown4);

	ImGui::InputUInt("Price", &this->_items.at(this->_itemIndex).stats.price);						if (ImGui::IsItemHovered()) ImGui::SetTooltip("sell = price / 2");

	if (ImGui::Checkbox("Equipment", &this->_hasEquip)) {

		if (this->_hasEquip)
			this->_items.at(this->_itemIndex).equipmentOffset = new EquipmentStruct;
		else {

			delete this->_items.at(this->_itemIndex).equipmentOffset;
			this->_items.at(this->_itemIndex).equipmentOffset = nullptr;

			for (size_t i = 0; i < 8; i++)
				EquipmentAilmentBitFlags[i] = 0;

		}

	}

	ImGui::SameLine();
	if (ImGui::Checkbox("Usable", &this->_hasUsable)) {

		if (this->_hasUsable)
			this->_items.at(this->_itemIndex).usableOffset = new UsableStruct;
		else {

			delete this->_items.at(this->_itemIndex).usableOffset;
			this->_items.at(this->_itemIndex).usableOffset = nullptr;

			for (size_t i = 0; i < 8; i++)
				UsableAilmentBitFlags[i] = 0;

		}

	}

	ImGui::End();

	if (this->_hasEquip) { //Item Equippable WINDOW

		ImGui::Begin("Item Equipment");

		if (ImGui::Checkbox("Ryudo", &EquipmentCharacterBitFlags[0]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Elena", &EquipmentCharacterBitFlags[1]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Millenia", &EquipmentCharacterBitFlags[2]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Roan", &EquipmentCharacterBitFlags[3]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[3] << 3);

		if (ImGui::Checkbox("Tio", &EquipmentCharacterBitFlags[4]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Mareg", &EquipmentCharacterBitFlags[5]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Prince Roan", &EquipmentCharacterBitFlags[6]))
			this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[6] << 6);

		ImGui::InputShort("Strength", &this->_items.at(this->_itemIndex).equipmentOffset->str);
		ImGui::InputShort("Vitality", &this->_items.at(this->_itemIndex).equipmentOffset->vit);
		ImGui::InputShort("Action", &this->_items.at(this->_itemIndex).equipmentOffset->act);
		ImGui::InputShort("Movement", &this->_items.at(this->_itemIndex).equipmentOffset->mov);

		if (ImGui::BeginCombo("Effective On", effectiveOn[this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn])) {

			for (size_t i = 0; i < 16; i++) {

				bool is_selected = (i == this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn);
				if (ImGui::Selectable(effectiveOn[i], is_selected))
					this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();

			}

			ImGui::EndCombo();

		}

		ImGui::InputByte("Fire %", &this->_items.at(this->_itemIndex).equipmentOffset->fireAffinity);
		ImGui::InputByte("Wind %", &this->_items.at(this->_itemIndex).equipmentOffset->windAffinity);
		ImGui::InputByte("Earth %", &this->_items.at(this->_itemIndex).equipmentOffset->earthAffinity);
		ImGui::InputByte("Lightning %", &this->_items.at(this->_itemIndex).equipmentOffset->lightningAffinity);
		ImGui::InputByte("Blizzard %", &this->_items.at(this->_itemIndex).equipmentOffset->blizzardAffinity);

		if (ImGui::Checkbox("Poison", &EquipmentAilmentBitFlags[0]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &EquipmentAilmentBitFlags[1]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &EquipmentAilmentBitFlags[2]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &EquipmentAilmentBitFlags[3]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &EquipmentAilmentBitFlags[4]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &EquipmentAilmentBitFlags[5]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &EquipmentAilmentBitFlags[6]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &EquipmentAilmentBitFlags[7]))
			this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[7] << 7);

		ImGui::InputUByte("Ailments Chance", &this->_items.at(this->_itemIndex).equipmentOffset->ailmentsChance);

		ImGui::InputByte("Increase Fire %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseFirePercent);
		ImGui::InputByte("Increase Wind %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseWindPercent);
		ImGui::InputByte("Increase Earth %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseEarthPercent);
		ImGui::InputByte("Increase Lightning %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseLightningPercent);
		ImGui::InputByte("Increase Blizzard %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseBlizzardPercent);
		ImGui::InputByte("Increase Water %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseWaterPercent);
		ImGui::InputByte("Increase Explosion %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseExplosionPercent);
		ImGui::InputByte("Increase Forest %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseForestPercent);

		ImGui::InputUShort("Special", &this->_items.at(this->_itemIndex).equipmentOffset->special);

		ImGui::End();

	}

	if (this->_hasUsable) {    //Item Usable WINDOW

		ImGui::Begin("Item Usable");

		if (ImGui::BeginCombo("Target Effect", targetEffects[this->_items.at(this->_itemIndex).usableOffset->targetEffect])) {
			for (size_t i = 0; i < 16; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->targetEffect);
				if (ImGui::Selectable(targetEffects[i], is_selected))
					this->_items.at(this->_itemIndex).usableOffset->targetEffect = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			ImGui::EndCombo();

		}

		if (ImGui::BeginCombo("Target Type", targetTypes[this->_items.at(this->_itemIndex).usableOffset->targetType])) {
			for (size_t i = 0; i < 16; i++) {

				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->targetType);
				if (ImGui::Selectable(targetTypes[i], is_selected))
					this->_items.at(this->_itemIndex).usableOffset->targetType = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();

			}

			ImGui::EndCombo();

		}

		ImGui::InputUShort("Power", &this->_items.at(this->_itemIndex).usableOffset->power);
		ImGui::InputUShort("Range", &this->_items.at(this->_itemIndex).usableOffset->range);
		ImGui::InputUShort("Cast Time", &this->_items.at(this->_itemIndex).usableOffset->castTime);
		ImGui::InputUShort("Recovery", &this->_items.at(this->_itemIndex).usableOffset->recoveryTime);

		if (ImGui::BeginCombo("Animation", animationIDs[this->_items.at(this->_itemIndex).usableOffset->animation])) {

			for (size_t i = 0; i < this->_numMoves; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->animation);
				if (ImGui::Selectable(animationIDs[i], is_selected))
					this->_items.at(this->_itemIndex).usableOffset->animation = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			ImGui::EndCombo();

		}

		if (ImGui::BeginCombo("Effective On", effectiveOn[this->_items.at(this->_itemIndex).usableOffset->effectiveOn])) {

			for (size_t i = 0; i < 16; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->effectiveOn);
				if (ImGui::Selectable(effectiveOn[i], is_selected))
					this->_items.at(this->_itemIndex).usableOffset->effectiveOn = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			ImGui::EndCombo();

		}

		ImGui::InputUByte("Unknown #1", &this->_items.at(this->_itemIndex).usableOffset->unknown1);

		ImGui::InputShort2("IP Damage/IP Cancel Damage", &this->_items.at(this->_itemIndex).usableOffset->ipDamage);
		ImGui::InputShort("Knockback", &this->_items.at(this->_itemIndex).usableOffset->knockback);

		if (ImGui::BeginCombo("Element", elements[this->_items.at(this->_itemIndex).usableOffset->element])) {

			for (size_t i = 0; i < 5; i++) {

				ImGui::PushID(i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->element);
				if (ImGui::Selectable(elements[i], is_selected))
					this->_items.at(this->_itemIndex).usableOffset->element = i;
				if (is_selected)
					ImGui::SetItemDefaultFocus();
				ImGui::PopID();

			}

			ImGui::EndCombo();

		}

		ImGui::InputUByte("Element Strength", &this->_items.at(this->_itemIndex).usableOffset->elementStr);

		if (ImGui::Checkbox("Poison", &UsableAilmentBitFlags[0]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &UsableAilmentBitFlags[1]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &UsableAilmentBitFlags[2]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &UsableAilmentBitFlags[3]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &UsableAilmentBitFlags[4]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &UsableAilmentBitFlags[5]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &UsableAilmentBitFlags[6]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &UsableAilmentBitFlags[7]))
			this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[7] << 7);

		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &this->_items.at(this->_itemIndex).usableOffset->atkMod);

		ImGui::InputUByte("Break Chance", &this->_items.at(this->_itemIndex).usableOffset->breakChance);
		ImGui::InputUByte("Special", &this->_items.at(this->_itemIndex).usableOffset->special);

		ImGui::InputUByte("Unknown #2", &this->_items.at(this->_itemIndex).usableOffset->unknown2);
		ImGui::InputUByte("Unknown #3", &this->_items.at(this->_itemIndex).usableOffset->unknown3);

		ImGui::End();

	}

}

void ItemsClass::outputToCSV() {



}

void ItemsClass::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());
	for (size_t i = 0; i < this->_items.size(); i += 100) {
		if (i == 0)
			std::shuffle(this->_items.begin() + i + 1, this->_items.begin() + i + 99, g);
		else
			std::shuffle(this->_items.begin() + i, this->_items.begin() + i + 99, g);
	}
	this->_hasUsable = (this->_items.at(this->_itemIndex).usableOffset > 0);
	this->_hasEquip = (this->_items.at(this->_itemIndex).equipmentOffset > 0);
}