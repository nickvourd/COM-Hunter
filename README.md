# COM-Hunter

COM Hijacking VOODOO

<p align="center">
  <img width="500" height="400" src="/Pictures/logo2.png"><br /><br />
  <img alt="GitHub License" src="https://img.shields.io/github/license/nickvourd/COM-Hunter?style=social&logo=GitHub&logoColor=purple">
  <img alt="GitHub Repo stars" src="https://img.shields.io/github/stars/nickvourd/COM-Hunter?logoColor=yellow">
  <img alt="GitHub forks" src="https://img.shields.io/github/forks/nickvourd/COM-Hunter?logoColor=red">
  <img alt="GitHub watchers" src="https://img.shields.io/github/watchers/nickvourd/COM-Hunter?logoColor=blue">
  <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/nickvourd/COM-Hunter?style=social&logo=GitHub&logoColor=green">
</p>

## Description

COM-Hunter is a COM Hijacking persistnce tool written in C#.

![Static Badge](https://img.shields.io/badge/.NET-4.8-blue?style=flat&logoSize=auto)

The following list explains the available modes:

- **Search Mode**: Searches for CLSIDs based on `InprocServer32`, `LocalServer32`, and registry hives `HKLM` and `HKCU`.
- **Classic Persist Mode**: Performs classic COM hijacking persistence using `LocalServer32` or `InprocServer32`.
- **Task Scheduler Mode**: Automatically establishes COM hijacking persistence via Task Scheduler using `LocalServer32` or `InprocServer32`.

This project created with :heart: by [@nickvourd](https://x.com/nickvourd) && [@S1ckB0y1337](https://x.com/S1ckB0y1337)

:information_source: Inspired by the [RTO course](https://courses.zeropointsecurity.co.uk/courses/red-team-ops) from [@zeropointsecltd](https://x.com/zeropointsecltd).

## Table of Contents

- [COM-Hunter](#com-hunter)
    - [Description](#description)
    - [Table of Contents](#table-of-contents)
    - [Usage](#usage)

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
Usage:  COM-Hunter.exe persist <CLSID> <binary_path> <options>
    -i, --inprocserver32        Set DLL implementation
    -l, --localserver32         Set EXE implementation

[+] Task Scheduler Mode:
Usage:  COM-Hunter.exe tasksch <binary_path> <option>
    -i, --inprocserver32        Set DLL implementation
    -l, --localserver32         Set EXE implementation
```