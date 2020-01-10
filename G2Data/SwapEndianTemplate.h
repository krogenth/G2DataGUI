#pragma once
#include <algorithm>

template <class T>
void swapEndian(T* typePtr) {

	unsigned char* memPtr = reinterpret_cast<unsigned char*>(typePtr);
	std::reverse(memPtr, memPtr + sizeof(T));

}