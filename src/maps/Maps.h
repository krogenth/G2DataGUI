#pragma once
#include <vector>

#include "../common/BaseDataInterface.h"
#include "./Map.h"

#include "../common/version_check.h"

class Maps : public BaseDataInterface {
public:
	Maps(const Maps&) = delete;
    Maps(const Maps&&) = delete;

    static Maps& getInstance() {
		static Maps instance;
		return instance;
	};

	void write();
	void read();
	void draw();
	void outputToCSV();
	void randomize();

	const std::vector<MdtStruct>& getMaps() { return _maps; };

private:
	Maps() {
		_directory = (Version::getInstance().isHDVersion() ? "./content/data/afs/map/" : "./data/afs/map/");
		read();
	};

	std::vector<MdtStruct> _maps;
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