#pragma once
#include <vector>
#include <string>
#include <fstream>

#include ".\imgui.h"


/*
A run down of the various chunks found within these files:
NJCM chunk:
	contains all of the vertex and bone structure of the mode, used to create the wire frame
NMDM chunk:
	contains all of the model motion for animations, this I have not dug into significantly, as it is not critical to the mod tools
POF0:
	dictates what type of pointer offset is used, POF0 is from the start of the chunk, POF1(which is never used) is from the start of the file
GIXL chunk:
	used within content/data/afs/C0x folders and content/data/afs/map folders, states the magic numbers to link the uv texture file to this specific model
GBIX chunk:
	used within content/data/afs/model to specify that PVRT's(essentially the same as the content/data/afs/map uv texture file) magic number specifier
MCS_ chunk:
	I have no clue, it has some data that seems significant, but unsure how it plays into the data itself
GPHD chunk:
	starts the model motion chunk section of a content/data/afs/map/xxxx/xxxx.chr file
GAMT chunk:
	starts all but the first(GPHD) MIXL chunk of a content/data/afs/map/xxxx/xxxx.chr file
MIXL chunk:
	specifies the number of NMDM chunks within this MIXL chunk, and specifies which NJCM chunk(within the current file, starting at index 0) this series of NMDM chunks is tied to
*/

#pragma pack(1)
struct GIXL_CHUNK {

	std::vector<ImU32> magicTex;						//	super magical *.dds offset 0x20 number used to link texture to model

};
#pragma pack()

#pragma pack(1)
struct POF0 {

	ImU32 length = 0;
	std::vector<ImU8> data;

};
#pragma pack()

#pragma pack(1)
struct MCS_ {

	ImU32 length = 0;
	ImU16 nbSizeOfIntChunks = 0;
	ImU16 unknown = 0;
	std::vector<ImU32> data;

};
#pragma pack()

//
//
//			NMDM(Model Motion)
//
//

#pragma pack(1)
struct nmdmStruct {

	ImU32 chunk_body_size = 0;
	std::vector<bool> chunk_body;
	POF0 chunk_pointer;

};
#pragma pack()

#pragma pack(1)
struct njcmStruct {

	size_t chunk_body_hash = 0;

	GIXL_CHUNK textureMagic;
	ImU32 chunk_body_size = 0;
	std::vector<bool> chunk_body;
	POF0 chunk_pointer;
	MCS_* chunk_mcs = nullptr;
	
	bool mixlRead = false;
	std::vector<nmdmStruct> motions;

	std::string filename = "";

};
#pragma pack()

void writeModel(std::vector<njcmStruct>& models);
void readModel(std::vector<njcmStruct>& models);													// used only for content/data/afs/model/xxx.dat files, used the same way as for C0x folders
void readModel(std::vector<njcmStruct>& models, MdtStruct* mdt, std::ifstream& input);				// used only for content/data/afs/map/xxxx/xxxx.chr files
void drawModel(std::vector<njcmStruct>& models, char** modelIDs, MdtStruct* mdt, bool* canClose);