#pragma once

#include <vector>
#include <string>

#include ".\ImGui.h"

struct njcmStruct;

//	header is 0x200 bytes long

#pragma pack(1)
struct MapEntriesStruct {		//	0x00000008 && 0x0000000C

	ImU32 ID = 0;		//	first is always 0x1000
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;
	float direction = 0.0f;
	float unknown = 0.0f;
	ImU32 unknown1 = 0;
	ImU32 unknown2 = 0;

};
#pragma pack()

#pragma pack(1)
struct InstancesStruct {		//	0x00000018 && 0x0000001C

	ImU16 ID = 0;
	ImU16 index = 0;
	ImU16 unknown = 0;
	ImU16 translation = 0;
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;
	float xAngle = 0.0f;
	float yAngle = 0.0f;
	float zAngle = 0.0f;
	float cx = 0.0f;
	float cy = 0.0f;
	float cz = 0.0f;

};
#pragma pack()

#pragma pack(1)
struct HTAStruct {				//	0x00000020 && 0x00000024

	ImU8 shape = 0;
	ImU8 type = 0;
	ImU8 trigger = 0;
	ImU32 unknown = 0;
	ImU8 unknown1 = 0;
	float xMin = 0.0f;
	float yMin = 0.0f;
	float zMin = 0.0f;
	float xMax = 0.0f;
	float yMax = 0.0f;
	float zMax = 0.0f;
	ImU32 unknown2 = 0;
	ImU32 unknown3 = 0;
	ImU32 unknown4 = 0;
	ImU32 unknown5 = 0;
	ImU32 unknown6 = 0;
	ImU32 unknown7 = 0;
	ImU32 unknown8 = 0;
	ImU32 unknown9 = 0;

};
#pragma pack()

#pragma pack(1)
struct EnemyPositionStruct {	//	0x00000038 && 0x0000003C

	ImU16 index = 0;
	ImU16 unknown = 0;
	float xPosMin = 0.0f;
	float zPosMin = 0.0f;
	float xPosMax = 0.0f;
	float zPosMax = 0.0f;
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;

	float unknownX = 0.0f;
	float unknownY = 0.0f;
	float unknownZ = 0.0f;
	float unknownX1 = 0.0f;
	float unknownY1 = 0.0f;
	float unknownZ1 = 0.0f;
	float unknownX2 = 0.0f;
	float unknownY2 = 0.0f;
	float unknownZ2 = 0.0f;

};
#pragma pack()

#pragma pack(1)
struct EnemyFirstUnknownStruct {

	ImU16 unknown = 0;
	ImU16 unknown1 = 0;
	ImU16 unknown2 = 0;
	ImU16 unknown3 = 0;
	ImU16 unknown4 = 0;
	float unknown5 = 0;
	ImU16 unknown6 = 0;
	ImU16 unknown7 = 0;
	ImU16 unknown8 = 0;
	ImU16 unknown9 = 0;

};
#pragma pack()

#pragma pack(1)
struct EnemyTypeStruct {

	ImU16 index = 0;			//	unsure about this
	ImU16 numEnemy = 0;
	ImU16 enemyOffset = 0;
	ImU16 unknown = 0;
	ImU16 unknown1 = 0;
	ImU16 unknown2 = 0;
	ImU16 unknown3 = 0;

};
#pragma pack()

#pragma pack(1)
struct EnemySecondUnknownStruct {

	ImU16 unknown = 0;
	ImU16 unknown1 = 0;
	ImU16 unknown2 = 0;
	ImU16 unknown3 = 0;
	ImU16 unknown4 = 0;
	ImU16 unknown5 = 0;
	ImU16 unknown6 = 0;
	ImU16 unknown7 = 0;
	ImU16 unknown8 = 0;
	ImU16 unknown9 = 0;
	ImU16 unknown10 = 0;
	ImU16 unknown11 = 0;
	ImU16 unknown12 = 0;
	ImU16 unknown13 = 0;
	ImU16 unknown14 = 0;
	ImU16 unknown15 = 0;
	ImU16 unknown16 = 0;
	ImU16 unknown17 = 0;
	ImU16 unknown18 = 0;
	ImU16 unknown19 = 0;

};
#pragma pack()

#pragma pack(1)
struct EnemyGroupStruct {		//	0x00000048 && 0x0000004C

	ImU16 index = 0;
	EnemyFirstUnknownStruct unknown;
	EnemyTypeStruct enemies[4];
	EnemySecondUnknownStruct unknown1;

};
#pragma pack()

#pragma pack(1)
struct MOSStruct {				//	0x00000050 && 0x00000054

	ImU16 ID = 0;
	ImU16 index = 0;
	ImU32 unknown = 0;
	ImS16 xPos = 0;
	ImS16 yPos = 0;
	ImS16 zPos = 0;
	ImU32 unknown1 = 0;
	ImU32 unknown2 = 0;
	ImU32 unknown3 = 0;
	ImU32 unknown4 = 0;
	ImU16 unknown5 = 0;

};
#pragma pack()

#pragma pack(1)
struct IconStruct {				//	0x00000098 && 0x0000009C

	ImU8 ID = 0;
	ImU16 unknown = 0;
	ImU8 unknown1 = 0;
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;
	float unknown2 = 0;
	float yAngle = 0;
	ImU16 item1 = 0;
	ImU16 item2 = 0;
	ImU16 item3 = 0;
	ImU16 flag = 0;

};
#pragma pack()

#pragma pack(1)
struct ShopItemStruct {

	ImU16 item = 0;		//	offset by 0x0800

};
#pragma pack()

#pragma pack(1)
struct ShopStruct {			//	0x000000A8 && 0x000000AC

	ImU16 mapID = 0;
	ImU16 catagories = 0;
	char weaponChar[8];
	ShopItemStruct weapons[12];
	char armorChar[8];
	ShopItemStruct armors[12];
	char jewelryChar[8];
	ShopItemStruct jewelry[12];
	char itemsChar[8];
	ShopItemStruct items[12];
	char regionalChar[8];
	ShopItemStruct regionals[12];

};
#pragma pack()

#pragma pack(1)
struct mdtHeader {

	ImU32 headerLength = 0;
	ImU32 unknown = 0;

	ImU32 numMapEntries = 0;
	ImU32 offsetMapEntries = 0;

	ImU32 unknown1 = 0;
	ImU32 unknown2 = 0;

	ImU32 numInstances = 0;
	ImU32 offsetInstances = 0;

	ImU32 numHTA = 0;
	ImU32 offsetHTA = 0;

	ImU32 numScripts = 0;
	ImU32 offsetScripts = 0;

	ImU32 unknown3 = 0;
	ImU32 unknown4 = 0;

	ImU32 numEnemyPos = 0;
	ImU32 offsetEnemyPos = 0;

	ImU32 unknown5 = 0;
	ImU32 unknown6 = 0;

	ImU32 numEnemyGroup = 0;
	ImU32 offsetEnemyGroup = 0;

	ImU32 numMOS = 0;
	ImU32 offsetMOS = 0;

	ImU32 unknown7 = 0;
	ImU32 unknown8 = 0;

	ImU32 unknown9 = 0;
	ImU32 unknown10 = 0;

	ImU32 unknown11 = 0;
	ImU32 unknown12 = 0;

	ImU32 unknown13 = 0;		//	check D800.mdt
	ImU32 unknown14 = 0;

	ImU32 lengthDialogue = 0;
	ImU32 offsetDialogue = 0;

	ImU32 unknown15 = 0;
	ImU32 unknown16 = 0;

	ImU32 unknown17 = 0;
	ImU32 unknown18 = 0;

	ImU32 unknown19 = 0;
	ImU32 unknown20 = 0;

	ImU32 numIcons = 0;
	ImU32 offsetIcons = 0;

	ImU32 unknown21 = 0;
	ImU32 unknown22 = 0;

	ImU32 lengthShop = 0;
	ImU32 offsetShop = 0;

	ImU32 unknown23 = 0;		//	so far always 0x0000
	ImU32 unknown24 = 0;		//	so far always 0x0000

	ImU32 unknown25 = 0;
	ImU32 unknown26 = 0;

	ImU32 unknown27 = 0;
	ImU32 unknown28 = 0;

};
#pragma pack()

struct MdtStruct {

	mdtHeader header;
	std::vector<MapEntriesStruct> mapEntries;
	std::vector<InstancesStruct> instances;
	std::vector<HTAStruct> HTA;
	std::vector<EnemyPositionStruct> enemyPositions;
	std::vector<EnemyGroupStruct> enemyGroups;
	std::vector<MOSStruct> MOS;
	std::vector<IconStruct> icons;
	std::vector<ShopStruct> shop;

	std::vector<uint32_t> models;

	std::string mapname = "";
	std::string filename = "";
	std::string filenameChr = "";

};

void writeMdt(std::vector<MdtStruct>& mdt);
void readMdt(std::vector<MdtStruct>& mdt, std::vector<njcmStruct>& models, std::string filepath = "content/data/afs/map");
void drawMdt(std::vector<MdtStruct>& mdt, char** mapIDs, bool* canClose, char** itemIDs, const size_t& numItems, std::vector<njcmStruct>& models, char** modelIDs);