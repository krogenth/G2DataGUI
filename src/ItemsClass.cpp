#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include "./include/ItemsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/char_constants.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./imgui.h"

void Items::write() {
	ImU32 dataOffset = 0x0000F9B0; // 80 * 799
	ImU32 badOffset = 0xFFFFFFFF;

	std::ofstream stream;
	stream.open(this->_filename, std::ios::binary);

	if (!stream.is_open()) {
		throw new std::exception("ITEM.BIN not found to be written!");
	}

	for (size_t i = 0; i < this->_items.size(); i++) {
		stream.write(this->_items.at(i).name, 18);
		stream.write(this->_items.at(i).description, 40);

		writeRaw<ItemStatsStruct>(stream, this->_items.at(i).stats);

		if (!this->_items.at(i).equipmentOffset) {
			writeRaw<ImU32>(stream, badOffset);
		}
		else {
			writeRaw<ImU32>(stream, dataOffset);
			dataOffset += sizeof(EquipmentStruct);
		}

		if (!this->_items.at(i).usableOffset) {
			writeRaw<ImU32>(stream, badOffset);
		}
		else {
			writeRaw<ImU32>(stream, dataOffset);
			dataOffset += sizeof(UsableStruct);
		}

		writeRaw<ImU32>(stream, this->_items.at(i).id);
	}

	for (size_t i = 0; i < this->_items.size(); i++) {
		if (this->_items.at(i).equipmentOffset) {
			writeRaw<EquipmentStruct>(stream, *(this->_items.at(i).equipmentOffset));
		}
		if (this->_items.at(i).usableOffset) {
			writeRaw<UsableStruct>(stream, *(this->_items.at(i).usableOffset));
		}
	}

	stream.close();
}

void Items::read() {
	std::ifstream stream(this->_filename, std::ios::binary);

	if (!stream.is_open()) {
		throw new std::exception("ITEM.BIN not found to be read!");
	}

	std::streampos pos = 0;
	this->_items.resize(0x31F); // entries are broken down into 3 possible parts(first is always there, 80 bytes long; second is equipment, 28 bytes long; third is usables, 32 bytes long)

	for (size_t i = 0; i < this->_items.size(); i++) {
		stream.read(this->_items.at(i).name, 18);
		replaceNulls(this->_items.at(i).name, 18);
		stream.read(this->_items.at(i).description, 40);
		replaceNulls(this->_items.at(i).description, 40);
		this->_items.at(i).stats = readRaw<ItemStatsStruct>(stream);

		// check if equipment offset is available
		int32_t equipOffset = readRaw<int32_t>(stream);
		pos = stream.tellg();
		if (equipOffset > 0) {
			this->_items.at(i).equipmentOffset = new EquipmentStruct;
			stream.seekg(equipOffset, std::ios::beg);
			*(this->_items.at(i).equipmentOffset) = readRaw<EquipmentStruct>(stream);
		}

		// check if usable offset is available
		stream.seekg(pos, std::ios::beg);

		int32_t usableOffset = readRaw<int32_t>(stream);
		pos = stream.tellg();
		if (usableOffset > 0) {
			this->_items.at(i).usableOffset = new UsableStruct;
			stream.seekg(usableOffset, std::ios::beg);
			*(this->_items.at(i).usableOffset) = readRaw<UsableStruct>(stream);
		}

		stream.seekg(pos, std::ios::beg);
		readRaw<uint32_t>(stream); // read in last four bytes, they represent the id but can be overwritten
		this->_items.at(i).id = (uint32_t)i;
	}

	stream.close();
}

void Items::draw() {
	ImGui::Begin("ITEMS");

	if (ImGui::Button("Save")) {
		this->write();
	}

	ImGui::SameLine();
	if (ImGui::Button("Copy")) {
		copyObj(&this->_items.at(this->_itemIndex), "Item");
	}

	ImGui::SameLine();
	if (ImGui::Button("Paste") && checkObjType("Item")) {
		ItemStruct* tempObj = ((ItemStruct*)pasteObj());
		this->_items.at(this->_itemIndex) = *tempObj;

		if (tempObj->equipmentOffset) {
			if (!this->_items.at(this->_itemIndex).equipmentOffset) {
				this->_items.at(this->_itemIndex).equipmentOffset = new EquipmentStruct;
			}

			*(this->_items.at(this->_itemIndex).equipmentOffset) = *(tempObj->equipmentOffset);

			for (size_t i = 0; i < 7; i++) {
				EquipmentCharacterBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag & (1 << i);
			}

			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag & (1 << i);
			}

			this->_hasEquip = true;
		}
		else if (this->_items.at(this->_itemIndex).equipmentOffset) {
			this->_hasEquip = false;
			delete this->_items.at(this->_itemIndex).equipmentOffset;
		}

		if (tempObj->usableOffset) {
			if (!this->_items.at(this->_itemIndex).usableOffset) {
				this->_items.at(this->_itemIndex).usableOffset = new UsableStruct;
			}

			*(this->_items.at(this->_itemIndex).usableOffset) = *(tempObj->usableOffset);

			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag & (1 << i);
			}

			this->_hasUsable = true;
		}
		else if (this->_items.at(this->_itemIndex).usableOffset) {
			delete this->_items.at(this->_itemIndex).usableOffset;
			this->_hasUsable = false;
		}
	}

	if (ImGui::BeginCombo("Item Index", this->_items.at(this->_itemIndex).name)) {
		for (size_t i = 0; i < this->_items.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_itemIndex);
			if (ImGui::Selectable(this->_items.at(i).name, is_selected)) {
				this->_itemIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		(this->_items.at(this->_itemIndex).equipmentOffset) ? this->_hasEquip = true : this->_hasEquip = false;
		(this->_items.at(this->_itemIndex).usableOffset) ? this->_hasUsable = true : this->_hasUsable = false;

		if (this->_hasEquip) {
			for (size_t i = 0; i < 7; i++) {
				EquipmentCharacterBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag & (1 << i);
			}
			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag & (1 << i);
			}
		}
		else {
			for (size_t i = 0; i < 7; i++) {
				EquipmentCharacterBitFlags[i] = false;
			}

			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = false;
			}

		}

		if (this->_hasUsable) {
			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag & (1 << i);
			}
		}
		else {
			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = 0;
			}
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", this->_items.at(this->_itemIndex).name, 19);
	ImGui::InputText("Description", this->_items.at(this->_itemIndex).description, 41);
	ImGui::Combo("Entry Type", &this->_items.at(this->_itemIndex).stats.entryType, entryIDs, 16);
	ImGui::InputUByte("Unknown #1", &this->_items.at(this->_itemIndex).stats.unknown1);
	ImGui::InputUByte("Unknown #2", &this->_items.at(this->_itemIndex).stats.unknown2);
	ImGui::InputUByte("Unknown #3", &this->_items.at(this->_itemIndex).stats.unknown3);
	ImGui::InputUByte("Icon", &this->_items.at(this->_itemIndex).stats.icon);
	ImGui::InputUByte("Unknown #5", &this->_items.at(this->_itemIndex).stats.unknown4);
	ImGui::InputUInt("Price", &this->_items.at(this->_itemIndex).stats.price);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("sell = price / 2");

	if (ImGui::Checkbox("Equipment", &this->_hasEquip)) {
		if (this->_hasEquip) {
			this->_items.at(this->_itemIndex).equipmentOffset = new EquipmentStruct;
		}
		else {
			delete this->_items.at(this->_itemIndex).equipmentOffset;
			this->_items.at(this->_itemIndex).equipmentOffset = nullptr;

			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = false;
			}
		}
	}

	ImGui::SameLine();
	if (ImGui::Checkbox("Usable", &this->_hasUsable)) {
		if (this->_hasUsable) {
			this->_items.at(this->_itemIndex).usableOffset = new UsableStruct;
		}
		else {
			delete this->_items.at(this->_itemIndex).usableOffset;
			this->_items.at(this->_itemIndex).usableOffset = nullptr;

			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = false;
			}
		}
	}

	if (this->_hasEquip) {
		if (ImGui::CollapsingHeader("Item Equipment")) {
			for (size_t i = 0; i < 7; i++) {
				if (ImGui::Checkbox(statIDs[i], &EquipmentCharacterBitFlags[i])) {
					this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[i] << i);
				}
				if ((i+1) % 4 && (i+1) < 7) {
					ImGui::SameLine();
				}
			}

			ImGui::InputShort("Strength", &this->_items.at(this->_itemIndex).equipmentOffset->str);
			ImGui::InputShort("Vitality", &this->_items.at(this->_itemIndex).equipmentOffset->vit);
			ImGui::InputShort("Action", &this->_items.at(this->_itemIndex).equipmentOffset->act);
			ImGui::InputShort("Movement", &this->_items.at(this->_itemIndex).equipmentOffset->mov);

			if (ImGui::BeginCombo("Effective On", effectiveOn[this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn])) {
				for (uint8_t i = 0; i < 16; i++) {
					bool is_selected = (i == this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn);
					if (ImGui::Selectable(effectiveOn[i], is_selected)) {
						this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
				}

				ImGui::EndCombo();
			}

			ImGui::InputByte("Fire %", &this->_items.at(this->_itemIndex).equipmentOffset->fireAffinity);
			ImGui::InputByte("Wind %", &this->_items.at(this->_itemIndex).equipmentOffset->windAffinity);
			ImGui::InputByte("Earth %", &this->_items.at(this->_itemIndex).equipmentOffset->earthAffinity);
			ImGui::InputByte("Lightning %", &this->_items.at(this->_itemIndex).equipmentOffset->lightningAffinity);
			ImGui::InputByte("Blizzard %", &this->_items.at(this->_itemIndex).equipmentOffset->blizzardAffinity);

			for (size_t i = 0; i < 8; i++) {
				if (ImGui::Checkbox(statuses[i], &EquipmentAilmentBitFlags[0])) {
					this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[0] << 0);
				}
				if ((i+1) % 4) {
					ImGui::SameLine();
				}
			}

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
		}
	}

	

	if (this->_hasUsable) {
		if (ImGui::CollapsingHeader("Item Usable")) {
			if (ImGui::BeginCombo("Target Effect", targetEffects[this->_items.at(this->_itemIndex).usableOffset->targetEffect])) {
				for (uint8_t i = 0; i < 16; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->targetEffect);
					if (ImGui::Selectable(targetEffects[i], is_selected)) {
						this->_items.at(this->_itemIndex).usableOffset->targetEffect = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Target Type", targetTypes[this->_items.at(this->_itemIndex).usableOffset->targetType])) {
				for (uint8_t i = 0; i < 16; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->targetType);
					if (ImGui::Selectable(targetTypes[i], is_selected)) {
						this->_items.at(this->_itemIndex).usableOffset->targetType = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			ImGui::InputUShort("Power", &this->_items.at(this->_itemIndex).usableOffset->power);
			ImGui::InputUShort("Range", &this->_items.at(this->_itemIndex).usableOffset->range);
			ImGui::InputUShort("Cast Time", &this->_items.at(this->_itemIndex).usableOffset->castTime);
			ImGui::InputUShort("Recovery", &this->_items.at(this->_itemIndex).usableOffset->recoveryTime);

			if (ImGui::BeginCombo("Animation", animationIDs[this->_items.at(this->_itemIndex).usableOffset->animation])) {
				for (size_t i = 0; i < this->_moves->size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->animation);
					if (ImGui::Selectable(animationIDs[i], is_selected)) {
						this->_items.at(this->_itemIndex).usableOffset->animation = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Effective On", effectiveOn[this->_items.at(this->_itemIndex).usableOffset->effectiveOn])) {
				for (uint8_t i = 0; i < 16; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->effectiveOn);
					if (ImGui::Selectable(effectiveOn[i], is_selected)) {
						this->_items.at(this->_itemIndex).usableOffset->effectiveOn = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			ImGui::InputUByte("Unknown #1", &this->_items.at(this->_itemIndex).usableOffset->unknown1);
			ImGui::InputShort2("IP Damage/IP Cancel Damage", &this->_items.at(this->_itemIndex).usableOffset->ipDamage);
			ImGui::InputShort("Knockback", &this->_items.at(this->_itemIndex).usableOffset->knockback);

			if (ImGui::BeginCombo("Element", elements[this->_items.at(this->_itemIndex).usableOffset->element])) {
				for (uint8_t i = 0; i < 5; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->element);
					if (ImGui::Selectable(elements[i], is_selected)) {
						this->_items.at(this->_itemIndex).usableOffset->element = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			ImGui::InputUByte("Element Strength", &this->_items.at(this->_itemIndex).usableOffset->elementStr);

			for (size_t i = 0; i < 8; i++) {
				if (ImGui::Checkbox(statuses[i], &EquipmentAilmentBitFlags[0])) {
					this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[0] << 0);
				}
				if ((i+1) % 4) {
					ImGui::SameLine();
				}
			}

			ImGui::InputByte4("Atk/Def/Act/Mov Mods", &this->_items.at(this->_itemIndex).usableOffset->atkMod);
			ImGui::InputUByte("Break Chance", &this->_items.at(this->_itemIndex).usableOffset->breakChance);
			ImGui::InputUByte("Special", &this->_items.at(this->_itemIndex).usableOffset->special);
			ImGui::InputUByte("Unknown #2", &this->_items.at(this->_itemIndex).usableOffset->unknown2);
			ImGui::InputUByte("Unknown #3", &this->_items.at(this->_itemIndex).usableOffset->unknown3);
		}
	}

	ImGui::End();
}

void Items::outputToCSV() {

}