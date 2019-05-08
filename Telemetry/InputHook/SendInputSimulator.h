#pragma once
#include <windows.h>

static class SendInputSimulator
{
public:
	__declspec(dllexport) static void KeyPress(unsigned short hexKey);
	__declspec(dllexport) static void KeyDown(unsigned short hexKey);
	__declspec(dllexport) static void KeyUp(unsigned short hexKey);
	__declspec(dllexport) static void MouseMove(long x, long y);

private:
	static INPUT ConstructKeyboardInput(unsigned short hexKey, unsigned long keyeventf);
	static INPUT ConstructMouseInput(long x, long y, bool absolute);
};