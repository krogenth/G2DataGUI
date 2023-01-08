#include <tchar.h>
#include <vector>
#include <filesystem>
#include <string>

#include "./include/MovesClass.h"
#include "./include/ManaEggsClass.h"
#include "./include/SkillsClass.h"
#include "./include/SkillBooksClass.h"
#include "./include/SpecialsClass.h"
#include "./include/ItemsClass.h"
#include "./include/StartStatsClass.h"
#include "./include/EnemiesClass.h"
#include "./include/MdtsClass.h"
#include "./include/LevelupClass.h"

#include "./include/ImGuiInstance.h"

int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPTSTR lpCmdLine, _In_ int nCmdShow) {
    MovesClass moves;
    ManaEggsClass manaeggs;
    SkillsClass skills;
    SkillBooksClass skillbooks;
    SpecialsClass specials;
    ItemsClass items;
    StartStatsClass startStats;
    EnemiesClass enemies;
    MdtsClass mdts;
    LevelupClass levelups;

    bool isHDVersion = true;

    if (!std::filesystem::exists("content")) {
        isHDVersion = false;
    }

    // read in data
    if (isHDVersion) {
        moves.read("./content/data/afs/xls_data/MS_PARAM.BIN");
        manaeggs.read("./content/data/afs/xls_data/TB_MAGIC.BIN");
        skills.read("./content/data/afs/xls_data/SK_PARAM.BIN");
        skillbooks.read("./content/data/afs/xls_data/TB_SKILL.BIN");
        specials.read("./content/data/afs/xls_data/TB_SPCL.BIN");
        items.read("./content/data/afs/xls_data/ITEM.BIN");
        startStats.read("./content/data/afs/xls_data/PC_INIT.BIN");
        enemies.read("./content/data/afs/");
        mdts.read("./content/data/afs/map/");
        levelups.read("./content/data/afs/xls_data/TB_LVUP.BIN");
    }
    else {
        moves.read("./data/afs/xls_data/MS_PARAM.BIN");
        manaeggs.read("./data/afs/xls_data/TB_MAGIC.BIN");
        skills.read("./data/afs/xls_data/SK_PARAM.BIN");
        skillbooks.read("./data/afs/xls_data/TB_SKILL.BIN");
        specials.read("./data/afs/xls_data/TB_SPCL.BIN");
        items.read("./data/afs/xls_data/ITEM.BIN");
        startStats.read("./data/afs/xls_data/PC_INIT.BIN");
        enemies.read("./data/afs/");
        mdts.read("./data/afs/map/");
        levelups.read("./data/afs/xls_data/TB_LVUP.BIN");
    }

    //  now store all needed data into relevant classes
    manaeggs.storeMoves(moves.getMoves());
    skillbooks.storeSkills(skills.getSkills());
    specials.storeMoves(moves.getMoves());
    items.storeMoves(moves.getMoves());
    startStats.storeItems(items.getItems());
    enemies.storeMoves(moves.getMoves());
    enemies.storeItems(items.getItems());
    mdts.storeItems(items.getItems());

    if (!StartImGui()) {
        return EXIT_FAILURE;
    }

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
            TranslateMessage(&msg);
            DispatchMessage(&msg);
            continue;
        }

        StartFrame();
        DrawMenuBar(lpCmdLine, &moves, &manaeggs, &skills, &skillbooks, &specials, &items, &startStats, &enemies, &mdts, &levelups);

        moves.draw();
        manaeggs.draw();
        skills.draw();
        skillbooks.draw();
        specials.draw();
        items.draw();
        startStats.draw();
        enemies.draw();
        mdts.draw();
        levelups.draw();

        EndFrame();
    }

    EndImGui();

    return EXIT_SUCCESS;
}