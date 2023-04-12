#include "./string_manip.h"

void replaceNulls(char* str, uint32_t size) {
	for (uint32_t count = 0; count < size; count++) {
		if (str[count] == '\0') {
			str[count] = ' ';
		} else {
			return;
		}
	}
}