using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    internal class Info
    {
        // ShowLogo method
        public static void ShowLogo()
        {
            string logo = @"
              
 ██████╗ ██████╗ ███╗   ███╗      ██╗  ██╗██╗   ██╗███╗   ██╗████████╗███████╗██████╗ 
██╔════╝██╔═══██╗████╗ ████║      ██║  ██║██║   ██║████╗  ██║╚══██╔══╝██╔════╝██╔══██╗
██║     ██║   ██║██╔████╔██║█████╗███████║██║   ██║██╔██╗ ██║   ██║   █████╗  ██████╔╝
██║     ██║   ██║██║╚██╔╝██║╚════╝██╔══██║██║   ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗
╚██████╗╚██████╔╝██║ ╚═╝ ██║      ██║  ██║╚██████╔╝██║ ╚████║   ██║   ███████╗██║  ██║
 ╚═════╝ ╚═════╝ ╚═╝     ╚═╝      ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                                                      
                                   Version: {0}
                             {1} && {2}
                  {3}
    ";

            Console.WriteLine(logo, Settings.version, Settings.authors[0], Settings.authors[1], Settings.quote);
        }

        // ShowUsage method
        public static void ShowUsage()
        {
            var usage = @"
Usage: COM_Hunter.exe <mode> <options>

[+] Available Modes:
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

[+] TreatAs Mode:
Usage:  COM-Hunter.exe treatas <CLSID> <fake_CLSID> <binary_path> <option>
    -i, --inprocserver32        Set DLL implementation
    -l, --localserver32         Set EXE implementation
            ";

            Console.WriteLine(usage);
        }

        // SuccessMessage method
        public static void SuccessMessage()
        {
            Console.WriteLine("[+] Operation completed successfully!");
        }

    }
}
