#include <windows.h>
#include "beacon.h"
#include "bofdefs.h"
#include "base.c"

// Server type mode flags (first parameter)
#define COMHUNTER_SERVER_ALL           0  // Both InprocServer32 & LocalServer32
#define COMHUNTER_SERVER_INPROCSERVER  1  // InprocServer32 only
#define COMHUNTER_SERVER_LOCALSERVER   2  // LocalServer32 only

// Hive mode flags (second parameter - optional)
#define COMHUNTER_HIVE_ALL             0  // Both HKLM & HKCU (default)
#define COMHUNTER_HIVE_MACHINE         1  // HKLM only
#define COMHUNTER_HIVE_USER            2  // HKCU only

void BuildRegistryKey(const char* clsid, char* inprocPath, char* localPath, DWORD pathSize)
{
    MSVCRT$_snprintf(inprocPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\InprocServer32", clsid);
    MSVCRT$_snprintf(localPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\LocalServer32", clsid);
}

DWORD SearchRegistry(HKEY hRootKey, const char* hKeyPath, const char* hiveName)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    HKEY hKey = NULL;
    char valueBuffer[MAX_PATH] = {0};
    DWORD valueSize = MAX_PATH;
    DWORD valueType = 0;
    const char* serverType = "Unknown";

    if (MSVCRT$strstr(hKeyPath, "InprocServer32"))
        serverType = "InprocServer32";
    else if (MSVCRT$strstr(hKeyPath, "LocalServer32"))
        serverType = "LocalServer32";

    dwErrorCode = ADVAPI32$RegOpenKeyExA(
        hRootKey, 
        hKeyPath, 
        0, 
        KEY_READ | KEY_WOW64_64KEY, 
        &hKey
    );

    if (dwErrorCode == ERROR_SUCCESS && hKey != NULL)
    {
        internal_printf("[+] %s Found in %s\n", serverType, hiveName);
        internal_printf("[+] Registry Key Path: Computer\\%s\\%s\n", hiveName, hKeyPath);

        dwErrorCode = ADVAPI32$RegQueryValueExA(
            hKey, 
            NULL,
            NULL, 
            &valueType, 
            (LPBYTE)valueBuffer, 
            &valueSize
        );

        if (dwErrorCode == ERROR_SUCCESS)
        {
            internal_printf("[+] Registry Key Value: %s\n\n", valueBuffer);
        }
        else
        {
            internal_printf("[!] Could not read default value: %lX\n\n", dwErrorCode);
        }

        ADVAPI32$RegCloseKey(hKey);
        dwErrorCode = ERROR_SUCCESS;
    }
    else
    {
        internal_printf("[!] %s COM Object NOT Found in %s\n\n", serverType, hiveName);
        dwErrorCode = ERROR_SUCCESS;
    }

    return dwErrorCode;
}

DWORD SearchRegistryLocalMachine(const char* hKeyPath)
{
    return SearchRegistry(HKEY_LOCAL_MACHINE, hKeyPath, "HKLM");
}

DWORD SearchRegistryCurrentUser(const char* hKeyPath)
{
    return SearchRegistry(HKEY_CURRENT_USER, hKeyPath, "HKCU");
}

void SearchBoth(const char* inprocServer, const char* localServer, BOOL searchMachine, BOOL searchUser)
{
    if (inprocServer != NULL)
    {
        if (searchMachine)
            SearchRegistryLocalMachine(inprocServer);
        if (searchUser)
            SearchRegistryCurrentUser(inprocServer);
    }

    if (localServer != NULL)
    {
        if (searchMachine)
            SearchRegistryLocalMachine(localServer);
        if (searchUser)
            SearchRegistryCurrentUser(localServer);
    }
}

DWORD ComHunterSearch(const char* clsid, int serverMode, int hiveMode)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    char inprocPath[512] = {0};
    char localPath[512] = {0};
    const char* inprocPtr = NULL;
    const char* localPtr = NULL;
    BOOL searchMachine = FALSE;
    BOOL searchUser = FALSE;

    BuildRegistryKey(clsid, inprocPath, localPath, sizeof(inprocPath));

    internal_printf("[*] Starting Search Mode...\n");
    internal_printf("[*] CLSID: %s\n", clsid);
    internal_printf("[*] Server Mode: %d\n", serverMode);
    internal_printf("[*] Hive Mode: %d\n\n", hiveMode);

    // Determine which server types to search
    switch (serverMode)
    {
        case COMHUNTER_SERVER_ALL:
            inprocPtr = inprocPath;
            localPtr = localPath;
            break;

        case COMHUNTER_SERVER_INPROCSERVER:
            inprocPtr = inprocPath;
            localPtr = NULL;
            break;

        case COMHUNTER_SERVER_LOCALSERVER:
            inprocPtr = NULL;
            localPtr = localPath;
            break;

        default:
            internal_printf("[!] Invalid server mode: %d\n", serverMode);
            internal_printf("[*] Valid server modes:\n");
            internal_printf("    0 = All (InprocServer32 & LocalServer32)\n");
            internal_printf("    1 = InprocServer32 only\n");
            internal_printf("    2 = LocalServer32 only\n");
            dwErrorCode = ERROR_INVALID_PARAMETER;
            goto ComHunterSearch_end;
    }

    // Determine which hives to search
    switch (hiveMode)
    {
        case COMHUNTER_HIVE_ALL:
            searchMachine = TRUE;
            searchUser = TRUE;
            break;

        case COMHUNTER_HIVE_MACHINE:
            searchMachine = TRUE;
            searchUser = FALSE;
            break;

        case COMHUNTER_HIVE_USER:
            searchMachine = FALSE;
            searchUser = TRUE;
            break;

        default:
            internal_printf("[!] Invalid hive mode: %d\n", hiveMode);
            internal_printf("[*] Valid hive modes:\n");
            internal_printf("    0 = All (HKLM & HKCU)\n");
            internal_printf("    1 = Machine only (HKLM)\n");
            internal_printf("    2 = User only (HKCU)\n");
            dwErrorCode = ERROR_INVALID_PARAMETER;
            goto ComHunterSearch_end;
    }

    SearchBoth(inprocPtr, localPtr, searchMachine, searchUser);

ComHunterSearch_end:
    return dwErrorCode;
}

#ifdef BOF
VOID go( 
    IN PCHAR Buffer, 
    IN ULONG Length 
) 
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    datap parser = {0};
    const char* clsid = NULL;
    int serverMode = 0;
    int hiveMode = 0;

    BeaconDataParse(&parser, Buffer, Length);
    clsid = BeaconDataExtract(&parser, NULL);
    serverMode = BeaconDataInt(&parser);
    hiveMode = BeaconDataInt(&parser);

    if(!bofstart())
    {
        return;
    }

    dwErrorCode = ComHunterSearch(clsid, serverMode, hiveMode);
    if(ERROR_SUCCESS != dwErrorCode)
    {
        BeaconPrintf(CALLBACK_ERROR, "ComHunterSearch failed: %lX\n", dwErrorCode);
        goto go_end;
    }

    internal_printf("SUCCESS.\n");

go_end:
    printoutput(TRUE);
    bofstop();
};
#else
int main(int argc, char** argv)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    const char* clsid = "{00000000-0000-0000-0000-000000000000}";
    int serverMode = COMHUNTER_SERVER_ALL;
    int hiveMode = COMHUNTER_HIVE_ALL;

    if (argc >= 3)
    {
        clsid = argv[1];
        serverMode = atoi(argv[2]);
    }
    if (argc >= 4)
    {
        hiveMode = atoi(argv[3]);
    }

    dwErrorCode = ComHunterSearch(clsid, serverMode, hiveMode);
    if(ERROR_SUCCESS != dwErrorCode)
    {
        BeaconPrintf(CALLBACK_ERROR, "ComHunterSearch failed: %lX\n", dwErrorCode);
        goto main_end;
    }

    internal_printf("SUCCESS.\n");

main_end:
    return dwErrorCode;
}
#endif