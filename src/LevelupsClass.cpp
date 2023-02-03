#include <fstream>
#include <filesystem>
#include <format>

#include "./include/LevelupsClass.h"

#include "./include/common/io_util.h"
#include "./include/common/copypaste_obj.h"

#include "./include/JsonDefinitions.h"

#include "./include/common/imgui_wrappers.h"
#include "./imgui.h"

void Levelups::write() {
    std::ofstream output;
    output.open(this->_filename, std::ios::binary);

    if (!output.is_open()) {
        throw new std::exception("TB_LVUP.BIN not found to be written!");
    }

    for (size_t characterIndex = 0; characterIndex < this->_levelups.size(); characterIndex++) {
        for (size_t levelupIndex = 0; levelupIndex < maxLevel; levelupIndex++) {
            writeRaw<LevelupStruct>(output, this->_levelups.at(characterIndex).levelups[levelupIndex]);
        }
    }
}

void Levelups::read() {
    std::ifstream input(this->_filename, std::ios::binary);
    
    if (!input.is_open()) {
        throw new std::exception("TB_LVUP.BIN not found to be read!");
    }

    std::filesystem::path filePath(this->_filename);
    auto fileSize = std::filesystem::file_size(filePath);
    this->_levelups.resize(fileSize / (24 * maxLevel)); // entries are 24 bytes long, and 99 entries per character

    for (size_t characterIndex = 0; characterIndex < this->_levelups.size(); characterIndex++) {
        for (size_t levelupIndex = 0; levelupIndex < maxLevel; levelupIndex++) {
            this->_levelups.at(characterIndex).levelups[levelupIndex] = readRaw<LevelupStruct>(input);
        }
    }

    input.close();
}

void Levelups::draw() {
    ImGui::Begin("LEVEUPS");

    auto characterDefs = JsonDefinitions::getInstance().getDefinitions("characters");

    if (ImGui::Button("Save")) {
        this->write();
    }

    if (ImGui::BeginCombo("Character", characterDefs.at(this->_characterIndex).c_str())) {
        for (size_t index = 0; index < this->_levelups.size(); index++) {
            ImGui::PushID((int)index);
            bool is_selected = (index == this->_characterIndex);
            if (ImGui::Selectable(characterDefs.at(index).c_str(), is_selected)) {
                this->_characterIndex = index;
            }
            if (is_selected) {
                ImGui::SetItemDefaultFocus();
            }
            ImGui::PopID();
        }
        ImGui::EndCombo();
    }

    if (ImGui::BeginCombo("Level", std::format("Level {}", this->_characterLevelupIndex + 1).c_str())) {
        for (size_t index = 0; index < maxLevel; index ++) {
            ImGui::PushID((int)index);
            bool is_selected = (index == this->_characterLevelupIndex);
            if (ImGui::Selectable(std::format("Level {}", index + 1).c_str(), is_selected)) {
                this->_characterLevelupIndex = index;
            }
            if (is_selected) {
                ImGui::SetItemDefaultFocus();
            }
            ImGui::PopID();
        }
        ImGui::EndCombo();
    }

    drawInput("EXP", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].exp);
    drawInput("HP Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].hpIncrease);
    drawInput("MP Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].mpIncrease);
    drawInput("SP Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].spIncrease);
    drawInput("STR Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].strIncrease);
    drawInput("VIT Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].vitIncrease);
    drawInput("MAG Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].magIncrease);
    drawInput("MEN Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].menIncrease);
    drawInput("ACT Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].actIncrease);
    drawInput("MOV Increase", &this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].movIncrease);
    drawInput("Skill Slot Increase",&this->_levelups.at(this->_characterIndex).levelups[this->_characterLevelupIndex].skillSlotIncrease);

    ImGui::End();
}

void Levelups::outputToCSV() {
    auto characterDefs = JsonDefinitions::getInstance().getDefinitions("characters");
    std::ofstream output;
    output.open("./csv/TB_LVUP.CSV");

    if (!output.is_open()) {
        return;
    }

    output << "Name,EXP,HP Increase,MP Increase,SP Increase,STR Increase,VIT Increase,ACT Increase,MOV Increase,MAG Increase,MEN Increase,Skill Slot Increase\n";

    for (size_t characterIndex = 0; characterIndex < this->_levelups.size(); characterIndex++) {
        for (size_t characterLevelupIndex = 0; characterLevelupIndex < maxLevel; characterLevelupIndex++) {
            output << characterDefs.at(characterIndex) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].exp) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].hpIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].mpIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].spIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].strIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].vitIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].actIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].movIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].magIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].menIncrease) << ','
                << std::to_string(this->_levelups.at(characterIndex).levelups[characterLevelupIndex].skillSlotIncrease) << '\n';
        }
    }

    output.close();
}