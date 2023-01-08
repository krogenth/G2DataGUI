#pragma once

#include <vector>

#include "BaseDataClass.h"
#include "SkillsClass.h"

struct SkillImplementationStruct {
	uint8_t skillOffset = 0;
	uint8_t startingLevel = 0;
	uint8_t bookLevelRequired = 0;
	uint8_t unknown1 = 0;
};

struct SkillBookStruct {
	SkillImplementationStruct skills[6];
};

class SkillBooksClass : public BaseDataClass {
public:
	SkillBooksClass() {};
	void write();
	void read(std::string);
	void draw();
	void outputToCSV();
	void randomize();
	
	void storeSkills(const std::vector<SkillStruct>* skills) { _skills = skills; };
	const std::vector<SkillBookStruct>* getSkillBooks() { return &_skillbooks; };
	size_t getNumSKillBooks() { return _skillbooks.size(); };
	

private:
	std::vector<SkillBookStruct> _skillbooks;
	size_t _bookIndex = 0;
	size_t _skillIndex = 0;

	const std::vector<SkillStruct>* _skills = nullptr;
};