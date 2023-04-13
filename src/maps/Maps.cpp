#include <fstream>
#include <filesystem>
#include <random>
#include <format>

#include "./maps.h"
#include "../items/Items.h"

#include "../common/io_util.h"
#include "../common/string_manip.h"
#include "../common/copypaste_obj.h"

#include "../common/JsonDefinitions.h"

#include "./imgui.h"
#include "../ui/imgui_wrappers.h"

void Maps::write() {
	std::fstream output;
	uint32_t offset = 0;

	for (size_t i = 0; i < _maps.size(); i++) {
		output.open(_maps.at(i).filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open()) {
			throw new std::exception(_maps.at(i).filename.c_str());
		}

		// write in map entries
		output.seekg(_maps.at(i).header.offsetMapEntries, std::ios::beg);
		for (auto& val : _maps.at(i).mapEntries) {
			writeRaw<MapEntry>(output, val);
		}

		// write in instances
		output.seekg(_maps.at(i).header.offsetInstances, std::ios::beg);
		for (auto& val : _maps.at(i).instances) {
			writeRaw<Instance>(output, val);
		}

		// write in HTA
		output.seekg(_maps.at(i).header.offsetHTA, std::ios::beg);
		for (auto& val : _maps.at(i).HTA) {
			writeRaw<HTA>(output, val);
		}

		// write in enemy positions
		output.seekg(_maps.at(i).header.offsetEnemyPos, std::ios::beg);
		for (auto& val : _maps.at(i).enemyPositions) {
			writeRaw<EnemyPosition>(output, val);
		}

		// write in enemy groups
		output.seekg(_maps.at(i).header.offsetEnemyGroups, std::ios::beg);
		for (auto& val : _maps.at(i).enemyGroups) {
			writeRaw<EnemyGroup>(output, val);
		}

		// write in MOS
		output.seekg(_maps.at(i).header.offsetMOS, std::ios::beg);
		for (auto& val : _maps.at(i).MOS) {
			writeRaw<MOS>(output, val);
		}

		// write in icons
		output.seekg(_maps.at(i).header.offsetIcons, std::ios::beg);
		for (auto& val : _maps.at(i).icons) {
			writeRaw<Icon>(output, val);
		}

		// write in shop
		if (_maps.at(i).shop.size()) {
			for (ImU16 j = 0; j < 12; j++) {
				if (_maps.at(i).shop[0].weapons.items[j].item) {
					_maps.at(i).shop[0].weapons.items[j].item += 0x0800;
				}

				if (_maps.at(i).shop[0].armors.items[j].item) {
					_maps.at(i).shop[0].armors.items[j].item += 0x0800;
				}

				if (_maps.at(i).shop[0].jewelry.items[j].item) {
					_maps.at(i).shop[0].jewelry.items[j].item += 0x0800;
				}

				if (_maps.at(i).shop[0].items.items[j].item) {
					_maps.at(i).shop[0].items.items[j].item += 0x0800;
				}

				if (_maps.at(i).shop[0].regionals.items[j].item) {
					_maps.at(i).shop[0].regionals.items[j].item += 0x0800;
				}
			}

			output.seekg(_maps[i].header.offsetShop, std::ios::beg);
			writeRaw<ShopStruct>(output, _maps[i].shop[0]);

			for (ImU16 j = 0; j < 12; j++) {
				if (_maps.at(i).shop[0].weapons.items[j].item) {
					_maps.at(i).shop[0].weapons.items[j].item -= 0x0800;
				}

				if (_maps.at(i).shop[0].armors.items[j].item) {
					_maps.at(i).shop[0].armors.items[j].item -= 0x0800;
				}

				if (_maps.at(i).shop[0].jewelry.items[j].item) {
					_maps.at(i).shop[0].jewelry.items[j].item -= 0x0800;
				}

				if (_maps.at(i).shop[0].items.items[j].item) {
					_maps.at(i).shop[0].items.items[j].item -= 0x0800;
				}

				if (_maps.at(i).shop[0].regionals.items[j].item) {
					_maps.at(i).shop[0].regionals.items[j].item -= 0x0800;
				}
			}
		}

		output.close();
	}
}

void Maps::read() {
	uint8_t readChar = 0;
	std::ifstream input;
	uint32_t offset = 0;

	for (const auto& p : std::filesystem::directory_iterator(_directory)) {
		if (std::filesystem::is_directory(p)) {
			for (const auto& q : std::filesystem::directory_iterator(p)) {
				std::string filename = q.path().string();

				if (!std::strstr(filename.c_str(), ".mdt")) {
					continue;
				}

				input.open(filename.c_str(), std::ios::binary);

				if (!input.is_open()) {
					throw new std::exception(filename.c_str());
				}

				_maps.emplace_back(MdtStruct());
				_maps.back().header = readRaw<Header>(input);

				// read in map entries
				_maps.back().mapEntries.resize(_maps.back().header.numMapEntries);
				input.seekg(_maps.back().header.offsetMapEntries, std::ios::beg);
				for (size_t i = 0; i < _maps.back().mapEntries.size(); i++) {
					_maps.back().mapEntries[i] = readRaw<MapEntry>(input);
				}

				// read in instances
				_maps.back().instances.resize(_maps.back().header.numInstances);
				input.seekg(_maps.back().header.offsetInstances, std::ios::beg);
				for (size_t i = 0; i < _maps.back().instances.size(); i++) {
					_maps.back().instances[i] = readRaw<Instance>(input);
				}

				// read in HTA
				_maps.back().HTA.resize(_maps.back().header.numHTA);
				input.seekg(_maps.back().header.offsetHTA, std::ios::beg);
				for (size_t i = 0; i < _maps.back().HTA.size(); i++) {
					_maps.back().HTA[i] = readRaw<HTA>(input);
				}

				// read in enemy positions
				_maps.back().enemyPositions.resize(_maps.back().header.numEnemyPos);
				input.seekg(_maps.back().header.offsetEnemyPos, std::ios::beg);
				for (size_t i = 0; i < _maps.back().enemyPositions.size(); i++) {
					_maps.back().enemyPositions[i] = readRaw<EnemyPosition>(input);
				}

				// read in enemy groups
				_maps.back().enemyGroups.resize(_maps.back().header.numEnemyGroups);
				input.seekg(_maps.back().header.offsetEnemyGroups, std::ios::beg);
				for (size_t i = 0; i < _maps.back().enemyGroups.size(); i++) {
					_maps.back().enemyGroups[i] = readRaw<EnemyGroup>(input);
				}

				// read in MOS
				_maps.back().MOS.resize(_maps.back().header.numMOS);
				input.seekg(_maps.back().header.offsetMOS, std::ios::beg);
				for (size_t i = 0; i < _maps.back().MOS.size(); i++)
					_maps.back().MOS[i] = readRaw<MOS>(input);

				// read in icons
				_maps.back().icons.resize(_maps.back().header.numIcons);
				input.seekg(_maps.back().header.offsetIcons, std::ios::beg);
				for (size_t i = 0; i < _maps.back().icons.size(); i++) {
					_maps.back().icons[i] = readRaw<Icon>(input);
				}


				// read in shop
				if (_maps.back().header.offsetShop) {
					_maps.back().shop.resize(1);
					input.seekg(_maps.back().header.offsetShop, std::ios::beg);

					_maps.back().shop[0] = readRaw<ShopStruct>(input);

					for (ImU16 i = 0; i < 12; i++) {
						if (_maps.back().shop[0].weapons.items[i].item) {
							_maps.back().shop[0].weapons.items[i].item -= 0x0800;
						}

						if (_maps.back().shop[0].armors.items[i].item) {
							_maps.back().shop[0].armors.items[i].item -= 0x0800;
						}

						if (_maps.back().shop[0].jewelry.items[i].item) {
							_maps.back().shop[0].jewelry.items[i].item -= 0x0800;
						}

						if (_maps.back().shop[0].items.items[i].item) {
							_maps.back().shop[0].items.items[i].item -= 0x0800;
						}

						if (_maps.back().shop[0].regionals.items[i].item) {
							_maps.back().shop[0].regionals.items[i].item -= 0x0800;
						}
					}
				}
				
				input.seekg(0x78, std::ios::beg);
				uint32_t sectionLength = readRaw<uint32_t>(input);
				uint32_t sectionOffset = readRaw<uint32_t>(input);

				// dialogue section exists, lets do our thing
				if (sectionLength && sectionOffset) {
					input.seekg(sectionOffset, std::ios::beg);
					uint32_t diagHeaderSize = readRaw<uint32_t>(input);
					input.seekg(sectionOffset, std::ios::beg);

					for (size_t offset = 10; offset < diagHeaderSize; offset += 4) {
						input.seekg(offset, std::ios::cur);
						uint16_t diagOffset = readRaw<uint16_t>(input);
						// check if it's padding, if so, ignore it
						if (diagOffset == 0xFFFF) {
							continue;
						}
						input.seekg(sectionOffset + diagHeaderSize, std::ios::beg);
						input.seekg(diagOffset * 8, std::ios::cur);

						// check if it's the proper bytecode for a map name textbox
						if ((readRaw<uint8_t>(input) == (uint8_t)0x17) && (readRaw<uint8_t>(input) == (uint8_t)0x80)) {
							input.seekg(2, std::ios::cur);
							readChar = readRaw<uint8_t>(input);
							while (readChar != 0x17) {
								_maps.back().mapname.push_back(readChar);
								readChar = readRaw<uint8_t>(input);
							}

							// anything that contains the string "to " is not the current map name
							if (_maps.back().mapname.find("to ") != std::string::npos) {
								_maps.back().mapname.erase(_maps.back().mapname.begin(), _maps.back().mapname.end());
								input.seekg(sectionOffset, std::ios::beg);
								continue;
							}
							else break;

						}
						else {
							input.seekg(sectionOffset, std::ios::beg);
						}

					}

				}

				_maps.back().filename = filename;
				_maps.back().filenameChr = filename.substr(0, filename.length() - 4) + ".chr";

				input.close();
			}
		}
	}
}

void Maps::draw() {
	ImGui::Begin("AREAS");

	if (ImGui::Button("Save")) {
		write();
	}

	if (ImGui::BeginCombo("MDT Index", _maps.at(_mdtIndex).mapname.c_str())) {
		for (size_t i = 0; i < _maps.size(); i++) {
			ImGui::PushID((int)i);
			bool is_selected = (i == _mdtIndex);
			if (ImGui::Selectable(_maps.at(i).mapname.c_str(), is_selected)) {
				_mdtIndex = i;
				_mapEntryIndex = 0;
				_instanceIndex = 0;
				_htaIndex = 0;
				_ePosIndex = 0;
				_eGroupIndex = 0;
				_eGroupPosIndex = 0;
				_mosIndex = 0;
				_iconIndex = 0;
				_shopIndex = 0;
			}
			if (is_selected) {
				ImGui::SetItemDefaultFocus();
			}
			ImGui::PopID();
		}

		ImGui::EndCombo();
	}

	if (_maps.at(_mdtIndex).mapEntries.size()) {
		if (ImGui::CollapsingHeader("Map Entries")) {
			if (ImGui::BeginCombo("Map Entry #", std::to_string(_maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).ID).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).mapEntries.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _mapEntryIndex);
					if (ImGui::Selectable(std::to_string(_maps.at(_mdtIndex).mapEntries[i].ID).c_str(), is_selected)) {
						_mapEntryIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("Connected Map", &_maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).ID);
			drawInputN("X/Y/Z Position", (float*) & _maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).position, 3);
			drawInput("Direction", &_maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).direction);
			drawInput("Unknown #1", &_maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).unknown1);
			drawInput("Unknown #2", &_maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).unknown2);
			drawInput("Unknown #3", &_maps.at(_mdtIndex).mapEntries.at(_mapEntryIndex).unknown3);
		}
	}

	if (_maps.at(_mdtIndex).instances.size()) {
		if (ImGui::CollapsingHeader("Instances")) {
			if (ImGui::BeginCombo("Instance #", std::format("Instance {}", _instanceIndex + 1).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).instances.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _instanceIndex);
					if (ImGui::Selectable(std::format("Instance {}", i + 1).c_str(), is_selected)) {
						_instanceIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("ID", &_maps.at(_mdtIndex).instances.at(_instanceIndex).ID);
			drawInput("Index", &_maps.at(_mdtIndex).instances.at(_instanceIndex).index);
			drawInput("Unknown #1", &_maps.at(_mdtIndex).instances.at(_instanceIndex).unknown1);
			drawInput("Translation", &_maps.at(_mdtIndex).instances.at(_instanceIndex).translation);
			drawInput("Unknown #2", &_maps.at(_mdtIndex).instances.at(_instanceIndex).unknown2);
			drawInputN("X/Y/Z Position", (float*)&_maps.at(_mdtIndex).instances.at(_instanceIndex).position, 3);
			drawInputN("X/Y/Z Angle", (float*)&_maps.at(_mdtIndex).instances.at(_instanceIndex).angle, 3);
			drawInputN("CX/CY/CZ", (float*)&_maps.at(_mdtIndex).instances.at(_instanceIndex).c, 3);
		}
	}

	if (_maps.at(_mdtIndex).HTA.size()) {
		if (ImGui::CollapsingHeader("HTA")) {
			if (ImGui::BeginCombo("HTA #", std::format("HTA {}", _htaIndex + 1).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).HTA.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _htaIndex);
					if (ImGui::Selectable(std::format("HTA {}", i + 1).c_str(), is_selected)) {
						_htaIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("Shape", &_maps.at(_mdtIndex).HTA.at(_htaIndex).shape);
			drawInput("Type", &_maps.at(_mdtIndex).HTA.at(_htaIndex).type);
			drawInput("Trigger", &_maps.at(_mdtIndex).HTA.at(_htaIndex).trigger);
			drawInput("Unknown #1", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown1);
			drawInput("Unknown #2", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown2);
			drawInput("attribute", &_maps.at(_mdtIndex).HTA.at(_htaIndex).attribute);
			drawInputN("X/Y/Z Min", (float*)&_maps.at(_mdtIndex).HTA.at(_htaIndex).minimum, 3);
			drawInputN("X/Y/Z Max", (float*)&_maps.at(_mdtIndex).HTA.at(_htaIndex).maximum, 3);
			drawInput("Unknown #3", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown3);
			drawInput("Unknown #4", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown4);
			drawInput("Unknown #5", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown5);
			drawInput("Unknown #6", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown6);
			drawInput("Unknown #7", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown7);
			drawInput("Unknown #8", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown8);
			drawInput("Unknown #9", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown9);
			drawInput("Unknown #10", &_maps.at(_mdtIndex).HTA.at(_htaIndex).unknown10);
		}
	}

	if (_maps.at(_mdtIndex).enemyPositions.size()) {
		if (ImGui::CollapsingHeader("Enemy Positions")) {
			if (ImGui::BeginCombo("Enemy Pos #", std::format("Enemy Position {}", _ePosIndex + 1).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).enemyPositions.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _ePosIndex);
					if (ImGui::Selectable(std::format("Enemy Position {}", i + 1).c_str(), is_selected)) {
						_ePosIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("Index", &_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).index);
			drawInput("Unknown #1", &_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown1);
			drawInputN("X/Z Min", (float*)& _maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).minimum, 2);
			drawInputN("X/Z Max", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).maximum, 2);
			drawInputN("X/Y/Z Pos", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).position, 3);
			drawInputN("X/Y/Z #1", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown2, 3);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			drawInputN("X/Y/Z #2", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown3, 3);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			drawInputN("X/Y/Z #3", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown4, 3);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			drawInputN("X/Y/Z #4", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown5, 3);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			drawInputN("X/Y/Z #5", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown6, 3);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			drawInputN("X/Y/Z #6", (float*)&_maps.at(_mdtIndex).enemyPositions.at(_ePosIndex).unknown7, 3);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
		}
	}

	if (_maps.at(_mdtIndex).enemyGroups.size()) {
		if (ImGui::CollapsingHeader("Enemy Groups")) {
			if (ImGui::BeginCombo("Enemy Group #", std::format("Enemy Group {}", _eGroupIndex + 1).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).enemyGroups.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _eGroupIndex);
					if (ImGui::Selectable(std::format("Enemy Group {}", i + 1).c_str(), is_selected)) {
						_eGroupIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("Group Index", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).index);

			if (ImGui::BeginCombo("Enemy #", std::format("Enemy {}", _eGroupPosIndex + 1).c_str())) {
				// there can only ever be 4 unique enemies in a group, as far as I can tell
				for (size_t i = 0; i < 4; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _eGroupPosIndex);
					if (ImGui::Selectable(std::format("Enemy {}", i+ 1).c_str(), is_selected)) {
						_eGroupPosIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("Enemy Index", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].index);
			drawInput("# of Enemy", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].numEnemy);
			drawInput("Enemy Offset", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].enemyOffset);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("References enemy/boss.csv");
			drawInput("Unknown #1", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].unknown1);
			drawInput("Unknown #2", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].unknown2);
			drawInput("Unknown #3", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].unknown3);
			drawInput("Unknown #4", &_maps.at(_mdtIndex).enemyGroups.at(_eGroupIndex).enemies[_eGroupPosIndex].unknown4);
		}
	}

	if (_maps.at(_mdtIndex).MOS.size()) {
		if (ImGui::CollapsingHeader("MOS")) {
			if (ImGui::BeginCombo("MOS #", std::format("MOS {}", _mosIndex + 1).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).MOS.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _mosIndex);
					if (ImGui::Selectable(std::format("MOS {}", i + 1).c_str(), is_selected)) {
						_mosIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("ID", &_maps.at(_mdtIndex).MOS.at(_mosIndex).ID);
			drawInput("Index", &_maps.at(_mdtIndex).MOS.at(_mosIndex).index);
			drawInput("Unknown #1", &_maps.at(_mdtIndex).MOS.at(_mosIndex).unknown1);
			drawInputN("X/Y/Z Pos", (float*)&_maps.at(_mdtIndex).MOS.at(_mosIndex).position, 3);
			drawInput("Unknown #2", &_maps.at(_mdtIndex).MOS.at(_mosIndex).unknown2);
			drawInput("Unknown #3", &_maps.at(_mdtIndex).MOS.at(_mosIndex).unknown3);
			drawInput("Unknown #4", &_maps.at(_mdtIndex).MOS.at(_mosIndex).unknown4);
			drawInput("Unknown #5", &_maps.at(_mdtIndex).MOS.at(_mosIndex).unknown5);
			drawInput("Unknown #6", &_maps.at(_mdtIndex).MOS.at(_mosIndex).unknown6);
		}
	}

	if (_maps.at(_mdtIndex).icons.size()) {
		if (ImGui::CollapsingHeader("Icons")) {
			if (ImGui::BeginCombo("Icon #", std::format("Icon {}", _iconIndex + 1).c_str())) {
				for (size_t i = 0; i < _maps.at(_mdtIndex).icons.size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _iconIndex);
					if (ImGui::Selectable(std::format("Icon {}", i + 1).c_str(), is_selected)) {
						_iconIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("ID", &_maps.at(_mdtIndex).icons.at(_iconIndex).ID);
			drawInput("Unknown #1", &_maps.at(_mdtIndex).icons.at(_iconIndex).unknown1);
			drawInput("Unknown #2", &_maps.at(_mdtIndex).icons.at(_iconIndex).unknown2);
			drawInputN("X/Y/Z Pos", &_maps.at(_mdtIndex).icons.at(_iconIndex).xPos, 3);
			drawInput("Unknown #3", &_maps.at(_mdtIndex).icons.at(_iconIndex).unknown3);
			drawInput("Y Angle", &_maps.at(_mdtIndex).icons.at(_iconIndex).yAngle);
			if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unsure");

			if (ImGui::BeginCombo("Item #1", Items::getInstance().getItems().at(_maps.at(_mdtIndex).icons.at(_iconIndex).item1).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).icons.at(_iconIndex).item1);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).icons.at(_iconIndex).item1 = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Item #2", Items::getInstance().getItems().at(_maps.at(_mdtIndex).icons.at(_iconIndex).item2).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).icons.at(_iconIndex).item2);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected))
						_maps.at(_mdtIndex).icons.at(_iconIndex).item2 = (uint16_t)i;
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Item #3", Items::getInstance().getItems().at(_maps.at(_mdtIndex).icons.at(_iconIndex).item3).name)) {

				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {

					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).icons.at(_iconIndex).item3);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).icons.at(_iconIndex).item3 = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			drawInput("Flag", &_maps.at(_mdtIndex).icons.at(_iconIndex).flag);
		}
	}

	if (_maps.at(_mdtIndex).shop.size()) {
		if (ImGui::CollapsingHeader("Shops")) {
			if (ImGui::BeginCombo("Weapons #", std::format("Weapons {}", _shopWeaponIndex + 1).c_str())) {
				for (size_t i = 0; i < 12; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _shopWeaponIndex);
					if (ImGui::Selectable(std::format("Weapons {}", i + 1).c_str(), is_selected)) {
						_shopWeaponIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Weapon Item", Items::getInstance().getItems().at(_maps.at(_mdtIndex).shop[0].weapons.items[_shopWeaponIndex].item).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).shop[0].weapons.items[_shopWeaponIndex].item);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).shop[0].weapons.items[_shopWeaponIndex].item = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Armors #", std::format("Armors {}", _shopArmorIndex + 1).c_str())) {
				for (size_t i = 0; i < 12; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _shopArmorIndex);
					if (ImGui::Selectable(std::format("Armors {}", i + 1).c_str(), is_selected)) {
						_shopArmorIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Armor Item", Items::getInstance().getItems().at(_maps.at(_mdtIndex).shop[0].armors.items[_shopArmorIndex].item).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).shop[0].armors.items[_shopArmorIndex].item);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).shop[0].armors.items[_shopArmorIndex].item = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Jewelry #", std::format("Jewelry {}", _shopJewelIndex + 1).c_str())) {
				for (size_t i = 0; i < 12; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _shopJewelIndex);
					if (ImGui::Selectable(std::format("Jewelry {}", i + 1).c_str(), is_selected)) {
						_shopJewelIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Jewelry Item", Items::getInstance().getItems().at(_maps.at(_mdtIndex).shop[0].jewelry.items[_shopJewelIndex].item).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).shop[0].jewelry.items[_shopJewelIndex].item);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).shop[0].jewelry.items[_shopJewelIndex].item = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Items #", std::format("Item {}", _shopItemIndex + 1).c_str())) {
				for (size_t i = 0; i < 12; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _shopItemIndex);
					if (ImGui::Selectable(std::format("Item {}", i + 1).c_str(), is_selected)) {
						_shopItemIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Item Item", Items::getInstance().getItems().at(_maps.at(_mdtIndex).shop[0].items.items[_shopItemIndex].item).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).shop[0].items.items[_shopItemIndex].item);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).shop[0].items.items[_shopItemIndex].item = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Regionals #", std::format("Regionals {}", _shopRegionalIndex + 1).c_str())) {
				for (size_t i = 0; i < 12; i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _shopRegionalIndex);
					if (ImGui::Selectable(std::format("Regionals {}", i + 1).c_str(), is_selected)) {
						_shopRegionalIndex = i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}

			if (ImGui::BeginCombo("Regional Item", Items::getInstance().getItems().at(_maps.at(_mdtIndex).shop[0].regionals.items[_shopRegionalIndex].item).name)) {
				for (size_t i = 0; i < Items::getInstance().getItems().size(); i++) {
					ImGui::PushID((int)i);
					bool is_selected = (i == _maps.at(_mdtIndex).shop[0].regionals.items[_shopRegionalIndex].item);
					if (ImGui::Selectable(Items::getInstance().getItems().at(i).name, is_selected)) {
						_maps.at(_mdtIndex).shop[0].regionals.items[_shopRegionalIndex].item = (uint16_t)i;
					}
					if (is_selected) {
						ImGui::SetItemDefaultFocus();
					}
					ImGui::PopID();
				}

				ImGui::EndCombo();
			}
		}
	}

	// we are ignoring models for now until we understand the NJCM format
	//drawModel(models, &mdt[mapID], canClose);

	ImGui::End();
}

void Maps::outputToCSV() {
	std::ofstream output;
	output.open("./csv/MAP_NAMES.CSV");
	output << "File,Map\n";

	for (const auto& val : _maps) {
		output << val.filename << ",\"" << val.mapname << "\"\n";
	}

	output.close();
}

void Maps::randomize() {
	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& mdt : _maps) {
		for (auto& icon : mdt.icons) {
			if (icon.item1) {
				do {
					icon.item1 = (uint16_t)(g() % Items::getInstance().getItems().size());
				} while (std::string(Items::getInstance().getItems().at(icon.item1).name).find_first_not_of(' ') == std::string::npos);
			}

			if (icon.item2) {
				do {
					icon.item2 = (uint16_t)(g() % Items::getInstance().getItems().size());
				} while (std::string(Items::getInstance().getItems().at(icon.item2).name).find_first_not_of(' ') == std::string::npos);

			}

			if (icon.item3) {
				do {
					icon.item3 = (uint16_t)(g() % Items::getInstance().getItems().size());
				} while (std::string(Items::getInstance().getItems().at(icon.item3).name).find_first_not_of(' ') == std::string::npos);
			}
		}

		for (auto& shop : mdt.shop) {
			// weapons
			for (size_t i = 0; i < 12; i++) {
				do {
					shop.weapons.items[i].item = 300 + g() % 200;
				} while (std::string(Items::getInstance().getItems().at(shop.weapons.items[i].item).name).find_first_not_of(' ') == std::string::npos);
			}

			//	armors
			for (size_t i = 0; i < 12; i++) {
				do {
					shop.armors.items[i].item = 500 + g() % 200;
				} while (std::string(Items::getInstance().getItems().at(shop.armors.items[i].item).name).find_first_not_of(' ') == std::string::npos);
			}

			//	accessories
			for (size_t i = 0; i < 12; i++) {
				do {
					shop.jewelry.items[i].item = 700 + g() % 99;
				} while (std::string(Items::getInstance().getItems().at(shop.jewelry.items[i].item).name).find_first_not_of(' ') == std::string::npos);
			}

			//	items
			for (size_t i = 0; i < 12; i++) {
				do {
					shop.items.items[i].item = 100 + g() % 100;
				} while (std::string(Items::getInstance().getItems().at(shop.items.items[i].item).name).find_first_not_of(' ') == std::string::npos);
			}

			//	regionals
			for (size_t i = 0; i < 12; i++) {
				do {
					shop.regionals.items[i].item = 200 + g() % 100;
				} while (std::string(Items::getInstance().getItems().at(shop.regionals.items[i].item).name).find_first_not_of(' ') == std::string::npos);
			}
		}
	}
}