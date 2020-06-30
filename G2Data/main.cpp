#include "ImGui/imgui.h"
#include "ImGui/imgui_impl_win32.h"
#include "ImGui/imgui_impl_dx11.h"
#include <d3d11.h>
#define DIRECTINPUT_VERSION 0x0800
#include <dinput.h>
#include <tchar.h>

#include <cstdlib>
#include <Windows.h>
#include <type_traits>
#include <string>

#include "MoveStruct.h"
#include "SkillStruct.h"
#include "ManaEggStruct.h"
#include "SkillBookStruct.h"
#include "SpecialMoveStruct.h"
#include "StartStats.h"
#include "ItemStruct.h"
#include "EnemyStatsStruct.h"

static ID3D11Device* g_pd3dDevice = NULL;
static ID3D11DeviceContext* g_pd3dDeviceContext = NULL;
static IDXGISwapChain* g_pSwapChain = NULL;
static ID3D11RenderTargetView* g_mainRenderTargetView = NULL;

bool CreateDeviceD3D(HWND hWnd);
void CleanupDeviceD3D();
void CreateRenderTarget();
void CleanupRenderTarget();
LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

/*
TODO:
    Implement enemy data, groupings(from *.mdt files in /maps/), and patrolling sections(?)(from *.mdt files in /maps/)
    Implement hints for various entries(ex: specials, stamina, etc.)

*/

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow) {

    // Create application window
    WNDCLASSEX wc = { sizeof(WNDCLASSEX), CS_CLASSDC, WndProc, 0L, 0L, GetModuleHandle(NULL), NULL, NULL, NULL, NULL, _T("G2Data"), NULL };
    RegisterClassEx(&wc);
    HWND hwnd = CreateWindow(wc.lpszClassName, _T("G2Data"), WS_OVERLAPPEDWINDOW, 100, 100, 1920, 1080, NULL, NULL, wc.hInstance, NULL);

    // Initialize Direct3D 
    if (!CreateDeviceD3D(hwnd)) {

        CleanupDeviceD3D();
        UnregisterClass(wc.lpszClassName, wc.hInstance);
        return 1;

    }

    // Show the window
    ShowWindow(hwnd, SW_SHOWDEFAULT);
    UpdateWindow(hwnd);

    // Setup Dear ImGui context
    IMGUI_CHECKVERSION();
    ImGui::CreateContext();
    ImGuiIO& io = ImGui::GetIO();
    io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard;     // Enable Keyboard Controls
    //io.ConfigFlags |= ImGuiConfigFlags_NavEnableGamepad;      // Enable Gamepad Controls

    // Setup Dear ImGui style
    ImGui::StyleColorsDark();

    // Setup Platform/Renderer bindings
    ImGui_ImplWin32_Init(hwnd);
    ImGui_ImplDX11_Init(g_pd3dDevice, g_pd3dDeviceContext);

    ImVec4 clear_color = ImVec4(0.00f, 0.00f, 0.00f, 1.00f);

    bool* canClose = new bool[1]{ true };

    ImU16 numMoves = 0;
    MoveStruct* moves = nullptr;

    ImU16 numSkills = 0;
    SkillStruct* skills = nullptr;

    ImU16 numEggs = 0;
    ManaEggStruct* eggs = nullptr;

    ImU16 numBooks = 0;
    SkillBookStruct* books = nullptr;

    ImU16 numSpecials = 0;
    SpecialMoveStruct* specials = nullptr;

    ImU16 numItems = 0;
    ItemStruct* items = nullptr;

    ImU16 numStats = 0;
    StartStatsStruct* stats = nullptr;

    ImU16 numEnemies = 0;
    EnemyStatsStruct* enemies = nullptr;
    
    const char* moveIcons[] =       { "Fire", "Wind", "Earth", "Lightning", "Blizzard", "Water", "Explosion", "Forest", "Light", "Darkness", "Sword", "Staff", "Crossbow", "Dagger", "Poleaxe", "Chakram", "Red Shoe" };
    const char* targetEffects[] =   { "NULL", "Restore HP(MEN)", "Restore MP", "Restore SP", "Ally Buff/Debuff", "Physical Damage(STR)", "Magical Damage(MAG)", "Enemy Buff/Debuff", "Status Change", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Special" };
    const char* targetTypes[] =     { "NULL", "One Ally", "Area Allies", "All Allies", "One Enemy", "Area Enemies", "All Enemies", "Enemy Line", "Self", "Unknown", "Area Around Self", "Unknown", "Unknown", "Area Around Self", "Unknown", "Unknown" };
    const char* effectiveOn[] =     { "NULL", "Bird", "Bug", "Reptile", "Animal", "Humanoid", "Unknown", "Undead", "Valmar", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown" };
    const char* elements[] =        { "Fire", "Wind", "Earth", "Lightning", "Blizzard" };

    const char* eggIDs[] =          { "NULL", "Holy Egg", "Chaos Egg", "Mist Egg", "Gravity Egg", "Soul Egg", "Star Egg", "Tutor Egg", "Change Egg", "Fairy Egg", "Dragon Egg" };
    const char* bookIDs[] =         { "NULL", "Adventure Book", "Book of Wizards", "Book of Warriors", "Book of Priests", "Book of Gales", "Book of Swords", "Book of War", "Book of Sages", "Book of Learning" };
    const char* specialIDs[] =      { "NULL", "Ryduo", "Elena", "Millenia", "Roan", "Tio", "Mareg", "Prince Roan", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" };
    const char* statIDs[] =         { "Ryduo", "Elena", "Millenia", "Roan", "Tio", "Mareg", "Prince Roan", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" };
    
    //CALL READ DATA FUNCTIONS
    {

        try {

            moves = readMS(numMoves);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            skills = readSK(numSkills);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            eggs = readMAG(numEggs);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            books = readSKI(numBooks);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            specials = readSPC(numSpecials);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            items = readITE(numItems);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            stats = readPC(numStats);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

        try {

            enemies = readEnemyStats(numEnemies);

        }
        catch (const std::exception& e) {

            ImGui::Begin("ERROR", canClose);
            ImGui::LabelText("", e.what());
            ImGui::End();

        }

    }

    char** moveIDs = new char* [numMoves] {};
    char** skillIDs = new char* [numSkills] {};
    char** itemIDs = new char* [numItems] {};
    char** enemyIDs = new char* [numEnemies] {};

    for (ImU16 i = 0; i < numMoves; i++)
        moveIDs[i] = moves[i].name;

    for (ImU16 i = 0; i < numSkills; i++)
        skillIDs[i] = skills[i].name;

    for (ImU16 i = 0; i < numItems; i++)
        itemIDs[i] = items[i].name;

    for (ImU16 i = 0; i < numEnemies; i++)
        enemyIDs[i] = enemies[i].name;

    // Main loop
    MSG msg;
    ZeroMemory(&msg, sizeof(msg));

    while (msg.message != WM_QUIT) {

        // Poll and handle messages (inputs, window resize, etc.)
        // You can read the io.WantCaptureMouse, io.WantCaptureKeyboard flags to tell if dear imgui wants to use your inputs.
        // - When io.WantCaptureMouse is true, do not dispatch mouse input data to your main application.
        // - When io.WantCaptureKeyboard is true, do not dispatch keyboard input data to your main application.
        // Generally you may always pass all inputs to dear imgui, and hide them from your application based on those two flags.
        if (PeekMessage(&msg, NULL, 0U, 0U, PM_REMOVE)) {

            ::TranslateMessage(&msg);
            ::DispatchMessage(&msg);
            continue;

        }

        // Start the Dear ImGui frame
        ImGui_ImplDX11_NewFrame();
        ImGui_ImplWin32_NewFrame();
        ImGui::NewFrame();

        //General Information WINDOW
        {

            ImGui::Begin("Information");
            ImGui::Text("Application average %.3f ms/frame (%.1f FPS)", 1000.0f / ImGui::GetIO().Framerate, ImGui::GetIO().Framerate);
            if (ImGui::Button("Save All")) {

                try {

                    writeMS(moves, numMoves);
                    writeSK(skills, numSkills);
                    writeMAG(eggs, numEggs);
                    writeSKI(books, numBooks);
                    writeSPC(specials, numSpecials);
                    writePC(stats, numStats);
                    writeITE(items, numItems);
                    writeEnemyStats(enemies, numEnemies);

                }
                catch (const std::exception& e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            ImGui::SameLine();

            if (ImGui::Button("Launch Grandia II")) {

                STARTUPINFO si;
                PROCESS_INFORMATION pi;
                LPCWSTR path = L"grandia2.exe";

                // set the size of the structures
                ZeroMemory(&si, sizeof(si));
                si.cb = sizeof(si);
                ZeroMemory(&pi, sizeof(pi));

                // start the program up
                CreateProcess(path,   // the path
                    lpCmdLine,        // Command line
                    NULL,             // Process handle not inheritable
                    NULL,             // Thread handle not inheritable
                    FALSE,            // Set handle inheritance to FALSE
                    0,                // No creation flags
                    NULL,             // Use parent's environment block
                    NULL,             // Use parent's starting directory 
                    &si,              // Pointer to STARTUPINFO structure
                    &pi               // Pointer to PROCESS_INFORMATION structure (removed extra parentheses)
                );

                // Close process and thread handles. 
                CloseHandle(pi.hProcess);
                CloseHandle(pi.hThread);

            }

            ImGui::End();

        }

        //MS_PARAM WINDOW
        {
            
            static ImU16 moveID = 0;
            static bool AilmentBitFlags[8] = {};

            ImGui::Begin("MS_PARAM");
            
            if (ImGui::Combo("Index", &moveID, moveIDs, (int)numMoves))
                for (size_t i = 0; i < 8; i++)
                    AilmentBitFlags[i] = moves[moveID].ailmentsBitflag & (1 << i);

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                    writeMS(moves, numMoves);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            //ImGui::LabelText("ID", std::to_string(moves[moveID].id).c_str());

            //ImGui::InputUByte("Icon", &moves[moveID].icon);
            ImGui::Combo("Icon", &moves[moveID].icon, moveIcons, 17);
            ImGui::InputText("Name", moves[moveID].name, 19);
            ImGui::InputUShort("Cost", &moves[moveID].cost);

            ImGui::Combo("Target Effect", &moves[moveID].targetEffect, targetEffects, 16);
            ImGui::Combo("Target Type", &moves[moveID].targetType, targetTypes, 16);

            ImGui::InputUShort("Strength", &moves[moveID].str);
            ImGui::InputUShort("Power", &moves[moveID].pow);
            ImGui::InputUShort("Range", &moves[moveID].range);

            ImGui::InputUShort2("Cast Time Lv1/Lv5", &moves[moveID].cast1);

            ImGui::InputUShort("Recovery", &moves[moveID].recovery);
            ImGui::InputUShort("Animation", &moves[moveID].animation);
            ImGui::InputUByte("Unknown #1", &moves[moveID].unknown1);
            ImGui::InputUByte("knockDown Flag", &moves[moveID].knockDown);

            ImGui::InputShort2("IP Stun/IP Cancel Stun", &moves[moveID].ipStun);

            ImGui::InputShort("Knockback", &moves[moveID].knockback);

            ImGui::Combo("Element", &moves[moveID].element, elements, 5);
            ImGui::InputUByte("Element Strength", &moves[moveID].elementStr);

            if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[0] << 0);
            ImGui::SameLine();
            if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[1] << 1);
            ImGui::SameLine();
            if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[2] << 2);
            ImGui::SameLine();
            if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[3] << 3);
            
            if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[4] << 4);
            ImGui::SameLine();
            if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[5] << 5);
            ImGui::SameLine();
            if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[6] << 6);
            ImGui::SameLine();
            if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
                moves[moveID].ailmentsBitflag ^= (AilmentBitFlags[7] << 7);

            ImGui::InputUByte("Ailments Chance", &moves[moveID].ailmentsChance);
            ImGui::InputByte4("Atk/Def/Act/Mov Mods", &moves[moveID].atkMod);
            ImGui::InputUShort("Special", &moves[moveID].special);
            ImGui::InputUShort("Coin Cost Lv1", &moves[moveID].coinCost1);
            ImGui::InputUShort("Coin Cost Lv2", &moves[moveID].coinCost2);
            ImGui::InputUShort("Coin Cost Lv3", &moves[moveID].coinCost3);
            ImGui::InputUShort("Coin Cost Lv4", &moves[moveID].coinCost4);
            ImGui::InputUShort("Coin Cost Lv5", &moves[moveID].coinCost5);
            ImGui::InputUShort("Multiplier", &moves[moveID].multiplier);
            ImGui::InputText("Description", moves[moveID].description, 41);
            
            ImGui::End();

        }

        //SK_PARAM WINDOW
        {

            static ImU16 skillID = 0;

            ImGui::Begin("SK_PARAM");

            ImGui::Combo("Index", &skillID, skillIDs, (int)numSkills); ImGui::SameLine();

                ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                writeSK(skills, numSkills);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            ImGui::InputText("Name", skills[skillID].name, 19);
            ImGui::InputUByte("Cost Type #1", &skills[skillID].cost1);
            ImGui::InputUByte("Cost Type #2", &skills[skillID].cost2);
            ImGui::InputUShort("Base HP", &skills[skillID].baseHp);
            ImGui::InputUShort("Base MP", &skills[skillID].baseMp);
            ImGui::InputUShort("Base SP", &skills[skillID].baseSp);
            ImGui::InputUShort("Base STR", &skills[skillID].baseStr);
            ImGui::InputUShort("Base VIT", &skills[skillID].baseVit);
            ImGui::InputUShort("Base ACT", &skills[skillID].baseAct);
            ImGui::InputUShort("Base MOV", &skills[skillID].baseMov);
            ImGui::InputUShort("Base MAG", &skills[skillID].baseMag);
            ImGui::InputUShort("Base MEN", &skills[skillID].baseMen);
            ImGui::InputUByte("Unknown #1", &skills[skillID].unknown1);
            ImGui::InputUByte("Unknown #2", &skills[skillID].unknown2);
            ImGui::InputUByte("Unknown #3", &skills[skillID].unknown3);
            ImGui::InputUByte("Unknown #4", &skills[skillID].unknown4);
            ImGui::InputUByte("Unknown #5", &skills[skillID].unknown5);
            ImGui::InputByte("Base Fire %", &skills[skillID].baseFirePercent);
            ImGui::InputByte("Base Wind %", &skills[skillID].baseWindPercent);
            ImGui::InputByte("Base Earth %", &skills[skillID].baseEarthPercent);
            ImGui::InputByte("Base Lightning %", &skills[skillID].baseLightningPercent);
            ImGui::InputByte("Base Blizzard %", &skills[skillID].baseBlizzardPercent);
            ImGui::InputByte("Base Water %", &skills[skillID].baseWaterPercent);
            ImGui::InputByte("Base Explosion %", &skills[skillID].baseExplosionPercent);
            ImGui::InputByte("Base Forest %", &skills[skillID].baseForestPercent);
            ImGui::InputUByte("Special", &skills[skillID].special);
            ImGui::InputUShort("Coin Cost Lv1", &skills[skillID].coinCost1);
            ImGui::InputUShort("Coin Cost Lv2", &skills[skillID].coinCost2);
            ImGui::InputUShort("Coin Cost Lv3", &skills[skillID].coinCost3);
            ImGui::InputUShort("Coin Cost Lv4", &skills[skillID].coinCost4);
            ImGui::InputUShort("Coin Cost Lv5", &skills[skillID].coinCost5);
            ImGui::InputUShort("Multiplier", &skills[skillID].multiplier);
            ImGui::InputText("Description", skills[skillID].description, 41);

            ImGui::End();

        }

        //TB_MAGIC WINDOW
        {

            static ImU16 eggID = 0;
            static ImU16 spellSlot = 0;
            const char* slotIDs[] = {"Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6", "Slot 7", "Slot 8", "Slot 9", "Slot 10", "Slot 11", "Slot 12", "Slot 13", "Slot 14", "Slot 15", "Slot 16", "Slot 17", "Slot 18"};

            ImGui::Begin("TB_MAGIC");

            ImGui::Combo("Index", &eggID, eggIDs, (int)numEggs); ImGui::SameLine();

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                writeMAG(eggs, numEggs);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            ImGui::Combo("Slot", &spellSlot, slotIDs, 18); ImGui::NewLine();

            ImGui::Combo("Spell", &eggs[eggID].spells[spellSlot].spellOffset, moveIDs, (int)numMoves);
            ImGui::InputUByte("Starting Level", &eggs[eggID].spells[spellSlot].startingLevel);
            ImGui::InputUByte("Egg Level Required", &eggs[eggID].spells[spellSlot].eggLevelRequired);
            ImGui::InputUByte("Unknown #1", &eggs[eggID].spells[spellSlot].unknown1); ImGui::NewLine();

            ImGui::End();

        }

        //TB_SKILL WINDOW
        {

            static ImU16 bookID = 0;
            static ImU16 skillSlot = 0;
            const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6"};

            ImGui::Begin("TB_SKILL");

            ImGui::Combo("Index", &bookID, bookIDs, (int)numBooks); ImGui::SameLine();

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                    writeSKI(books, numBooks);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }
            }

            ImGui::Combo("Slot", &skillSlot, slotIDs, 6); ImGui::NewLine();

            ImGui::Combo("Skill", &books[bookID].skills[skillSlot].skillOffset, skillIDs, (int)numSkills);
            ImGui::InputUByte("Starting Level", &books[bookID].skills[skillSlot].startingLevel);
            ImGui::InputUByte("Book Level Required", &books[bookID].skills[skillSlot].bookLevelRequired);
            ImGui::InputUByte("Unknown #1", &books[bookID].skills[skillSlot].unknown1); ImGui::NewLine();

            ImGui::End();

        }

        //TB_SPCL WINDOW
        {

            static ImU16 specialID = 0;
            static ImU16 moveSlot = 0;
            const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6" };

            ImGui::Begin("TB_SPCL");

            ImGui::Combo("Index", &specialID, specialIDs, (int)numSpecials); ImGui::SameLine();

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                writeSPC(specials, numSpecials);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            ImGui::Combo("Slot", &moveSlot, slotIDs, 6); ImGui::NewLine();

            ImGui::Combo("Move", &specials[specialID].moves[moveSlot].moveOffset, moveIDs, (int)numMoves);
            ImGui::InputUByte("Starting Level", &specials[specialID].moves[moveSlot].startingLevel);
            ImGui::InputUShort("Story Flag", &specials[specialID].moves[moveSlot].storyFlag);

            ImGui::End();

        }

        //PC_INIT WINDOW
        {

            static ImU16 statID = 0;

            ImGui::Begin("PC_INIT");

            ImGui::Combo("Index", &statID, statIDs, (int)numStats); ImGui::SameLine();

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                writePC(stats, numStats);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            ImGui::InputUInt("EXP", &stats[statID].exp);

            ImGui::Combo("Weapon", &stats[statID].weapon, itemIDs, (int)numItems);
            ImGui::Combo("Armour", &stats[statID].armour, itemIDs, (int)numItems);
            ImGui::Combo("Headgear", &stats[statID].headgear, itemIDs, (int)numItems);
            ImGui::Combo("Footwear", &stats[statID].footwear, itemIDs, (int)numItems);
            ImGui::Combo("Accessory", &stats[statID].accessory, itemIDs, (int)numItems);
            ImGui::Combo("Mana Egg", &stats[statID].manaEgg, itemIDs, (int)numItems);

            ImGui::InputShort("Stamina", &stats[statID].stamina);

            ImGui::InputUShort("Unknown #1", &stats[statID].unknown1);
            ImGui::InputUShort("Unknown #2", &stats[statID].unknown2);
            ImGui::InputUShort("Unknown #3", &stats[statID].unknown3);
            ImGui::InputUShort("Unknown #4", &stats[statID].unknown4);
            ImGui::InputUShort("Unknown #5", &stats[statID].unknown5);
            ImGui::InputUShort("Unknown #6", &stats[statID].unknown6);
            ImGui::InputUShort("Unknown #7", &stats[statID].unknown7);
            ImGui::InputUShort("Unknown #8", &stats[statID].unknown8);
            ImGui::InputUShort("Unknown #9", &stats[statID].unknown9);
            ImGui::InputUShort("Unknown #10", &stats[statID].unknown10);

            ImGui::InputShort("IP Stun", &stats[statID].ipStun);
            ImGui::InputShort("IP Cancel Stun", &stats[statID].ipCancelStun);

            ImGui::InputUByte("Combo SP Regen", &stats[statID].comboSpRegen);
            ImGui::InputUByte("Critical SP Regen", &stats[statID].critSpRegen);

            ImGui::InputUByte("Unknown #11", &stats[statID].unknown11);

            ImGui::InputUByte("Damaged SP Regen", &stats[statID].hitSpRegen);

            ImGui::InputUByte("Unknown #12", &stats[statID].unknown12);

            ImGui::InputByte("Still Evasion Rate", &stats[statID].evasionStillRate);
            ImGui::InputByte("Move Evasion Rate", &stats[statID].evasionMovingRate);
            ImGui::InputByte("Knockback Resist Rate", &stats[statID].ResistKnockback);

            ImGui::InputUShort("Unknown #13", &stats[statID].unknown13);

            ImGui::InputShort("TREC", &stats[statID].TREC);
            ImGui::InputShort("TDMG", &stats[statID].TDMG);

            ImGui::InputUShort("Unknown #14", &stats[statID].unknown14);

            ImGui::InputShort("THEAL", &stats[statID].THEAL);

            ImGui::InputShort("Size", &stats[statID].size);

            ImGui::InputUShort("Unknown #15", &stats[statID].unknown15);
            ImGui::InputUShort("Unknown #16", &stats[statID].unknown16);
            ImGui::InputUShort("Unknown #17", &stats[statID].unknown17);
            ImGui::InputUShort("Unknown #18", &stats[statID].unknown18);
            ImGui::InputUShort("Unknown #19", &stats[statID].unknown19);
            ImGui::InputUShort("Unknown #20", &stats[statID].unknown20);
            ImGui::InputUShort("Unknown #21", &stats[statID].unknown21);
            ImGui::InputUShort("Unknown #22", &stats[statID].unknown22);
            ImGui::InputUShort("Unknown #23", &stats[statID].unknown23);

            ImGui::End();

        }

        //ITEM WINDOW
        {

            static ImU16 itemID = 0;
            static bool hasEquip = false;
            static bool hasUsable = false;
            static bool EquipmentCharacterBitFlags[7] = {};
            static bool EquipmentAilmentBitFlags[8] = {};
            static bool UsableAilmentBitFlags[8] = {};
            const char* entryIDs[] = { "NULL", "Item", "Item", "Perm. Stat Modifier(?)", "Weapon", "Armour", "Headgear", "Footwear", "Accessory", "Mana Egg", "Unknown", "Valuable", "Skillbook", "Special", "Unknown", "Unknown" };

            ImGui::Begin("ITEM");

            if (ImGui::Combo("Index", &itemID, itemIDs, (int)numItems)) {

                (items[itemID].equipmentOffset) ? hasEquip = true : hasEquip = false;
                (items[itemID].usableOffset) ? hasUsable = true : hasUsable = false;

                if (hasEquip) {

                    for (size_t i = 0; i < 7; i++)
                        EquipmentCharacterBitFlags[i] = items[itemID].equipmentOffset->characterBitflag & (1 << i);

                    for (size_t i = 0; i < 8; i++)
                        EquipmentAilmentBitFlags[i] = items[itemID].equipmentOffset->ailmentsBitflag & (1 << i);

                }
                else {

                    for (size_t i = 0; i < 7; i++)
                        EquipmentCharacterBitFlags[i] = 0;

                    for (size_t i = 0; i < 8; i++)
                        EquipmentAilmentBitFlags[i] = 0;

                }

                if (hasUsable)
                    for (size_t i = 0; i < 8; i++)
                        UsableAilmentBitFlags[i] = items[itemID].usableOffset->ailmentsBitflag & (1 << i);
                else
                    for (size_t i = 0; i < 8; i++)
                        UsableAilmentBitFlags[i] = 0;

            }

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                writeITE(items, numItems);

                }
                catch (const std::exception & e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            //ImGui::LabelText("ID", std::to_string(items[itemID].id).c_str());   //it does come last in the file, but better to allow quick finding of the ID

            ImGui::InputText("Name", items[itemID].name, 19);
            ImGui::InputText("Description", items[itemID].description, 41);

            ImGui::Combo("Entry Type", &items[itemID].entryType, entryIDs, 16);

            ImGui::InputUByte("Unknown #1", &items[itemID].unknown1);
            ImGui::InputUByte("Unknown #2", &items[itemID].unknown2);
            ImGui::InputUByte("Unknown #3", &items[itemID].unknown3);
            ImGui::InputUByte("Icon", &items[itemID].icon);
            ImGui::InputUByte("Unknown #5", &items[itemID].unknown5);

            ImGui::InputUInt("Price", &items[itemID].price);

            if (ImGui::Checkbox("Equipment", &hasEquip)) {

                if (hasEquip)
                    items[itemID].equipmentOffset = new EquipmentStruct;
                else {

                    delete items[itemID].equipmentOffset;
                    items[itemID].equipmentOffset = nullptr;

                    for (size_t i = 0; i < 8; i++)
                        EquipmentAilmentBitFlags[i] = 0;

                }

            }
            
            ImGui::SameLine();

            if (ImGui::Checkbox("Usable", &hasUsable)) {

                if (hasUsable)
                    items[itemID].usableOffset = new UsableStruct;
                else {

                    delete items[itemID].usableOffset;
                    items[itemID].usableOffset = nullptr;

                    for (size_t i = 0; i < 8; i++)
                        UsableAilmentBitFlags[i] = 0;

                }

            }

            if (hasEquip) { //Item Equippable WINDOW

                ImGui::Begin("Item Equipment");

                if (ImGui::Checkbox("Ryudo", &EquipmentCharacterBitFlags[0]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[0] << 0);
                ImGui::SameLine();
                if (ImGui::Checkbox("Elena", &EquipmentCharacterBitFlags[1]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[1] << 1);
                ImGui::SameLine();
                if (ImGui::Checkbox("Millenia", &EquipmentCharacterBitFlags[2]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[2] << 2);
                ImGui::SameLine();
                if (ImGui::Checkbox("Roan", &EquipmentCharacterBitFlags[3]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[3] << 3);

                if (ImGui::Checkbox("Tio", &EquipmentCharacterBitFlags[4]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[4] << 4);
                ImGui::SameLine();
                if (ImGui::Checkbox("Mareg", &EquipmentCharacterBitFlags[5]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[5] << 5);
                ImGui::SameLine();
                if (ImGui::Checkbox("Prince Roan", &EquipmentCharacterBitFlags[6]))
                    items[itemID].equipmentOffset->characterBitflag ^= (EquipmentCharacterBitFlags[6] << 6);

                ImGui::InputShort("Strength", &items[itemID].equipmentOffset->str);
                ImGui::InputShort("Vitality", &items[itemID].equipmentOffset->vit);
                ImGui::InputShort("Action", &items[itemID].equipmentOffset->act);
                ImGui::InputShort("Movement", &items[itemID].equipmentOffset->mov);

                ImGui::Combo("Effective On", &items[itemID].equipmentOffset->effectiveOn, effectiveOn, 16);

                ImGui::InputByte("Fire %", &items[itemID].equipmentOffset->fireAffinity);
                ImGui::InputByte("Wind %", &items[itemID].equipmentOffset->windAffinity);
                ImGui::InputByte("Earth %", &items[itemID].equipmentOffset->earthAffinity);
                ImGui::InputByte("Lightning %", &items[itemID].equipmentOffset->lightningAffinity);
                ImGui::InputByte("Blizzard %", &items[itemID].equipmentOffset->blizzardAffinity);

                if (ImGui::Checkbox("Poison", &EquipmentAilmentBitFlags[0]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[0] << 0);
                ImGui::SameLine();
                if (ImGui::Checkbox("Sleep", &EquipmentAilmentBitFlags[1]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[1] << 1);
                ImGui::SameLine();
                if (ImGui::Checkbox("Paralysis", &EquipmentAilmentBitFlags[2]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[2] << 2);
                ImGui::SameLine();
                if (ImGui::Checkbox("Confusion", &EquipmentAilmentBitFlags[3]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[3] << 3);

                if (ImGui::Checkbox("Plague", &EquipmentAilmentBitFlags[4]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[4] << 4);
                ImGui::SameLine();
                if (ImGui::Checkbox("Magic Block", &EquipmentAilmentBitFlags[5]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[5] << 5);
                ImGui::SameLine();
                if (ImGui::Checkbox("Move Block", &EquipmentAilmentBitFlags[6]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[6] << 6);
                ImGui::SameLine();
                if (ImGui::Checkbox("Death", &EquipmentAilmentBitFlags[7]))
                    items[itemID].equipmentOffset->ailmentsBitflag ^= (EquipmentAilmentBitFlags[7] << 7);

                ImGui::InputUByte("Ailments Chance", &items[itemID].equipmentOffset->ailmentsChance);

                ImGui::InputUByte("Unknown #1", &items[itemID].equipmentOffset->unknown1);
                ImGui::InputUByte("Unknown #2", &items[itemID].equipmentOffset->unknown2);
                ImGui::InputUByte("Unknown #3", &items[itemID].equipmentOffset->unknown3);
                ImGui::InputUByte("Unknown #4", &items[itemID].equipmentOffset->unknown4);
                ImGui::InputUByte("Unknown #5", &items[itemID].equipmentOffset->unknown5);
                ImGui::InputUByte("Unknown #6", &items[itemID].equipmentOffset->unknown6);
                ImGui::InputUByte("Unknown #7", &items[itemID].equipmentOffset->unknown7);
                ImGui::InputUByte("Unknown #8", &items[itemID].equipmentOffset->unknown8);

                ImGui::InputUShort("Special", &items[itemID].equipmentOffset->special);

                ImGui::End();

            }

            if (hasUsable) {    //Item Usable WINDOW

                ImGui::Begin("Item Usable");

                ImGui::Combo("Target Effect", &items[itemID].usableOffset->targetEffect, targetEffects, 16);
                ImGui::Combo("Target Type", &items[itemID].usableOffset->targetType, targetTypes, 16);

                ImGui::InputUShort("Power", &items[itemID].usableOffset->power);
                ImGui::InputUShort("Range", &items[itemID].usableOffset->range);
                ImGui::InputUShort("Cast Time", &items[itemID].usableOffset->castTime);
                ImGui::InputUShort("Recovery", &items[itemID].usableOffset->recoveryTime);
                ImGui::InputUShort("Animation", &items[itemID].usableOffset->animation);

                ImGui::Combo("Effective On", &items[itemID].usableOffset->effectiveOn, effectiveOn, 16);

                ImGui::InputUByte("Unknown #1", &items[itemID].usableOffset->unknown1);

                ImGui::InputShort("IP Damage", &items[itemID].usableOffset->ipDamage);
                ImGui::InputShort("IP Cancel Damage", &items[itemID].usableOffset->ipCancelDamage);
                ImGui::InputShort("Knockback", &items[itemID].usableOffset->knockback);

                ImGui::Combo("Element", &items[itemID].usableOffset->element, elements, 5);
                ImGui::InputUByte("Element Strength", &items[itemID].usableOffset->elementStr);

                if (ImGui::Checkbox("Poison", &UsableAilmentBitFlags[0]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[0] << 0);
                ImGui::SameLine();
                if (ImGui::Checkbox("Sleep", &UsableAilmentBitFlags[1]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[1] << 1);
                ImGui::SameLine();
                if (ImGui::Checkbox("Paralysis", &UsableAilmentBitFlags[2]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[2] << 2);
                ImGui::SameLine();
                if (ImGui::Checkbox("Confusion", &UsableAilmentBitFlags[3]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[3] << 3);

                if (ImGui::Checkbox("Plague", &UsableAilmentBitFlags[4]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[4] << 4);
                ImGui::SameLine();
                if (ImGui::Checkbox("Magic Block", &UsableAilmentBitFlags[5]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[5] << 5);
                ImGui::SameLine();
                if (ImGui::Checkbox("Move Block", &UsableAilmentBitFlags[6]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[6] << 6);
                ImGui::SameLine();
                if (ImGui::Checkbox("Death", &UsableAilmentBitFlags[7]))
                    items[itemID].usableOffset->ailmentsBitflag ^= (UsableAilmentBitFlags[7] << 7);

                ImGui::InputByte4("Atk/Def/Act/Mov Mods", &items[itemID].usableOffset->atkMod);

                ImGui::InputUByte("Break Chance", &items[itemID].usableOffset->breakChance);
                ImGui::InputUByte("Special", &items[itemID].usableOffset->special);

                ImGui::InputUByte("Unknown #2", &items[itemID].usableOffset->unknown2);
                ImGui::InputUByte("Unknown #3", &items[itemID].usableOffset->unknown3);

                ImGui::End();

            }

            ImGui::End();

        }

        //ENEMY WINDOW
        {

            static ImU16 enemyID = 0;
            static bool AilmentBitFlags[8] = {};
            static bool showMoves = false;
            static bool MoveAilmentBitFlags[8] = {};
            static ImU16 moveSlot = 0;
            const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5" };

            ImGui::Begin("ENEMIES");

            if (ImGui::Combo("Index", &enemyID, enemyIDs, (int)numEnemies), 100)
                for (size_t i = 0; i < 8; i++)
                    AilmentBitFlags[i] = enemies[enemyID].ailmentsBitflag & (1 << i);

            ImGui::SameLine();
            if (ImGui::Button("Save")) {

                try {

                    writeEnemyStats(enemies, numEnemies);

                }
                catch (const std::exception& e) {

                    ImGui::Begin("ERROR", canClose);
                    ImGui::LabelText("", e.what());
                    ImGui::End();

                }

            }

            ImGui::InputText("Name", enemies[enemyID].name, 19);

            ImGui::InputUByte("Type1", &enemies[enemyID].type1);
            ImGui::InputUByte("Type2", &enemies[enemyID].type2);

            ImGui::InputShort("Level", &enemies[enemyID].level);
            ImGui::InputInt("Health", &enemies[enemyID].health);
            ImGui::InputShort("MP", &enemies[enemyID].mp);
            ImGui::InputShort("SP", &enemies[enemyID].sp);
            ImGui::InputShort("VIT", &enemies[enemyID].vit);
            ImGui::InputShort("AGI", &enemies[enemyID].agi);
            ImGui::InputShort("SPD", &enemies[enemyID].spd);
            ImGui::InputShort("MEN", &enemies[enemyID].men);
            ImGui::InputShort("Stamina", &enemies[enemyID].stamina);
            ImGui::InputShort("IP Stun", &enemies[enemyID].ipStun);
            ImGui::InputShort("IP Cancel Stun", &enemies[enemyID].ipCancelStun);
            ImGui::InputByte("Still Evasion Rate", &enemies[enemyID].evasionStillRate);
            ImGui::InputByte("Moving Evasion Rate", &enemies[enemyID].evasionMovingRate);
            ImGui::InputByte("Fire Resist", &enemies[enemyID].fireResist);
            ImGui::InputByte("Wind Resist", &enemies[enemyID].windResist);
            ImGui::InputByte("Earth Resist", &enemies[enemyID].earthResist);
            ImGui::InputByte("Lightning Resist", &enemies[enemyID].lightningResist);
            ImGui::InputByte("Blizzard Resist", &enemies[enemyID].blizzardResist);

            if (ImGui::Checkbox("Poison", &AilmentBitFlags[0]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[0] << 0);
            ImGui::SameLine();
            if (ImGui::Checkbox("Sleep", &AilmentBitFlags[1]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[1] << 1);
            ImGui::SameLine();
            if (ImGui::Checkbox("Paralysis", &AilmentBitFlags[2]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[2] << 2);
            ImGui::SameLine();
            if (ImGui::Checkbox("Confusion", &AilmentBitFlags[3]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[3] << 3);

            if (ImGui::Checkbox("Plague", &AilmentBitFlags[4]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[4] << 4);
            ImGui::SameLine();
            if (ImGui::Checkbox("Magic Block", &AilmentBitFlags[5]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[5] << 5);
            ImGui::SameLine();
            if (ImGui::Checkbox("Move Block", &AilmentBitFlags[6]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[6] << 6);
            ImGui::SameLine();
            if (ImGui::Checkbox("Death", &AilmentBitFlags[7]))
                enemies[enemyID].ailmentsBitflag ^= (AilmentBitFlags[7] << 7);

            ImGui::InputShort("Knockback Resist", &enemies[enemyID].knockbackResist);
            ImGui::InputShort("Size", &enemies[enemyID].size);
            ImGui::InputByte("No Run", &enemies[enemyID].noRunFlag);
            ImGui::InputInt("EXP", &enemies[enemyID].exp);
            ImGui::InputInt("Skill Coins", &enemies[enemyID].skillCoins);
            ImGui::InputInt("Magic Coins", &enemies[enemyID].magicCoins);
            ImGui::InputInt("Gold Coins", &enemies[enemyID].goldCoins);

            ImGui::Combo("Item1", &enemies[enemyID].item1, itemIDs, (int)numItems);
            ImGui::Combo("Item2", &enemies[enemyID].item2, itemIDs, (int)numItems);

            ImGui::InputByte("Item1 Chance", &enemies[enemyID].item1Chance);
            ImGui::InputByte("Item2 Chance", &enemies[enemyID].item2Chance);

            //ImGui::LabelText("Filename", enemies[enemyID].filename.c_str());      //Used for testing to verify files are correct

            ImGui::Checkbox("Show Moves", &showMoves);

            if(showMoves) {

                ImGui::Begin("ENEMY MOVES");

                if (ImGui::Combo("Slot", &moveSlot, slotIDs, 5))
                    for (size_t i = 0; i < 8; i++)
                        MoveAilmentBitFlags[i] = enemies[enemyID].moves[moveSlot].ailmentsBitflag & (1 << i);

                ImGui::InputText("Name", enemies[enemyID].moves[moveSlot].name, 19);
                ImGui::InputUShort("MP Cost", &enemies[enemyID].moves[moveSlot].mp);
                ImGui::InputUShort("SP Cost", &enemies[enemyID].moves[moveSlot].sp);
                ImGui::InputUByte("Unknown #1", &enemies[enemyID].moves[moveSlot].unknown);

                ImGui::Combo("Target Effect", &enemies[enemyID].moves[moveSlot].targetEffect, targetEffects, 16);

                ImGui::InputUShort("Strength", &enemies[enemyID].moves[moveSlot].str);
                ImGui::InputUShort("Power", &enemies[enemyID].moves[moveSlot].pow);
                ImGui::InputUShort("Damage(?)", &enemies[enemyID].moves[moveSlot].ad);

                ImGui::Combo("Target Type", &enemies[enemyID].moves[moveSlot].targetType, targetTypes, 16);

                ImGui::InputUByte("Unknown #2", &enemies[enemyID].moves[moveSlot].unknown1);
                ImGui::InputUShort("Distance", &enemies[enemyID].moves[moveSlot].distance);
                ImGui::InputUShort("Accuracy", &enemies[enemyID].moves[moveSlot].accuracy);
                ImGui::InputUShort("Range", &enemies[enemyID].moves[moveSlot].range);
                ImGui::InputUShort("Cast Time", &enemies[enemyID].moves[moveSlot].castTime);
                ImGui::InputUShort("Recovery", &enemies[enemyID].moves[moveSlot].recovery);

                //ImGui::InputUByte("Animation", &enemies[enemyID].moves[moveSlot].animation);
                ImGui::Combo("Animation", &enemies[enemyID].moves[moveSlot].animation, moveIDs, numMoves);

                ImGui::InputUByte("Knockdown", &enemies[enemyID].moves[moveSlot].knockDown);

                ImGui::InputShort2("IP Stun/IP Cancel Stun", &enemies[enemyID].moves[moveSlot].ipStun);
                ImGui::InputShort("Knockback", &enemies[enemyID].moves[moveSlot].knockback);

                ImGui::Combo("Element", &enemies[enemyID].moves[moveSlot].element, elements, 5);
                ImGui::InputUByte("Element Strength", &enemies[enemyID].moves[moveSlot].elementStr);

                if (ImGui::Checkbox("Poison", &MoveAilmentBitFlags[0]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[0] << 0);
                ImGui::SameLine();
                if (ImGui::Checkbox("Sleep", &MoveAilmentBitFlags[1]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[1] << 1);
                ImGui::SameLine();
                if (ImGui::Checkbox("Paralysis", &MoveAilmentBitFlags[2]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[2] << 2);
                ImGui::SameLine();
                if (ImGui::Checkbox("Confusion", &MoveAilmentBitFlags[3]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[3] << 3);

                if (ImGui::Checkbox("Plague", &MoveAilmentBitFlags[4]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[4] << 4);
                ImGui::SameLine();
                if (ImGui::Checkbox("Magic Block", &MoveAilmentBitFlags[5]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[5] << 5);
                ImGui::SameLine();
                if (ImGui::Checkbox("Move Block", &MoveAilmentBitFlags[6]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[6] << 6);
                ImGui::SameLine();
                if (ImGui::Checkbox("Death", &MoveAilmentBitFlags[7]))
                    enemies[enemyID].moves[moveSlot].ailmentsBitflag ^= (MoveAilmentBitFlags[7] << 7);

                ImGui::InputUByte("Ailments Chance", &enemies[enemyID].moves[moveSlot].ailmentsChance);

                ImGui::InputByte4("Atk/Def/Act/Mov Mods", &enemies[enemyID].moves[moveSlot].atkMod);

                ImGui::InputUShort("Special", &enemies[enemyID].moves[moveSlot].special);

                ImGui::End();

            }

            ImGui::End();

        }

        // Rendering
        ImGui::Render();
        g_pd3dDeviceContext->OMSetRenderTargets(1, &g_mainRenderTargetView, NULL);
        g_pd3dDeviceContext->ClearRenderTargetView(g_mainRenderTargetView, (float*)&clear_color);
        ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());

        g_pSwapChain->Present(1, 0); // Present with vsync

    }

    // Cleanup
    ImGui_ImplDX11_Shutdown();
    ImGui_ImplWin32_Shutdown();
    ImGui::DestroyContext();

    CleanupDeviceD3D();
    DestroyWindow(hwnd);
    UnregisterClass(wc.lpszClassName, wc.hInstance);

    delete[] moves;
    moves = nullptr;

    delete[] skills;
    skills = nullptr;

    delete[] eggs;
    eggs = nullptr;

    delete[] books;
    books = nullptr;

    delete[] specials;
    specials = nullptr;

    delete[] items;
    items = nullptr;

    delete[] stats;
    stats = nullptr;

    return 0;

}


bool CreateDeviceD3D(HWND hWnd)
{
    // Setup swap chain
    DXGI_SWAP_CHAIN_DESC sd;
    ZeroMemory(&sd, sizeof(sd));
    sd.BufferCount = 2;
    sd.BufferDesc.Width = 0;
    sd.BufferDesc.Height = 0;
    sd.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
    sd.BufferDesc.RefreshRate.Numerator = 60;
    sd.BufferDesc.RefreshRate.Denominator = 1;
    sd.Flags = DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH;
    sd.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
    sd.OutputWindow = hWnd;
    sd.SampleDesc.Count = 1;
    sd.SampleDesc.Quality = 0;
    sd.Windowed = TRUE;
    sd.SwapEffect = DXGI_SWAP_EFFECT_DISCARD;

    UINT createDeviceFlags = 0;
    //createDeviceFlags |= D3D11_CREATE_DEVICE_DEBUG;
    D3D_FEATURE_LEVEL featureLevel;
    const D3D_FEATURE_LEVEL featureLevelArray[2] = { D3D_FEATURE_LEVEL_11_0, D3D_FEATURE_LEVEL_10_0, };
    if (D3D11CreateDeviceAndSwapChain(NULL, D3D_DRIVER_TYPE_HARDWARE, NULL, createDeviceFlags, featureLevelArray, 2, D3D11_SDK_VERSION, &sd, &g_pSwapChain, &g_pd3dDevice, &featureLevel, &g_pd3dDeviceContext) != S_OK)
        return false;

    CreateRenderTarget();
    return true;
}

void CleanupDeviceD3D()
{
    CleanupRenderTarget();
    if (g_pSwapChain) { g_pSwapChain->Release(); g_pSwapChain = NULL; }
    if (g_pd3dDeviceContext) { g_pd3dDeviceContext->Release(); g_pd3dDeviceContext = NULL; }
    if (g_pd3dDevice) { g_pd3dDevice->Release(); g_pd3dDevice = NULL; }
}

void CreateRenderTarget()
{
    ID3D11Texture2D* pBackBuffer;
    g_pSwapChain->GetBuffer(0, IID_PPV_ARGS(&pBackBuffer));
    g_pd3dDevice->CreateRenderTargetView(pBackBuffer, NULL, &g_mainRenderTargetView);
    pBackBuffer->Release();
}

void CleanupRenderTarget()
{
    if (g_mainRenderTargetView) { g_mainRenderTargetView->Release(); g_mainRenderTargetView = NULL; }
}

// Win32 message handler
extern LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);
LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    if (ImGui_ImplWin32_WndProcHandler(hWnd, msg, wParam, lParam))
        return true;

    switch (msg)
    {
    case WM_SIZE:
        if (g_pd3dDevice != NULL && wParam != SIZE_MINIMIZED)
        {
            CleanupRenderTarget();
            g_pSwapChain->ResizeBuffers(0, (UINT)LOWORD(lParam), (UINT)HIWORD(lParam), DXGI_FORMAT_UNKNOWN, 0);
            CreateRenderTarget();
        }
        return 0;
    case WM_SYSCOMMAND:
        if ((wParam & 0xfff0) == SC_KEYMENU) // Disable ALT application menu
            return 0;
        break;
    case WM_DESTROY:
        ::PostQuitMessage(0);
        return 0;
    }
    return ::DefWindowProc(hWnd, msg, wParam, lParam);
}