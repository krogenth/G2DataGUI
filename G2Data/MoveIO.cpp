#include <fstream>
#include <exception>
#ifdef _WIN32
#include <intrin.h>
#endif
#include <iostream>

#include "SwapEndianTemplate.h"
#include "StringManip.h"
#include "MoveStruct.h"

/*
void writeMS() {

	uint8_t offset = 0;
	uint16_t data16 = 0;
	uint32_t data32 = 0;
	std::string data;
	std::bitset<8> binary[108];

	std::ifstream input("data/afs/xls_data/MS_PARAM.txt");
	FILE* output;
	fopen_s(&output, "data/afs/xls_data/MS_PARAM.BIN", "w+b");
	assert(output);

	input.clear();
	input.seekg(0, std::ios::beg);

	if (input.is_open()) {

		while (std::getline(input, data)) {

			while (data[offset] == ' ')
				offset++;

			//ID/offset
			binary[0] = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
			offset = data.find(' ', static_cast<size_t>(offset));

			while (data[offset] == ' ')
				offset++;

			//Icon
			binary[1] = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
			offset = data.find(' ', static_cast<size_t>(offset));

			//Name
			for (int count = 0; count < 18; count++)
				binary[2 + count] = data[offset + count + 1];

			data.erase(0, offset + 19);
			offset = 0;

			while (data[offset] == ' ')
				offset++;

			//Cost
			data16 = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
			binary[20] = data16 & 0x00FF;
			binary[21] = (data16 & 0xFF00) >> 8;
			offset = data.find(' ', static_cast<size_t>(offset));

			while (data[offset] == ' ')
				offset++;

			//Target Effect
			binary[22] = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
			offset = data.find(' ', static_cast<size_t>(offset));

			while (data[offset] == ' ')
				offset++;

			//Target Type
			binary[23] = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
			offset = data.find(' ', static_cast<size_t>(offset));

			//strength, power, range, cast lv1, cast lv5, recovery, animation
			for (int count = 0; count < 7; count++) {

				while (data[offset] == ' ')
					offset++;

				data16 = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
				binary[24 + (count * 2)] = data16 & 0x00FF;
				binary[25 + (count * 2)] = (data16 & 0xFF00) >> 8;
				offset = data.find(' ', static_cast<size_t>(offset));

			}

			//unknown 2 bytes
			for (int count = 0; count < 2; count++) {

				while (data[offset] == ' ')
					offset++;

				binary[38 + count] = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
				offset = data.find(' ', static_cast<size_t>(offset));

			}

			//IP Damage, IP Cancel Damage, Knockback
			for (int count = 0; count < 3; count++) {

				while (data[offset] == ' ')
					offset++;

				data16 = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
				binary[40 + (count * 2)] = data16 & 0x00FF;
				binary[41 + (count * 2)] = (data16 & 0xFF00) >> 8;
				offset = data.find(' ', static_cast<size_t>(offset));

			}

			//element, elem strength, status effects, % chance effect, strength, defense, action, movement
			for (int count = 0; count < 8; count++) {

				while (data[offset] == ' ')
					offset++;

				binary[46 + count] = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
				offset = data.find(' ', static_cast<size_t>(offset));

			}

			//special effect, lv1 - 5, mlt
			for (int count = 0; count < 7; count++) {

				while (data[offset] == ' ')
					offset++;

				data16 = std::stoi(data.substr(static_cast<size_t>(offset), data.find(' ', static_cast<size_t>(offset)) - offset));
				binary[54 + (count * 2)] = data16 & 0x00FF;
				binary[55 + (count * 2)] = (data16 & 0xFF00) >> 8;
				offset = data.find(' ', static_cast<size_t>(offset));

			}

			for (int count = 0; count < 40; count++)
				binary[68 + count] = data[offset + count];

			for (int count = 0; count < 108; count++)
				fputc(binary[count].to_ulong(), output);

			offset = 0;

		}

	}
	else
		return;

	input.close();
	fclose(output);

}
*/

void writeMS(MoveStruct* moves, ImU16& count) {

	std::ofstream output("content/data/afs/xls_data/MS_PARAM.BIN", std::ios::binary);

	if (!output.is_open())
		return;

	for (size_t i = 0; i < count; i++) {

		/*
		ImU8 id = 0;// overwrite this with array index, used to reference which move manaeggs and specials reference
		ImU8 icon = 0;
		char* name = new char[19]{};
		ImU16 cost = 0;
		ImU8 targetEffect = 0;
		ImU8 targetType = 0;
		ImU16 str = 0;
		ImU16 pow = 0;
		ImU16 range = 0;
		ImU16 cast1 = 0;
		ImU16 cast5 = 0;
		ImU16 recovery = 0;
		ImU16 animation = 0;
		ImU8 unknown1 = 0;
		ImU8 unknown2 = 0;
		ImS16 ipDamage = 0;
		ImS16 ipCancelDamage = 0;
		ImS16 knockback = 0;
		ImU8 element = 0;
		ImU8 elementStr = 0;
		ImU8 ailmentsBitflag = 0;
		ImU8 ailmentsChance = 0;
		ImS8 atkMod = 0;
		ImS8 defMod = 0;
		ImS8 actMod = 0;
		ImS8 movMod = 0;
		ImU16 special = 0;
		ImU16 coinCost1 = 0;
		ImU16 coinCost2 = 0;
		ImU16 coinCost3 = 0;
		ImU16 coinCost4 = 0;
		ImU16 coinCost5 = 0;
		ImU16 multiplier = 0;
		char* description = new char[41]{};
		*/

		output.put(moves[i].id);
		output.put(moves[i].icon);
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
		output.put(moves[i].unknown2);
		
		output.put(moves[i].ipDamage);
		output.put((moves[i].ipDamage >> 8));
		
		output.put(moves[i].ipCancelDamage);
		output.put((moves[i].ipCancelDamage >> 8));
		
		output.put(moves[i].knockback);
		output.put((moves[i].knockback >> 8));

		output.put(moves[i].element);
		output.put(moves[i].elementStr);
		output.put(moves[i].ailmentsBitflag);
		output.put(moves[i].ailmentsChance);
		output.put(moves[i].atkMod);
		output.put(moves[i].defMod);
		output.put(moves[i].actMod);
		output.put(moves[i].movMod);
		
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

MoveStruct* readMS(ImU16& count) {

	char* readByte = new char[2]{};

	std::streampos fileSize = 0;
	std::ifstream input("content/data/afs/xls_data/MS_PARAM.BIN", std::ios::binary);

	fileSize = input.tellg();
	input.seekg(0, std::ios::end);
	fileSize = input.tellg() - fileSize;
	input.seekg(0, std::ios::beg);

	count = (fileSize / 108) + 1;
	MoveStruct* moves = new MoveStruct[(fileSize / 108) + 1];	//entries are 108 bytes long

	if (!input.is_open())
		throw new std::exception;

	for (int i = 0; !input.eof(); i++) {

		input.read(readByte, 1);
		moves[i].id = i;	//overwrite all IDs, we want them to be equal to the offset of the entry in the file

		input.read(readByte, 1);
		moves[i].icon = (ImU8)readByte[0];

		input.read(&moves[i].name[0], 18);
		replaceNulls(moves[i].name, 18);

		input.read(readByte, 2);
		moves[i].cost = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 1);
		moves[i].targetEffect = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].targetType = (ImU8)readByte[0];

		input.read(readByte, 2);
		moves[i].str = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((ImU8)(readByte[0]));

		input.read(readByte, 2);
		moves[i].pow = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].range = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].cast1 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].cast5 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].recovery = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].animation = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 1);
		moves[i].unknown1 = (ImU8)readByte[0];

		input.read(readByte, 1);
		moves[i].unknown2 = (ImU8)readByte[0];

		input.read(readByte, 2);
		moves[i].ipDamage = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));
		
		input.read(readByte, 2);
		moves[i].ipCancelDamage = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].knockback = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

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
		moves[i].special = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost1 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost2 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost3 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost4 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].coinCost5 = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(readByte, 2);
		moves[i].multiplier = ((ImU16)((unsigned char)(readByte[1])) << 8) + (ImU16)((unsigned char)(readByte[0]));

		input.read(&moves[i].description[0], 40);
		replaceNulls(moves[i].description, 40);

	}

	input.close();

	return moves;

}