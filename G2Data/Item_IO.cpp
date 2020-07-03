#include <fstream>
#include <exception>

#include "StringManip.h"
#include "ItemStruct.h"

void writeITE(ItemStruct* items, const ImU16& count) {

	size_t offset = 0xF9B0;	//80 * 799

	std::ofstream output("content/data/afs/xls_data/ITEM.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("ITEM.BIN not found to be written!");

	for (size_t i = 0; i < count; i++) {

		output.write(items[i].name, 18);
		output.write(items[i].description, 40);

		output.put(items[i].entryType);

		output.put(items[i].unknown1);
		output.put(items[i].unknown2);
		output.put(items[i].unknown3);
		output.put(items[i].icon);
		output.put(items[i].unknown5);

		output.put(items[i].price);
		output.put((items[i].price >> 8));
		output.put((items[i].price >> 16));
		output.put((items[i].price >> 24));

		if (!items[i].equipmentOffset) {

			output.put(0xFF);
			output.put(0xFF);
			output.put(0xFF);
			output.put(0xFF);

		}
		else {

			output.put(offset);
			output.put((offset >> 8));
			output.put((offset >> 16));
			output.put((offset >> 24));

			offset += 0x1C;

		}

		if (!items[i].usableOffset) {

			output.put(0xFF);
			output.put(0xFF);
			output.put(0xFF);
			output.put(0xFF);

		}
		else {

			output.put(offset);
			output.put((offset >> 8));
			output.put((offset >> 16));
			output.put((offset >> 24));

			offset += 0x20;

		}

		output.put(items[i].id);
		output.put((items[i].id >> 8));
		output.put((items[i].id >> 16));
		output.put((items[i].id >> 24));

	}

	for (size_t i = 0; i < count; i++) {

		if (items[i].equipmentOffset) {

			output.put(items[i].equipmentOffset->characterBitflag);
			output.put((items[i].equipmentOffset->characterBitflag >> 8));

			output.put(items[i].equipmentOffset->str);
			output.put((items[i].equipmentOffset->str >> 8));

			output.put(items[i].equipmentOffset->vit);
			output.put((items[i].equipmentOffset->vit >> 8));

			output.put(items[i].equipmentOffset->act);
			output.put((items[i].equipmentOffset->act >> 8));

			output.put(items[i].equipmentOffset->mov);
			output.put((items[i].equipmentOffset->mov >> 8));

			output.put(items[i].equipmentOffset->effectiveOn);

			output.put(items[i].equipmentOffset->fireAffinity);

			output.put(items[i].equipmentOffset->windAffinity);

			output.put(items[i].equipmentOffset->earthAffinity);

			output.put(items[i].equipmentOffset->lightningAffinity);

			output.put(items[i].equipmentOffset->blizzardAffinity);

			output.put(items[i].equipmentOffset->ailmentsBitflag);

			output.put(items[i].equipmentOffset->ailmentsChance);

			output.put(items[i].equipmentOffset->unknown1);
			output.put(items[i].equipmentOffset->unknown2);
			output.put(items[i].equipmentOffset->unknown3);
			output.put(items[i].equipmentOffset->unknown4);
			output.put(items[i].equipmentOffset->unknown5);
			output.put(items[i].equipmentOffset->unknown6);
			output.put(items[i].equipmentOffset->unknown7);
			output.put(items[i].equipmentOffset->unknown8);

			output.put(items[i].equipmentOffset->special);
			output.put((items[i].equipmentOffset->special >> 8));

		}

		if (items[i].usableOffset) {

			output.put(items[i].usableOffset->targetEffect);

			output.put(items[i].usableOffset->targetType);

			output.put(items[i].usableOffset->power);
			output.put((items[i].usableOffset->power >> 8));

			output.put(items[i].usableOffset->range);
			output.put((items[i].usableOffset->range >> 8));

			output.put(items[i].usableOffset->castTime);
			output.put((items[i].usableOffset->castTime >> 8));

			output.put(items[i].usableOffset->recoveryTime);
			output.put((items[i].usableOffset->recoveryTime >> 8));

			output.put(items[i].usableOffset->animation);
			output.put((items[i].usableOffset->animation >> 8));

			output.put(items[i].usableOffset->effectiveOn);

			output.put(items[i].usableOffset->unknown1);

			output.put(items[i].usableOffset->ipDamage);
			output.put((items[i].usableOffset->ipDamage >> 8));

			output.put(items[i].usableOffset->ipCancelDamage);
			output.put((items[i].usableOffset->ipCancelDamage >> 8));

			output.put(items[i].usableOffset->knockback);
			output.put((items[i].usableOffset->knockback >> 8));

			output.put(items[i].usableOffset->element);

			output.put(items[i].usableOffset->elementStr);

			output.put(items[i].usableOffset->ailmentsBitflag);

			output.put(items[i].usableOffset->ailmentsChance);

			output.put(items[i].usableOffset->atkMod % 6);

			output.put(items[i].usableOffset->defMod % 6);

			output.put(items[i].usableOffset->actMod % 6);

			output.put(items[i].usableOffset->movMod % 6);

			output.put(items[i].usableOffset->breakChance);

			output.put(items[i].usableOffset->special);

			output.put(items[i].usableOffset->unknown2);

			output.put(items[i].usableOffset->unknown3);

		}

	}

	output.close();

}

void readITE(std::promise<ItemStruct*>&& ftr, ImU16& count) {

	char* readByte = new char[4]{};
	std::streampos pos = 0;

	std::ifstream input("content/data/afs/xls_data/ITEM.BIN", std::ios::binary);

	count = 0x31F;
	ItemStruct* items = new ItemStruct[count];	//entries are broken down into 3 possible parts(first is always there, 80 bytes long; second is equipment, 28 bytes long; third is usables, 32 bytes long)

	if (!input.is_open())
		throw new std::exception("ITEM.BIN not found to be read!");

	for (size_t i = 0; i < count; i++) {

		input.read(items[i].name, 18);
		replaceNulls(items[i].name, 18);

		input.read(items[i].description, 40);
		replaceNulls(items[i].description, 40);

		input.read(readByte, 1);
		items[i].entryType = (ImU8)readByte[0];

		input.read(readByte, 1);
		items[i].unknown1 = (ImU8)readByte[0];

		input.read(readByte, 1);
		items[i].unknown2 = (ImU8)readByte[0];

		input.read(readByte, 1);
		items[i].unknown3 = (ImU8)readByte[0];

		input.read(readByte, 1);
		items[i].icon = (ImU8)readByte[0];

		input.read(readByte, 1);
		items[i].unknown5 = (ImU8)readByte[0];

		input.read(readByte, 4);
		items[i].price = ((((ImU32)((ImU8)readByte[3])) << 24) + (((ImU32)((ImU8)readByte[2])) << 16) + (((ImU32)((ImU8)readByte[1])) << 8) + (ImU32)((ImU8)readByte[0]));

		input.read(readByte, 4);
		pos = input.tellg();

		//check if equipment offset is available
		if (((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])) > 0) {

			items[i].equipmentOffset = new EquipmentStruct;
			input.seekg(((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])), std::ios::beg);

			input.read(readByte, 2);
			items[i].equipmentOffset->characterBitflag = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].equipmentOffset->str = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].equipmentOffset->vit = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].equipmentOffset->act = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].equipmentOffset->mov = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			items[i].equipmentOffset->effectiveOn = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->fireAffinity = readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->windAffinity = readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->earthAffinity = readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->lightningAffinity = readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->blizzardAffinity = readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->ailmentsBitflag = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->ailmentsChance = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown1 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown2 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown3 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown4 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown5 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown6 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown7 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].equipmentOffset->unknown8 = (ImU8)readByte[0];

			input.read(readByte, 2);
			items[i].equipmentOffset->special = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		}

		input.seekg(pos, std::ios::beg);

		input.read(readByte, 4);
		pos = input.tellg();

		if (((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])) > 0) {

			items[i].usableOffset = new UsableStruct;
			input.seekg(((((ImS32)((ImU8)readByte[3])) << 24) + (((ImS32)((ImU8)readByte[2])) << 16) + (((ImS32)((ImU8)readByte[1])) << 8) + (ImS32)((ImU8)readByte[0])), std::ios::beg);

			input.read(readByte, 1);
			items[i].usableOffset->targetEffect = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->targetType = (ImU8)readByte[0];

			input.read(readByte, 2);
			items[i].usableOffset->power = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].usableOffset->range = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].usableOffset->castTime = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].usableOffset->recoveryTime = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].usableOffset->animation = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			items[i].usableOffset->effectiveOn = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->unknown1 = (ImU8)readByte[0];

			input.read(readByte, 2);
			items[i].usableOffset->ipDamage = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].usableOffset->ipCancelDamage = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 2);
			items[i].usableOffset->knockback = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

			input.read(readByte, 1);
			items[i].usableOffset->element = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->elementStr = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->ailmentsBitflag = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->ailmentsChance = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->atkMod = readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->defMod = readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->actMod = readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->movMod = readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->breakChance = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->special = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->unknown2 = (ImU8)readByte[0];

			input.read(readByte, 1);
			items[i].usableOffset->unknown3 = (ImU8)readByte[0];

		}

		input.seekg(pos, std::ios::beg);

		input.read(readByte, 4);
		items[i].id = i;

	}

	input.close();

	ftr.set_value(items);

}

void drawITE(ItemStruct* items, char** itemIDs, ImU16& numItems, bool* canClose) {

	const char* entryIDs[] = { "NULL", "Item", "Item", "Perm. Stat Modifier(?)", "Weapon", "Armour", "Headgear", "Footwear", "Accessory", "Mana Egg", "Unknown", "Valuable", "Skillbook", "Special", "Unknown", "Unknown" };
	const char* targetEffects[] = { "NULL", "Restore HP(MEN)", "Restore MP", "Restore SP", "Ally Buff/Debuff", "Physical Damage(STR)", "Magical Damage(MAG)", "Enemy Buff/Debuff", "Status Change", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Special" };
	const char* targetTypes[] = { "NULL", "One Ally", "Area Allies", "All Allies", "One Enemy", "Area Enemies", "All Enemies", "Enemy Line", "Self", "Unknown", "Area Around Self", "Unknown", "Unknown", "Area Around Self", "Unknown", "Unknown" };
	const char* effectiveOn[] = { "NULL", "Bird", "Bug", "Reptile", "Animal", "Humanoid", "Unknown", "Undead", "Valmar", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown" };
	const char* elements[] = { "Fire", "Wind", "Earth", "Lightning", "Blizzard" };
	static ImU16 itemID = 0;
	static bool hasEquip = false;
	static bool hasUsable = false;
	static bool EquipmentCharacterBitFlags[7] = {};
	static bool EquipmentAilmentBitFlags[8] = {};
	static bool UsableAilmentBitFlags[8] = {};

	ImGui::Begin("ITEM");

	if (ImGui::Combo("Index", &itemID, itemIDs, (int)numItems)) {

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

			writeITE(items, numItems);

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

	ImGui::Combo("Entry Type", &items[itemID].entryType, entryIDs, 16);

	ImGui::InputUByte("Unknown #1", &items[itemID].unknown1);
	ImGui::InputUByte("Unknown #2", &items[itemID].unknown2);
	ImGui::InputUByte("Unknown #3", &items[itemID].unknown3);
	ImGui::InputUByte("Icon", &items[itemID].icon);
	ImGui::InputUByte("Unknown #5", &items[itemID].unknown5);

	ImGui::InputUInt("Price", &items[itemID].price);

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