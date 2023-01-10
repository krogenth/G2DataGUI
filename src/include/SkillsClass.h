#pragma once

#include <vector>

#include "./common/version_check.h"

#include "./BaseDataClass.h"

#pragma pack(1)
struct SkillStatsStruct {
	uint8_t cost1 = 0;
	uint8_t cost2 = 0;
	uint16_t baseHp = 0;
	uint16_t baseMp = 0;
	uint16_t baseSp = 0;
	uint16_t baseStr = 0;
	uint16_t baseVit = 0;
	uint16_t baseAct = 0;
	uint16_t baseMov = 0;
	uint16_t baseMag = 0;
	uint16_t baseMen = 0;
	uint8_t unknown1 = 0;
	uint8_t unknown2 = 0;
	uint8_t unknown3 = 0;
	uint8_t unknown4 = 0;
	uint8_t unknown5 = 0;
	int8_t increaseFirePercent = 0;
	int8_t increaseWindPercent = 0;
	int8_t increaseEarthPercent = 0;
	int8_t increaseLightningPercent = 0;
	int8_t increaseBlizzardPercent = 0;
	int8_t increaseWaterPercent = 0;
	int8_t increaseExplosionPercent = 0;
	int8_t increaseForestPercent = 0;
	uint8_t special = 0;
	uint16_t coinCost1 = 0;
	uint16_t coinCost2 = 0;
	uint16_t coinCost3 = 0;
	uint16_t coinCost4 = 0;
	uint16_t coinCost5 = 0;
	uint16_t multiplier = 0;
};
#pragma pack()

struct SkillStruct {
	char name[19];
	SkillStatsStruct stats;
	char description[41];
};

class Skills : public BaseDataClass {
public:
	Skills(const Skills&) = delete;
    Skills(const Skills&&) = delete;

    static Skills& getInstance() {
		static Skills instance;
		return instance;
	};
	
	void write();
	void read();
	void draw();
	void outputToCSV();

	const std::vector<SkillStruct>* getSkills() { return &_skills; };
	size_t getNumSkills() { return _skills.size(); };

private:
	Skills() {
		this->_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/SK_PARAM.BIN" : "./data/afs/xls_data/SK_PARAM.BIN");
	};

	std::vector<SkillStruct> _skills;
	size_t _skillIndex = 0;
};