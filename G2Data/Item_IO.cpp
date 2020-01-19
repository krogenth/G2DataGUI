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

ItemStruct* readITE(ImU16& count) {

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
			items[i].usableOffset->ipDamage = (readByte[1] << 8) + readByte[0];

			input.read(readByte, 2);
			items[i].usableOffset->ipCancelDamage = (readByte[1] << 8) + readByte[0];

			input.read(readByte, 2);
			items[i].usableOffset->knockback = (readByte[1] << 8) + readByte[0];

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

	return items;

}

/*
SpecialMoveStruct* readSPC(ImU16& count) {

	char* readByte = new char[2]{};

	std::ifstream input("content/data/afs/xls_data/TB_SPCL.BIN", std::ios::binary);

	count = 0x0E;
	SpecialMoveStruct* specials = new SpecialMoveStruct[count];	//entries are 24 bytes long(each special is 4 bytes long, 6 specials per book)

	if (!input.is_open())
		throw new std::exception();

	for (size_t i = 0; i < count; i++) {

		for (size_t j = 0; j < 6; j++) {

			input.read(readByte, 1);
			specials[i].moves[j].moveOffset = (ImU8)readByte[0];

			input.read(readByte, 1);
			specials[i].moves[j].startingLevel = (ImU8)readByte[0];

			input.read(readByte, 2);
			specials[i].moves[j].storyFlag = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		}

	}

	input.close();

	return specials;

}
*/