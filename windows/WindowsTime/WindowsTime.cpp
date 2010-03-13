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

#define _CRT_SECURE_NO_DEPRECATE

#include <windows.h>
#include <ctime>
#include <cstdio>

using namespace std;

void showmsg(DWORD err) {
    LPVOID lpMsgBuf;
    FormatMessage(
        FORMAT_MESSAGE_ALLOCATE_BUFFER|FORMAT_MESSAGE_FROM_SYSTEM,
        NULL,
        err,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPTSTR) &lpMsgBuf,
        0,
        NULL );
    MessageBox(NULL,(LPCSTR)lpMsgBuf, "Error", MB_OK);
    LocalFree(lpMsgBuf);
}


int main(int argc,char *argv[]) {

    HKEY hKey;
    DWORD installtime;
    DWORD type;
    DWORD size;
    bool found=false;
    DWORD error;

    LPCTSTR datepath="Software\\Microsoft\\Windows NT\\CurrentVersion";
    LPCTSTR item="InstallDate";
    LPCTSTR datepath2="Software\\Microsoft\\Windows\\CurrentVersion";
    LPCTSTR item2="FirstInstallDate";

    long ret;
	ret=RegOpenKeyEx(HKEY_LOCAL_MACHINE,datepath,0,KEY_READ,&hKey);
    if(ret==ERROR_SUCCESS) {
        ret=RegQueryValueEx(hKey,item,NULL,&type,(LPBYTE)&installtime,&size);
        if(ret==ERROR_SUCCESS) {
            found=true;
        }
		RegCloseKey(hKey);
	} 
	else {
		error=GetLastError();
	}
	if (!found) {
		ret=RegOpenKeyEx(HKEY_LOCAL_MACHINE,datepath2,0,KEY_READ,&hKey);
        if(ret==ERROR_SUCCESS) {
			ret=RegQueryValueEx(hKey,item2,NULL,&type,(LPBYTE)&installtime,&size);
			if(ret==ERROR_SUCCESS) {
				found=true;
			};
			RegCloseKey(hKey);
		}
		else {
			error=error||GetLastError();
		}
    }

	bool show_curr_time = true;
	if(show_curr_time) {
		char tmp[128];
		char weekdays[][10]={"Sunday","Monday","Tuesday","Wednesday",
							 "Thursday","Friday","Saturday"};
		SYSTEMTIME systime;
		GetLocalTime(&systime);
		sprintf(tmp,"%4d/%02d/%02d %02d:%02d:%02d.%03d %s\n" 
		        ,systime.wYear,systime.wMonth,systime.wDay 
                ,systime.wHour,systime.wMinute,systime.wSecond,systime.wMilliseconds 
                ,weekdays[systime.wDayOfWeek]);
		MessageBox(NULL,(LPCSTR)tmp, "Current Time", MB_OK);
	}

	if(found) {
        time_t tm;
        tm=installtime;
        char tmp[128];
        strftime(tmp, sizeof(tmp), "%z %Y-%m-%d %X %A", localtime((const time_t*)&tm) );
		MessageBox(NULL,(LPCSTR)tmp, "Install Time", MB_OK);
    }
    else {
		showmsg(error);
    }

	bool show_start_time = true;
	if (show_start_time) {
		DWORD startup = GetTickCount();
		startup/=1000;
		int hour=startup/(60*60);
		startup%=(60*60);
		int min=startup/60;
		int sec=startup%60;
		char tmp[128];
		sprintf(tmp,"Started %d hours, %d minutes, %d seconds",hour,min,sec);
		MessageBox(NULL,(LPCSTR)tmp, "Startup Time", MB_OK);
	}
    return 0;
}
