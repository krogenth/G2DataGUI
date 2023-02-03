#include <string>
#include <type_traits>

#include "./imgui.h"

template <typename T>
ImGuiDataType getDataType(const T* data) {
    bool is_signed = std::is_signed<T>::value;
    switch(sizeof(T)) {
        case 2: {
            return is_signed ? ImGuiDataType_S16 : ImGuiDataType_U16;
        }
        case 4: {
            return is_signed ? ImGuiDataType_S32 : ImGuiDataType_U32;
        }
        case 8: {
            return is_signed ? ImGuiDataType_S64 : ImGuiDataType_U64;
        }
        default: {
            return is_signed ? ImGuiDataType_S8 : ImGuiDataType_U8;
        }
    }
}

template <typename T>
void drawInput(std::string label, T* data) {
    ImGuiDataType type = getDataType(data);
    ImGui::InputScalar(label.c_str(), type, data);
}

template <typename T>
void drawInputN(std::string label, T* data, int count = 1) {
    ImGuiDataType type = getDataType(data);
    ImGui::InputScalarN(label.c_str(), type, data, count);
}