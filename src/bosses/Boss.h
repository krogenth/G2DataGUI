#pragma once
#include <vector>
#include <string>

#include "../enemies/EnemyStats.h"
#include "../enemies/EnemyAI.h"
#include "../enemies/EnemyMoves.h"

struct Boss {
    char* name = new char[19]{};
    EnemyStats stats;
    EnemyAI ai[6]{};
    std::vector<EnemyMoves> moveSets{};
    bool isSecond = false;

    std::string filename = "";
};