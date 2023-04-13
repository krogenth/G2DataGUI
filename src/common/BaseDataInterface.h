#pragma once

#include <string>

class BaseDataInterface {
public:
	virtual void write() = 0;
	virtual void read() = 0;
	virtual void draw() = 0;
	virtual void outputToCSV() = 0;

protected:
	std::string _filename;
	std::string _directory;
};