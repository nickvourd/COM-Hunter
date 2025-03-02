using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    internal class Check
    {
        // CheckExtension method
        public static void CheckExtension(string filePath, string serverType)
        {
            try
            {
                string extension = Path.GetExtension(filePath)?.ToLower();

                switch (serverType)
                {
                    case "InprocServer32":
                        if (extension != ".dll")
                        {
                            Console.WriteLine($"[!] Wrong file extension for InprocServer32. Expected .dll, got {extension}\n");
                            Settings.ExitCodeMethod(Settings.exitCodeError);
                        }
                        break;
                    case "LocalServer32":
                        if (extension != ".exe")
                        {
                            Console.WriteLine($"[!] Wrong file extension for LocalServer32. Expected .exe, got {extension}\n");
                            Settings.ExitCodeMethod(Settings.exitCodeError);
                        }
                        break;
                    default:
                        Console.WriteLine($"[!] Invalid server type: {serverType}\n");
                        Settings.ExitCodeMethod(Settings.exitCodeError);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[!] Error checking file extension: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        // CheckCLSIDFormat method
        public static bool CheckCLSIDFormat(string input)
        {
            // Check if the input is null or empty
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // Remove any curly braces if present
            string cleanInput = input.Trim().Replace("{", "").Replace("}", "");

            // Define the CLSID pattern: 8-4-4-4-12 hexadecimal characters
            // Example: 01575CFE-9A55-4003-A5E1-F38D1EBDCBE1
            string pattern = @"^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$";

            // Check if the input matches the pattern
            bool isValid = System.Text.RegularExpressions.Regex.IsMatch(cleanInput, pattern);

            return isValid;
        }
    }
}
