#include ".\include\copypaste_obj.h"

void* clipboard = nullptr;
std::string objType = "";

void copyObj(void* obj, std::string type) {

	clipboard = obj;
	objType = type;

}

void* pasteObj() {

	return clipboard;

}

bool checkObjType(std::string type) {

	return (objType == type);

}