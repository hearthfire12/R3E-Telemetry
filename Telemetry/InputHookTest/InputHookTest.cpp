// InputHookTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "SendInputSimulator.h"

int main()
{
	HWND hWnd = FindWindow(NULL, L"RaceRoom Racing Experience");
	SetForegroundWindow(hWnd);
	SendInputSimulator::MouseMove(1000, 800);
}