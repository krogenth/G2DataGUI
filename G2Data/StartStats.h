#pragma once
#include "imgui.h"

struct StratStatsStruct {

public:
	ImU32 exp = 0;
	ImU16 weapon = 0;
	ImU16 armor = 0;
	ImU16 headgear = 0;
	ImU16 footwear = 0;
	ImU16 accessory = 0;
	ImU16 manaEgg = 0;
	ImU16 stamina = 0;
	ImU8 ailmentResistBitflag = 0;
	ImU8 REGIST = 0;// (bit flag, from DreamCast Debug Menu)
	ImU32 unknown = 0;
	ImU16 hp = 0;
	ImU16 mp = 0;
	ImU16 sp = 0;
	ImU16 str = 0;
	ImU16 vit = 0;
	ImU16 agi = 0;
	ImU16 mov = 0;
	ImU16 mag = 0;
	ImU16 men = 0;
	ImU16 ipStun = 0;
	ImU16 ipCancelStun = 0;
	ImU8 comboSPRegen = 0;
	ImU8 critSPRegen = 0;
	ImU8 unknown2 = 0;
	ImU8 hitSpRegen = 0;
	ImU8 unkown3 = 0;
	ImU8 evasionStillRate = 0;
	ImU8 evasionMovingRate = 0;
	ImU8 RKB = 0;// R KB from DreamCast Debug Menu
	ImU16 unknown4 = 0;
	ImU16 TREC = 0;// TREC from DreamCast Debug Menu
	ImU16 TDMG = 0;// TDMG from DreamCast Debug Menu
	ImU16 unknown5 = 0;
	ImU16 THEAL = 0;// THEAL from DreamCast Debug Menu
	ImU16 size = 0;
	//18 remaining bytes, unsure what they are

};

/*
4 byte EXP - starting EXP
2 byte SW - Starting Weapon
2 byte SA - Starting Armour
2 byte SH - Starting Headgear
2 byte SF - Starting Footwear
2 byte SAC - Starting Accessory
2 byte SE - Starting Mana Egg
2 byte STA - Stamina(Duration chatacter can move before getting "tired out")
1 byte ST? - STATUS RESIST
1 byte ??? - REGIST(bit flag, from DreamCast Debug Menu)
4 byte ??? - Unknown
2 byte HP? - Maximum Starting HP?
2 byte MP? - Maximum Starting MP?
2 byte SP? - Maximum Starting SP?
2 byte STR? - Maximum Starting STR?
2 byte VIT? - Maximum Starting VIT?
2 byte AGI? - Maximum Starting AGI?
2 byte SPD? - Maximum Starting SPD?
2 byte MAG? - Maximum Starting MAG?
2 byte MEN? - Maximum Starting MEN?
2 byte IPS - IP Stun from hit
2 byte IPCS - IP Stun from cancel
1 byte oSP - SP gained from combo hits
1 byte rSP - SP gained from criital hits
1 byte ??? - Unknown
1 byte hSP - SP gained from getting damaged
1 byte ??? - Unknown
1 byte EV  - Evasion rate
1 byte EVM  - Evasion rate moving
1 byte RKB - R KB from DreamCast Debug Menu
2 byte ???
2 byte TR - TREC from DreamCast Debug Menu
2 byte TD - TDMG from DreamCast Debug Menu
2 byte ???
2 byte TH - THEAL from DreamCast Debug Menu
2 byte SZ - SIZE
*/