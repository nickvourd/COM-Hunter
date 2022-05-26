using System;

namespace COM_Hunter
{
    internal class Info
    {
        public static void ShowLogo()
        {

            var logo = @"


                _____ ____  __  __        _    _             _            
               / ____/ __ \|  \/  |      | |  | |           | |           
              | |   | |  | | \  / |______| |__| |_   _ _ __ | |_ ___ _ __ 
              | |   | |  | | |\/| |______|  __  | | | | '_ \| __/ _ \ '__|
              | |___| |__| | |  | |      | |  | | |_| | | | | ||  __/ |   
               \_____\____/|_|  |_|      |_|  |_|\__,_|_| |_|\__\___|_|   
                                                             

            ";

            Console.Write(logo);
            Console.Write("                       Version: {0}\r\n                                 Author: {1}\r\n\n                  {2}\r\n\n", Settings.Version, Settings.Author, Settings.Inspiration);
        }

        public static void ShowUsage()
        {
            var usage = @"
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
    ";
            Console.WriteLine(usage);
        }

        public static void HelpMsg()
        {
            Console.WriteLine("\n[!] Use -h, --help to see valid options!\n");
        }

        public static void About()
        {
            Console.WriteLine("\n[+] COM Hunter is automated COM Hijacking persistence tool written in C#.\n\n[+] Special Thanks:\n\n@dimtsikopoulos\n@0xvm\n\n[+] More Info here: https://github.com/nickvourd/COM-Hunter\n[+] Follow me on Twitter: @nickvourd\n[+] Personal blog: nickvourd.eu\n\n");
        }

        public static void ErrorMsg()
        {
            Console.WriteLine("\n[!] An error occurred!\n");
        }
        
        public static void ErrorMsg2()
        {
            Console.WriteLine("\n[!] This CLSID is not valid!\n");
        }

        public static void ErrorMsg3()
        {
            Console.WriteLine("\n[!] This CLSID already exists in HKCU!\n");
        }

        public static void ErrorMsgArch()
        {
            Console.WriteLine("\n[!] 32-bit Systems not supported!");
        }

        public static void NotFoundMsg(string category)
        {
            Console.WriteLine("\n[!] Can't find any intersting CLSID for " + category + "!\n");
        }

        public static void NotFoundMsg2()
        {
            Console.WriteLine("\n[!] Nothing suspect found here! Your System appears to be COM persistence free...\n");
        }

        public static void SuccessMsg()
        {
            Console.WriteLine("\n[+] New CLSID created successful in the HKCU with evil payload...\n");
            Console.WriteLine("[+] You have COM Persistence in this system :)\n");
        }
    }
}
