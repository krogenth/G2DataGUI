#pragma once
#include "imgui.h"

struct SkillStruct {

public:
	char* name = new char[19]{};
	ImU8 cost1 = 0;
	ImU8 cost2 = 0;
	ImU16 baseHp = 0;
	ImU16 baseSp = 0;
	ImU16 baseStr = 0;
	ImU16 baseVit = 0;
	ImU16 baseAct = 0;
	ImU16 baseMov = 0;
	ImU16 baseMag = 0;
	ImU16 baseMen = 0;
	ImU8 baseFirePercent = 0;
	ImU8 baseWindPercent = 0;
	ImU8 baseEarthPercent = 0;
	ImU8 baseLightningPercent = 0;
	ImU8 baseBlizzardPercent = 0;
	ImU8 baseWaterPercent = 0;
	ImU8 baseExplosionPercent = 0;
	ImU8 baseForestPercent = 0;
	ImU16 special = 0;
	ImU16 coinCost1 = 0;
	ImU16 coinCost2 = 0;
	ImU16 coinCost3 = 0;
	ImU16 coinCost4 = 0;
	ImU16 coinCost5 = 0;
	ImU16 multiplier = 0;
	char* description = new char[41]{};

};

/*
18 byte Name - name of skill
01 byte C - cost type(MC/SP)
02 byte HP - Base HP increase value
02 byte MP - Base MP increase value
02 byte SP - Base SP increase value
02 byte ST - Base STR increase value
02 byte VI - Base VIT increase value
02 byte AC - Base ACT increase value
02 byte MO - Base MOV increase value
02 byte MA - base MAG increase value
02 byte ME - Base MEN increase value
01 byte ?
01 byte ?
01 byte ?
01 byte ?
01 byte ?
01 byte FI - Base Fire damage dealt(1 = 10%)
01 byte WI - Base Wind damage dealt(1 = 10%)
01 byte EA - Base Earth damage dealt(1 = 10%)
01 byte LI - Base Lightning damage dealt(1 = 10%)
01 byte BZ - Base Blizzard damage dealt(1 = 10%)
01 byte WA - Base Water healing dealt(1 = 10%)
01 byte EX - Base Explosion damage dealt(1 = 10%)
01 byte FO - Base Forest damage dealt(1 = 10%)
01 byte SP - Special Effect
	Special_Effects.txt
02 byte LV1 - cost of lv1
02 byte LV2 - cost of lv2
02 byte LV3 - cost of lv3
02 byte LV4 - cost of lv4
02 byte LV5 - cost of lv5
02 byte Mlt - Skill value multiplier(Does not appear to affect anything)
40 byte Desc - Description of skill
*/