#include <fstream>
#include <filesystem>
#include <format>

#include "./Levelups.h"

#include "../common/io_util.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "../ui/imgui_wrappers.h"
#include "./imgui.h"

void Levelups::write() {
    std::ofstream output;
    output.open(_filename, std::ios::binary);

    if (!output.is_open()) {
        throw new std::exception("TB_LVUP.BIN not found to be written!");
    }

    for (size_t characterIndex = 0; characterIndex < _levelups.size(); characterIndex++) {
        writeRaw<CharacterLevelups>(output, _levelups.at(characterIndex));
        // for (size_t levelupIndex = 0; levelupIndex < MAX_LEVEL; levelupIndex++) {
        //     writeRaw<Levelup>(output, _levelups.at(characterIndex).levelups[levelupIndex]);
        // }
    }
}

void Levelups::read() {
    std::ifstream input(_filename, std::ios::binary);
    
    if (!input.is_open()) {
        throw new std::exception("TB_LVUP.BIN not found to be read!");
    }

    std::filesystem::path filePath(_filename);
    auto fileSize = std::filesystem::file_size(filePath);
    _levelups.resize(fileSize / (24 * MAX_LEVEL)); // entries are 24 bytes long, and 99 entries per character

    for (size_t characterIndex = 0; characterIndex < _levelups.size(); characterIndex++) {
        _levelups.at(characterIndex) = readRaw<CharacterLevelups>(input);
        // for (size_t levelupIndex = 0; levelupIndex < MAX_LEVEL; levelupIndex++) {
        //     _levelups.at(characterIndex).levelups[levelupIndex] = readRaw<Levelup>(input);
        // }
    }

    input.close();
}

void Levelups::draw() {
    ImGui::Begin("LEVEUPS");

    auto characterDefs = JsonDefinitions::getInstance().getDefinitions("characters");

    if (ImGui::Button("Save")) {
        write();
    }

    if (ImGui::BeginCombo("Character", characterDefs.at(_characterIndex).c_str())) {
        for (size_t index = 0; index < _levelups.size(); index++) {
            ImGui::PushID((int)index);
            bool is_selected = (index == _characterIndex);
            if (ImGui::Selectable(characterDefs.at(index).c_str(), is_selected)) {
                _characterIndex = index;
            }
            if (is_selected) {
                ImGui::SetItemDefaultFocus();
            }
            ImGui::PopID();
        }
        ImGui::EndCombo();
    }

    if (ImGui::BeginCombo("Level", std::format("Level {}", _characterLevelupIndex + 1).c_str())) {
        for (size_t index = 0; index < MAX_LEVEL; index ++) {
            ImGui::PushID((int)index);
            bool is_selected = (index == _characterLevelupIndex);
            if (ImGui::Selectable(std::format("Level {}", index + 1).c_str(), is_selected)) {
                _characterLevelupIndex = index;
            }
            if (is_selected) {
                ImGui::SetItemDefaultFocus();
            }
            ImGui::PopID();
        }
        ImGui::EndCombo();
    }

    drawInput("EXP", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].exp);
    drawInput("HP Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].hpIncrease);
    drawInput("MP Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].mpIncrease);
    drawInput("SP Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].spIncrease);
    drawInput("STR Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].strIncrease);
    drawInput("VIT Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].vitIncrease);
    drawInput("MAG Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].magIncrease);
    drawInput("MEN Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].menIncrease);
    drawInput("ACT Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].actIncrease);
    drawInput("MOV Increase", &_levelups.at(_characterIndex).levelups[_characterLevelupIndex].movIncrease);
    drawInput("Skill Slot Increase",&_levelups.at(_characterIndex).levelups[_characterLevelupIndex].skillSlotIncrease);

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

    for (size_t characterIndex = 0; characterIndex < _levelups.size(); characterIndex++) {
        for (size_t characterLevelupIndex = 0; characterLevelupIndex < MAX_LEVEL; characterLevelupIndex++) {
            output << characterDefs.at(characterIndex) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].exp) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].hpIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].mpIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].spIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].strIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].vitIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].actIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].movIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].magIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].menIncrease) << ','
                << std::to_string(_levelups.at(characterIndex).levelups[characterLevelupIndex].skillSlotIncrease) << '\n';
        }
    }

    output.close();
}