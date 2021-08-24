#include <fstream>

#include ".\include\ModelClass.h"

#include ".\include\common\io_util.h"

void ModelClass::write() {



}

void ModelClass::read(std::string filename) {

	uint32_t readBytes = 0;
	std::ifstream input(filename, std::ios::binary);
	uint32_t numEntries = 0;

	while (!input.eof()) {

		readBytes = readRaw<uint32_t>(input);

		switch (readBytes) {

		case 0x48444443:		//	CDDH header
			//	we don't care about the CDDH header, so just advance past it
			numEntries = readRaw<uint32_t>(input);
			input.seekg((numEntries * 4) + 4, std::ios::cur);
			break;

		case 0x4C584947:		//	GIXL section
			//	GIXL's start a new model, so place a new one in back
			this->_models.emplace_back(modelStruct());
			numEntries = readRaw<uint32_t>(input);
			for(size_t i = 0; i < numEntries; i++)
				this->_models.back().textureMagic.magicTex.push_back(readRaw<uint32_t>(input));
			//	compare every model to the last one(the one we just inserted)
			for (auto it = this->_models.begin(); it != this->_models.end() - 1; it++) {

				if ((it->textureMagic.magicTex.size() == this->_models.back().textureMagic.magicTex.size()) && std::equal(it->textureMagic.magicTex.begin(), it->textureMagic.magicTex.end(), this->_models.back().textureMagic.magicTex.begin())) {

					this->_models.pop_back();
					//	since we don't care about this entire model, move to the next significant section(GIXL or MIXL)
					uint32_t magicCheck = 0;
					do {
						magicCheck = readRaw<uint32_t>(input);
					} while (magicCheck != 0x4C584947 || magicCheck != 0x4C58494D);
					break;

				}

			}
			break;

		case 0x4D434A4E:		//	NJCM section
			break;

		case 0x30464F50:		//	POF0 section
			break;

		case 0x5F53434D:		//	MCS_ section
			break;

		case 0x4C58494D:		//	MIXL section
			break;

		case 0x4D444D4E:		//	NMDM section
			break;

		}

	}

	input.close();

}
void ModelClass::draw() {



}

void ModelClass::outputToCSV() {



}