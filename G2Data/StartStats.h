#pragma once
#include "ImGui/imgui.h"

struct StartStatsStruct {

public:
	ImU32 exp = 0;
	ImU16 weapon = 0;
	ImU16 armour = 0;
	ImU16 headgear = 0;
	ImU16 footwear = 0;
	ImU16 accessory = 0;
	ImU16 manaEgg = 0;
	ImS16 stamina = 0;
	ImU16 unknown1 = 0;
	ImU16 unknown2 = 0;
	ImU16 unknown3 = 0;
	ImU16 unknown4 = 0;
	ImU16 unknown5 = 0;
	ImU16 unknown6 = 0;
	ImU16 unknown7 = 0;
	ImU16 unknown8 = 0;
	ImU16 unknown9 = 0;
	ImU16 unknown10 = 0;
	ImS16 ipStun = 0;
	ImS16 ipCancelStun = 0;
	ImU8 comboSpRegen = 0;
	ImU8 critSpRegen = 0;
	ImU8 unknown11 = 0;
	ImU8 hitSpRegen = 0;
	ImU8 unknown12 = 0;
	ImS8 evasionStillRate = 0;
	ImS8 evasionMovingRate = 0;
	ImS8 ResistKnockback = 0;// R KB from DreamCast Debug Menu
	ImU16 unknown13 = 0;
	ImS16 TREC = 0;// T REC from DreamCast Debug Menu
	ImS16 TDMG = 0;// T DMG from DreamCast Debug Menu
	ImU16 unknown14 = 0;
	ImS16 THEAL = 0;// T HEAL from DreamCast Debug Menu
	ImS16 size = 0;
	ImU16 unknown15 = 0;
	ImU16 unknown16 = 0;
	ImU16 unknown17 = 0;
	ImU16 unknown18 = 0;
	ImU16 unknown19 = 0;
	ImU16 unknown20 = 0;
	ImU16 unknown21 = 0;
	ImU16 unknown22 = 0;
	ImU16 unknown23 = 0;

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
2 byte ??? - Unknown
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
1 byte RKB - Resist Knockback(R KB from DreamCast Debug Menu)[Ryduo DreamCast Debug Menu value is 0.80, likely 1.0 - (value / 100)?]
2 byte ???
2 byte TR - TREC from DreamCast Debug Menu
2 byte TD - TDMG from DreamCast Debug Menu
2 byte ???
2 byte TH - THEAL from DreamCast Debug Menu
2 byte SZ - SIZE
*/

void writePC(StartStatsStruct* stats, const ImU16& count);
StartStatsStruct* readPC(ImU16& count);
void drawPC(StartStatsStruct* stats, ImU16& numStats, bool* canClose, char** itemIDs, ImU16& numItems);