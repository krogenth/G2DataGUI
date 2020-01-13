#include <fstream>
#include <exception>

#include "StringManip.h"
#include "ManaEggStruct.h"

/*
void writeSK(SkillStruct* skills, ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/SK_PARAM.BIN", std::ios::binary);

	if (!output.is_open())
		return;

	for (size_t i = 0; i < count; i++) {

		//output.put(skills[i].id);
		//output.put(skills[i].icon);
		output.write((char*)skills[i].name, 18);
		output.put(skills[i].cost1);
		output.put(skills[i].cost2);

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
		output.put(skills[i].unknown6);

		output.put(skills[i].baseFirePercent % 11);
		output.put(skills[i].baseWindPercent % 11);
		output.put(skills[i].baseEarthPercent % 11);
		output.put(skills[i].baseLightningPercent % 11);
		output.put(skills[i].baseBlizzardPercent % 11);
		output.put(skills[i].baseWaterPercent % 11);
		output.put(skills[i].baseExplosionPercent % 11);
		output.put(skills[i].baseForestPercent % 11);

		output.put(skills[i].special);
		output.put((skills[i].special >> 8));

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
*/

void writeMAG(ManaEggStruct* eggs, const ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	if (!output.is_open())
		return;

	for (size_t i = 0; i < 0x0B; i++) {

		for (size_t j = 0; j < 18; j++) {

			output.put(eggs[i].spells[j].spellOffset);
			output.put(eggs[i].spells[j].startingLevel);
			output.put(eggs[i].spells[j].eggLevelRequired);
			output.put(eggs[i].spells[j].unknown1);

		}

	}

	output.close();

}

ManaEggStruct* readMAG(ImU16& count) {

	char* readByte = new char[1]{};

	std::ifstream input("content/data/afs/xls_data/TB_MAGIC.BIN", std::ios::binary);

	count = 0x0B;
	ManaEggStruct* eggs = new ManaEggStruct[count];	//entries are 72 bytes long(each spell is 4 bytes long, 18 spells per egg)

	if (!input.is_open())
		throw new std::exception();

	for (size_t i = 0; i < count; i++) {

		/*
		SpellImplementationStruct spells[18];
		ImU8 spellOffset = 0;
		ImU8 startingLevel = 0;
		ImU8 eggLevelRequired = 0;
		ImU8 unkown = 0;
		*/

		for (size_t j = 0; j < 18; j++) {

			input.read(readByte, 1);
			eggs[i].spells[j].spellOffset = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].startingLevel = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].eggLevelRequired = (ImU8)readByte[0];

			input.read(readByte, 1);
			eggs[i].spells[j].unknown1 = (ImU8)readByte[0];

		}

	}

	input.close();

	return eggs;

}