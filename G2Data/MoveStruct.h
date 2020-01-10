#pragma once
#include <tuple>

#include "imgui.h"

struct MoveStruct {

public:
	ImU8 id = 0;// overwrite this with array index, used to reference which move manaeggs and specials reference
	ImU8 icon = 0;
	char* name = new char[19]{};
	ImU16 cost = 0;
	ImU8 targetEffect = 0;
	ImU8 targetType = 0;
	ImU16 str = 0;
	ImU16 pow = 0;
	ImU16 range = 0;
	ImU16 cast1 = 0;
	ImU16 cast5 = 0;
	ImU16 recovery = 0;
	ImU16 animation = 0;
	ImU8 unknown1 = 0;
	ImU8 unknown2 = 0;
	ImS16 ipDamage = 0;
	ImS16 ipCancelDamage = 0;
	ImS16 knockback = 0;
	ImU8 element = 0;
	ImU8 elementStr = 0;
	ImU8 ailmentsBitflag = 0;
	ImU8 ailmentsChance = 0;
	ImS8 atkMod = 0;
	ImS8 defMod = 0;
	ImS8 actMod = 0;
	ImS8 movMod = 0;
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
01 byte OFF - entry # offset
01 byte ICON - icon used for entry(1 + element type)
18 byte Name - name of spell/move
02 byte C - cost of spell/move
01 byte T1 - target effect
01 byte T2 - target type
02 byte STR - Strength added to base stat of character
02 byte POW - Power of spell/move
02 byte Range - Range the spell/move hits, based on T1 and T2
02 byte Cast1 - Time it takes to cast spell/move, lower = faster
02 byte Cast5 - Time it takes to cast spell/move, lower = faster
02 byte Rec - recovery time
02 byte Ani - animation of move
01 byte ?
01 byte ?
02 byte IP - IP damage from spell/move
02 byte IPC - IP cancel damage from spell/move
02 byte KB - knockback from spell/move
01 byte E - element of spell/move
01 byte ES - element strength of spell/move
01 byte S - status effect bitflag(Add together for multiple effects)
01 byte % - % chance of status effect
02 byte ATK - attack stat change
02 byte DEF - defense stat change
02 byte ACT - action stat change
02 byte MOV - movement stat change
02 byte SP - special effect
02 byte LV1 - cost of lv1
02 byte LV2 - cost of lv2
02 byte LV3 - cost of lv3
02 byte LV4 - cost of lv4
02 byte LV5 - cost of lv5
02 byte Mlt - Power Multiplier for spell/move
40 byte Desc - Description of spell/move
*/

//void writeMS();
void writeMS(MoveStruct* moves, ImU16& count);
MoveStruct * readMS(ImU16& count);