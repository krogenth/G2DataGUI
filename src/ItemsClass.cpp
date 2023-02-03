#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include "./include/ItemsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/string_manip.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./include/common/imgui_wrappers.h"
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

	// set starting states for equipment and usable
	if (this->_items.at(0).equipmentOffset) {
		this->_hasEquip = true;
		for (size_t i = 0; i < 7; i++) {
			this->EquipmentCharacterBitFlags[i] = (this->_items.at(0).equipmentOffset->characterBitflag & (1 << i)) > 0;
		}
		for (size_t i = 0; i < 8; i++) {
			this->EquipmentAilmentBitFlags[i] = (this->_items.at(0).equipmentOffset->ailmentsBitflag & (1 << i)) > 0;
		}
	}
	if (this->_items.at(0).usableOffset) {
		this->_hasUsable = true;
		for (size_t i = 0; i < 8; i++) {
			this->UsableAilmentBitFlags[i] = (this->_items.at(0).usableOffset->ailmentsBitflag & (1 << i)) > 0;
		}
	}
}

void Items::draw() {
	ImGui::Begin("ITEMS");

	auto itemEntryDefs = JsonDefinitions::getInstance().getDefinitions("itemEntries");
	auto characterDefs = JsonDefinitions::getInstance().getDefinitions("characters");
	auto effectiveOnDefs = JsonDefinitions::getInstance().getDefinitions("effectiveOn");
	auto statusDefs = JsonDefinitions::getInstance().getDefinitions("statuses");
	auto targetEffectDefs = JsonDefinitions::getInstance().getDefinitions("targetEffects");
	auto targetTypeDefs = JsonDefinitions::getInstance().getDefinitions("targetTypes");
	auto animationDefs = JsonDefinitions::getInstance().getDefinitions("animations");
	auto elementDefs = JsonDefinitions::getInstance().getDefinitions("elements");

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

	if (ImGui::BeginCombo("Entry Type", itemEntryDefs.at(this->_items.at(this->_itemIndex).stats.entryType).c_str())) {
		for (size_t i = 0; i < itemEntryDefs.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == this->_items.at(this->_itemIndex).stats.entryType);
			if (ImGui::Selectable(itemEntryDefs.at(i).c_str(), is_selected)) {
				this->_items.at(this->_itemIndex).stats.entryType = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Unknown #1", &this->_items.at(this->_itemIndex).stats.unknown1);
	drawInput("Unknown #2", &this->_items.at(this->_itemIndex).stats.unknown2);
	drawInput("Unknown #3", &this->_items.at(this->_itemIndex).stats.unknown3);
	drawInput("Icon", &this->_items.at(this->_itemIndex).stats.icon);
	drawInput("Unknown #5", &this->_items.at(this->_itemIndex).stats.unknown4);
	drawInput("Price", &this->_items.at(this->_itemIndex).stats.price);
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

	ImGui::End();

	if (this->_hasEquip) {
		ImGui::Begin("ITEM EQUIPMENT");
		for (size_t i = 0; i < 7; i++) {
			if (ImGui::Checkbox(characterDefs.at(i).c_str(), &EquipmentCharacterBitFlags[i])) {
				this->_items.at(this->_itemIndex).equipmentOffset->characterBitflag ^= (1 << i);
			}
			if ((i+1) % 4 && (i+1) < 7) {
				ImGui::SameLine();
			}
		}

		drawInput("Strength", &this->_items.at(this->_itemIndex).equipmentOffset->str);
		drawInput("Vitality", &this->_items.at(this->_itemIndex).equipmentOffset->vit);
		drawInput("Action", &this->_items.at(this->_itemIndex).equipmentOffset->act);
		drawInput("Movement", &this->_items.at(this->_itemIndex).equipmentOffset->mov);

		if (ImGui::BeginCombo("Effective On", effectiveOnDefs.at(this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				bool is_selected = (i == this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn);
				if (ImGui::Selectable(effectiveOnDefs.at(i).c_str(), is_selected)) {
					this->_items.at(this->_itemIndex).equipmentOffset->effectiveOn = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
			}

			ImGui::EndCombo();
		}

		drawInput("Fire %", &this->_items.at(this->_itemIndex).equipmentOffset->fireAffinity);
		drawInput("Wind %", &this->_items.at(this->_itemIndex).equipmentOffset->windAffinity);
		drawInput("Earth %", &this->_items.at(this->_itemIndex).equipmentOffset->earthAffinity);
		drawInput("Lightning %", &this->_items.at(this->_itemIndex).equipmentOffset->lightningAffinity);
		drawInput("Blizzard %", &this->_items.at(this->_itemIndex).equipmentOffset->blizzardAffinity);

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statusDefs.at(i).c_str(), &EquipmentAilmentBitFlags[i])) {
				this->_items.at(this->_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[i] << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInput("Ailments Chance", &this->_items.at(this->_itemIndex).equipmentOffset->ailmentsChance);
		drawInput("Increase Fire %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseFirePercent);
		drawInput("Increase Wind %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseWindPercent);
		drawInput("Increase Earth %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseEarthPercent);
		drawInput("Increase Lightning %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseLightningPercent);
		drawInput("Increase Blizzard %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseBlizzardPercent);
		drawInput("Increase Water %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseWaterPercent);
		drawInput("Increase Explosion %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseExplosionPercent);
		drawInput("Increase Forest %", &this->_items.at(this->_itemIndex).equipmentOffset->increaseForestPercent);
		drawInput("Special", &this->_items.at(this->_itemIndex).equipmentOffset->special);

		ImGui::End();
	}

	if (this->_hasUsable) {
		ImGui::Begin("ITEM USABLE");
		if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(this->_items.at(this->_itemIndex).usableOffset->targetEffect).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->targetEffect);
				if (ImGui::Selectable(targetEffectDefs.at(i).c_str(), is_selected)) {
					this->_items.at(this->_itemIndex).usableOffset->targetEffect = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(this->_items.at(this->_itemIndex).usableOffset->targetType).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->targetType);
				if (ImGui::Selectable(targetTypeDefs.at(i).c_str(), is_selected)) {
					this->_items.at(this->_itemIndex).usableOffset->targetType = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Power", &this->_items.at(this->_itemIndex).usableOffset->power);
		drawInput("Range", &this->_items.at(this->_itemIndex).usableOffset->range);
		drawInput("Cast Time", &this->_items.at(this->_itemIndex).usableOffset->castTime);
		drawInput("Recovery", &this->_items.at(this->_itemIndex).usableOffset->recoveryTime);

		if (ImGui::BeginCombo("Animation", animationDefs.at(this->_items.at(this->_itemIndex).usableOffset->animation).c_str())) {
			for (size_t i = 0; i < this->_moves->size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->animation);
				if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
					this->_items.at(this->_itemIndex).usableOffset->animation = (uint16_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		if (ImGui::BeginCombo("Effective On", effectiveOnDefs.at(this->_items.at(this->_itemIndex).usableOffset->effectiveOn).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->effectiveOn);
				if (ImGui::Selectable(effectiveOnDefs.at(i).c_str(), is_selected)) {
					this->_items.at(this->_itemIndex).usableOffset->effectiveOn = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Unknown #1", &this->_items.at(this->_itemIndex).usableOffset->unknown1);
		drawInputN("IP Damage/IP Cancel Damage", &this->_items.at(this->_itemIndex).usableOffset->ipDamage, 2);
		drawInput("Knockback", &this->_items.at(this->_itemIndex).usableOffset->knockback);

		if (ImGui::BeginCombo("Element", elementDefs.at(this->_items.at(this->_itemIndex).usableOffset->element).c_str())) {
			for (uint8_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == this->_items.at(this->_itemIndex).usableOffset->element);
				if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
					this->_items.at(this->_itemIndex).usableOffset->element = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Element Strength", &this->_items.at(this->_itemIndex).usableOffset->elementStr);

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statusDefs.at(i).c_str(), &UsableAilmentBitFlags[i])) {
				this->_items.at(this->_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[i] << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInputN("Atk/Def/Act/Mov Mods", &this->_items.at(this->_itemIndex).usableOffset->atkMod, 4);
		drawInput("Break Chance", &this->_items.at(this->_itemIndex).usableOffset->breakChance);
		drawInput("Special", &this->_items.at(this->_itemIndex).usableOffset->special);
		drawInput("Unknown #2", &this->_items.at(this->_itemIndex).usableOffset->unknown2);
		drawInput("Unknown #3", &this->_items.at(this->_itemIndex).usableOffset->unknown3);

		ImGui::End();
	}
}

void Items::outputToCSV() {

}