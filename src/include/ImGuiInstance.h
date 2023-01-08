#pragma once

#include <Windows.h>
#include <d3d12.h>
#include <dxgi1_4.h>

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

struct FrameContext
{
    ID3D12CommandAllocator* CommandAllocator;
    UINT64                  FenceValue;
};

static int const                    NUM_FRAMES_IN_FLIGHT = 3;
static FrameContext                 g_frameContext[NUM_FRAMES_IN_FLIGHT] = {};
static UINT                         g_frameIndex = 0;

static int const                    NUM_BACK_BUFFERS = 3;
static ID3D12Device* g_pd3dDevice = NULL;
static ID3D12DescriptorHeap* g_pd3dRtvDescHeap = NULL;
static ID3D12DescriptorHeap* g_pd3dSrvDescHeap = NULL;
static ID3D12CommandQueue* g_pd3dCommandQueue = NULL;
static ID3D12GraphicsCommandList* g_pd3dCommandList = NULL;
static ID3D12Fence* g_fence = NULL;
static HANDLE                       g_fenceEvent = NULL;
static UINT64                       g_fenceLastSignaledValue = 0;
static IDXGISwapChain3* g_pSwapChain = NULL;
static HANDLE                       g_hSwapChainWaitableObject = NULL;
static ID3D12Resource* g_mainRenderTargetResource[NUM_BACK_BUFFERS] = {};
static D3D12_CPU_DESCRIPTOR_HANDLE  g_mainRenderTargetDescriptor[NUM_BACK_BUFFERS] = {};

bool StartImGui();
void EndImGui();
void StartFrame();
void EndFrame();
void DrawMenuBar(_In_ LPTSTR lpCmdLine, MovesClass* moves, ManaEggsClass* manaeggs, SkillsClass* skills, SkillBooksClass* skillbooks, SpecialsClass* specials, ItemsClass* items, StartStatsClass* startStats, EnemiesClass* enemies, MdtsClass* mdts, LevelupClass* levelups);

bool CreateDeviceD3D(HWND hWnd);
void CleanupDeviceD3D();
void CreateRenderTarget();
void CleanupRenderTarget();
void WaitForLastSubmittedFrame();
FrameContext* WaitForNextFrameResources();
void ResizeSwapChain(HWND hWnd, int width, int height);
LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);