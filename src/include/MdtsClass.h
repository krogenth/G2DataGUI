#pragma once

#include <vector>

#include "./BaseDataClass.h"

#include "./ItemsClass.h"
#include "./EnemiesClass.h"
#include "./ModelClass.h"

#pragma pack(1)
struct MapEntriesStruct {		//	0x00000008 && 0x0000000C

	uint32_t ID = 0;		//	first is always 0x1000
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;
	float direction = 0.0f;
	float unknown = 0.0f;
	uint32_t unknown1 = 0;
	uint32_t unknown2 = 0;

};
#pragma pack()

#pragma pack(1)
struct InstancesStruct {		//	0x00000018 && 0x0000001C
	uint16_t ID = 0;
	uint16_t index = 0;
	uint16_t unknown = 0;
	uint16_t translation = 0;
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
	uint8_t shape = 0;
	uint8_t type = 0;
	uint8_t trigger = 0;
	uint32_t unknown = 0;
	uint8_t unknown1 = 0;
	float xMin = 0.0f;
	float yMin = 0.0f;
	float zMin = 0.0f;
	float xMax = 0.0f;
	float yMax = 0.0f;
	float zMax = 0.0f;
	uint32_t unknown2 = 0;
	uint32_t unknown3 = 0;
	uint32_t unknown4 = 0;
	uint32_t unknown5 = 0;
	uint32_t unknown6 = 0;
	uint32_t unknown7 = 0;
	uint32_t unknown8 = 0;
	uint32_t unknown9 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyPositionStruct {	//	0x00000038 && 0x0000003C
	uint16_t index = 0;
	uint16_t unknown = 0;
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
	uint16_t unknown = 0;
	uint16_t unknown1 = 0;
	uint16_t unknown2 = 0;
	uint16_t unknown3 = 0;
	uint16_t unknown4 = 0;
	float unknown5 = 0;
	uint16_t unknown6 = 0;
	uint16_t unknown7 = 0;
	uint16_t unknown8 = 0;
	uint16_t unknown9 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyTypeStruct {
	uint16_t index = 0;			//	unsure about this
	uint16_t numEnemy = 0;
	uint16_t enemyOffset = 0;
	uint16_t unknown = 0;
	uint16_t unknown1 = 0;
	uint16_t unknown2 = 0;
	uint16_t unknown3 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemySecondUnknownStruct {
	uint16_t unknown = 0;
	uint16_t unknown1 = 0;
	uint16_t unknown2 = 0;
	uint16_t unknown3 = 0;
	uint16_t unknown4 = 0;
	uint16_t unknown5 = 0;
	uint16_t unknown6 = 0;
	uint16_t unknown7 = 0;
	uint16_t unknown8 = 0;
	uint16_t unknown9 = 0;
	uint16_t unknown10 = 0;
	uint16_t unknown11 = 0;
	uint16_t unknown12 = 0;
	uint16_t unknown13 = 0;
	uint16_t unknown14 = 0;
	uint16_t unknown15 = 0;
	uint16_t unknown16 = 0;
	uint16_t unknown17 = 0;
	uint16_t unknown18 = 0;
	uint16_t unknown19 = 0;
};
#pragma pack()

#pragma pack(1)
struct EnemyGroupStruct {		//	0x00000048 && 0x0000004C
	uint16_t index = 0;
	EnemyFirstUnknownStruct unknown;
	EnemyTypeStruct enemies[4];
	EnemySecondUnknownStruct unknown1;
};
#pragma pack()

#pragma pack(1)
struct MOSStruct {				//	0x00000050 && 0x00000054
	uint16_t ID = 0;
	uint16_t index = 0;
	uint32_t unknown = 0;
	int16_t xPos = 0;
	int16_t yPos = 0;
	int16_t zPos = 0;
	uint32_t unknown1 = 0;
	uint32_t unknown2 = 0;
	uint32_t unknown3 = 0;
	uint32_t unknown4 = 0;
	uint16_t unknown5 = 0;
};
#pragma pack()

#pragma pack(1)
struct IconStruct {				//	0x00000098 && 0x0000009C
	uint8_t ID = 0;
	uint16_t unknown = 0;
	uint8_t unknown1 = 0;
	float xPos = 0.0f;
	float yPos = 0.0f;
	float zPos = 0.0f;
	float unknown2 = 0;
	float yAngle = 0;
	uint16_t item1 = 0;
	uint16_t item2 = 0;
	uint16_t item3 = 0;
	uint16_t flag = 0;
};
#pragma pack()

#pragma pack(1)
struct ShopItemStruct {
	uint16_t item = 0;		//	offset by 0x0800
};
#pragma pack()

#pragma pack(1)
struct ShopStruct {			//	0x000000A8 && 0x000000AC
	uint16_t mapID = 0;
	uint16_t catagories = 0;
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
	uint32_t headerLength = 0;
	uint32_t unknown = 0;

	uint32_t numMapEntries = 0;
	uint32_t offsetMapEntries = 0;

	uint32_t unknown1 = 0;
	uint32_t unknown2 = 0;

	uint32_t numInstances = 0;
	uint32_t offsetInstances = 0;

	uint32_t numHTA = 0;
	uint32_t offsetHTA = 0;

	uint32_t numScripts = 0;
	uint32_t offsetScripts = 0;

	uint32_t unknown3 = 0;
	uint32_t unknown4 = 0;

	uint32_t numEnemyPos = 0;
	uint32_t offsetEnemyPos = 0;

	uint32_t unknown5 = 0;
	uint32_t unknown6 = 0;

	uint32_t numEnemyGroup = 0;
	uint32_t offsetEnemyGroup = 0;

	uint32_t numMOS = 0;
	uint32_t offsetMOS = 0;

	uint32_t unknown7 = 0;
	uint32_t unknown8 = 0;

	uint32_t unknown9 = 0;
	uint32_t unknown10 = 0;

	uint32_t unknown11 = 0;
	uint32_t unknown12 = 0;

	uint32_t unknown13 = 0;		//	check D800.mdt
	uint32_t unknown14 = 0;

	uint32_t lengthDialogue = 0;
	uint32_t offsetDialogue = 0;

	uint32_t unknown15 = 0;
	uint32_t unknown16 = 0;

	uint32_t unknown17 = 0;
	uint32_t unknown18 = 0;

	uint32_t unknown19 = 0;
	uint32_t unknown20 = 0;

	uint32_t numIcons = 0;
	uint32_t offsetIcons = 0;

	uint32_t unknown21 = 0;
	uint32_t unknown22 = 0;

	uint32_t lengthShop = 0;
	uint32_t offsetShop = 0;

	uint32_t unknown23 = 0;		//	so far always 0x0000
	uint32_t unknown24 = 0;		//	so far always 0x0000

	uint32_t unknown25 = 0;
	uint32_t unknown26 = 0;

	uint32_t unknown27 = 0;
	uint32_t unknown28 = 0;
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

	//std::vector<modelStruct*> models;

	std::string mapname = "";
	std::string filename = "";
	std::string filenameChr = "";
};

class MdtsClass : public BaseDataClass {
public:
	MdtsClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();
	void randomize();

	void storeItems(const std::vector<ItemStruct>* items) { _items = items; };
	void storeEnemies(const std::vector<EnemyStruct>* enemies) { _enemies = enemies; };
	void storeModels(const std::vector<modelStruct>* models) { _models = models; };
	const std::vector<MdtStruct>* getMdts() { return &_mdts; };

private:
	std::vector<MdtStruct> _mdts;
	size_t _mdtIndex = 0;
	size_t _mapEntryIndex = 0;
	size_t _instanceIndex = 0;
	size_t _htaIndex = 0;
	size_t _ePosIndex = 0;
	size_t _eGroupIndex = 0;
	size_t _eGroupPosIndex = 0;
	size_t _mosIndex = 0;
	size_t _iconIndex = 0;
	size_t _shopIndex = 0;

	size_t _shopWeaponIndex = 0;
	size_t _shopArmorIndex = 0;
	size_t _shopJewelIndex = 0;
	size_t _shopItemIndex = 0;
	size_t _shopRegionalIndex = 0;

	const std::vector<ItemStruct>* _items = nullptr;
	const std::vector<EnemyStruct>* _enemies = nullptr;
	const std::vector<modelStruct>* _models = nullptr;
};