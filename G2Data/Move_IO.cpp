#include <fstream>
#include <exception>
#include <string>

#include "StringManip.h"
#include "MoveStruct.h"

void writeMS(MoveStruct* moves, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/MS_PARAM.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("MS_PARAM.BIN not found to be written!");

	for (size_t i = 0; i < count; i++) {

		output.put(moves[i].id);
		output.put((moves[i].icon + 1));
		output.write((char*)moves[i].name, 18);

		output.put(moves[i].cost);
		output.put((moves[i].cost >> 8));

		output.put(moves[i].targetEffect);
		output.put(moves[i].targetType);

		output.put(moves[i].str);
		output.put((moves[i].str >> 8));
		
		output.put(moves[i].pow);
		output.put((moves[i].pow >> 8));
		
		output.put(moves[i].range);
		output.put((moves[i].range >> 8));
		
		output.put(moves[i].cast1);
		output.put((moves[i].cast1 >> 8));
		
		output.put(moves[i].cast5);
		output.put((moves[i].cast5 >> 8));
		
		output.put(moves[i].recovery);
		output.put((moves[i].recovery >> 8));
		
		output.put(moves[i].animation);
		output.put((moves[i].animation >> 8));

		output.put(moves[i].unknown1);
		output.put(moves[i].knockDown % 2);
		
		output.put(moves[i].ipStun);
		output.put((moves[i].ipStun >> 8));
		
		output.put(moves[i].ipCancelStun);
		output.put((moves[i].ipCancelStun >> 8));
		
		output.put(moves[i].knockback);
		output.put((moves[i].knockback >> 8));

		output.put(moves[i].element);
		output.put(moves[i].elementStr);
		output.put(moves[i].ailmentsBitflag);
		output.put(moves[i].ailmentsChance);
		output.put(moves[i].atkMod % 6);
		output.put(moves[i].defMod % 6);
		output.put(moves[i].actMod % 6);
		output.put(moves[i].movMod % 6);
		
		output.put(moves[i].special);
		output.put((moves[i].special >> 8));
		
		output.put(moves[i].coinCost1);
		output.put((moves[i].coinCost1 >> 8));
		
		output.put(moves[i].coinCost2);
		output.put((moves[i].coinCost2 >> 8));
		
		output.put(moves[i].coinCost3);
		output.put((moves[i].coinCost3 >> 8));
		
		output.put(moves[i].coinCost4);
		output.put((moves[i].coinCost4 >> 8));
		
		output.put(moves[i].coinCost5);
		output.put((moves[i].coinCost5 >> 8));
		
		output.put(moves[i].multiplier);
		output.put((moves[i].multiplier >> 8));
		output.write((char*)moves[i].description, 40);

	}

	output.close();

}

void readMS(std::promise<MoveStruct*> ftr, ImU16& count) {

	char* readByte = new char[2]{};

	std::ifstream input("content/data/afs/xls_data/MS_PARAM.BIN", std::ios::binary);

	count = 0x81;
	MoveStruct* moves = new MoveStruct[count];	//entries are 108 bytes long

	if (!input.is_open())
		throw new std::exception("MS_PARAM.BIN not found to be read!");

	for (int i = 0; i < count; i++) {

		input.read(readByte, 1);
		moves[i].id = i;	//overwrite all IDs, we want them to be equal to the offset of the entry in the file

		input.read(readByte, 1);
		if ((ImU8)readByte[0] == 0)
			moves[i].icon = 0;
		else
			moves[i].icon = (ImU8)readByte[0] - 1;

		input.read(&moves[i].name[0], 18);
		replaceNulls(moves[i].name, 18);

		input.read(readByte, 2);
		moves[i].cost = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 1);
		moves[i].targetEffect = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].targetType = (ImU8)readByte[0];

		input.read(readByte, 2);
		moves[i].str = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].pow = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].range = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].cast1 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].cast5 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].recovery = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].animation = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 1);
		moves[i].unknown1 = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].knockDown = (ImU8)readByte[0];

		input.read(readByte, 2);
		moves[i].ipStun = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].ipCancelStun = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].knockback = ((ImS16)((ImU8)(readByte[1])) << 8) + (ImS16)((ImU8)(readByte[0]));

		input.read(readByte, 1);
		moves[i].element = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].elementStr = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].ailmentsBitflag = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].ailmentsChance = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].atkMod = readByte[0];

		input.read(readByte, 1);
		moves[i].defMod = readByte[0];

		input.read(readByte, 1);
		moves[i].actMod = readByte[0];

		input.read(readByte, 1);
		moves[i].movMod = readByte[0];

		input.read(readByte, 2);
		moves[i].special = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost1 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost2 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost3 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost4 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost5 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].multiplier = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(&moves[i].description[0], 40);
		replaceNulls(moves[i].description, 40);

	}

	input.close();

	ftr.set_value(moves);

}

void drawMS(MoveStruct* moves, char** moveIDs, ImU16& numMoves, bool* canClose) {

	const char* moveIcons[] = { "Fire", "Wind", "Earth", "Lightning", "Blizzard", "Water", "Explosion", "Forest", "Light", "Darkness", "Sword", "Staff", "Crossbow", "Dagger", "Poleaxe", "Chakram", "Red Shoe" };
	const char* targetEffects[] = { "NULL", "Restore HP(MEN)", "Restore MP", "Restore SP", "Ally Buff/Debuff", "Physical Damage(STR)", "Magical Damage(MAG)", "Enemy Buff/Debuff", "Status Change", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Special" };
	const char* targetTypes[] = { "NULL", "One Ally", "Area Allies", "All Allies", "One Enemy", "Area Enemies", "All Enemies", "Enemy Line", "Self", "Unknown", "Area Around Self", "Unknown", "Unknown", "Area Around Self", "Unknown", "Unknown" };
	const char* elements[] = { "Fire", "Wind", "Earth", "Lightning", "Blizzard" };

	static ImU16 moveID = 0;
	static bool AilmentBitFlags[8] = {};

	ImGui::Begin("MS_PARAM");

	if (ImGui::Combo("Index", &moveID, moveIDs, (int)numMoves))
		for (size_t i = 0; i < 8; i++)
			AilmentBitFlags[i] = moves[moveID].ailmentsBitflag & (1 << i);

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeMS(moves, numMoves);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	//ImGui::LabelText("ID", std::to_string(moves[moveID].id).c_str());

	//ImGui::InputUByte("Icon", &moves[moveID].icon);
	ImGui::Combo("Icon", &moves[moveID].icon, moveIcons, 17);
	ImGui::InputText("Name", moves[moveID].name, 19);
	ImGui::InputUShort("Cost", &moves[moveID].cost);

	ImGui::Combo("Target Effect", &moves[moveID].targetEffect, targetEffects, 16);
	ImGui::Combo("Target Type", &moves[moveID].targetType, targetTypes, 16);

	ImGui::InputUShort("Strength", &moves[moveID].str);
	ImGui::InputUShort("Power", &moves[moveID].pow);
	ImGui::InputUShort("Range", &moves[moveID].range);

	ImGui::InputUShort2("Cast Time Lv1/Lv5", &moves[moveID].cast1);

	ImGui::InputUShort("Recovery", &moves[moveID].recovery);
	ImGui::InputUShort("Animation", &moves[moveID].animation);
	ImGui::InputUByte("Unknown #1", &moves[moveID].unknown1);
	ImGui::InputUByte("Knockdown", &moves[moveID].knockDown);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("Will this move knockdown those hit?");

	ImGui::InputShort2("IP Stun/IP Cancel Stun", &moves[moveID].ipStun);

	ImGui::InputShort("Knockback", &moves[moveID].knockback);               if (ImGui::IsItemHovered()) ImGui::SetTooltip("How much move will knockback those hit.");

	ImGui::Combo("Element", &moves[moveID].element, elements, 5);
	ImGui::InputUByte("Element Strength", &moves[moveID].elementStr);

	if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[0] << 0);
	ImGui::SameLine();
	if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[1] << 1);
	ImGui::SameLine();
	if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[2] << 2);
	ImGui::SameLine();
	if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[3] << 3);

	if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[4] << 4);
	ImGui::SameLine();
	if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[5] << 5);
	ImGui::SameLine();
	if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[6] << 6);
	ImGui::SameLine();
	if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
		moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[7] << 7);

	ImGui::InputUByte("Ailments Chance", &moves[moveID].ailmentsChance);
	ImGui::InputByte4("Atk/Def/Act/Mov Mods", &moves[moveID].atkMod);
	ImGui::InputUShort("Special", &moves[moveID].special);
	ImGui::InputUShort("Coin Cost Lv1", &moves[moveID].coinCost1);
	ImGui::InputUShort("Coin Cost Lv2", &moves[moveID].coinCost2);
	ImGui::InputUShort("Coin Cost Lv3", &moves[moveID].coinCost3);
	ImGui::InputUShort("Coin Cost Lv4", &moves[moveID].coinCost4);
	ImGui::InputUShort("Coin Cost Lv5", &moves[moveID].coinCost5);
	ImGui::InputUShort("Multiplier", &moves[moveID].multiplier);
	ImGui::InputText("Description", moves[moveID].description, 41);

	ImGui::End();

}