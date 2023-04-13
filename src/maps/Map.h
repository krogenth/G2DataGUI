#pragma once
#include <string>
#include <vector>

#include "./EnemyGroup.h"
#include "./EnemyPosition.h"
#include "./Header.h"
#include "./HTA.h"
#include "./Icon.h"
#include "./Instance.h"
#include "./MapEntry.h"
#include "./MOS.h"
#include "./Shop.h"

#pragma pack(1)
struct MdtStruct {
	Header header;
	std::vector<MapEntry> mapEntries;
	std::vector<Instance> instances;
	std::vector<HTA> HTA;
	std::vector<EnemyPosition> enemyPositions;
	std::vector<EnemyGroup> enemyGroups;
	std::vector<MOS> MOS;
	std::vector<Icon> icons;
	std::vector<ShopStruct> shop;

	//std::vector<modelStruct*> models;

	std::string mapname = "";
	std::string filename = "";
	std::string filenameChr = "";
};
#pragma pack()