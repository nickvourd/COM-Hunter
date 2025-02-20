using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter.Packages
{
    internal class Search
    {
        // SearchRegistryLocalMachine method
        public static void SearchRegistryLocalMachine(string hKeyPath)
        {
            string computerVar = "Computer";
            try
            {
                using (var hKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(hKeyPath, false))
                {
                    // Check InprocServer32
                    if (hKey != null)
                    {
                        object defaultValue = hKey.GetValue("");
                        switch (hKeyPath)
                        {
                            case string path when path.Contains("InprocServer32"):
                                Console.WriteLine($"[+] InprocServer32 Found in HKLM");
                                break;

                            case string path when path.Contains("LocalServer32"):
                                Console.WriteLine($"[+] LocalServer32 Found in HKLM");
                                break;

                            default:
                                Console.WriteLine($"[!] Unknown server type found in HKLM");
                                Settings.ExitCodeMethod(Settings.exitCodeError);
                                break;
                        }
                        Console.WriteLine("[+] Registry Key Path: " + computerVar + "\\" + hKey);
                        Console.WriteLine($"[+] Registry Key Value: {defaultValue}\n");
                    }
                    else
                    {
                        if (hKeyPath.Contains("InprocServer32") || hKeyPath.Contains("LocalServer32"))
                        {
                            string serverType = hKeyPath.Contains("InprocServer32") ? "InprocServer32" : "LocalServer32";
                            Console.WriteLine($"[!] {serverType} COM Object NOT Found in HKLM\n");
                            //Settings.ExitCodeMethod(Settings.exitCodeError);
                        }
                        else
                        {
                            Console.WriteLine("[!] Unknown server type found in HKLM");
                            Settings.ExitCodeMethod(Settings.exitCodeError);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Error accessing registry: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        // SearchRegistryCurrentUser method
        public static void SearchRegistryCurrentUser(string hKeyPath)
        {
            string computerVar = "Computer";
            try
            {
                using (var hKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(hKeyPath, false))
                {
                    // Check InprocServer32
                    if (hKey != null)
                    {
                        object defaultValue = hKey.GetValue("");
                        switch (hKeyPath)
                        {
                            case string path when path.Contains("InprocServer32"):
                                Console.WriteLine($"[+] InprocServer32 Found in HKCU");
                                break;

                            case string path when path.Contains("LocalServer32"):
                                Console.WriteLine($"[+] LocalServer32 Found in HKCU");
                                break;

                            default:
                                Console.WriteLine($"[!] Unknown server type found in HKCU");
                                Settings.ExitCodeMethod(Settings.exitCodeError);
                                break;
                        }
                        Console.WriteLine("[+] Registry Key Path: " + computerVar + "\\" + hKey);
                        Console.WriteLine($"[+] Registry Key Value: {defaultValue}\n");
                    }
                    else
                    {
                        if (hKeyPath.Contains("InprocServer32") || hKeyPath.Contains("LocalServer32"))
                        {
                            string serverType = hKeyPath.Contains("InprocServer32") ? "InprocServer32" : "LocalServer32";
                            Console.WriteLine($"[!] {serverType} COM Object NOT Found in HKCU\n");
                            //Settings.ExitCodeMethod(Settings.exitCodeError);
                        }
                        else
                        {
                            Console.WriteLine("[!] Unknown server type found in HKLM");
                            Settings.ExitCodeMethod(Settings.exitCodeError);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Error accessing registry: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }
    }
}
