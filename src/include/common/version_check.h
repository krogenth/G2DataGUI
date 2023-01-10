#pragma once
#include <filesystem>
#include <iostream>

class Version {
public:
    Version(const Version&) = delete;
    Version(const Version&&) = delete;

    static Version& getInstance() {
        static Version instance;
        return instance;
    };

    bool isHDVersion() { return isHD; };
private:
    Version() {
        if (!std::filesystem::exists("content")) {
            isHD = false;
        }
        std::cout << isHD << std::endl;
    };

    bool isHD = true;
};