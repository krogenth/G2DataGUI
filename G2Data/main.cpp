#include "imgui.h"
#include "imgui_impl_win32.h"
#include "imgui_impl_dx11.h"
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

static ID3D11Device* g_pd3dDevice = NULL;
static ID3D11DeviceContext* g_pd3dDeviceContext = NULL;
static IDXGISwapChain* g_pSwapChain = NULL;
static ID3D11RenderTargetView* g_mainRenderTargetView = NULL;

bool CreateDeviceD3D(HWND hWnd);
void CleanupDeviceD3D();
void CreateRenderTarget();
void CleanupRenderTarget();
LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow) {

    /*
    	01(0x01) Restore HP
	02(0x02) Restore MP
	03(0x03) Restore SP
	04(0x04) Ally(?) Buff/Debuff
	05(0x05) Attack/Move(?) Damage
	06(0x06) Magic(?) Damage
	07(0x07) Enemy(?) Buff/Debuff
	08(0x08) Status Change
	15(0x0F) Special(Spellbinding Eye, Gravity, etc.)
    */
    const char* targetEffects[] = {"NULL\0", "Restore HP(MEN)\0", "Restore MP\0", "Restore SP\0", "Ally Buff/Debuff\0", "Physical Damage(STR)\0", "Magical Damage(MAG)\0", "Enemy Buff/Debuff\0", "Status Change\0", "Unknown\0", "Unknown\0", "Unknown\0", "Unknown\0", "Unknown\0", "Unknown\0", "Special\0"};
    const char* targetTypes[] = {"NULL\0", "One Ally\0", "Area Allies\0", "All Allies\0", "One Enemy\0", "Area Enemies\0", "All Enemies\0", "Enemy Line\0", "Self\0", "Unknown\0", "Area Around Self\0", "Unknown\0", "Unknown\0", "Area Around Self\0", "Unknown\0", "Unknown\0"};

    ImU16 numMoves = 0;
    MoveStruct* moves = readMS(numMoves);

    ImU16 numSkills = 0;
    SkillStruct* skills = readSK(numSkills);

    ImU16 numEggs = 0;
    ManaEggStruct* eggs = readMAG(numEggs);

    char** moveIDs = new char* [numMoves] {};
    char** skillIDs = new char* [numSkills] {};
    const char* eggIDs[] = {"NULL", "Holy Egg", "Chaos Egg", "Mist Egg", "Gravity Egg", "Soul Egg", "Star Egg", "Tutor Egg", "Change Egg", "Fairy Egg", "Dragon Egg"};

    for (ImU16 i = 0; i < numMoves; i++)
        moveIDs[i] = moves[i].name;

    for (ImU16 i = 0; i < numSkills; i++)
        skillIDs[i] = skills[i].name;

    // Create application window
    WNDCLASSEX wc = { sizeof(WNDCLASSEX), CS_CLASSDC, WndProc, 0L, 0L, GetModuleHandle(NULL), NULL, NULL, NULL, NULL, _T("G2Data"), NULL };
    RegisterClassEx(&wc);
    HWND hwnd = CreateWindow(wc.lpszClassName, _T("G2Data"), WS_OVERLAPPEDWINDOW, 100, 100, 1280, 800, NULL, NULL, wc.hInstance, NULL);

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
    ImGuiIO& io = ImGui::GetIO(); (void)io;
    io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard;     // Enable Keyboard Controls
    //io.ConfigFlags |= ImGuiConfigFlags_NavEnableGamepad;      // Enable Gamepad Controls

    // Setup Dear ImGui style
    ImGui::StyleColorsDark();
    //ImGui::StyleColorsClassic();

    // Setup Platform/Renderer bindings
    ImGui_ImplWin32_Init(hwnd);
    ImGui_ImplDX11_Init(g_pd3dDevice, g_pd3dDeviceContext);

    ImVec4 clear_color = ImVec4(0.00f, 0.00f, 0.00f, 1.00f);

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

        {   //MS_PARAM WINDOW
            
            static ImU16 moveID = 0;
            static bool AilmentBitFlags[8] = {};

            ImGui::Begin("MS_PARAM");

            /*
            if (ImGui::Combo("Index", (int*)&moveID, moveIDs, (int)numMoves), -1)
                for (size_t i = 0; i < 8; i++)
                    AilmentBitFlags[i] = moves[moveID].ailmentsBitflag & (1 << i);

            ImGui::SameLine();
            if (ImGui::Button("Save"))
                writeMS(moves, numMoves);

            ImGui::InputUByte2("ID/Icon", &moves[moveID].id);

            ImGui::InputText("Name", moves[moveID].name, 19);
            ImGui::InputUShort("Cost", &moves[moveID].cost);
            ImGui::InputUByte("Target Effect", &moves[moveID].targetEffect);
            //ImGui::Combo("Target Effect", (int*)&moves[moveID].targetEffect, targetEffects, 16, -1);
            ImGui::InputUByte("Target Type", &moves[moveID].targetType);
            //ImGui::Combo("Target Type", (int*)&moves[moveID].targetType, targetTypes, 16, -1);

            ImGui::InputUShort2("Strength/Power", &moves[moveID].str);

            ImGui::InputUShort("Range", &moves[moveID].range);

            ImGui::InputUShort2("Cast Time Lv1/Lv5", &moves[moveID].cast1);

            ImGui::InputUShort("Recovery", &moves[moveID].recovery);
            ImGui::InputUShort("Animation", &moves[moveID].animation);
            ImGui::InputUByte("Unknown #1", &moves[moveID].unknown1);
            ImGui::InputUByte("Unknown #2", &moves[moveID].unknown2);

            ImGui::InputShort2("IP Damage/IP Cancel Damage", &moves[moveID].ipDamage);

            ImGui::InputShort("Knockback", &moves[moveID].knockback);
            ImGui::InputUByte("Element", &moves[moveID].element);
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
            ImGui::SameLine();
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

            ImGui::InputByte4("Atk/Def/Act/Mov Modifiers", &moves[moveID].atkMod);

            ImGui::InputUShort("Special", &moves[moveID].special);
            ImGui::InputUShort("Coin Cost Lv1", &moves[moveID].coinCost1);
            ImGui::InputUShort("Coin Cost Lv2", &moves[moveID].coinCost2);
            ImGui::InputUShort("Coin Cost Lv3", &moves[moveID].coinCost3);
            ImGui::InputUShort("Coin Cost Lv4", &moves[moveID].coinCost4);
            ImGui::InputUShort("Coin Cost Lv5", &moves[moveID].coinCost5);
            ImGui::InputUShort("Multiplier", &moves[moveID].multiplier);
            ImGui::InputText("Description", moves[moveID].description, 41);
            */
            
            if (ImGui::Combo("Index", &moveID, moveIDs, (int)numMoves))
                for (size_t i = 0; i < 8; i++)
                    AilmentBitFlags[i] = moves[moveID].ailmentsBitflag & (1 << i);

            ImGui::SameLine();
            if (ImGui::Button("Save"))
                writeMS(moves, numMoves);

            ImGui::LabelText("ID", std::to_string(moves[moveID].id).c_str());
            //ImGui::InputUByte("ID", &moves[moveID].id);
            ImGui::InputUByte("Icon", &moves[moveID].icon);
            ImGui::InputText("Name", moves[moveID].name, 19);
            ImGui::InputUShort("Cost", &moves[moveID].cost);

            //ImGui::InputUByte("Target Effect", &moves[moveID].targetEffect);
            //ImGui::InputUByte("Target Type", &moves[moveID].targetType);

            ImGui::Combo("Target Effect", &moves[moveID].targetEffect, targetEffects, 16, -1);
            ImGui::Combo("Target Type", &moves[moveID].targetType, targetTypes, 16, -1);

            ImGui::InputUShort("Strength", &moves[moveID].str);
            ImGui::InputUShort("Power", &moves[moveID].pow);
            ImGui::InputUShort("Range", &moves[moveID].range);
            ImGui::InputUShort("Cast Time Lv1", &moves[moveID].cast1);
            ImGui::InputUShort("Cast Time Lv5", &moves[moveID].cast5);
            ImGui::InputUShort("Recovery", &moves[moveID].recovery);
            ImGui::InputUShort("Animation", &moves[moveID].animation);
            ImGui::InputUByte("Unknown #1", &moves[moveID].unknown1);
            ImGui::InputUByte("Unknown #2", &moves[moveID].unknown2);
            ImGui::InputShort("IP Damage", &moves[moveID].ipDamage);
            ImGui::InputShort("IP Cancel Damage", &moves[moveID].ipCancelDamage);
            ImGui::InputShort("Knockback", &moves[moveID].knockback);
            ImGui::InputUByte("Element", &moves[moveID].element);
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
            ImGui::InputByte4("Atk/Def/Act/Mov Modifiers", &moves[moveID].atkMod);
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

        {   //SK_PARAM WINDOW

            static ImU16 skillID = 0;

            ImGui::Begin("SK_PARAM");

            ImGui::Combo("Index", &skillID, skillIDs, (int)numSkills); ImGui::SameLine();

                ImGui::SameLine();
            if (ImGui::Button("Save"))
                writeSK(skills, numSkills);

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
            ImGui::InputByte("Base Fire", &skills[skillID].baseFirePercent);
            ImGui::InputByte("Base Wind", &skills[skillID].baseWindPercent);
            ImGui::InputByte("Base Earth", &skills[skillID].baseEarthPercent);
            ImGui::InputByte("Base Lightning", &skills[skillID].baseLightningPercent);
            ImGui::InputByte("Base Blizzard", &skills[skillID].baseBlizzardPercent);
            ImGui::InputByte("Base Water", &skills[skillID].baseWaterPercent);
            ImGui::InputByte("Base Explosion", &skills[skillID].baseExplosionPercent);
            ImGui::InputByte("Base Forest", &skills[skillID].baseForestPercent);
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

        {   //TB_MAGIC WINDOW

            static ImU16 eggID = 0;

            ImGui::Begin("TB_MAGIC");

            ImGui::Combo("Index", &eggID, eggIDs, (int)numEggs); ImGui::SameLine();

            ImGui::SameLine();
            if (ImGui::Button("Save"))
                writeMAG(eggs, numEggs); ImGui::NewLine();

            
            for (size_t i = 0; i < 18; i++) {

                ImGui::Combo("Spell", &eggs[eggID].spells[i].spellOffset, moveIDs, (int)numMoves);
                ImGui::InputUByte("Spell ID", &eggs[eggID].spells[i].spellOffset);
                ImGui::InputUByte("Starting Level", &eggs[eggID].spells[i].startingLevel);
                ImGui::InputUByte("Egg Level Required", &eggs[eggID].spells[i].eggLevelRequired);
                ImGui::InputUByte("Unknown #1", &eggs[eggID].spells[i].unknown1); ImGui::NewLine();

                //ImGui::InputUByte4("Spell ID/Starting Level/Egg Level Required/Unknown #1", &eggs[eggID].spells[i].spellOffset);

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