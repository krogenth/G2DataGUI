#include <fstream>
#include <filesystem>
#include <random>
#include <iterator>
#include <algorithm>

#include "./Items.h"
#include "../moves/Moves.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void Items::write() {
	ImU32 dataOffset = 0x0000F9B0; // 80 * 799
	ImU32 badOffset = 0xFFFFFFFF;

	std::ofstream stream;
	stream.open(_filename, std::ios::binary);

	if (!stream.is_open()) {
		throw new std::exception("ITEM.BIN not found to be written!");
	}

	for (size_t i = 0; i < _items.size(); i++) {
		stream.write(_items.at(i).name, 18);
		stream.write(_items.at(i).description, 40);

		writeRaw<ItemStats>(stream, _items.at(i).stats);

		if (!_items.at(i).equipmentOffset) {
			writeRaw<ImU32>(stream, badOffset);
		}
		else {
			writeRaw<ImU32>(stream, dataOffset);
			dataOffset += sizeof(ItemEquipment);
		}

		if (!_items.at(i).usableOffset) {
			writeRaw<ImU32>(stream, badOffset);
		}
		else {
			writeRaw<ImU32>(stream, dataOffset);
			dataOffset += sizeof(ItemUsable);
		}

		writeRaw<ImU32>(stream, _items.at(i).id);
	}

	for (size_t i = 0; i < _items.size(); i++) {
		if (_items.at(i).equipmentOffset) {
			writeRaw<ItemEquipment>(stream, *(_items.at(i).equipmentOffset));
		}
		if (_items.at(i).usableOffset) {
			writeRaw<ItemUsable>(stream, *(_items.at(i).usableOffset));
		}
	}

	stream.close();
}

void Items::read() {
	std::ifstream stream(_filename, std::ios::binary);

	if (!stream.is_open()) {
		throw new std::exception("ITEM.BIN not found to be read!");
	}

	std::streampos pos = 0;
	_items.resize(0x31F); // entries are broken down into 3 possible parts(first is always there, 80 bytes long; second is equipment, 28 bytes long; third is usables, 32 bytes long)

	for (size_t i = 0; i < _items.size(); i++) {
		stream.read(_items.at(i).name, 18);
		replaceNulls(_items.at(i).name, 18);
		stream.read(_items.at(i).description, 40);
		replaceNulls(_items.at(i).description, 40);
		_items.at(i).stats = readRaw<ItemStats>(stream);

		// check if equipment offset is available
		int32_t equipOffset = readRaw<int32_t>(stream);
		pos = stream.tellg();
		if (equipOffset > 0) {
			_items.at(i).equipmentOffset = new ItemEquipment;
			stream.seekg(equipOffset, std::ios::beg);
			*(_items.at(i).equipmentOffset) = readRaw<ItemEquipment>(stream);
		}

		// check if usable offset is available
		stream.seekg(pos, std::ios::beg);

		int32_t usableOffset = readRaw<int32_t>(stream);
		pos = stream.tellg();
		if (usableOffset > 0) {
			_items.at(i).usableOffset = new ItemUsable;
			stream.seekg(usableOffset, std::ios::beg);
			*(_items.at(i).usableOffset) = readRaw<ItemUsable>(stream);
		}

		stream.seekg(pos, std::ios::beg);
		readRaw<uint32_t>(stream); // read in last four bytes, they represent the id but can be overwritten
		_items.at(i).id = (uint32_t)i;
	}

	stream.close();

	// set starting states for equipment and usable
	if (_items.at(0).equipmentOffset) {
		_hasEquip = true;
		for (size_t i = 0; i < 7; i++) {
			EquipmentCharacterBitFlags[i] = (_items.at(0).equipmentOffset->characterBitflag & (1 << i)) > 0;
		}
		for (size_t i = 0; i < 8; i++) {
			EquipmentAilmentBitFlags[i] = (_items.at(0).equipmentOffset->ailmentsBitflag & (1 << i)) > 0;
		}
	}
	if (_items.at(0).usableOffset) {
		_hasUsable = true;
		for (size_t i = 0; i < 8; i++) {
			UsableAilmentBitFlags[i] = (_items.at(0).usableOffset->ailmentsBitflag & (1 << i)) > 0;
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
		write();
	}

	ImGui::SameLine();
	if (ImGui::Button("Copy")) {
		copyObj(&_items.at(_itemIndex), "Item");
	}

	ImGui::SameLine();
	if (ImGui::Button("Paste") && checkObjType("Item")) {
		Item* tempObj = ((Item*)pasteObj());
		_items.at(_itemIndex) = *tempObj;

		if (tempObj->equipmentOffset) {
			if (!_items.at(_itemIndex).equipmentOffset) {
				_items.at(_itemIndex).equipmentOffset = new ItemEquipment;
			}

			*(_items.at(_itemIndex).equipmentOffset) = *(tempObj->equipmentOffset);

			for (size_t i = 0; i < 7; i++) {
				EquipmentCharacterBitFlags[i] = _items.at(_itemIndex).equipmentOffset->characterBitflag & (1 << i);
			}

			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = _items.at(_itemIndex).equipmentOffset->ailmentsBitflag & (1 << i);
			}

			_hasEquip = true;
		}
		else if (_items.at(_itemIndex).equipmentOffset) {
			_hasEquip = false;
			delete _items.at(_itemIndex).equipmentOffset;
		}

		if (tempObj->usableOffset) {
			if (!_items.at(_itemIndex).usableOffset) {
				_items.at(_itemIndex).usableOffset = new ItemUsable;
			}

			*(_items.at(_itemIndex).usableOffset) = *(tempObj->usableOffset);

			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = _items.at(_itemIndex).usableOffset->ailmentsBitflag & (1 << i);
			}

			_hasUsable = true;
		}
		else if (_items.at(_itemIndex).usableOffset) {
			delete _items.at(_itemIndex).usableOffset;
			_hasUsable = false;
		}
	}

	if (ImGui::BeginCombo("Item Index", _items.at(_itemIndex).name)) {
		for (size_t i = 0; i < _items.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _itemIndex);
			if (ImGui::Selectable(_items.at(i).name, is_selected)) {
				_itemIndex = i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		(_items.at(_itemIndex).equipmentOffset) ? _hasEquip = true : _hasEquip = false;
		(_items.at(_itemIndex).usableOffset) ? _hasUsable = true : _hasUsable = false;

		if (_hasEquip) {
			for (size_t i = 0; i < 7; i++) {
				EquipmentCharacterBitFlags[i] = _items.at(_itemIndex).equipmentOffset->characterBitflag & (1 << i);
			}
			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = _items.at(_itemIndex).equipmentOffset->ailmentsBitflag & (1 << i);
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

		if (_hasUsable) {
			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = _items.at(_itemIndex).usableOffset->ailmentsBitflag & (1 << i);
			}
		}
		else {
			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = 0;
			}
		}

		ImGui::EndCombo();
	}

	ImGui::InputText("Name", _items.at(_itemIndex).name, 19);
	ImGui::InputText("Description", _items.at(_itemIndex).description, 41);

	if (ImGui::BeginCombo("Entry Type", itemEntryDefs.at(_items.at(_itemIndex).stats.entryType).c_str())) {
		for (size_t i = 0; i < itemEntryDefs.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _items.at(_itemIndex).stats.entryType);
			if (ImGui::Selectable(itemEntryDefs.at(i).c_str(), is_selected)) {
				_items.at(_itemIndex).stats.entryType = (uint8_t)i;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	drawInput("Unknown #1", &_items.at(_itemIndex).stats.unknown1);
	drawInput("Unknown #2", &_items.at(_itemIndex).stats.unknown2);
	drawInput("Unknown #3", &_items.at(_itemIndex).stats.unknown3);
	drawInput("Icon", &_items.at(_itemIndex).stats.icon);
	drawInput("Unknown #5", &_items.at(_itemIndex).stats.unknown4);
	drawInput("Price", &_items.at(_itemIndex).stats.price);
	if (ImGui::IsItemHovered()) ImGui::SetTooltip("sell = price / 2");

	if (ImGui::Checkbox("Equipment", &_hasEquip)) {
		if (_hasEquip) {
			_items.at(_itemIndex).equipmentOffset = new ItemEquipment;
		}
		else {
			delete _items.at(_itemIndex).equipmentOffset;
			_items.at(_itemIndex).equipmentOffset = nullptr;

			for (size_t i = 0; i < 8; i++) {
				EquipmentAilmentBitFlags[i] = false;
			}
		}
	}

	ImGui::SameLine();
	if (ImGui::Checkbox("Usable", &_hasUsable)) {
		if (_hasUsable) {
			_items.at(_itemIndex).usableOffset = new ItemUsable;
		}
		else {
			delete _items.at(_itemIndex).usableOffset;
			_items.at(_itemIndex).usableOffset = nullptr;

			for (size_t i = 0; i < 8; i++) {
				UsableAilmentBitFlags[i] = false;
			}
		}
	}

	ImGui::End();

	if (_hasEquip) {
		ImGui::Begin("ITEM EQUIPMENT");
		for (size_t i = 0; i < 7; i++) {
			if (ImGui::Checkbox(characterDefs.at(i).c_str(), &EquipmentCharacterBitFlags[i])) {
				_items.at(_itemIndex).equipmentOffset->characterBitflag ^= (1 << i);
			}
			if ((i+1) % 4 && (i+1) < 7) {
				ImGui::SameLine();
			}
		}

		drawInput("Strength", &_items.at(_itemIndex).equipmentOffset->str);
		drawInput("Vitality", &_items.at(_itemIndex).equipmentOffset->vit);
		drawInput("Action", &_items.at(_itemIndex).equipmentOffset->act);
		drawInput("Movement", &_items.at(_itemIndex).equipmentOffset->mov);

		if (ImGui::BeginCombo("Effective On", effectiveOnDefs.at(_items.at(_itemIndex).equipmentOffset->effectiveOn).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				bool is_selected = (i == _items.at(_itemIndex).equipmentOffset->effectiveOn);
				if (ImGui::Selectable(effectiveOnDefs.at(i).c_str(), is_selected)) {
					_items.at(_itemIndex).equipmentOffset->effectiveOn = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
			}

			ImGui::EndCombo();
		}

		drawInput("Fire %", &_items.at(_itemIndex).equipmentOffset->fireAffinity);
		drawInput("Wind %", &_items.at(_itemIndex).equipmentOffset->windAffinity);
		drawInput("Earth %", &_items.at(_itemIndex).equipmentOffset->earthAffinity);
		drawInput("Lightning %", &_items.at(_itemIndex).equipmentOffset->lightningAffinity);
		drawInput("Blizzard %", &_items.at(_itemIndex).equipmentOffset->blizzardAffinity);

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statusDefs.at(i).c_str(), &EquipmentAilmentBitFlags[i])) {
				_items.at(_itemIndex).equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[i] << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInput("Ailments Chance", &_items.at(_itemIndex).equipmentOffset->ailmentsChance);
		drawInput("Increase Fire %", &_items.at(_itemIndex).equipmentOffset->increaseFirePercent);
		drawInput("Increase Wind %", &_items.at(_itemIndex).equipmentOffset->increaseWindPercent);
		drawInput("Increase Earth %", &_items.at(_itemIndex).equipmentOffset->increaseEarthPercent);
		drawInput("Increase Lightning %", &_items.at(_itemIndex).equipmentOffset->increaseLightningPercent);
		drawInput("Increase Blizzard %", &_items.at(_itemIndex).equipmentOffset->increaseBlizzardPercent);
		drawInput("Increase Water %", &_items.at(_itemIndex).equipmentOffset->increaseWaterPercent);
		drawInput("Increase Explosion %", &_items.at(_itemIndex).equipmentOffset->increaseExplosionPercent);
		drawInput("Increase Forest %", &_items.at(_itemIndex).equipmentOffset->increaseForestPercent);
		drawInput("Special", &_items.at(_itemIndex).equipmentOffset->special);

		ImGui::End();
	}

	if (_hasUsable) {
		ImGui::Begin("ITEM USABLE");
		if (ImGui::BeginCombo("Target Effect", targetEffectDefs.at(_items.at(_itemIndex).usableOffset->targetEffect).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _items.at(_itemIndex).usableOffset->targetEffect);
				if (ImGui::Selectable(targetEffectDefs.at(i).c_str(), is_selected)) {
					_items.at(_itemIndex).usableOffset->targetEffect = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		if (ImGui::BeginCombo("Target Type", targetTypeDefs.at(_items.at(_itemIndex).usableOffset->targetType).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _items.at(_itemIndex).usableOffset->targetType);
				if (ImGui::Selectable(targetTypeDefs.at(i).c_str(), is_selected)) {
					_items.at(_itemIndex).usableOffset->targetType = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Power", &_items.at(_itemIndex).usableOffset->power);
		drawInput("Range", &_items.at(_itemIndex).usableOffset->range);
		drawInput("Cast Time", &_items.at(_itemIndex).usableOffset->castTime);
		drawInput("Recovery", &_items.at(_itemIndex).usableOffset->recoveryTime);

		if (ImGui::BeginCombo("Animation", animationDefs.at(_items.at(_itemIndex).usableOffset->animation).c_str())) {
			for (size_t i = 0; i < Moves::getInstance().getMoves().size(); i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _items.at(_itemIndex).usableOffset->animation);
				if (ImGui::Selectable(animationDefs.at(i).c_str(), is_selected)) {
					_items.at(_itemIndex).usableOffset->animation = (uint16_t)i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		if (ImGui::BeginCombo("Effective On", effectiveOnDefs.at(_items.at(_itemIndex).usableOffset->effectiveOn).c_str())) {
			for (uint8_t i = 0; i < 16; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _items.at(_itemIndex).usableOffset->effectiveOn);
				if (ImGui::Selectable(effectiveOnDefs.at(i).c_str(), is_selected)) {
					_items.at(_itemIndex).usableOffset->effectiveOn = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Unknown #1", &_items.at(_itemIndex).usableOffset->unknown1);
		drawInputN("IP Damage/IP Cancel Damage", &_items.at(_itemIndex).usableOffset->ipDamage, 2);
		drawInput("Knockback", &_items.at(_itemIndex).usableOffset->knockback);

		if (ImGui::BeginCombo("Element", elementDefs.at(_items.at(_itemIndex).usableOffset->element).c_str())) {
			for (uint8_t i = 0; i < 5; i++) {
				ImGui::PushID((int)i);
				bool is_selected = (i == _items.at(_itemIndex).usableOffset->element);
				if (ImGui::Selectable(elementDefs.at(i).c_str(), is_selected)) {
					_items.at(_itemIndex).usableOffset->element = i;
				}
				if (is_selected) {
					ImGui::SetItemDefaultFocus();
				}
				ImGui::PopID();
			}

			ImGui::EndCombo();
		}

		drawInput("Element Strength", &_items.at(_itemIndex).usableOffset->elementStr);

		for (size_t i = 0; i < 8; i++) {
			if (ImGui::Checkbox(statusDefs.at(i).c_str(), &UsableAilmentBitFlags[i])) {
				_items.at(_itemIndex).usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[i] << i);
			}
			if ((i+1) % 4) {
				ImGui::SameLine();
			}
		}

		drawInputN("Atk/Def/Act/Mov Mods", &_items.at(_itemIndex).usableOffset->atkMod, 4);
		drawInput("Break Chance", &_items.at(_itemIndex).usableOffset->breakChance);
		drawInput("Special", &_items.at(_itemIndex).usableOffset->special);
		drawInput("Unknown #2", &_items.at(_itemIndex).usableOffset->unknown2);
		drawInput("Unknown #3", &_items.at(_itemIndex).usableOffset->unknown3);

		ImGui::End();
	}
}

void Items::outputToCSV() {

}