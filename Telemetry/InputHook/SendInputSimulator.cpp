#pragma once

#include "SendInputSimulator.h"

extern "C"
{
	__declspec(dllexport) void SendInputSimulator::KeyPress(unsigned short hexKey)
	{
		KeyDown(hexKey);
		Sleep(50);
		KeyUp(hexKey);
	}

	__declspec(dllexport) void SendInputSimulator::KeyDown(unsigned short hexKey)
	{
		INPUT ip = ConstructKeyboardInput(hexKey, 0);
		SendInput(1, &ip, sizeof(INPUT));
	}

	__declspec(dllexport) void SendInputSimulator::KeyUp(unsigned short hexKey)
	{
		INPUT ip = ConstructKeyboardInput(hexKey, KEYEVENTF_KEYUP);
		SendInput(1, &ip, sizeof(INPUT));
	}

	__declspec(dllexport) void SendInputSimulator::MouseMove(long x, long y)
	{
		INPUT ip = ConstructMouseInput(x, y, true);
		SendInput(1, &ip, sizeof(INPUT));
	}

}

INPUT SendInputSimulator::ConstructKeyboardInput(unsigned short hexKey, unsigned long keyeventf)
{
	INPUT ip;

	ip.type = INPUT_KEYBOARD;
	ip.ki.wScan = hexKey;
	ip.ki.dwFlags = keyeventf;
	ip.ki.wVk = 0;
	ip.ki.time = 0;
	ip.ki.dwExtraInfo = 0;

	return ip;
}

#define SCREEN_WIDTH 1920
#define SCREEN_HEIGHT 1080

INPUT SendInputSimulator::ConstructMouseInput(long x, long y, bool absolute)
{
	INPUT ip;

	ip.type = INPUT_MOUSE;
	ip.mi.dx = x * 0xFFFF / SCREEN_WIDTH + 1;
	ip.mi.dy = y * 0xFFFF / SCREEN_HEIGHT + 1;
	ip.mi.dwFlags = absolute ? MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE : MOUSEEVENTF_MOVE;
	ip.mi.mouseData = 0;
	ip.mi.dwExtraInfo = 0;
	ip.mi.time = 0;

	return ip;
}

