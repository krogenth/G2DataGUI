#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include "ImGui/imgui.h"
#include "ImGui/imgui_impl_win32.h"
#include "ImGui/imgui_impl_dx11.h"
#include <d3d11.h>
#define DIRECTINPUT_VERSION 0x0800
#include <dinput.h>
#include <tchar.h>
#include <vector>
#include <experimental/filesystem>

#include <cstdlib>
#include <Windows.h>
#include <type_traits>
#include <string>
//#include <thread>
#include <future>
#include <utility>

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

bool isHDVersion = true;

bool* canClose = new bool[1]{ true };

/*
TODO:
    Implement enemy groupings(from *.mdt files in /maps/), and patrolling sections(?)(from *.mdt files in /maps/)
    Include TB_LVUP.BIN
*/

int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPTSTR lpCmdLine, _In_ int nCmdShow) {

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

    // Setup Dear ImGui style
    ImGui::StyleColorsDark();

    // Setup Platform/Renderer bindings
    ImGui_ImplWin32_Init(hwnd);
    ImGui_ImplDX11_Init(g_pd3dDevice, g_pd3dDeviceContext);

    ImVec4 clear_color = ImVec4(0.00f, 0.00f, 0.00f, 1.00f);

    std::vector<MoveStruct> moves;
    std::vector<SkillStruct> skills;
    std::vector<ManaEggStruct> eggs;
    std::vector<SkillBookStruct> books;
    std::vector<SpecialMoveStruct> specials;
    std::vector<ItemStruct> items;
    std::vector<StartStatsStruct> stats;
    std::vector<EnemyStruct> enemies;

    if (!std::experimental::filesystem::exists("content"))
        isHDVersion = false;

    //CALL READ DATA FUNCTIONS
    {

        if (isHDVersion) {

            try {

                //std::thread movesThread(readMS, std::ref(moves));
                //std::thread skillsThread(readSK, std::ref(skills));
                //std::thread itemsThread(readITE, std::ref(items));
                //std::thread enemiesThread(readEnemyStats, std::ref(enemies));
                readMS(moves);
                readSK(skills);
                readITE(items);
                readEnemyStats(enemies);
                readMAG(eggs);
                readSKI(books);
                readSPC(specials);
                readPC(stats);

                //movesThread.join();
                //skillsThread.join();
                //itemsThread.join();
                //enemiesThread.join();

            }
            catch (const std::exception& e) {

                ImGui::Begin("ERROR", canClose);
                ImGui::LabelText("", e.what());
                ImGui::End();

            }

        }
        else {

            try {

                //std::thread movesThread(readMS, std::ref(moves), "data/afs/xls_data/MS_PARAM.BIN");
                //std::thread skillsThread(readSK, std::ref(skills), "data/afs/xls_data/SK_PARAM.BIN");
                //std::thread itemsThread(readITE, std::ref(items), "data/afs/xls_data/ITEM.BIN");
                //std::thread enemiesThread(readEnemyStats, std::ref(enemies), "data/afs/enemy");
                readMS(moves, "data/afs/xls_data/MS_PARAM.BIN");
                readSK(skills, "data/afs/xls_data/SK_PARAM.BIN");
                readITE(items, "data/afs/xls_data/ITEM.BIN");
                readEnemyStats(enemies, "data/afs/");
                readMAG(eggs, "data/afs/xls_data/TB_MAGIC.BIN");
                readSKI(books, "data/afs/xls_data/TB_SKILL.BIN");
                readSPC(specials, "data/afs/xls_data/TB_SPCL.BIN");
                readPC(stats, "data/afs/xls_data/PC_INIT.BIN");

                //movesThread.join();
                //skillsThread.join();
                //itemsThread.join();
                //enemiesThread.join();

            }
            catch (const std::exception& e) {

                ImGui::Begin("ERROR", canClose);
                ImGui::LabelText("", e.what());
                ImGui::End();

            }

        }

    }

    char** moveIDs = new char* [moves.size()] {};
    char** skillIDs = new char* [skills.size()] {};
    char** itemIDs = new char* [items.size()] {};
    char** enemyIDs = new char* [enemies.size()] {};

    for (ImU16 i = 0; i < moves.size(); i++)
        moveIDs[i] = moves[i].name;

    for (ImU16 i = 0; i < skills.size(); i++)
        skillIDs[i] = skills[i].name;

    for (ImU16 i = 0; i < items.size(); i++)
        itemIDs[i] = items[i].name;

    for (ImU16 i = 0; i < enemies.size(); i++)
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

            static bool my_tool_active = true;

            ImGui::Begin("Information", &my_tool_active, ImGuiWindowFlags_MenuBar);

            if (ImGui::BeginMenuBar())
            {
                if (ImGui::BeginMenu("File"))
                {

                    if (ImGui::MenuItem("Launch G2", "Ctrl+L")) {

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

                    if (ImGui::MenuItem("Save All", "Ctrl+S")) {
                        
                        try {

                            writeMS(moves);
                            writeSK(skills);
                            writeMAG(eggs);
                            writeSKI(books);
                            writeSPC(specials);
                            writePC(stats);
                            writeITE(items);
                            writeEnemyStats(enemies);

                        }
                        catch (const std::exception& e) {

                            ImGui::Begin("ERROR", canClose);
                            ImGui::LabelText("", e.what());
                            ImGui::End();

                        }

                    }
                    if (ImGui::MenuItem("Close", "Ctrl+W")) {

                        exit(0);

                    }
                    ImGui::EndMenu();
                }
                ImGui::EndMenuBar();
            }

            ImGui::Text("%.3f ms/frame (%.1f FPS)", 1000.0f / ImGui::GetIO().Framerate, ImGui::GetIO().Framerate);

            ImGui::End();

        }

        drawMS(moves, canClose, moveIDs, moves.size());
        drawSK(skills, skillIDs, canClose);
        drawMAG(eggs, canClose, moveIDs, moves.size());
        drawSKI(books, canClose, skillIDs, skills.size());
        drawSPC(specials, canClose, moveIDs, moves.size());
        drawPC(stats, canClose, itemIDs, items.size());
        drawITE(items, itemIDs, canClose);
        drawEnemyStats(enemies, enemyIDs, canClose, moveIDs, moves.size(), itemIDs, items.size());

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

    return EXIT_SUCCESS;

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