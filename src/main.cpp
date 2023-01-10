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
#include "./include/BossesClass.h"
#include "./include/MdtsClass.h"
#include "./include/LevelupsClass.h"

#include "./include/ImGuiInstance.h"

int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPTSTR lpCmdLine, _In_ int nCmdShow) {
    // read data
    Moves::getInstance().read();
    ManaEggs::getInstance().read();
    Skills::getInstance().read();
    SkillBooks::getInstance().read();
    Specials::getInstance().read();
    Items::getInstance().read();
    StartStats::getInstance().read();
    Enemies::getInstance().read();
    Bosses::getInstance().read();
    Mdts::getInstance().read();
    Levelups::getInstance().read();

    //  now store all needed data into relevant classes
    ManaEggs::getInstance().storeMoves(Moves::getInstance().getMoves());
    SkillBooks::getInstance().storeSkills(Skills::getInstance().getSkills());
    Specials::getInstance().storeMoves(Moves::getInstance().getMoves());
    Items::getInstance().storeMoves(Moves::getInstance().getMoves());
    StartStats::getInstance().storeItems(Items::getInstance().getItems());
    Enemies::getInstance().storeMoves(Moves::getInstance().getMoves());
    Enemies::getInstance().storeItems(Items::getInstance().getItems());
    Bosses::getInstance().storeMoves(Moves::getInstance().getMoves());
    Bosses::getInstance().storeItems(Items::getInstance().getItems());
    Mdts::getInstance().storeItems(Items::getInstance().getItems());

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
        DrawMenuBar(lpCmdLine);

        Moves::getInstance().draw();
        ManaEggs::getInstance().draw();
        Skills::getInstance().draw();
        SkillBooks::getInstance().draw();
        Specials::getInstance().draw();
        Items::getInstance().draw();
        StartStats::getInstance().draw();
        Enemies::getInstance().draw();
        Bosses::getInstance().draw();
        Mdts::getInstance().draw();
        Levelups::getInstance().draw();

        EndFrame();
    }

    EndImGui();

    return EXIT_SUCCESS;
}