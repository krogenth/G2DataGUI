#include <tchar.h>
#include <vector>
#include <filesystem>
#include <string>

#include "./moves/Moves.h"
#include "./moves/ManaEggs.h"
#include "./moves/Specials.h"
#include "./skills/Skills.h"
#include "./skills/SkillBooks.h"
#include "./items/Items.h"
#include "./stats/StartStats.h"
#include "./enemies/Enemies.h"
#include "./bosses/Bosses.h"
#include "./maps/Maps.h"
#include "./levelups/Levelups.h"

#include "./ui/ImGuiInstance.h"

int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPTSTR lpCmdLine, _In_ int nCmdShow) {
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