using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace COM_Hunter
{
    public class Search
    {
        private List<string> foundKeyListLM = new List<string>();
        private List<string> foundKeyListCU = new List<string>();
        private string foundKeyLM { get; set; }
        private string foundKeyCU { get; set; }
        private string category { get; set; }
        private int counter { get; set; }
        private bool browserExists { get; set; }

        public void Searcher(string flag)
        {
            switch (flag)
            {
                case Settings.Entry:
                    Entry();
                    break;
                case Settings.TaskSch:
                    TaskSch();
                    break;
                case Settings.FindPersist:
                    FindPersist();
                    break;
                case Settings.FindTaskSch:
                    FindTaskSch();
                    break;
                default:
                    Default();
                    break;
            }
        }

        private void Entry()
        {
            Console.WriteLine("\n[+] Searching interesting CLSIDs...\n");
            category = "General";
            //Call method SearchRegistryLocalMachine
            SearchRegistryLocalMachine(Storage.DllHost, "DllHost", category);
            
            //Call Method BrowserInstalled
            browserExists = Applications.BrowserInstalled("Chrome");
            if (browserExists != false)
            {
                SearchRegistryLocalMachine(Storage.Chrome, "Chrome", category);
            }

            //Call Method BrowserInstalled
            browserExists = Applications.BrowserInstalled("Firefox");
            if (browserExists != false)
            {
                SearchRegistryLocalMachine(Storage.Firefox, "Firefox", category);
            }

            //Call method SearchRegistryLocalMachine
            SearchRegistryLocalMachine(Storage.SvcHost, "SvcHost", category);
            SearchRegistryLocalMachine(Storage.Pwsh, "PowerShell", category);
            SearchRegistryLocalMachine(Storage.IExplore, "IExplorer", category);
            SearchRegistryLocalMachine(Storage.Explorer, "Explorer", category);
            
            if (counter == 7)
            {
                //Show Not found message
                Info.NotFoundMsg(category);
            }
        }

        private void TaskSch()
        {
            category = "TaskSch";
            Console.WriteLine("\n[+] Searching interesting CLSIDs for Task Scheduler...\n");
            //Call method SearchRegistryLocalMachine
            SearchRegistryLocalMachine(Storage.Tasksch, "Task Scheduler", category);
            if (counter > 1)
            {
                //Show Not found message
                Info.NotFoundMsg(category);
            }
        }

        private void FindPersist()
        {
            Console.WriteLine("\n[+] Searching if someone already used any interesting CLSIDs...\n");
            category = "General";
            //Call method SearchRegistryCurrentUser
            SearchRegistryCurrentUser(Storage.DllHost, "DllHost", category);
            SearchRegistryCurrentUser(Storage.Chrome, "Chrome", category);
            SearchRegistryCurrentUser(Storage.Firefox, "Firefox", category);
            SearchRegistryCurrentUser(Storage.SvcHost, "SvcHost", category);
            SearchRegistryCurrentUser(Storage.Pwsh, "PowerShell", category);
            SearchRegistryCurrentUser(Storage.IExplore, "IExplorer", category);
            SearchRegistryCurrentUser(Storage.Explorer, "Explorer", category);

            if (counter == 7)
            {
                //Show Not found message
                Info.NotFoundMsg2();
            }
        }

        private void FindTaskSch()
        {
            Console.WriteLine("\n[+] Searching if someone already used any interesting CLSIDs via Task Scheduler...\n");
            category = "TaskSch";
            //Call method SearchRegistryCurrentUser
            SearchRegistryCurrentUser(Storage.TaskschUsed, "Task Scheduler", category);

            if (counter > 0)
            {
                //Show Not found message
                Info.NotFoundMsg2();
            }
        }

        private void Default()
        {
            Info.ErrorMsg();
            Settings.ExitCodeMethodError();
        }

        private void SearchRegistryLocalMachine(List<string> registryKeys, string outputName, string category)
        {
            //Try to find if CLSIDs exists in HKLM
            foundKeyListLM = new List<string>();
            foreach (string registryKey in registryKeys)
            {
                
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryKey, false);

                if (key != null)
                {
                    if (category != "General")
                    {
                        foundKeyLM = key + "\\" + Settings.KeyValue;
                    }
                    else
                    {
                        foundKeyLM = key.ToString();
                    }
                    foundKeyListLM.Add(foundKeyLM);
                    key.Close();  
                }
            }
            //If found any usefull CLSID show them
            if (foundKeyListLM.Count > 0)
            {
                Console.Write("\n[+] " + outputName + ":\n\n");
                foreach (string key in foundKeyListLM)
                {
                    Console.WriteLine(key);
                }
                Console.WriteLine("\n");

            }
            else
            {
                counter++;
            }
        }

        private void SearchRegistryCurrentUser(List<string> registryKeys, string outputName, string category)
        {
            //Try to find if CLSIDs exists in HKCU
            foundKeyListCU = new List<string>();
            foreach (string registryKey in registryKeys)
            {
                //Set registry key
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(registryKey, false);
                //If it does exist, retrieve the stored values
                if (key != null)
                {
                    foundKeyCU = key.ToString();
                    foundKeyListCU.Add(foundKeyCU);
                    key.Close();
                }
            }

            //If found any usefull CLSID show them
            if (foundKeyListCU.Count > 0)
            {
                Console.Write("\n[+] " + outputName + ":\n\n");
                foreach (string key in foundKeyListCU)
                {

                    Console.WriteLine(key);
                }
                Console.WriteLine("\n");
            }
            else
            {
                counter++;
            }
        }

    }
}