#pragma once
#include <cinttypes>
#include <string>

#include "./EnemyMoves.h"
#include "./EnemyStats.h"
#include "./EnemyAI.h"

struct Enemy {
	char* name = new char[19]{};
	EnemyStats stats;
	EnemyAI ai[6]{};
	EnemyMoves moveSet{};
	bool isSecond = false;

	std::string filename = "";
};