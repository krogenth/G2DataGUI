#pragma once
#include <vector>

#include "../common/BaseDataInterface.h"
#include "./EnemyGroup.h"
#include "./EnemyPosition.h"
#include "./Header.h"
#include "./HTA.h"
#include "./Icon.h"
#include "./Instance.h"
#include "./MapEntry.h"
#include "./MOS.h"
#include "./Shop.h"

#include "../common/version_check.h"

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

class Mdts : public BaseDataInterface {
public:
	Mdts(const Mdts&) = delete;
    Mdts(const Mdts&&) = delete;

    static Mdts& getInstance() {
		static Mdts instance;
		return instance;
	};

	void write();
	void read();
	void draw();
	void outputToCSV();
	void randomize();

	const std::vector<MdtStruct>& getMdts() { return _mdts; };

private:
	Mdts() {
		_directory = (Version::getInstance().isHDVersion() ? "./content/data/afs/map/" : "./data/afs/map/");
		read();
	};

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
};