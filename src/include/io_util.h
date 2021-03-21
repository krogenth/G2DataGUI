#pragma once

#include <fstream>

template<typename T>
T readRaw(std::ifstream& stream) {

	T val;
	stream.read(reinterpret_cast<char*>(&val), sizeof(T));
	//assert(!stream.fail());
	return val;

}

template<typename T>
T readRaw(std::fstream& stream) {

	T val;
	stream.read(reinterpret_cast<char*>(&val), sizeof(T));
	assert(!stream.fail());
	return val;

}

template<typename T>
void writeRaw(std::ofstream& stream, T& val) {

	stream.write(reinterpret_cast<char*>(&val), sizeof(T));
	assert(!stream.fail());

}

template<typename T>
void writeRaw(std::fstream& stream, T& val) {

	stream.write(reinterpret_cast<char*>(&val), sizeof(T));
	assert(!stream.fail());

}