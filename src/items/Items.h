#pragma once

#include <vector>

#include "../common/version_check.h"

#include "../common/BaseDataInterface.h"
#include "./Item.h"

class Items : public BaseDataInterface {
public:
	Items(const Items&) = delete;
    Items(const Items&&) = delete;

	static Items& getInstance() {
		static Items instance;
		return instance;
	};

	void write();
	void read();
	void draw();
	void outputToCSV();

	const std::vector<Item>& getItems() { return _items; };

private:
	Items() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/ITEM.BIN" : "./data/afs/xls_data/ITEM.BIN");
		read();
	};
	std::vector<Item> _items;
	size_t _itemIndex = 0;
	bool _hasEquip = false;
	bool _hasUsable = false;
	bool EquipmentCharacterBitFlags[7] = {};
	bool EquipmentAilmentBitFlags[8] = {};
	bool UsableAilmentBitFlags[8] = {};
};