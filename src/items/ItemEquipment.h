#pragma once
#include <cinttypes>

#pragma pack(1)
struct ItemEquipment {
	uint16_t characterBitflag = 0;
	int16_t str = 0;
	int16_t vit = 0;
	int16_t act = 0;
	int16_t mov = 0;
	uint8_t effectiveOn = 0;
	int8_t fireAffinity = 0;
	int8_t windAffinity = 0;
	int8_t earthAffinity = 0;
	int8_t lightningAffinity = 0;
	int8_t blizzardAffinity = 0;
	uint8_t ailmentsBitflag = 0b00000000;
	uint8_t ailmentsChance = 0;
	int8_t increaseFirePercent = 0;
	int8_t increaseWindPercent = 0;
	int8_t increaseEarthPercent = 0;
	int8_t increaseLightningPercent = 0;
	int8_t increaseBlizzardPercent = 0;
	int8_t increaseWaterPercent = 0;
	int8_t increaseExplosionPercent = 0;
	int8_t increaseForestPercent = 0;
	uint16_t special = 0;
};
#pragma pack()