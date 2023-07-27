#pragma once

#include <vector>

#include "../common/version_check.h"

#include "../common/BaseDataInterface.h"
#include "./SkillBook.h"


class SkillBooks : public BaseDataInterface {
public:
	SkillBooks(const SkillBooks&) = delete;
    SkillBooks(const SkillBooks&&) = delete;

    static SkillBooks& getInstance() {
		static SkillBooks instance;
		return instance;
	};
	
	void write();
	void read();
	void draw();
	void outputToCSV();
	void randomize();
	
	const std::vector<SkillBook>& getSkillBooks() { return _skillbooks; };
	
private:
	SkillBooks() {
		_filename = (Version::getInstance().isHDVersion() ? "./content/data/afs/xls_data/TB_SKILL.BIN" : "./data/afs/xls_data/TB_SKILL.BIN");
		read();
	};

	std::vector<SkillBook> _skillbooks;
	size_t _bookIndex = 0;
	size_t _skillIndex = 0;
};