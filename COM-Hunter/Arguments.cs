using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    internal class Arguments
    {
        public Arguments(List<string> arguments) {

            switch (arguments.Count)
            {
                case 3:
                    string command = arguments.ElementAt(0);
                    string path = arguments.ElementAt(1);
                    string option = arguments.ElementAt(2);
                    const string taskSchClsid = "{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}";

                    // Check if the argument is tasksch
                    if (command.ToLower() == "tasksch")
                    {
                        // Call method named BuildRegistryKey
                        var (inprocServer32, localServer32) = Build.BuildRegistryKey(taskSchClsid);

                        Console.WriteLine("[*] Starting Task Scheduler Mode...\n");

                        switch (option.ToLower())
                        {
                            case "-i":
                            case "--inprocserver32":
                                // Call method named CheckExtension
                                Check.CheckExtension(path, "InprocServer32");

                                Console.WriteLine("[+] Setting InprocServer32...\n");

                                // Call method named CreateRegistryCU
                                Build.CreateRegistryCU(inprocServer32, path);
                                break;
                            case "-l":
                            case "--localserver32":
                                // Call method named CheckExtension
                                Check.CheckExtension(path, "LocalServer32");

                                Console.WriteLine("[+] Setting LocalServer32...\n");

                                // Call method named CreateRegistryCU
                                Build.CreateRegistryCU(localServer32, path);
                                break;
                            default:
                                Info.ShowUsage();
                                Settings.ExitCodeMethod(Settings.exitCodeError);
                                break;
                        }
                    }else if(command.ToLower() == "search")
                    {
                        // Call method named BuildRegistryKey
                        var (inprocServer32, localServer32) = Build.BuildRegistryKey(path);

                        Console.WriteLine("[*] Starting Search Mode...\n");

                        switch (option.ToLower())
                        {
                            case "-a":
                            case "--all":
                                // Call method named SearchRegistryLocalMachine
                                Search.SearchRegistryLocalMachine(inprocServer32);
                                Search.SearchRegistryLocalMachine(localServer32);

                                // Call method named SearchRegistryCurrentUser
                                Search.SearchRegistryCurrentUser(inprocServer32);
                                Search.SearchRegistryCurrentUser(localServer32);
                                break;
                            case "-i":
                            case "--inprocserver32":
                                // Call method named SearchRegistryLocalMachine
                                Search.SearchRegistryLocalMachine(inprocServer32);

                                // Call method named SearchRegistryCurrentUser
                                Search.SearchRegistryCurrentUser(inprocServer32);
                                break;
                            case "-l":
                            case "--localserver32":
                                // Call method named SearchRegistryLocalMachine
                                Search.SearchRegistryLocalMachine(localServer32);

                                // Call method named SearchRegistryCurrentUser
                                Search.SearchRegistryCurrentUser(localServer32);
                                break;
                            case "-m":
                            case "--machine":
                                // Call method named SearchRegistryLocalMachine
                                Search.SearchRegistryLocalMachine(inprocServer32);
                                Search.SearchRegistryLocalMachine(localServer32);
                                break;
                            case "-u":
                            case "--user":
                                // Call method named SearchRegistryCurrentUser
                                Search.SearchRegistryCurrentUser(inprocServer32);
                                Search.SearchRegistryCurrentUser(localServer32);
                                break;
                            default:
                                Info.ShowUsage();
                                Settings.ExitCodeMethod(Settings.exitCodeError);
                                break;
                        }
                    }
                    else
                    {
                        Info.ShowUsage();
                        Settings.ExitCodeMethod(Settings.exitCodeError);
                    }
                    break;
                case 4:
                    string command2 = arguments.ElementAt(0);
                    string clsid2 = arguments.ElementAt(1);
                    string path2 = arguments.ElementAt(2);
                    string option2 = arguments.ElementAt(3);

                    // Check if the argument is persist
                    if (command2.ToLower() == "persist")
                    {   
                        // Build the registry key
                        var (inprocServer32, localServer32) = Build.BuildRegistryKey(clsid2);

                        Console.WriteLine("[*] Starting Classic Persist Mode...\n");
                        switch (option2.ToLower())
                        {
                            case "-i":
                            case "--inprocserver32":
                                // Call method named CheckExtension
                                Check.CheckExtension(path2, "InprocServer32");

                                Console.WriteLine("[*] Building HKCU key with InprocServer32...\n");

                                // Call method named CreateRegistryCU
                                Build.CreateRegistryCU(inprocServer32, path2);
                                break;
                            case "-l":
                            case "--localserver32":
                                // Call method named CheckExtension
                                Check.CheckExtension(path2, "LocalServer32");

                                Console.WriteLine("[*] Building HKCU key with LocalServer32...\n");

                                // Call method named CreateRegistryCU
                                Build.CreateRegistryCU(localServer32, path2);
                                break;
                            default:
                                Info.ShowUsage();
                                Settings.ExitCodeMethod(Settings.exitCodeError);
                                break;
                        }
                    }
                    else
                    {
                        Info.ShowUsage();
                        Settings.ExitCodeMethod(Settings.exitCodeError);
                    }
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }
    }
}
