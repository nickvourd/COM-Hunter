#include "bofdefs.h"

// SearchRegistryLocalMachine method
void SearchRegistryLocalMachine(char* hKeyPath){
    char computerVar[] = "Computer";
    HKEY hKey = NULL;
    LSTATUS lRes;
    char szValue[1024];
    DWORD dwSize = sizeof(szValue);
    DWORD dwType = REG_SZ;

    lRes = ADVAPI32$RegOpenKeyExA(HKEY_LOCAL_MACHINE, hKeyPath, 0, KEY_READ, &hKey);
    if (lRes == ERROR_SUCCESS) {
        // Read the default value from the key
        lRes = ADVAPI32$RegQueryValueExA(hKey, "", NULL, &dwType, (LPBYTE)szValue, &dwSize);
        // Check if the key contains InprocServer32 or LocalServer32
        if (strstr(hKeyPath, "InprocServer32") != NULL) {
            BeaconPrintf(CALLBACK_OUTPUT, "[+] InprocServer32 Found in HKLM\n");
        } else if (strstr(hKeyPath, "LocalServer32") != NULL) {
            BeaconPrintf(CALLBACK_OUTPUT, "[+] LocalServer32 Found in HKLM\n");
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Unknown server type found in HKLM\n");
            goto cleanup;
        }
        if(lRes == ERROR_SUCCESS){
            BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Path: %s\\%s\n", computerVar, hKeyPath);
            BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Value: %s\n\n", szValue);
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Failed to read registry value. Error code: %d\n", lRes);
            goto cleanup;
        }
        
    } else {
        if (strstr(hKeyPath, "InprocServer32") != NULL || strstr(hKeyPath, "LocalServer32") != NULL) {
            char* serverType = strstr(hKeyPath, "InprocServer32") != NULL ? "InprocServer32" : "LocalServer32";
            BeaconPrintf(CALLBACK_ERROR, "[-] %s COM Object NOT Found in HKLM\n\n", serverType);
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Unknown server type found in HKLM\n");
        }
    }

    cleanup:
    if(hKey != NULL){
        ADVAPI32$RegCloseKey(hKey);
    }
}

// SearchRegistryCurrentUser method
void SearchRegistryCurrentUser(char* hKeyPath){
    char computerVar[] = "Computer";
    HKEY hKey = NULL;
    LSTATUS lRes;
    char szValue[1024];
    DWORD dwSize = sizeof(szValue);
    DWORD dwType = REG_SZ;

    lRes = ADVAPI32$RegOpenKeyExA(HKEY_CURRENT_USER, hKeyPath, 0, KEY_READ | KEY_WOW64_64KEY, &hKey);
    if (lRes == ERROR_SUCCESS) {
        // Read the default value from the key
        lRes = ADVAPI32$RegQueryValueExA(hKey, "", NULL, &dwType, (LPBYTE)szValue, &dwSize);
        // Check if the key contains InprocServer32 or LocalServer32
        if (strstr(hKeyPath, "InprocServer32") != NULL) {
            BeaconPrintf(CALLBACK_OUTPUT, "[+] InprocServer32 Found in HKCU\n");
        } else if (strstr(hKeyPath, "LocalServer32") != NULL) {
            BeaconPrintf(CALLBACK_OUTPUT, "[+] LocalServer32 Found in HKCU\n");
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Unknown server type found in HKCU\n");
            goto cleanup;
        }
        if(lRes == ERROR_SUCCESS){
            BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Path: %s\\%s\n", computerVar, hKeyPath);
            BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Value: %s\n\n", szValue);
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Failed to read registry value. Error code: %d\n", lRes);
            goto cleanup;
        }
        
    } else {
        if (strstr(hKeyPath, "InprocServer32") != NULL || strstr(hKeyPath, "LocalServer32") != NULL) {
            char* serverType = strstr(hKeyPath, "InprocServer32") != NULL ? "InprocServer32" : "LocalServer32";
            BeaconPrintf(CALLBACK_ERROR, "[-] %s COM Object NOT Found in HKCU\n\n", serverType);
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Unknown server type found in HKCU\n");
        }
    }

    cleanup:
    if(hKey != NULL){
        ADVAPI32$RegCloseKey(hKey);
    }
}