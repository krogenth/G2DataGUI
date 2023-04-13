#pragma once
#include <cinttypes>

#include "../common/structs/Vector3.h"
#include "../common/structs/Vector2.h"

#pragma pack(1)
struct EnemyPosition {
    uint16_t index = 0;
	uint16_t unknown1 = 0;
    Vector2 minimum;
    Vector2 maximum;
    Vector3 position;

    Vector3 unknown2;
    Vector3 unknown3;
    Vector3 unknown4;
    Vector3 unknown5;
    Vector3 unknown6;
    Vector3 unknown7;
};
#pragma pack()