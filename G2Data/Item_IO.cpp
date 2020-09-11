#include <fstream>
#include <exception>

#include "StringManip.h"
#include "ItemStruct.h"
#include "io_util.h"
#include "char_constants.h"

extern bool isHDVersion;

void writeITE(std::vector<ItemStruct>& items, std::string filename) {

	ImU32 offset = 0x0000F9B0;	//80 * 799
	ImU32 badOffset = 0xFFFFFFFF;

	std::ofstream output(filename, std::ios::binary);

	if (!output.is_open())
		throw new std::exception("ITEM.BIN not found to be written!");

	for (size_t i = 0; i < items.size(); i++) {

		output.write(items[i].name, 18);
		output.write(items[i].description, 40);

		writeRaw<ItemStatsStruct>(output, items[i].stats);

		if (!items[i].equipmentOffset)
			writeRaw<ImU32>(output, badOffset);
		else {

			writeRaw<ImU32>(output, offset);
			offset += sizeof(EquipmentStruct);

		}

		if (!items[i].usableOffset)
			writeRaw<ImU32>(output, badOffset);
		else {

			writeRaw<ImU32>(output, offset);
			offset += sizeof(UsableStruct);

		}

		writeRaw<ImU32>(output, items[i].id);

	}

	for (size_t i = 0; i < items.size(); i++) {

		if (items[i].equipmentOffset)
			writeRaw<EquipmentStruct>(output, *(items[i].equipmentOffset));

		if (items[i].usableOffset)
			writeRaw<UsableStruct>(output, *(items[i].usableOffset));

	}

	output.close();

}

void readITE(std::vector<ItemStruct>& items, std::string filename) {

	std::ifstream input(filename, std::ios::binary);

	if (!input.is_open())
		throw new std::exception("ITEM.BIN not found to be read!");

	char* readByte = new char[4]{};
	std::streampos pos = 0;

	items.resize(0x31F);		//entries are broken down into 3 possible parts(first is always there, 80 bytes long; second is equipment, 28 bytes long; third is usables, 32 bytes long)

	for (size_t i = 0; i < items.size(); i++) {

		input.read(items[i].name, 18);
		replaceNulls(items[i].name, 18);

		input.read(items[i].description, 40);
		replaceNulls(items[i].description, 40);

		items[i].stats = readRaw<ItemStatsStruct>(input);

		input.read(readByte, 4);
		pos = input.tellg();

		//check if equipment offset is available
		if (((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])) > 0) {

			items[i].equipmentOffset = new EquipmentStruct;
			input.seekg(((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])), std::ios::beg);

			*(items[i].equipmentOffset) = readRaw<EquipmentStruct>(input);

		}

		input.seekg(pos, std::ios::beg);

		input.read(readByte, 4);
		pos = input.tellg();

		if (((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])) > 0) {

			items[i].usableOffset = new UsableStruct;
			input.seekg(((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])), std::ios::beg);

			*(items[i].usableOffset) = readRaw<UsableStruct>(input);

		}

		input.seekg(pos, std::ios::beg);

		input.read(readByte, 4);
		items[i].id = i;

	}

	input.close();

}

void drawITE(std::vector<ItemStruct>& items, char** itemIDs, bool* canClose) {

	static ImU16 itemID = 0;
	static bool hasEquip = false;
	static bool hasUsable = false;
	static bool EquipmentCharacterBitFlags[7] = {};
	static bool EquipmentAilmentBitFlags[8] = {};
	static bool UsableAilmentBitFlags[8] = {};

	ImGui::Begin("ITEM");

	if (ImGui::Combo("Index", &itemID, itemIDs, (int)items.size())) {

		(items[itemID].equipmentOffset) ? hasEquip = true : hasEquip = false;
		(items[itemID].usableOffset) ? hasUsable = true : hasUsable = false;

		if (hasEquip) {

			for (size_t i = 0; i < 7; i++)
				EquipmentCharacterBitFlags[i] = items[itemID].equipmentOffset->characterBitflag & (1 << i);

			for (size_t i = 0; i < 8; i++)
				EquipmentAilmentBitFlags[i] = items[itemID].equipmentOffset->ailmentsBitflag & (1 << i);

		}
		else {

			for (size_t i = 0; i < 7; i++)
				EquipmentCharacterBitFlags[i] = 0;

			for (size_t i = 0; i < 8; i++)
				EquipmentAilmentBitFlags[i] = 0;

		}

		if (hasUsable)
			for (size_t i = 0; i < 8; i++)
				UsableAilmentBitFlags[i] = items[itemID].usableOffset->ailmentsBitflag & (1 << i);
		else
			for (size_t i = 0; i < 8; i++)
				UsableAilmentBitFlags[i] = 0;

	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			if (isHDVersion)
				writeITE(items);
			else
				writeITE(items, "data/afs/xls_data/ITEM.BIN");

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	//ImGui::LabelText("ID", std::to_string(items[itemID].id).c_str());   //it does come last in the file, but better to allow quick finding of the ID

	ImGui::InputText("Name", items[itemID].name, 19);
	ImGui::InputText("Description", items[itemID].description, 41);

	ImGui::Combo("Entry Type", &items[itemID].stats.entryType, entryIDs, 16);

	ImGui::InputUByte("Unknown #1", &items[itemID].stats.unknown1);
	ImGui::InputUByte("Unknown #2", &items[itemID].stats.unknown2);
	ImGui::InputUByte("Unknown #3", &items[itemID].stats.unknown3);
	ImGui::InputUByte("Icon", &items[itemID].stats.icon);
	ImGui::InputUByte("Unknown #5", &items[itemID].stats.unknown5);

	ImGui::InputUInt("Price", &items[itemID].stats.price);						if (ImGui::IsItemHovered()) ImGui::SetTooltip("sell = price / 2");

	if (ImGui::Checkbox("Equipment", &hasEquip)) {

		if (hasEquip)
			items[itemID].equipmentOffset = new EquipmentStruct;
		else {

			delete items[itemID].equipmentOffset;
			items[itemID].equipmentOffset = nullptr;

			for (size_t i = 0; i < 8; i++)
				EquipmentAilmentBitFlags[i] = 0;

		}

	}

	ImGui::SameLine();

	if (ImGui::Checkbox("Usable", &hasUsable)) {

		if (hasUsable)
			items[itemID].usableOffset = new UsableStruct;
		else {

			delete items[itemID].usableOffset;
			items[itemID].usableOffset = nullptr;

			for (size_t i = 0; i < 8; i++)
				UsableAilmentBitFlags[i] = 0;

		}

	}

	if (hasEquip) { //Item Equippable WINDOW

		ImGui::Begin("Item Equipment");

		if (ImGui::Checkbox("Ryudo", &EquipmentCharacterBitFlags[0]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Elena", &EquipmentCharacterBitFlags[1]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Millenia", &EquipmentCharacterBitFlags[2]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Roan", &EquipmentCharacterBitFlags[3]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[3] << 3);

		if (ImGui::Checkbox("Tio", &EquipmentCharacterBitFlags[4]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Mareg", &EquipmentCharacterBitFlags[5]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Prince Roan", &EquipmentCharacterBitFlags[6]))
			items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[6] << 6);

		ImGui::InputShort("Strength", &items[itemID].equipmentOffset->str);
		ImGui::InputShort("Vitality", &items[itemID].equipmentOffset->vit);
		ImGui::InputShort("Action", &items[itemID].equipmentOffset->act);
		ImGui::InputShort("Movement", &items[itemID].equipmentOffset->mov);

		ImGui::Combo("Effective On", &items[itemID].equipmentOffset->effectiveOn, effectiveOn, 16);

		ImGui::InputByte("Fire %", &items[itemID].equipmentOffset->fireAffinity);
		ImGui::InputByte("Wind %", &items[itemID].equipmentOffset->windAffinity);
		ImGui::InputByte("Earth %", &items[itemID].equipmentOffset->earthAffinity);
		ImGui::InputByte("Lightning %", &items[itemID].equipmentOffset->lightningAffinity);
		ImGui::InputByte("Blizzard %", &items[itemID].equipmentOffset->blizzardAffinity);

		if (ImGui::Checkbox("Poison", &EquipmentAilmentBitFlags[0]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &EquipmentAilmentBitFlags[1]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &EquipmentAilmentBitFlags[2]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &EquipmentAilmentBitFlags[3]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &EquipmentAilmentBitFlags[4]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &EquipmentAilmentBitFlags[5]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &EquipmentAilmentBitFlags[6]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &EquipmentAilmentBitFlags[7]))
			items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[7] << 7);

		ImGui::InputUByte("Ailments Chance", &items[itemID].equipmentOffset->ailmentsChance);

		ImGui::InputUByte("Unknown #1", &items[itemID].equipmentOffset->unknown1);
		ImGui::InputUByte("Unknown #2", &items[itemID].equipmentOffset->unknown2);
		ImGui::InputUByte("Unknown #3", &items[itemID].equipmentOffset->unknown3);
		ImGui::InputUByte("Unknown #4", &items[itemID].equipmentOffset->unknown4);
		ImGui::InputUByte("Unknown #5", &items[itemID].equipmentOffset->unknown5);
		ImGui::InputUByte("Unknown #6", &items[itemID].equipmentOffset->unknown6);
		ImGui::InputUByte("Unknown #7", &items[itemID].equipmentOffset->unknown7);
		ImGui::InputUByte("Unknown #8", &items[itemID].equipmentOffset->unknown8);

		ImGui::InputUShort("Special", &items[itemID].equipmentOffset->special);

		ImGui::End();

	}

	if (hasUsable) {    //Item Usable WINDOW

		ImGui::Begin("Item Usable");

		ImGui::Combo("Target Effect", &items[itemID].usableOffset->targetEffect, targetEffects, 16);
		ImGui::Combo("Target Type", &items[itemID].usableOffset->targetType, targetTypes, 16);

		ImGui::InputUShort("Power", &items[itemID].usableOffset->power);
		ImGui::InputUShort("Range", &items[itemID].usableOffset->range);
		ImGui::InputUShort("Cast Time", &items[itemID].usableOffset->castTime);
		ImGui::InputUShort("Recovery", &items[itemID].usableOffset->recoveryTime);
		ImGui::InputUShort("Animation", &items[itemID].usableOffset->animation);

		ImGui::Combo("Effective On", &items[itemID].usableOffset->effectiveOn, effectiveOn, 16);

		ImGui::InputUByte("Unknown #1", &items[itemID].usableOffset->unknown1);

		ImGui::InputShort2("IP Damage/IP Cancel Damage", &items[itemID].usableOffset->ipDamage);
		ImGui::InputShort("Knockback", &items[itemID].usableOffset->knockback);

		ImGui::Combo("Element", &items[itemID].usableOffset->element, elements, 5);
		ImGui::InputUByte("Element Strength", &items[itemID].usableOffset->elementStr);

		if (ImGui::Checkbox("Poison", &UsableAilmentBitFlags[0]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[0] << 0);
		ImGui::SameLine();
		if (ImGui::Checkbox("Sleep", &UsableAilmentBitFlags[1]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[1] << 1);
		ImGui::SameLine();
		if (ImGui::Checkbox("Paralysis", &UsableAilmentBitFlags[2]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[2] << 2);
		ImGui::SameLine();
		if (ImGui::Checkbox("Confusion", &UsableAilmentBitFlags[3]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[3] << 3);

		if (ImGui::Checkbox("Plague", &UsableAilmentBitFlags[4]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[4] << 4);
		ImGui::SameLine();
		if (ImGui::Checkbox("Magic Block", &UsableAilmentBitFlags[5]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[5] << 5);
		ImGui::SameLine();
		if (ImGui::Checkbox("Move Block", &UsableAilmentBitFlags[6]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[6] << 6);
		ImGui::SameLine();
		if (ImGui::Checkbox("Death", &UsableAilmentBitFlags[7]))
			items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[7] << 7);

		ImGui::InputByte4("Atk/Def/Act/Mov Mods", &items[itemID].usableOffset->atkMod);

		ImGui::InputUByte("Break Chance", &items[itemID].usableOffset->breakChance);
		ImGui::InputUByte("Special", &items[itemID].usableOffset->special);

		ImGui::InputUByte("Unknown #2", &items[itemID].usableOffset->unknown2);
		ImGui::InputUByte("Unknown #3", &items[itemID].usableOffset->unknown3);

		ImGui::End();

	}

	ImGui::End();

}