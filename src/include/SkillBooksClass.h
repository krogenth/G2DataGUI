#ifndef SKILL_BOOKS_CLASS_H
#define SKILL_BOOKS_CLASS_H

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
	
	void storeSkills(SkillStruct* skills, size_t numSkills) { _skills = skills; _numSkills = numSkills; };
	SkillBookStruct* getSkillBooks() {

		if (_skillbooks.size())
			return &_skillbooks.at(0);
		else
			return nullptr;

	};
	size_t getNumSKillBooks() { return _skillbooks.size(); };
	

private:
	std::vector<SkillBookStruct> _skillbooks;
	size_t _bookIndex = 0;
	size_t _skillIndex = 0;

	SkillStruct* _skills = nullptr;
	size_t _numSkills = 0;

};

#endif