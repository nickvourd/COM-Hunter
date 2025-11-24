using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    internal class Remove
    {
        public static void RemoveRegistryLocalMachine(string hKeyPath)
        {
            try
            {
                using (var hKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64).OpenSubKey(hKeyPath, true))
                {
                    if (hKey != null)
                    {
                        hKey.DeleteValue("");
                        Console.WriteLine($"[+] Successfully removed registry value in HKLM:\\{hKeyPath}\n");
                    }
                    else
                    {
                        Console.WriteLine($"[!] Registry key not found in HKLM:\\{hKeyPath}\n");
                        Settings.ExitCodeMethod(Settings.exitCodeError);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[!] Error removing registry value: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        public static void RemoveRegistryCurrentUser(string hKeyPath)
        {
            try
            {
                using (var hKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry64).OpenSubKey(hKeyPath, true))
                {
                    if (hKey != null)
                    {
                        hKey.DeleteValue("");
                        Console.WriteLine($"[+] Successfully removed registry value in HKCU:\\{hKeyPath}\n");
                    }
                    else
                    {
                        Console.WriteLine($"[!] Registry key not found in HKCU:\\{hKeyPath}\n");
                        Settings.ExitCodeMethod(Settings.exitCodeError);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[!] Error removing registry value: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }


    }
}
