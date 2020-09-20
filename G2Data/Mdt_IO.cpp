#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <fstream>
#include <exception>
#include <experimental/filesystem>

#include "MdtStruct.h"
#include "io_util.h"
#include "char_constants.h"

void writeMdt(std::vector<MdtStruct>& mdt) {

	std::fstream output;
	char* readByte = new char[4]{};
	uint32_t offset = 0;

	for (size_t i = 0; i < mdt.size(); i++) {

		output.open(mdt[i].filename, std::ios::binary | std::ios::in | std::ios::out);

		if (!output.is_open())
			throw new std::exception(mdt[i].filename.c_str());

		//	write in map entries
		output.seekg(mdt[i].header.offsetMapEntries, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numMapEntries; j++)
			writeRaw<MapEntriesStruct>(output, mdt[i].mapEntries[j]);

		//	write in instances
		output.seekg(mdt[i].header.offsetInstances, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numInstances; j++)
			writeRaw<InstancesStruct>(output, mdt[i].instances[j]);

		//	write in HTA
		output.seekg(mdt[i].header.offsetHTA, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numHTA; j++)
			writeRaw<HTAStruct>(output, mdt[i].HTA[j]);

		//	write in enemy positions
		output.seekg(mdt[i].header.offsetEnemyPos, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numEnemyPos; j++)
			writeRaw<EnemyPositionStruct>(output, mdt[i].enemyPositions[j]);

		//	write in enemy groups
		output.seekg(mdt[i].header.offsetEnemyGroup, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numEnemyGroup; j++)
			writeRaw<EnemyGroupStruct>(output, mdt[i].enemyGroups[j]);

		//	write in MOS
		output.seekg(mdt[i].header.offsetMOS, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numMOS; j++)
			writeRaw<MOSStruct>(output, mdt[i].MOS[j]);

		//	write in icons
		output.seekg(mdt[i].header.offsetIcons, std::ios::beg);
		for (size_t j = 0; j < mdt[i].header.numIcons; j++)
			writeRaw<IconStruct>(output, mdt[i].icons[j]);

		//	write in shop
		if (mdt[i].shop.size()) {

			for (ImU16 j = 0; j < 12; j++) {

				if (mdt[i].shop[0].weapons[j].item)
					mdt[i].shop[0].weapons[j].item += 0x0800;

				if (mdt[i].shop[0].armors[j].item)
					mdt[i].shop[0].armors[j].item += 0x0800;

				if (mdt[i].shop[0].jewelry[j].item)
					mdt[i].shop[0].jewelry[j].item += 0x0800;

				if (mdt[i].shop[0].items[j].item)
					mdt[i].shop[0].items[j].item += 0x0800;

				if (mdt[i].shop[0].regionals[j].item)
					mdt[i].shop[0].regionals[j].item += 0x0800;

			}

			output.seekg(mdt[i].header.offsetShop, std::ios::beg);
			writeRaw<ShopStruct>(output, mdt[i].shop[0]);

			for (ImU16 j = 0; j < 12; j++) {

				if (mdt[i].shop[0].weapons[j].item)
					mdt[i].shop[0].weapons[j].item -= 0x0800;

				if (mdt[i].shop[0].armors[j].item)
					mdt[i].shop[0].armors[j].item -= 0x0800;

				if (mdt[i].shop[0].jewelry[j].item)
					mdt[i].shop[0].jewelry[j].item -= 0x0800;

				if (mdt[i].shop[0].items[j].item)
					mdt[i].shop[0].items[j].item -= 0x0800;

				if (mdt[i].shop[0].regionals[j].item)
					mdt[i].shop[0].regionals[j].item -= 0x0800;

			}

		}

		output.close();

	}

}

void readMdt(std::vector<MdtStruct>& mdt, std::string filepath) {

	ImU32 readByte = 0;
	ImU16 readDiagHeader = 0;
	ImU8 readChar = 0;
	std::ifstream input;

	std::string filename = "";
	uint32_t offset = 0;

	for (const auto& p : std::experimental::filesystem::directory_iterator(filepath)) {

		if (std::experimental::filesystem::is_directory(p)) {

			for (const auto& q : std::experimental::filesystem::directory_iterator(p)) {

				filename = q.path().u8string();

				if (!std::strstr(filename.c_str(), ".mdt"))
					continue;

				input.open(filename.c_str(), std::ios::binary);

				if (!input.is_open())
					throw new std::exception(filename.c_str());

				mdt.emplace_back(MdtStruct());

				mdt[mdt.size() - 1].header = readRaw<mdtHeader>(input);

				//	read in map entries
				mdt[mdt.size() - 1].mapEntries.resize(mdt[mdt.size() - 1].header.numMapEntries);
				input.seekg(mdt[mdt.size() - 1].header.offsetMapEntries, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].mapEntries.size(); i++)
					mdt[mdt.size() - 1].mapEntries[i] = readRaw<MapEntriesStruct>(input);


				//	read in instances
				mdt[mdt.size() - 1].instances.resize(mdt[mdt.size() - 1].header.numInstances);
				input.seekg(mdt[mdt.size() - 1].header.offsetInstances, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].instances.size(); i++)
					mdt[mdt.size() - 1].instances[i] = readRaw<InstancesStruct>(input);


				//	read in HTA
				mdt[mdt.size() - 1].HTA.resize(mdt[mdt.size() - 1].header.numHTA);
				input.seekg(mdt[mdt.size() - 1].header.offsetHTA, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].HTA.size(); i++)
					mdt[mdt.size() - 1].HTA[i] = readRaw<HTAStruct>(input);


				//	read in enemy positions
				mdt[mdt.size() - 1].enemyPositions.resize(mdt[mdt.size() - 1].header.numEnemyPos);
				input.seekg(mdt[mdt.size() - 1].header.offsetEnemyPos, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].enemyPositions.size(); i++)
					mdt[mdt.size() - 1].enemyPositions[i] = readRaw<EnemyPositionStruct>(input);


				//	read in enemy groups
				mdt[mdt.size() - 1].enemyGroups.resize(mdt[mdt.size() - 1].header.numEnemyGroup);
				input.seekg(mdt[mdt.size() - 1].header.offsetEnemyGroup, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].enemyGroups.size(); i++)
					mdt[mdt.size() - 1].enemyGroups[i] = readRaw<EnemyGroupStruct>(input);


				//	read in MOS
				mdt[mdt.size() - 1].MOS.resize(mdt[mdt.size() - 1].header.numMOS);
				input.seekg(mdt[mdt.size() - 1].header.offsetMOS, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].MOS.size(); i++)
					mdt[mdt.size() - 1].MOS[i] = readRaw<MOSStruct>(input);


				//	read in icons
				mdt[mdt.size() - 1].icons.resize(mdt[mdt.size() - 1].header.numIcons);
				input.seekg(mdt[mdt.size() - 1].header.offsetIcons, std::ios::beg);

				for (size_t i = 0; i < mdt[mdt.size() - 1].icons.size(); i++)
					mdt[mdt.size() - 1].icons[i] = readRaw<IconStruct>(input);


				//	read in shop
				if (mdt[mdt.size() - 1].header.offsetShop) {

					mdt[mdt.size() - 1].shop.resize(1);
					input.seekg(mdt[mdt.size() - 1].header.offsetShop, std::ios::beg);

					mdt[mdt.size() - 1].shop[0] = readRaw<ShopStruct>(input);

					for (ImU16 i = 0; i < 12; i++) {

						if(mdt[mdt.size() - 1].shop[0].weapons[i].item)
							mdt[mdt.size() - 1].shop[0].weapons[i].item -= 0x0800;

						if (mdt[mdt.size() - 1].shop[0].armors[i].item)
							mdt[mdt.size() - 1].shop[0].armors[i].item -= 0x0800;

						if (mdt[mdt.size() - 1].shop[0].jewelry[i].item)
							mdt[mdt.size() - 1].shop[0].jewelry[i].item -= 0x0800;

						if (mdt[mdt.size() - 1].shop[0].items[i].item)
							mdt[mdt.size() - 1].shop[0].items[i].item -= 0x0800;

						if (mdt[mdt.size() - 1].shop[0].regionals[i].item)
							mdt[mdt.size() - 1].shop[0].regionals[i].item -= 0x0800;

					}

				}

				/*

				//	find and read map name
				input.seekg(0x0000007C, std::ios::beg);
				readByte = readRaw<ImU32>(input);
				input.seekg(readByte, std::ios::beg);

				size_t headerLength = readRaw<ImU32>(input);
				input.seekg(headerLength - 10, std::ios::cur);		//	first 8 bytes are useless, and we want to read the last 2 bytes
				std::streampos diagHeader = input.tellg();

				for (ImU32 i = 0; i < ((headerLength - 4) / 4); i++) {

					readDiagHeader = readRaw<ImU16>(input);

					input.seekg(diagHeader);
					input.seekg((ImU32)((size_t)i * -4), std::ios::cur);

					if (readDiagHeader == 0xFFFF)
						continue;

					input.seekg(diagHeader);
					input.seekg(readDiagHeader + 2, std::ios::cur);

					readDiagHeader = readRaw<ImU16>(input);
					if (readDiagHeader != 0x1780)
						continue;

					input.seekg(2, std::ios::cur);

					readChar = readRaw<ImU8>(input);
					while (readChar != 0x17) {

						mdt[mdt.size() - 1].mapname.push_back(readChar);
						readChar = readRaw<ImU8>(input);

					}

					break;

				}

				*/

				mdt[mdt.size() - 1].filename = q.path().u8string();

				input.close();

			}

		}

	}

}

void drawMdt(std::vector<MdtStruct>& mdt, char** mapIDs, bool* canClose, char** itemIDs, const size_t& numItems) {

	static ImU16 mapID = 0;
	static ImU16 mapEntryID = 0;
	static ImU16 instanceID = 0;
	static ImU16 htaID = 0;
	static ImU16 ePosID = 0;
	static ImU16 eGroupID = 0;
	static ImU16 eGroupPosID = 0;
	static ImU16 mosID = 0;
	static ImU16 iconID = 0;
	static ImU16 shopID = 0;


	ImGui::Begin("MDT");

	ImGui::Combo("Map", &mapID, mapIDs, (int)mdt.size(), 100);

	ImGui::SameLine();
	if (ImGui::Button("Save")) {

		try {

			writeMdt(mdt);

		}
		catch (const std::exception& e) {

			ImGui::Begin("ERROR", canClose);
			ImGui::LabelText("", e.what());
			ImGui::End();

		}

	}

	if (mdt[mapID].mapEntries.size()) {

		if (ImGui::CollapsingHeader("Map Entries")) {

			ImGui::Combo("Map Entry #", &mapEntryID, slotIDs, mdt[mapID].mapEntries.size());
			ImGui::InputUInt("Connected Map", &mdt[mapID].mapEntries[mapEntryID].ID);
			ImGui::InputFloat3("X/Y/Z Position", &mdt[mapID].mapEntries[mapEntryID].xPos);
			ImGui::InputFloat("Direction", &mdt[mapID].mapEntries[mapEntryID].direction);
			ImGui::InputFloat("Unknown #1", &mdt[mapID].mapEntries[mapEntryID].unknown);
			ImGui::InputUInt("Unknown #2", &mdt[mapID].mapEntries[mapEntryID].unknown1);
			ImGui::InputUInt("Unknown #3", &mdt[mapID].mapEntries[mapEntryID].unknown2);

		}

	}

	if (mdt[mapID].instances.size()) {

		if (ImGui::CollapsingHeader("Instances")) {

			ImGui::Combo("Instance #", &instanceID, slotIDs, mdt[mapID].instances.size());
			ImGui::InputUShort("ID", &mdt[mapID].instances[instanceID].ID);
			ImGui::InputUShort("Index", &mdt[mapID].instances[instanceID].index);
			ImGui::InputUShort("Unknown", &mdt[mapID].instances[instanceID].unknown);
			ImGui::InputUShort("Translation", &mdt[mapID].instances[instanceID].translation);
			ImGui::InputFloat3("X/Y/Z Position", &mdt[mapID].instances[instanceID].xPos);
			ImGui::InputFloat3("X/Y/Z Angle", &mdt[mapID].instances[instanceID].xAngle);
			ImGui::InputFloat3("CX/CY/CZ", &mdt[mapID].instances[instanceID].cx);

		}

	}

	if (mdt[mapID].HTA.size()) {

		if (ImGui::CollapsingHeader("HTA")) {

			ImGui::Combo("HTA #", &htaID, slotIDs, mdt[mapID].HTA.size());
			ImGui::InputUByte("Shape", &mdt[mapID].HTA[htaID].shape);
			ImGui::InputUByte("Type", &mdt[mapID].HTA[htaID].type);
			ImGui::InputUByte("Trigger", &mdt[mapID].HTA[htaID].trigger);
			ImGui::InputUInt("Unknown #1", &mdt[mapID].HTA[htaID].unknown);
			ImGui::InputUByte("Unknown #2", &mdt[mapID].HTA[htaID].unknown1);
			ImGui::InputFloat3("X/Y/Z Min", &mdt[mapID].HTA[htaID].xMin);
			ImGui::InputFloat3("X/Y/Z Max", &mdt[mapID].HTA[htaID].xMax);
			ImGui::InputUInt("Unknown #3", &mdt[mapID].HTA[htaID].unknown2);
			ImGui::InputUInt("Unknown #4", &mdt[mapID].HTA[htaID].unknown3);
			ImGui::InputUInt("Unknown #5", &mdt[mapID].HTA[htaID].unknown4);
			ImGui::InputUInt("Unknown #6", &mdt[mapID].HTA[htaID].unknown5);
			ImGui::InputUInt("Unknown #7", &mdt[mapID].HTA[htaID].unknown6);
			ImGui::InputUInt("Unknown #8", &mdt[mapID].HTA[htaID].unknown7);
			ImGui::InputUInt("Unknown #9", &mdt[mapID].HTA[htaID].unknown8);
			ImGui::InputUInt("Unknown #10", &mdt[mapID].HTA[htaID].unknown9);

		}

	}

	if (mdt[mapID].enemyPositions.size()) {

		if (ImGui::CollapsingHeader("Enemy Positions")) {

			ImGui::Combo("Enemy Pos #", &ePosID, slotIDs, mdt[mapID].enemyPositions.size());
			ImGui::InputUShort("Index", &mdt[mapID].enemyPositions[ePosID].index);
			ImGui::InputUShort("Unknown #1", &mdt[mapID].enemyPositions[ePosID].unknown);

			ImGui::InputFloat2("X/Z Min", &mdt[mapID].enemyPositions[ePosID].xPosMin);
			ImGui::InputFloat2("X/Z Max", &mdt[mapID].enemyPositions[ePosID].xPosMax);
			ImGui::InputFloat3("X/Y/Z Pos", &mdt[mapID].enemyPositions[ePosID].xPos);

			ImGui::InputFloat3("X/Y/Z #1", &mdt[mapID].enemyPositions[ePosID].unknownX);				if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			ImGui::InputFloat3("X/Y/Z #2", &mdt[mapID].enemyPositions[ePosID].unknownX1);				if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");
			ImGui::InputFloat3("X/Y/Z #3", &mdt[mapID].enemyPositions[ePosID].unknownX2);				if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unknown");

		}

	}

	if (mdt[mapID].enemyGroups.size()) {

		if (ImGui::CollapsingHeader("Enemy Groups")) {

			ImGui::Combo("Enemy Group #", &eGroupID, slotIDs, mdt[mapID].enemyGroups.size());

			ImGui::InputUShort("Group Index", &mdt[mapID].enemyGroups[eGroupID].index);
			ImGui::Combo("Enemy #", &eGroupPosID, slotIDs, 4);

			ImGui::InputUShort("Enemy Index", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].index);
			ImGui::InputUShort("# of Enemy", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].numEnemy);
			ImGui::InputUShort("Enemy Offset", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].enemyOffset);			if (ImGui::IsItemHovered()) ImGui::SetTooltip("References enemy/boss.csv");
			ImGui::InputUShort("Unknown #1", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].unknown);
			ImGui::InputUShort("Unknown #2", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].unknown1);
			ImGui::InputUShort("Unknown #3", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].unknown2);
			ImGui::InputUShort("Unknown #4", &mdt[mapID].enemyGroups[eGroupID].enemies[eGroupPosID].unknown3);

		}

	}
	
	if (mdt[mapID].MOS.size()) {

		if (ImGui::CollapsingHeader("MOS")) {

			ImGui::Combo("MOS #", &mosID, slotIDs, mdt[mapID].MOS.size());
			ImGui::InputUShort("ID", &mdt[mapID].MOS[mosID].ID);
			ImGui::InputUShort("Index", &mdt[mapID].MOS[mosID].index);
			ImGui::InputUInt("Unknown #1", &mdt[mapID].MOS[mosID].unknown);
			ImGui::InputShort3("X/Y/Z Pos", &mdt[mapID].MOS[mosID].xPos);
			ImGui::InputUInt("Unknown #2", &mdt[mapID].MOS[mosID].unknown1);
			ImGui::InputUInt("Unknown #3", &mdt[mapID].MOS[mosID].unknown2);
			ImGui::InputUInt("Unknown #4", &mdt[mapID].MOS[mosID].unknown3);
			ImGui::InputUInt("Unknown #5", &mdt[mapID].MOS[mosID].unknown4);
			ImGui::InputUShort("Unknown #6", &mdt[mapID].MOS[mosID].unknown5);

		}

	}

	if (mdt[mapID].icons.size()) {

		if (ImGui::CollapsingHeader("Icons")) {

			ImGui::Combo("Icon #", &iconID, slotIDs, mdt[mapID].icons.size());

			ImGui::InputUByte("ID", &mdt[mapID].icons[iconID].ID);
			ImGui::InputUShort("Unknown #1", &mdt[mapID].icons[iconID].unknown);
			ImGui::InputUByte("Unknown #2", &mdt[mapID].icons[iconID].unknown1);
			ImGui::InputFloat3("X/Y/Z Pos", &mdt[mapID].icons[iconID].xPos);
			ImGui::InputFloat("Unknown #3", &mdt[mapID].icons[iconID].unknown2);
			ImGui::InputFloat("Y Angle", &mdt[mapID].icons[iconID].yAngle);						if (ImGui::IsItemHovered()) ImGui::SetTooltip("Unsure");

			ImGui::Combo("Item #1", &mdt[mapID].icons[iconID].item1, itemIDs, (int)numItems);
			ImGui::Combo("Item #2", &mdt[mapID].icons[iconID].item2, itemIDs, (int)numItems);
			ImGui::Combo("Item #3", &mdt[mapID].icons[iconID].item3, itemIDs, (int)numItems);

			ImGui::InputUShort("Flag", &mdt[mapID].icons[iconID].flag);

		}

	}

	if (mdt[mapID].shop.size()) {

		if (ImGui::CollapsingHeader("Shops")) {

			static ImU16 shopWeaponID = 0;
			static ImU16 shopArmorID = 0;
			static ImU16 shopJewelryID = 0;
			static ImU16 shopItemID = 0;
			static ImU16 shopRegionalID = 0;

			ImGui::Combo("Weapons #", &shopWeaponID, slotIDs, 12);
			ImGui::Combo("Weapon Item", &mdt[mapID].shop[0].weapons[shopWeaponID].item, itemIDs, (int)numItems);

			ImGui::Combo("Armors #", &shopArmorID, slotIDs, 12);
			ImGui::Combo("Armor Item", &mdt[mapID].shop[0].armors[shopArmorID].item, itemIDs, (int)numItems);

			ImGui::Combo("Jewelry #", &shopJewelryID, slotIDs, 12);
			ImGui::Combo("Jewelry Item", &mdt[mapID].shop[0].jewelry[shopJewelryID].item, itemIDs, (int)numItems);

			ImGui::Combo("Items #", &shopItemID, slotIDs, 12);
			ImGui::Combo("Item Item", &mdt[mapID].shop[0].items[shopItemID].item, itemIDs, (int)numItems);

			ImGui::Combo("Regionals #", &shopRegionalID, slotIDs, 12);
			ImGui::Combo("Regional Item", &mdt[mapID].shop[0].regionals[shopRegionalID].item, itemIDs, (int)numItems);

		}

	}

	ImGui::End();

}