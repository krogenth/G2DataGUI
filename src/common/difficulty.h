#pragma once

class Difficulty {
public:
    Difficulty(const Difficulty&) = delete;
    Difficulty(const Difficulty&&) = delete;

    static Difficulty& getInstance() {
        static Difficulty instance;
        return instance;
    }

    void setDifficulty(bool isHardMode = false) { isHard = isHardMode; }
    bool isHardMode() { return isHard; }
private:
    Difficulty() {}

    bool isHard = false;
};