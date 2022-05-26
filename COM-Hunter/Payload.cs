using System.Collections.Generic;
using System.IO;


namespace COM_Hunter
{
    public class Payload
    {
        private static List<string> ArgumentsList = new List<string>();

        public List<string> FileChecker(string argument, string argument2)
        {
            string argumentToLow = argument.ToLower();
            string argument2ToLow = argument2.ToLower();
            if (argumentToLow.Contains(".dll") && argumentToLow.Contains(":"))
            {
                if (argument2ToLow.Contains("{") && argument2ToLow.Contains("}") && argument2ToLow.Contains("clsid") && argument2ToLow.Contains("class"))
                {
                    ArgumentsList.Add(argument);
                    ArgumentsList.Add(argument2);
                }
                else
                {
                    Info.ErrorMsg();
                    Settings.ExitCodeMethodError();
                }
            }
            else if (argumentToLow.Contains("{") && argumentToLow.Contains("}") && argumentToLow.Contains("clsid") && argumentToLow.Contains("class"))
            {
                if (argument2ToLow.Contains(".dll") && argument2ToLow.Contains(":"))
                {
                    ArgumentsList.Add(argument2);
                    ArgumentsList.Add(argument);
                }
                else
                {
                    Info.ErrorMsg();
                    Settings.ExitCodeMethodError();
                }
            }
            else
            {
                Info.ErrorMsg();
                Settings.ExitCodeMethodError();
            }
            return ArgumentsList;
        }

        public string ClsidChecker(string clsid)
        {
            clsid = clsid.ToUpper();
            if (clsid.Contains("HKCU") || clsid.Contains("HKEY_CURRENT_USER"))
            {
                if (clsid.StartsWith("HKCU:\\"))
                {
                    clsid = clsid.Replace("HKCU:\\", "");
                }
                else if (clsid.StartsWith("HKCU:"))
                {
                    clsid = clsid.Replace("HKCU:", "");
                }
                else if (clsid.StartsWith("HKCU\\"))
                {
                    clsid = clsid.Replace("HKCU\\", "");
                }
                else if (clsid.StartsWith("HKEY_CURRENT_USER:\\"))
                {
                    clsid = clsid.Replace("HKEY_CURRENT_USER:\\", "");
                }
                else if (clsid.StartsWith("HKEY_CURRENT_USER:"))
                {
                    clsid = clsid.Replace("HKEY_CURRENT_USER:", "");
                }
                else if (clsid.StartsWith("HKEY_CURRENT_USER\\"))
                {
                    clsid = clsid.Replace("HKEY_CURRENT_USER\\", "");
                }
                else
                {
                    Info.ErrorMsg();
                    Settings.ExitCodeMethodError();
                }
            }
            else if (clsid.StartsWith("SOFTWARE\\"))
            {
                clsid = clsid;
            }
            else
            {
                Info.ErrorMsg();
                Settings.ExitCodeMethodError();
            }

            return clsid;
        }

        public void FileTypeChecker(string file)
        {
            FileAttributes attr = File.GetAttributes(file);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Info.ErrorMsg();
                Settings.ExitCodeMethodError();
            }
        }
    }
}
