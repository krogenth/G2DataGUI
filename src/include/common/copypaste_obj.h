#pragma once

#include <string>

void copyObj(void* obj, std::string type);
void* pasteObj();

bool checkObjType(std::string type);