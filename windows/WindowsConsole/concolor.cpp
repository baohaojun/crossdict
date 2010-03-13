/**
Copyright 2009 http://code.google.com/p/toolkits/. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:
  * Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above
    copyright notice, this list of conditions and the following
    disclaimer in the documentation and/or other materials provided
    with the distribution.
  * Neither the name of http://code.google.com/p/toolkits/ nor the names of its
    contributors may be used to endorse or promote products derived
    from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**/

/*
Colorful Console
Read http://linxh.appspot.com/blog/get/20 for more information
*/

#include <windows.h>
#include <string>
#include <ctime>

enum Colors {
    BLACK = 0,
    BLUE = 1,
    DARK_GREEN = 2,
    LIGHT_BLUE = 3,
    RED = 4,
    PURPLE = 5,
    ORANGE = 6,
    GREY = 7,
    DARKER_GREY = 8,
    MEDIUM_BLUE = 9,
    LIGHT_GREEN = 10,
    TEAL = 11,
    RED_ORANGE = 12,
    LIGHT_PURPLE = 13,
    YELLOW = 14,
    WHITE = 15
};

void set_cursor(short x, short y) {
    COORD point = {x, y};
    ::SetConsoleCursorPosition(::GetStdHandle(STD_OUTPUT_HANDLE), point);
}

void set_color(WORD color) {
    ::SetConsoleTextAttribute(::GetStdHandle(STD_OUTPUT_HANDLE), color);
}

void delay(unsigned int delay) {
    ::Sleep(delay);
}

void set_title(std::string title) {
    ::SetConsoleTitle(title.c_str());
}

void show_cursor(bool show, int size = 25) {
    CONSOLE_CURSOR_INFO cci;
    if (size <= 0) size = 1;
    if (size > 100) size = 100;
    cci.dwSize = size;
    cci.bVisible = show;
    ::SetConsoleCursorInfo(::GetStdHandle(STD_OUTPUT_HANDLE), &cci);
}

void clear_screen() {
    system("cls");
}

int main()  {
	std::string title("Colorful Console");
	set_title(title);
	WORD color=FOREGROUND_GREEN|BACKGROUND_BLUE;
	set_color(color);
	delay(3000);
	return 0;
}