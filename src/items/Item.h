#pragma once
#include <cinttypes>

#include "./ItemStats.h"
#include "./ItemEquipment.h"
#include "./ItemUsable.h"

// the game only has 700 items total, and this is split into segments with 100 entries(items) each
#pragma pack(1)
struct Item {
	char name[19];
	char description[41];
	ItemStats stats;
	ItemEquipment* equipmentOffset = nullptr; // 0xFFFFFFFF means null
	ItemUsable* usableOffset = nullptr; // 0xFFFFFFFF means null
	uint32_t id = 0; // overwrite this to array index, index is used for overworld items and enemy drops
};
#pragma pack()