# COM-Hunter

COM Hijacking VOODOO

<p align="center">
  <img width="500" height="400" src="/Pictures/logo2.png"><br /><br />
  <img alt="Static Badge" src="https://img.shields.io/badge/License-MIT-green?link=https%3A%2F%2Fgithub.com%2Fnickvourd%2FCOM-Hunter%2Fblob%2Fmain%2FLICENSE">
  <img alt="GitHub Repo stars" src="https://img.shields.io/github/stars/nickvourd/COM-Hunter?logoColor=yellow">
  <img alt="GitHub forks" src="https://img.shields.io/github/forks/nickvourd/COM-Hunter?logoColor=red">
  <img alt="GitHub watchers" src="https://img.shields.io/github/watchers/nickvourd/COM-Hunter?logoColor=blue">
  <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/nickvourd/COM-Hunter?style=social&logo=GitHub&logoColor=green">
</p>

## Description

COM-Hunter is a COM Hijacking persistnce tool written in C#.

The following list explains the available modes:

- **Search Mode**: Searches for CLSIDs based on `InprocServer32`, `LocalServer32`, and registry hives `HKLM` and `HKCU`.
- **Classic Persist Mode**: Performs classic COM hijacking persistence using `LocalServer32` or `InprocServer32`.
- **Task Scheduler Mode**: Automatically establishes COM hijacking persistence via Task Scheduler using `LocalServer32` or `InprocServer32`.

This project created with :heart: by [@nickvourd](https://x.com/nickvourd) && [@S1ckB0y1337](https://x.com/S1ckB0y1337)

:information_source: Inspired by the [RTO course](https://courses.zeropointsecurity.co.uk/courses/red-team-ops) from [@zeropointsecltd](https://x.com/zeropointsecltd).s

## Table of Contents

- [COM-Hunter](#com-hunter)
    - [Description](#description)
    - [Table of Contents](#table-of-contents)
