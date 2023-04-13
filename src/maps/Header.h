#pragma once
#include <cinttypes>

#pragma pack(1)
struct Header {
    uint32_t headerLength = 0;
    uint32_t unknown1 = 0;

    uint32_t numMapEntries = 0;
    uint32_t offsetMapEntries = 0;

    uint32_t unknown2 = 0;
    uint32_t unknown3 = 0;

    uint32_t numInstances = 0;
    uint32_t offsetInstances = 0;

    uint32_t numHTA = 0;
    uint32_t offsetHTA = 0;

    uint32_t numScripts = 0;
    uint32_t offsetScripts = 0;

    uint32_t unknown4 = 0;
    uint32_t unknown5 = 0;

    uint32_t numEnemyPos = 0;
    uint32_t offsetEnemyPos = 0;

    uint32_t unknown6 = 0;
    uint32_t unknown7 = 0;

    uint32_t numEnemyGroups = 0;
    uint32_t offsetEnemyGroups = 0;

    uint32_t numMOS = 0;
    uint32_t offsetMOS = 0;

    uint32_t unknown8 = 0;
    uint32_t unknown9 = 0;

    uint32_t unknown10 = 0;
    uint32_t unknown11 = 0;

    uint32_t unknown12 = 0;
    uint32_t unknown13 = 0;

    uint32_t unknown14 = 0; // check D800.mdt
    uint32_t unknown15 = 0;

    uint32_t lengthDialogue = 0;
    uint32_t offsetDialogue = 0;

    uint32_t unknown16 = 0;
    uint32_t unknown17 = 0;

    uint32_t unknown18 = 0;
    uint32_t unknown19 = 0;

    uint32_t unknown20 = 0;
    uint32_t unknown21 = 0;

    uint32_t numIcons = 0;
    uint32_t offsetIcons = 0;

    uint32_t unknown22 = 0;
    uint32_t unknown23 = 0;

    uint32_t lengthShop = 0;
    uint32_t offsetShop = 0;

    uint32_t unknown24 = 0; // so far always 0x0000
    uint32_t unknown25 = 0; // so far always 0x0000

    uint32_t unknown26 = 0;
    uint32_t unknown27 = 0;

    uint32_t unknown28 = 0;
    uint32_t unknown29 = 0;
};
#pragma pack()