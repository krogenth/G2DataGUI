#include <fstream>
#include <filesystem>
#include <exception>

#include ".\include\MdtStruct.h"
#include ".\include\modelStruct.h"
#include ".\include\io_util.h"
#include ".\include\char_constants.h"

void writeModel(std::vector<njcmStruct>& models) {



}

void readModel(std::vector<njcmStruct>& models, MdtStruct* mdt, std::ifstream& input) {

	//	for this function, we have already read in all model data from content/data/afs/model, so we need to grab each model within this ifstream, hash it, and compare it to all other hashes currently stored(there can't be a brand new one, I hope)
	//	after we find that model, we must store what index it is at this file's index(starting at 0) so we can quickly link each model motion to it

	uint32_t readByte = 0;
	uint8_t mixlCount = 0;
	uint8_t mixlModelIndex = 0;
	uint8_t njcmChunkCount = 0;
	njcmStruct temp;
	uint32_t skipCount = 0;
	bool didBreak = false;
	uint32_t nbMagicTex = 0;
	
	readByte = readRaw<uint32_t>(input);

	while (!input.eof()) {

		switch (readByte) {

		case 0x4C584947:		//	GIXL chunk			//	this chunk is already read in with the GBIX chunks before, so we can just skip it
			nbMagicTex = readRaw<ImU32>(input);
			nbMagicTex = readRaw<ImU32>(input);
			temp.textureMagic.magicTex.resize(nbMagicTex);
			for (size_t i = 0; i < nbMagicTex; i++)
				temp.textureMagic.magicTex[i] = readRaw<uint32_t>(input);
			//skipCount = readRaw<uint32_t>(input);
			//input.seekg(skipCount, std::ios::cur);
			break;

		case 0x4D434A4E:		//	NJCM chunk
			temp.chunk_body_size = readRaw<uint32_t>(input);
			temp.chunk_body.resize(temp.chunk_body_size);
			for (size_t i = 0; i < temp.chunk_body_size; i++)
				temp.chunk_body[i] = readRaw<uint8_t>(input);

			//temp.chunk_body_hash = boost::hash_range(temp.chunk_body.begin(), temp.chunk_body.end());
			temp.chunk_body_hash = std::hash<std::vector<bool>>{}(temp.chunk_body);

			didBreak = false;
			for (size_t i = 0; i < models.size(); i++) {

				if (temp.chunk_body_hash == models[i].chunk_body_hash) {

					didBreak = true;
					mdt->models.push_back(uint32_t(i));
					break;

				}

			}

			if (!didBreak) {

				models.push_back(njcmStruct());
				models.back() = temp;
				models.back().filename = mdt->filenameChr;
				mdt->models.push_back(uint32_t(models.size() - 1));

			}

			temp.chunk_body.clear();
			temp.textureMagic.magicTex.clear();
			break;

		case 0x30464F50:		//	POF0 chunk
			if (mdt->models.size() && models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions.size()) {	//	check if we have NMDM chunks read or not

				if (!models[(uint32_t)(mdt->models[mdt->models.size() - 1])].mixlRead) {

					skipCount = readRaw<ImU32>(input);
					input.seekg(skipCount, std::ios::cur);
					break;

				}

				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions[models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions.size() - 1].chunk_pointer.length = readRaw<ImU32>(input);
				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions[models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions.size() - 1].chunk_pointer.data.resize(models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions[models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions.size() - 1].chunk_pointer.length);
				for (size_t i = 0; i < models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions[models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions.size() - 1].chunk_pointer.length; i++)
					models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions[models[(uint32_t)(mdt->models[mdt->models.size() - 1])].motions.size() - 1].chunk_pointer.data[i] = readRaw<ImU8>(input);

			}
			else {	//	otherwise we can just skip it, we have it from previously

				models[models.size() - 1].chunk_pointer.length = readRaw<uint32_t>(input);
				models[models.size() - 1].chunk_pointer.data.resize(models[models.size() - 1].chunk_pointer.length);
				for (size_t i = 0; i < models[models.size() - 1].chunk_pointer.length; i++)
					models[models.size() - 1].chunk_pointer.data[i] = readRaw<uint8_t>(input);

			}
			break;

		case 0x5F53434D:		//	MCS_ chunk		//NPCs and characters have this chunk, since we cannot read in NPCs anywhere else, we must do so here
			if (!models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs) {

				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs = new MCS_;
				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->length = readRaw<ImU32>(input);
				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->nbSizeOfIntChunks = readRaw<ImU16>(input);
				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->unknown = readRaw<ImU16>(input);
				models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->data.resize(models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->nbSizeOfIntChunks);
				for (size_t i = 0; i < models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->nbSizeOfIntChunks; i++)
					models[(uint32_t)(mdt->models[mdt->models.size() - 1])].chunk_mcs->data[i] = readRaw<ImU32>(input);

			}
			break;

		case 0x4C58494D:		//	MIXL chunk
			input.seekg(4, std::ios::cur);											//	we can skip over the length, since we only want 2 bytes of data, we can skip again after
			mixlModelIndex = readRaw<uint8_t>(input);
			mixlCount = readRaw<uint8_t>(input);
			input.seekg(2, std::ios::cur);
			break;

		case 0x4D444D4E:		//	NMDM chunk
			if (mixlModelIndex >= mdt->models.size() || models[(uint32_t)(mdt->models[mixlModelIndex])].mixlRead) {

				skipCount = readRaw<ImU32>(input);
				input.seekg(skipCount, std::ios::cur);
				break;

			}
			models[(uint32_t)(mdt->models[mixlModelIndex])].motions.push_back(nmdmStruct());
			models[(uint32_t)(mdt->models[mixlModelIndex])].motions[models[(uint32_t)(mdt->models[mixlModelIndex])].motions.size() - 1].chunk_body_size = readRaw<uint32_t>(input);
			models[(uint32_t)(mdt->models[mixlModelIndex])].motions[models[(uint32_t)(mdt->models[mixlModelIndex])].motions.size() - 1].chunk_body.resize(models[(uint32_t)(mdt->models[mixlModelIndex])].motions[models[(uint32_t)(mdt->models[mixlModelIndex])].motions.size() - 1].chunk_body_size);
			for (size_t i = 0; i < models[(uint32_t)(mdt->models[mixlModelIndex])].motions[models[(uint32_t)(mdt->models[mixlModelIndex])].motions.size() - 1].chunk_body_size; i++)
				models[(uint32_t)(mdt->models[mixlModelIndex])].motions[models[(uint32_t)(mdt->models[mixlModelIndex])].motions.size() - 1].chunk_body[i] = readRaw<uint8_t>(input);
			njcmChunkCount++;
			if (njcmChunkCount == mixlCount) {

				njcmChunkCount = 0;
				models[(uint32_t)(mdt->models[mixlModelIndex])].mixlRead = true;

			}
			break;

		default:
			break;

		}

		readByte = readRaw<uint32_t>(input);

	}

}

void drawModel(std::vector<njcmStruct>& models, MdtStruct* mdt, bool* canClose) {

	static ImU16 modelSlot = 0;
	static ImU16 modelID = 0;

	if (mdt->models.size()) {

		if (ImGui::CollapsingHeader("Models")) {

			ImGui::Combo("Model #", &modelSlot, slotIDs, int(mdt->models.size()));
			ImGui::Text("MDT Filename source: %s", &mdt->filename[0]);
			//ImGui::Text("Model Filename: %s", models[mdt->models[modelSlot]].filename[0]);
			ImGui::InputText("MDT Filename", &mdt->filename[0], mdt->filename.length());
			ImGui::InputText("Filename", &models[mdt->models[modelSlot]].filename[0], models[mdt->models[modelSlot]].filename.length());

		}

	}

}