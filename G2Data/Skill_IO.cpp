#include <fstream>
#include <exception>

#include "StringManip.h"
#include "SkillStruct.h"

void writeSK(SkillStruct* skills, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/SK_PARAM.BIN", std::ios::binary);

	if (!output.is_open())
		throw new std::exception("SK_PARAM.BIN not found to be written!");

	for (size_t i = 0; i < count; i++) {

		output.write((char*)skills[i].name, 18);
		output.put((skills[i].cost1 % 3));
		output.put((skills[i].cost1 % 3));

		output.put(skills[i].baseHp);
		output.put((skills[i].baseHp >> 8));

		output.put(skills[i].baseMp);
		output.put((skills[i].baseMp >> 8));

		output.put(skills[i].baseSp);
		output.put((skills[i].baseSp >> 8));

		output.put(skills[i].baseStr);
		output.put((skills[i].baseStr >> 8));

		output.put(skills[i].baseVit);
		output.put((skills[i].baseVit >> 8));

		output.put(skills[i].baseAct);
		output.put((skills[i].baseAct >> 8));

		output.put(skills[i].baseMov);
		output.put((skills[i].baseMov >> 8));

		output.put(skills[i].baseMag);
		output.put((skills[i].baseMag >> 8));

		output.put(skills[i].baseMen);
		output.put((skills[i].baseMen >> 8));

		output.put(skills[i].unknown1);
		output.put(skills[i].unknown2);
		output.put(skills[i].unknown3);
		output.put(skills[i].unknown4);
		output.put(skills[i].unknown5);

		output.put(skills[i].baseFirePercent % 11);
		output.put(skills[i].baseWindPercent % 11);
		output.put(skills[i].baseEarthPercent % 11);
		output.put(skills[i].baseLightningPercent % 11);
		output.put(skills[i].baseBlizzardPercent % 11);
		output.put(skills[i].baseWaterPercent % 11);
		output.put(skills[i].baseExplosionPercent % 11);
		output.put(skills[i].baseForestPercent % 11);

		output.put(skills[i].special);

		output.put(skills[i].coinCost1);
		output.put((skills[i].coinCost1 >> 8));

		output.put(skills[i].coinCost2);
		output.put((skills[i].coinCost2 >> 8));

		output.put(skills[i].coinCost3);
		output.put((skills[i].coinCost3 >> 8));

		output.put(skills[i].coinCost4);
		output.put((skills[i].coinCost4 >> 8));

		output.put(skills[i].coinCost5);
		output.put((skills[i].coinCost5 >> 8));

		output.put(skills[i].multiplier);
		output.put((skills[i].multiplier >> 8));

		output.write((char*)skills[i].description, 40);

	}

	output.close();

}

SkillStruct* readSK(ImU16& count) {

	char* readByte = new char[2]{};

	std::ifstream input("content/data/afs/xls_data/SK_PARAM.BIN", std::ios::binary);

	count = 0x83;
	SkillStruct* skills = new SkillStruct[count * 104];	//entries are 104 bytes long

	if (!input.is_open())
		throw new std::exception("SK_PARAM.BIN not found to be read!");

	for (size_t i = 0; i < count; i++) {

		input.read(skills[i].name, 18);

		input.read(readByte, 1);
		skills[i].cost1 = (ImU8)readByte[0];

		input.read(readByte, 1);
		skills[i].cost2 = (ImU8)readByte[0];	//cost2 is always the same as cost1, so just reference cost1

		input.read(readByte, 2);
		skills[i].baseHp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseMp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseSp = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseStr = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseVit = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseAct = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseMov = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseMag = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].baseMen = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 1);
		skills[i].unknown1 = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].unknown2 = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].unknown3 = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].unknown4 = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].unknown5 = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseFirePercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseWindPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseEarthPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseLightningPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseBlizzardPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseWaterPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseExplosionPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].baseForestPercent = (ImS8)readByte[0];

		input.read(readByte, 1);
		skills[i].special = (ImS8)readByte[0];

		input.read(readByte, 2);
		skills[i].coinCost1 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].coinCost2 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].coinCost3 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].coinCost4 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].coinCost5 = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		skills[i].multiplier = ((ImU16)((ImU8)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(skills[i].description, 40);

	}

	input.close();

	return skills;

}