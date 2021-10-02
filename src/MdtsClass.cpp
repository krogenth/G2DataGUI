#include <fstream>
#include <filesystem>
#include <random>

#include ".\include\MdtsClass.h"

#include ".\include\common\io_util.h"
#include ".\include\common\char_constants.h"
#include ".\include\common\string_manip.h"
#include ".\include\common\copypaste_obj.h"

#include ".\imgui.h"

void MdtsClass::write() {

	std::fstream output;
	char* readByte = new char[4]{};
	uint32_t offset = 0;

	for (size_t i = 0; i < this->_mdts.size(); i++) {

		output.open(this->_mdts.at(i).filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open())
			throw new std::exception(this->_mdts.at(i).filename.c_str());

		//	write in map entries
		output.seekg(this->_mdts.at(i).header.offsetMapEntries, std::ios::beg);
		for (auto& val : this->_mdts.at(i).mapEntries)
			writeRaw<MapEntriesStruct>(output, val);

		//	write in instances
		output.seekg(this->_mdts.at(i).header.offsetInstances, std::ios::beg);
		for (auto& val : this->_mdts.at(i).instances)
			writeRaw<InstancesStruct>(output, val);

		//	write in HTA
		output.seekg(this->_mdts.at(i).header.offsetHTA, std::ios::beg);
		for (auto& val : this->_mdts.at(i).HTA)
			writeRaw<HTAStruct>(output, val);

		//	write in enemy positions
		output.seekg(this->_mdts.at(i).header.offsetEnemyPos, std::ios::beg);
		for (auto& val : this->_mdts.at(i).enemyPositions)
			writeRaw<EnemyPositionStruct>(output, val);

		//	write in enemy groups
		output.seekg(this->_mdts.at(i).header.offsetEnemyGroup, std::ios::beg);
		for (auto& val : this->_mdts.at(i).enemyGroups)
			writeRaw<EnemyGroupStruct>(output, val);

		//	write in MOS
		output.seekg(this->_mdts.at(i).header.offsetMOS, std::ios::beg);
		for (auto& val : this->_mdts.at(i).MOS)
			writeRaw<MOSStruct>(output, val);

		//	write in icons
		output.seekg(this->_mdts.at(i).header.offsetIcons, std::ios::beg);
		for (auto& val : this->_mdts.at(i).icons)
			writeRaw<IconStruct>(output, val);

		//	write in shop
		if (this->_mdts.at(i).shop.size()) {

			for (ImU16 j = 0; j < 12; j++) {

				if (this->_mdts.at(i).shop[0].weapons[j].item)
					this->_mdts.at(i).shop[0].weapons[j].item += 0x0800;

				if (this->_mdts.at(i).shop[0].armors[j].item)
					this->_mdts.at(i).shop[0].armors[j].item += 0x0800;

				if (this->_mdts.at(i).shop[0].jewelry[j].item)
					this->_mdts.at(i).shop[0].jewelry[j].item += 0x0800;

				if (this->_mdts.at(i).shop[0].items[j].item)
					this->_mdts.at(i).shop[0].items[j].item += 0x0800;

				if (this->_mdts.at(i).shop[0].regionals[j].item)
					this->_mdts.at(i).shop[0].regionals[j].item += 0x0800;

			}

			output.seekg(this->_mdts[i].header.offsetShop, std::ios::beg);
			writeRaw<ShopStruct>(output, this->_mdts[i].shop[0]);

			for (ImU16 j = 0; j < 12; j++) {

				if (this->_mdts.at(i).shop[0].weapons[j].item)
					this->_mdts.at(i).shop[0].weapons[j].item -= 0x0800;

				if (this->_mdts.at(i).shop[0].armors[j].item)
					this->_mdts.at(i).shop[0].armors[j].item -= 0x0800;

				if (this->_mdts.at(i).shop[0].jewelry[j].item)
					this->_mdts.at(i).shop[0].jewelry[j].item -= 0x0800;

				if (this->_mdts.at(i).shop[0].items[j].item)
					this->_mdts.at(i).shop[0].items[j].item -= 0x0800;

				if (this->_mdts.at(i).shop[0].regionals[j].item)
					this->_mdts.at(i).shop[0].regionals[j].item -= 0x0800;

			}

		}

		output.close();

	}

}

void MdtsClass::read(std::string filepath) {

	uint32_t readByte = 0;
	uint8_t readChar = 0;
	std::ifstream input;

	std::string filename = "";
	uint32_t offset = 0;

	for (const auto& p : std::filesystem::directory_iterator(filepath)) {

		if (std::filesystem::is_directory(p)) {

			for (const auto& q : std::filesystem::directory_iterator(p)) {

				filename = q.path().u8string();

				if (!std::strstr(filename.c_str(), ".mdt"))
					continue;

				input.open(filename.c_str(), std::ios::binary);

				if (!input.is_open())
					throw new std::exception(filename.c_str());

				this->_mdts.emplace_back(MdtStruct());

				this->_mdts.back().header = readRaw<mdtHeader>(input);

				//	read in map entries
				this->_mdts.back().mapEntries.resize(this->_mdts.back().header.numMapEntries);
				input.seekg(this->_mdts.back().header.offsetMapEntries, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().mapEntries.size(); i++)
					this->_mdts.back().mapEntries[i] = readRaw<MapEntriesStruct>(input);


				//	read in instances
				this->_mdts.back().instances.resize(this->_mdts.back().header.numInstances);
				input.seekg(this->_mdts.back().header.offsetInstances, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().instances.size(); i++)
					this->_mdts.back().instances[i] = readRaw<InstancesStruct>(input);


				//	read in HTA
				this->_mdts.back().HTA.resize(this->_mdts.back().header.numHTA);
				input.seekg(this->_mdts.back().header.offsetHTA, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().HTA.size(); i++)
					this->_mdts.back().HTA[i] = readRaw<HTAStruct>(input);


				//	read in enemy positions
				this->_mdts.back().enemyPositions.resize(this->_mdts.back().header.numEnemyPos);
				input.seekg(this->_mdts.back().header.offsetEnemyPos, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().enemyPositions.size(); i++)
					this->_mdts.back().enemyPositions[i] = readRaw<EnemyPositionStruct>(input);


				//	read in enemy groups
				this->_mdts.back().enemyGroups.resize(this->_mdts.back().header.numEnemyGroup);
				input.seekg(this->_mdts.back().header.offsetEnemyGroup, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().enemyGroups.size(); i++)
					this->_mdts.back().enemyGroups[i] = readRaw<EnemyGroupStruct>(input);


				//	read in MOS
				this->_mdts.back().MOS.resize(this->_mdts.back().header.numMOS);
				input.seekg(this->_mdts.back().header.offsetMOS, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().MOS.size(); i++)
					this->_mdts.back().MOS[i] = readRaw<MOSStruct>(input);


				//	read in icons
				this->_mdts.back().icons.resize(this->_mdts.back().header.numIcons);
				input.seekg(this->_mdts.back().header.offsetIcons, std::ios::beg);

				for (size_t i = 0; i < this->_mdts.back().icons.size(); i++)
					this->_mdts.back().icons[i] = readRaw<IconStruct>(input);


				//	read in shop
				if (this->_mdts.back().header.offsetShop) {

					this->_mdts.back().shop.resize(1);
					input.seekg(this->_mdts.back().header.offsetShop, std::ios::beg);

					this->_mdts.back().shop[0] = readRaw<ShopStruct>(input);

					for (ImU16 i = 0; i < 12; i++) {

						if (this->_mdts.back().shop[0].weapons[i].item)
							this->_mdts.back().shop[0].weapons[i].item -= 0x0800;

						if (this->_mdts.back().shop[0].armors[i].item)
							this->_mdts.back().shop[0].armors[i].item -= 0x0800;

						if (this->_mdts.back().shop[0].jewelry[i].item)
							this->_mdts.back().shop[0].jewelry[i].item -= 0x0800;

						if (this->_mdts.back().shop[0].items[i].item)
							this->_mdts.back().shop[0].items[i].item -= 0x0800;

						if (this->_mdts.back().shop[0].regionals[i].item)
							this->_mdts.back().shop[0].regionals[i].item -= 0x0800;

					}

				}
				
				input.seekg(0x78, std::ios::beg);
				uint32_t sectionLength = readRaw<uint32_t>(input);
				uint32_t sectionOffset = readRaw<uint32_t>(input);

				//	dialogue section exists, lets do our thing
				if (sectionLength && sectionOffset) {

					input.seekg(sectionOffset, std::ios::beg);
					uint32_t diagHeaderSize = readRaw<uint32_t>(input);
					input.seekg(diagHeaderSize - 4, std::ios::beg);
					input.seekg(sectionOffset, std::ios::beg);

					for (size_t offset = 10; offset < diagHeaderSize; offset += 4) {

						input.seekg(offset, std::ios::cur);

						uint16_t diagOffset = readRaw<uint16_t>(input);
						//	check if it's padding, if so, ignore it
						if (diagOffset == 0xFFFF)
							continue;
						input.seekg(sectionOffset + diagHeaderSize, std::ios::beg);
						input.seekg(diagOffset * 8, std::ios::cur);

						//	check if it's the proper bytecode for a map name textbox
						if ((readRaw<uint8_t>(input) == (uint8_t)0x17) && (readRaw<uint8_t>(input) == (uint8_t)0x80)) {

							input.seekg(2, std::ios::cur);

							readChar = readRaw<uint8_t>(input);
							while (readChar != 0x17) {

								this->_mdts.back().mapname.push_back(readChar);
								readChar = readRaw<uint8_t>(input);

							}

							//	anything that contains the string "to " is not the current map name
							if (this->_mdts.back().mapname.find("to ") != std::string::npos) {

								this->_mdts.back().mapname.erase(this->_mdts.back().mapname.begin(), this->_mdts.back().mapname.end());
								input.seekg(sectionOffset, std::ios::beg);
								continue;

							}
							else
								break;

						}
						else
							input.seekg(sectionOffset, std::ios::beg);

					}

				}

				this->_mdts.back().filename = filename;
				this->_mdts.back().filenameChr = filename.substr(0, filename.length() - 4) + ".chr";

				input.close();

			}

		}

	}

}

void MdtsClass::draw() {

	ImGui::Begin("MDT");

	if (ImGui::BeginCombo("MDT Index", this->_mdts.at(this->_mdtIndex).mapname.c_str())) {

		for (size_t i = 0; i < this->_mdts.size(); i++) {

			ImGui::PushID(i);
			bool is_selected = (i == this->_mdtIndex);
			if (ImGui::Selectable(this->_mdts.at(i).mapname.c_str(), is_selected)) {

				this->_mdtIndex = i;

				this->_mapEntryIndex = 0;
				this->_instanceIndex = 0;
				this->_htaIndex = 0;
				this->_ePosIndex = 0;
				this->_eGroupIndex = 0;
				this->_eGroupPosIndex = 0;
				this->_mosIndex = 0;
				this->_iconIndex = 0;
				this->_shopIndex = 0;

			}
			if (is_selected)
				ImGui::SetItemDefaultFocus();
			ImGui::PopID();

		}

		ImGui::EndCombo();

	}

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		this->write();

	}

	if (this->_mdts.at(this->_mdtIndex).mapEntries.size()) {

		if (ImGui::CollapsingHeader("Map Entries")) {

			if (ImGui::BeginCombo("Map Entry #", std::to_string(this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].ID).c_str())) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).mapEntries.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mapEntryIndex);
					if (ImGui::Selectable(std::to_string(this->_mdts.at(this->_mdtIndex).mapEntries[i].ID).c_str(), is_selected))
						this->_mapEntryIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUInt("Connected Map", &this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].ID);
			ImGui::InputFloat3("X/Y/Z Position", &this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].xPos);
			ImGui::InputFloat("Direction", &this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].direction);
			ImGui::InputFloat("Unknown #1", &this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].unknown);
			ImGui::InputUInt("Unknown #2", &this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].unknown1);
			ImGui::InputUInt("Unknown #3", &this->_mdts.at(this->_mdtIndex).mapEntries[this->_mapEntryIndex].unknown2);

		}

	}

	if (this->_mdts.at(this->_mdtIndex).instances.size()) {

		if (ImGui::CollapsingHeader("Instances")) {

			if (ImGui::BeginCombo("Instance #", std::to_string(this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].ID).c_str())) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).mapEntries.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_instanceIndex);
					if (ImGui::Selectable(std::to_string(this->_mdts.at(this->_mdtIndex).instances[i].ID).c_str(), is_selected))
						this->_instanceIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUShort("ID", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].ID);
			ImGui::InputUShort("Index", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].index);
			ImGui::InputUShort("Unknown", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].unknown);
			ImGui::InputUShort("Translation", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].translation);
			ImGui::InputFloat3("X/Y/Z Position", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].xPos);
			ImGui::InputFloat3("X/Y/Z Angle", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].xAngle);
			ImGui::InputFloat3("CX/CY/CZ", &this->_mdts.at(this->_mdtIndex).instances[this->_instanceIndex].cx);

		}

	}

	if (this->_mdts.at(this->_mdtIndex).HTA.size()) {

		if (ImGui::CollapsingHeader("HTA")) {

			if (ImGui::BeginCombo("HTA #", slotIDs[this->_htaIndex])) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).HTA.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_htaIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_htaIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUByte("Shape", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].shape);
			ImGui::InputUByte("Type", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].type);
			ImGui::InputUByte("Trigger", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].trigger);
			ImGui::InputUInt("Unknown #1", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown);
			ImGui::InputUByte("Unknown #2", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown1);
			ImGui::InputFloat3("X/Y/Z Min", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].xMin);
			ImGui::InputFloat3("X/Y/Z Max", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].xMax);
			ImGui::InputUInt("Unknown #3", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown2);
			ImGui::InputUInt("Unknown #4", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown3);
			ImGui::InputUInt("Unknown #5", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown4);
			ImGui::InputUInt("Unknown #6", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown5);
			ImGui::InputUInt("Unknown #7", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown6);
			ImGui::InputUInt("Unknown #8", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown7);
			ImGui::InputUInt("Unknown #9", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown8);
			ImGui::InputUInt("Unknown #10", &this->_mdts.at(this->_mdtIndex).HTA[this->_htaIndex].unknown9);

		}

	}

	if (this->_mdts.at(this->_mdtIndex).enemyPositions.size()) {

		if (ImGui::CollapsingHeader("Enemy Positions")) {

			if (ImGui::BeginCombo("Enemy Pos #", slotIDs[this->_ePosIndex])) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).enemyPositions.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_ePosIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_ePosIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUShort("Index", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].index);
			ImGui::InputUShort("Unknown #1", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].unknown);

			ImGui::InputFloat2("X/Z Min", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].xPosMin);
			ImGui::InputFloat2("X/Z Max", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].xPosMax);
			ImGui::InputFloat3("X/Y/Z Pos", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].xPos);

			ImGui::InputFloat3("X/Y/Z #1", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].unknownX);				if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			ImGui::InputFloat3("X/Y/Z #2", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].unknownX1);				if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			ImGui::InputFloat3("X/Y/Z #3", &this->_mdts.at(this->_mdtIndex).enemyPositions[this->_ePosIndex].unknownX2);				if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");

		}

	}

	if (this->_mdts.at(this->_mdtIndex).enemyGroups.size()) {

		if (ImGui::CollapsingHeader("Enemy Groups")) {

			if (ImGui::BeginCombo("Enemy Group #", slotIDs[this->_eGroupIndex])) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).enemyGroups.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_eGroupIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_eGroupIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUShort("Group Index", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].index);

			if (ImGui::BeginCombo("Enemy #", slotIDs[this->_eGroupPosIndex])) {

				//	there can only ever be 4 unique enemies in a group, as far as I can tell
				for (size_t i = 0; i < 4; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_eGroupPosIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_eGroupPosIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUShort("Enemy Index", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].index);
			ImGui::InputUShort("# of Enemy", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].numEnemy);
			ImGui::InputUShort("Enemy Offset", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].enemyOffset);			if (ImGui::IsItemHovered()) ImGui::SetTooltip("References enemy/boss.csv");
			ImGui::InputUShort("Unknown #1", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].unknown);
			ImGui::InputUShort("Unknown #2", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].unknown1);
			ImGui::InputUShort("Unknown #3", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].unknown2);
			ImGui::InputUShort("Unknown #4", &this->_mdts.at(this->_mdtIndex).enemyGroups[this->_eGroupIndex].enemies[this->_eGroupPosIndex].unknown3);

		}

	}

	if (this->_mdts.at(this->_mdtIndex).MOS.size()) {

		if (ImGui::CollapsingHeader("MOS")) {

			if (ImGui::BeginCombo("MOS #", slotIDs[this->_mosIndex])) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).MOS.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mosIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_mosIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUShort("ID", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].ID);
			ImGui::InputUShort("Index", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].index);
			ImGui::InputUInt("Unknown #1", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].unknown);
			ImGui::InputShort3("X/Y/Z Pos", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].xPos);
			ImGui::InputUInt("Unknown #2", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].unknown1);
			ImGui::InputUInt("Unknown #3", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].unknown2);
			ImGui::InputUInt("Unknown #4", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].unknown3);
			ImGui::InputUInt("Unknown #5", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].unknown4);
			ImGui::InputUShort("Unknown #6", &this->_mdts.at(this->_mdtIndex).MOS[this->_mosIndex].unknown5);

		}

	}

	if (this->_mdts.at(this->_mdtIndex).icons.size()) {

		if (ImGui::CollapsingHeader("Icons")) {

			if (ImGui::BeginCombo("Icon #", slotIDs[this->_iconIndex])) {

				for (size_t i = 0; i < this->_mdts.at(this->_mdtIndex).icons.size(); i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_iconIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_iconIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUByte("ID", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].ID);
			ImGui::InputUShort("Unknown #1", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].unknown);
			ImGui::InputUByte("Unknown #2", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].unknown1);
			ImGui::InputFloat3("X/Y/Z Pos", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].xPos);
			ImGui::InputFloat("Unknown #3", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].unknown2);
			ImGui::InputFloat("Y Angle", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].yAngle);						if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unsure");

			if (ImGui::BeginCombo("Item #1", this->_items[this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item1].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item1);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item1 = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Item #2", this->_items[this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item2].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item2);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item2 = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Item #3", this->_items[this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item3].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item3);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].item3 = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			ImGui::InputUShort("Flag", &this->_mdts.at(this->_mdtIndex).icons[this->_iconIndex].flag);

		}

	}

	if (this->_mdts.at(this->_mdtIndex).shop.size()) {

		if (ImGui::CollapsingHeader("Shops")) {

			if (ImGui::BeginCombo("Weapons #", slotIDs[this->_shopWeaponIndex])) {

				for (size_t i = 0; i < 12; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_shopWeaponIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_shopWeaponIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Weapon Item", this->_items[this->_mdts.at(this->_mdtIndex).shop[0].weapons[this->_shopWeaponIndex].item].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).shop[0].weapons[this->_shopWeaponIndex].item);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).shop[0].weapons[this->_shopWeaponIndex].item = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Armors #", slotIDs[this->_shopArmorIndex])) {

				for (size_t i = 0; i < 12; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_shopArmorIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_shopArmorIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Armor Item", this->_items[this->_mdts.at(this->_mdtIndex).shop[0].armors[this->_shopArmorIndex].item].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).shop[0].armors[this->_shopArmorIndex].item);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).shop[0].armors[this->_shopArmorIndex].item = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Jewelry #", slotIDs[this->_shopJewelIndex])) {

				for (size_t i = 0; i < 12; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_shopJewelIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_shopJewelIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Jewelry Item", this->_items[this->_mdts.at(this->_mdtIndex).shop[0].jewelry[this->_shopJewelIndex].item].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).shop[0].jewelry[this->_shopJewelIndex].item);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).shop[0].jewelry[this->_shopJewelIndex].item = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Items #", slotIDs[this->_shopItemIndex])) {

				for (size_t i = 0; i < 12; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_shopItemIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_shopItemIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Item Item", this->_items[this->_mdts.at(this->_mdtIndex).shop[0].items[this->_shopItemIndex].item].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).shop[0].items[this->_shopItemIndex].item);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).shop[0].items[this->_shopItemIndex].item = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Regionals #", slotIDs[this->_shopRegionalIndex])) {

				for (size_t i = 0; i < 12; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_shopRegionalIndex);
					if (ImGui::Selectable(slotIDs[i], is_selected))
						this->_shopRegionalIndex = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

			if (ImGui::BeginCombo("Regional Item", this->_items[this->_mdts.at(this->_mdtIndex).shop[0].regionals[this->_shopRegionalIndex].item].name)) {

				for (size_t i = 0; i < this->_numItems; i++) {

					ImGui::PushID(i);
					bool is_selected = (i == this->_mdts.at(this->_mdtIndex).shop[0].regionals[this->_shopRegionalIndex].item);
					if (ImGui::Selectable(this->_items[i].name, is_selected))
						this->_mdts.at(this->_mdtIndex).shop[0].regionals[this->_shopRegionalIndex].item = i;
					if (is_selected)
						ImGui::SetItemDefaultFocus();
					ImGui::PopID();

				}

				ImGui::EndCombo();

			}

		}

	}

	//	we are ignoring models for now until we understand the NJCM format
	//drawModel(models, &mdt[mapID], canClose);

	ImGui::End();

}

void MdtsClass::outputToCSV() {



}

void MdtsClass::randomize() {

	std::random_device rd;
	std::mt19937 g(rd());

	for (auto& mdt : this->_mdts) {

		for (auto& icon : mdt.icons) {

			if (icon.item1) {

				do {

					icon.item1 = g() % this->_numItems;

				} while (std::string(this->_items[icon.item1].name).find_first_not_of(' ') == std::string::npos);

			}

			if (icon.item2) {

				do {

					icon.item2 = g() % this->_numItems;

				} while (std::string(this->_items[icon.item2].name).find_first_not_of(' ') == std::string::npos);

			}

			if (icon.item3) {

				do {

					icon.item2 = g() % this->_numItems;

				} while (std::string(this->_items[icon.item3].name).find_first_not_of(' ') == std::string::npos);

			}

		}

		for (auto& shop : mdt.shop) {

			//	weapons
			for (size_t i = 0; i < 12; i++) {

				do {

					shop.weapons[i].item = 300 + g() % 200;

				} while (std::string(this->_items[shop.weapons[i].item].name).find_first_not_of(' ') == std::string::npos);

			}

			//	armors
			for (size_t i = 0; i < 12; i++) {

				do {

					shop.armors[i].item = 500 + g() % 200;

				} while (std::string(this->_items[shop.armors[i].item].name).find_first_not_of(' ') == std::string::npos);

			}

			//	accessories
			for (size_t i = 0; i < 12; i++) {

				do {

					shop.jewelry[i].item = 700 + g() % 99;

				} while (std::string(this->_items[shop.jewelry[i].item].name).find_first_not_of(' ') == std::string::npos);

			}

			//	items
			for (size_t i = 0; i < 12; i++) {

				do {

					shop.items[i].item = 100 + g() % 100;

				} while (std::string(this->_items[shop.items[i].item].name).find_first_not_of(' ') == std::string::npos);

			}

			//	regionals
			for (size_t i = 0; i < 12; i++) {

				do {

					shop.regionals[i].item = 200 + g() % 100;

				} while (std::string(this->_items[shop.regionals[i].item].name).find_first_not_of(' ') == std::string::npos);

			}

		}

	}

}