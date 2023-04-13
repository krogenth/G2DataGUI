#pragma once
#include <vector>

#include "./Skill.h"

#include "../common/BaseDataInterface.h"
#include "../common/version_check.h"

class Skills : public BaseDataInterface {
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

	const std::vector<Skill>& getSkills() { return _skills; };

private:
	Skills() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/SK_PARAM.BIN" : "./data/afs/xls_data/SK_PARAM.BIN");
		read();
	};

	std::vector<Skill> _skills;
	size_t _skillIndex = 0;
};