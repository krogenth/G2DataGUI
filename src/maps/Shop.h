#pragma once
#include <cinttypes>

#pragma pack(1)
struct ShopItem {
	uint16_t item = 0; //	offset by 0x0800
};
#pragma pack()

#pragma pack(1)
struct ShopSection {
    char title[8];
    ShopItem items[12];
};
#pragma pack()

#pragma pack(1)
struct ShopStruct {
	uint16_t mapID = 0;
	uint16_t catagories = 0;
    ShopSection weapons;
    ShopSection armors;
    ShopSection jewelry;
    ShopSection items;
    ShopSection regionals;
};
#pragma pack()