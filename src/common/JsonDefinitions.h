#include <unordered_map>
#include <vector>
#include <fstream>

#include <nlohmann/json.hpp>

class JsonDefinitions {
public:
    JsonDefinitions(const JsonDefinitions&) = delete;
    JsonDefinitions(const JsonDefinitions&&) = delete;

    static JsonDefinitions& getInstance() {
        static JsonDefinitions instance;
        return instance;
    };

    std::vector<std::string> getDefinitions(std::string key) {
        auto iter = definitions.find(key);
        if (iter == definitions.end()) {
            return definitions["empty"];
        } else {
            return iter->second;
        }
    };

private:
    JsonDefinitions() {
        this->definitions = {
            {"empty", std::vector<std::string>{}},
        };
        this->readJson();
    };

    void readJson() {
        std::ifstream file("definitions.json");
        if (!file.is_open()) {
            throw std::exception("Unable to find definitions.json!");
        }

        auto json = nlohmann::json::parse(file);
        this->parseJson(json);
    };
    void parseJson(const nlohmann::json& json) {
        for (const auto& [key, value] : json.items()) {
            definitions[key] = std::vector<std::string>();
            for (auto it = value.begin(); it != value.end(); ++it) {
                definitions[key].push_back(*it);
            }
        }
    };

    std::unordered_map<std::string, std::vector<std::string>> definitions;
};