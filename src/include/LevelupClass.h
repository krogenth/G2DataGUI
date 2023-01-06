#pragma once

#include <vector>

#include "./BaseDataClass.h"

static const size_t maxLevel = 99;

#pragma pack(1)
struct LevelupStruct {
    int32_t exp = 0;
    int16_t hpIncrease = 0;
    int16_t mpIncrease = 0;
    int16_t spIncrease = 0;
    int16_t strIncrease = 0;
    int16_t vitIncrease = 0;
    int16_t magIncrease = 0;
    int16_t menIncrease = 0;
    int16_t movIncrease = 0;
    int16_t actIncrease = 0;
    int16_t skillSlotIncrease = 0;
};
#pragma pack()

struct CharacterLevelupStruct {
    LevelupStruct levelups[99];
};

class LevelupClass : public BaseDataClass {
public:
    LevelupClass() {};
    void write();
    void read(std::string);
    void draw();
    void outputToCSV();

    CharacterLevelupStruct* getLevelUps() {
        if (_levelups.size()) {
            return &_levelups.at(0);
        } else {
            return nullptr;
        }
    };

    size_t getNumCharacterLevelUps() { return _levelups.size(); };
private:
    std::vector<CharacterLevelupStruct> _levelups;
    size_t _characterIndex = 0;
    size_t _characterLevelupIndex = 0;
};