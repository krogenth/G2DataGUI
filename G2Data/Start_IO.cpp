#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <experimental/filesystem>
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

	std::experimental::filesystem::path filePath("content/data/afs/xls_data/PC_INIT.BIN");
	size_t fileSize = std::experimental::filesystem::file_size(filePath);

	char* readByte = new char[4]{};

	std::ifstream input("content/data/afs/xls_data/PC_INIT.BIN", std::ios::binary);

	count = fileSize / 80;
	StartStatsStruct* stats = new StartStatsStruct[count];	//entries are 80 bytes long

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

void drawPC(StartStatsStruct* stats, ImU16& numStats, bool* canClose, char** itemIDs, ImU16& numItems) {

	const char* statIDs[] = { "Ryduo", "Elena", "Millenia", "Roan", "Tio", "Mareg", "Prince Roan", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" };
	static ImU16 statID = 0;

	ImGui::Begin("PC_INIT");

	ImGui::Combo("Index", &statID, statIDs, (int)numStats); ImGui::SameLine();

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writePC(stats, numStats);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	ImGui::InputUInt("EXP", &stats[statID].exp);

	ImGui::Combo("Weapon", &stats[statID].weapon, itemIDs, (int)numItems);
	ImGui::Combo("Armour", &stats[statID].armour, itemIDs, (int)numItems);
	ImGui::Combo("Headgear", &stats[statID].headgear, itemIDs, (int)numItems);
	ImGui::Combo("Footwear", &stats[statID].footwear, itemIDs, (int)numItems);
	ImGui::Combo("Accessory", &stats[statID].accessory, itemIDs, (int)numItems);
	ImGui::Combo("Mana Egg", &stats[statID].manaEgg, itemIDs, (int)numItems);

	ImGui::InputShort("Stamina", &stats[statID].stamina);                                           if (ImGui::IsItemHovered()) ImGui::SetTooltip("How long can they move without tiring?");

	ImGui::InputUShort("Unknown #1", &stats[statID].unknown1);
	ImGui::InputUShort("Unknown #2", &stats[statID].unknown2);
	ImGui::InputUShort("Unknown #3", &stats[statID].unknown3);
	ImGui::InputUShort("Unknown #4", &stats[statID].unknown4);
	ImGui::InputUShort("Unknown #5", &stats[statID].unknown5);
	ImGui::InputUShort("Unknown #6", &stats[statID].unknown6);
	ImGui::InputUShort("Unknown #7", &stats[statID].unknown7);
	ImGui::InputUShort("Unknown #8", &stats[statID].unknown8);
	ImGui::InputUShort("Unknown #9", &stats[statID].unknown9);
	ImGui::InputUShort("Unknown #10", &stats[statID].unknown10);

	ImGui::InputShort2("IP Stun/IP Cancel Stun", &stats[statID].ipStun);                            if (ImGui::IsItemHovered()) ImGui::SetTooltip("IP Stun/IP Cancel Stun Resistance.");

	ImGui::InputUByte("Combo SP Regen", &stats[statID].comboSpRegen);                               if (ImGui::IsItemHovered()) ImGui::SetTooltip("SP regen from combo hit.");
	ImGui::InputUByte("Critical SP Regen", &stats[statID].critSpRegen);                             if (ImGui::IsItemHovered()) ImGui::SetTooltip("SP regen from critical hit.");

	ImGui::InputUByte("Unknown #11", &stats[statID].unknown11);

	ImGui::InputUByte("Damaged SP Regen", &stats[statID].hitSpRegen);                               if (ImGui::IsItemHovered()) ImGui::SetTooltip("SP regen from taking damage.");

	ImGui::InputUByte("Unknown #12", &stats[statID].unknown12);

	ImGui::InputByte("Still Evasion Rate", &stats[statID].evasionStillRate);
	ImGui::InputByte("Move Evasion Rate", &stats[statID].evasionMovingRate);
	ImGui::InputByte("Knockback Resist Rate", &stats[statID].ResistKnockback);

	ImGui::InputUShort("Unknown #13", &stats[statID].unknown13);

	ImGui::InputShort("TREC", &stats[statID].TREC);
	ImGui::InputShort("TDMG", &stats[statID].TDMG);

	ImGui::InputUShort("Unknown #14", &stats[statID].unknown14);

	ImGui::InputShort("THEAL", &stats[statID].THEAL);

	ImGui::InputShort("Size", &stats[statID].size);

	ImGui::InputUShort("Unknown #15", &stats[statID].unknown15);
	ImGui::InputUShort("Unknown #16", &stats[statID].unknown16);
	ImGui::InputUShort("Unknown #17", &stats[statID].unknown17);
	ImGui::InputUShort("Unknown #18", &stats[statID].unknown18);
	ImGui::InputUShort("Unknown #19", &stats[statID].unknown19);
	ImGui::InputUShort("Unknown #20", &stats[statID].unknown20);
	ImGui::InputUShort("Unknown #21", &stats[statID].unknown21);
	ImGui::InputUShort("Unknown #22", &stats[statID].unknown22);
	ImGui::InputUShort("Unknown #23", &stats[statID].unknown23);

	ImGui::End();

}