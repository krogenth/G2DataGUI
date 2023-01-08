#pragma once

#include <string>

class BaseDataClass {

public:
	virtual void write() {};
	virtual void read(std::string) {};
	virtual void draw() {};
	virtual void outputToCSV() {};

protected:
	std::string _filename;

};