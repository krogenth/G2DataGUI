#pragma once
#include <cinttypes>

#pragma pack(1)
struct Levelup {
    int32_t exp = 0;
    int16_t hpIncrease = 0;
    int16_t mpIncrease = 0;
    int16_t spIncrease = 0;
    int16_t strIncrease = 0;
    int16_t vitIncrease = 0;
    int16_t actIncrease = 0;
    int16_t movIncrease = 0;
    int16_t magIncrease = 0;
    int16_t menIncrease = 0;
    int16_t skillSlotIncrease = 0;
};
#pragma pack()

#pragma pack(1)
struct CharacterLevelups {
    Levelup levelups[99];
};
#pragma pack()