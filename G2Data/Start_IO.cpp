#include <fstream>
#include <exception>

#include "StartStats.h"

void writePC(StartStatsStruct* stats, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/PC_INIT.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("PC_INIT.BIN not found to be written!");

	for (size_t i = 0; i < count; i++) {

		output.put(stats[i].exp);
		output.put((stats[i].exp >> 8));
		output.put((stats[i].exp >> 16));
		output.put((stats[i].exp >> 24));

		output.put(stats[i].weapon);
		output.put((stats[i].weapon >> 8));

		output.put(stats[i].armour);
		output.put((stats[i].armour >> 8));

		output.put(stats[i].headgear);
		output.put((stats[i].headgear >> 8));

		output.put(stats[i].footwear);
		output.put((stats[i].footwear >> 8));

		output.put(stats[i].accessory);
		output.put((stats[i].accessory >> 8));

		output.put(stats[i].manaEgg);
		output.put((stats[i].manaEgg >> 8));

		output.put(stats[i].stamina);
		output.put((stats[i].stamina >> 8));

		output.put(stats[i].unknown1);
		output.put((stats[i].unknown1 >> 8));

		output.put(stats[i].unknown2);
		output.put((stats[i].unknown2 >> 8));

		output.put(stats[i].unknown3);
		output.put((stats[i].unknown3 >> 8));

		output.put(stats[i].unknown4);
		output.put((stats[i].unknown4 >> 8));

		output.put(stats[i].unknown5);
		output.put((stats[i].unknown5 >> 8));

		output.put(stats[i].unknown6);
		output.put((stats[i].unknown6 >> 8));

		output.put(stats[i].unknown7);
		output.put((stats[i].unknown7 >> 8));

		output.put(stats[i].unknown8);
		output.put((stats[i].unknown8 >> 8));

		output.put(stats[i].unknown9);
		output.put((stats[i].unknown9 >> 8));

		output.put(stats[i].unknown10);
		output.put((stats[i].unknown10 >> 8));

		output.put(stats[i].ipStun);
		output.put((stats[i].ipStun >> 8));

		output.put(stats[i].ipCancelStun);
		output.put((stats[i].ipCancelStun >> 8));

		output.put(stats[i].comboSpRegen);
		output.put(stats[i].critSpRegen);
		output.put(stats[i].unknown11);
		output.put(stats[i].hitSpRegen);
		output.put(stats[i].unknown12);
		output.put(stats[i].evasionStillRate);
		output.put(stats[i].evasionMovingRate);
		output.put(stats[i].ResistKnockback);

		output.put(stats[i].unknown13);
		output.put((stats[i].unknown13 >> 8));

		output.put(stats[i].TREC);
		output.put((stats[i].TREC >> 8));

		output.put(stats[i].TDMG);
		output.put((stats[i].TDMG >> 8));

		output.put(stats[i].unknown14);
		output.put((stats[i].unknown14 >> 8));

		output.put(stats[i].THEAL);
		output.put((stats[i].THEAL >> 8));

		output.put(stats[i].size);
		output.put((stats[i].size >> 8));

		output.put(stats[i].unknown15);
		output.put((stats[i].unknown15 >> 8));

		output.put(stats[i].unknown16);
		output.put((stats[i].unknown16 >> 8));

		output.put(stats[i].unknown17);
		output.put((stats[i].unknown17 >> 8));

		output.put(stats[i].unknown18);
		output.put((stats[i].unknown18 >> 8));

		output.put(stats[i].unknown19);
		output.put((stats[i].unknown19 >> 8));

		output.put(stats[i].unknown20);
		output.put((stats[i].unknown20 >> 8));

		output.put(stats[i].unknown21);
		output.put((stats[i].unknown21 >> 8));

		output.put(stats[i].unknown22);
		output.put((stats[i].unknown22 >> 8));

		output.put(stats[i].unknown23);
		output.put((stats[i].unknown23 >> 8));

	}

	output.close();

}

StartStatsStruct* readPC(ImU16& count) {
	//Upon starting a new game, all characters gain EXP together. The EXP value displayed is what each character starts the game at

	char* readByte = new char[4]{};

	std::ifstream input("content/data/afs/xls_data/PC_INIT.BIN", std::ios::binary);

	count = 0x0D;
	StartStatsStruct* stats = new StartStatsStruct[count * 80];	//entries are 80 bytes long

	if (!input.is_open())
		throw new std::exception("PC_INIT.BIN not found to be read!");

	for (size_t i = 0; i < count; i++) {

		input.read(readByte, 4);
		stats[i].exp = ((((ImU32)((ImU8)readByte[3])) << 24) + (((ImU32)((ImU8)readByte[2])) << 16) + (((ImU32)((ImU8)readByte[1])) << 8) + (ImU32)((ImU8)readByte[0]));

		input.read(readByte, 2);
		stats[i].weapon = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].armour = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].headgear = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].footwear = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].accessory = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].manaEgg = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].stamina = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown1 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown2 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown3 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown4 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown5 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown6 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown7 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown8 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown9 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown10 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].ipStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].ipCancelStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 1);
		stats[i].comboSpRegen = (ImU8)readByte[0];

		input.read(readByte, 1);
		stats[i].critSpRegen = (ImU8)readByte[0];

		input.read(readByte, 1);
		stats[i].unknown11 = (ImU8)readByte[0];

		input.read(readByte, 1);
		stats[i].hitSpRegen = (ImU8)readByte[0];

		input.read(readByte, 1);
		stats[i].unknown12 = (ImU8)readByte[0];

		input.read(readByte, 1);
		stats[i].evasionStillRate = readByte[0];

		input.read(readByte, 1);
		stats[i].evasionMovingRate = readByte[0];

		input.read(readByte, 1);
		stats[i].ResistKnockback = readByte[0];

		input.read(readByte, 2);
		stats[i].unknown13 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].TREC = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].TDMG = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown14 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].THEAL = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].size = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown15 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown16 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown17 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown18 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown19 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown20 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown21 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown22 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		stats[i].unknown23 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

	}

	input.close();

	return stats;

}