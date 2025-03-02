using System;
using System.Collections.Generic;
using System.Linq;

namespace COM_Hunter
{
    internal class Arguments
    {
        private const string TaskSchClsid = "{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}";

        public Arguments(List<string> arguments)
        {
            if (arguments.Count < 3 || arguments.Count > 5)
            {
                Info.ShowUsage();
                Settings.ExitCodeMethod(Settings.exitCodeError);
                //return;
            }

            string command = arguments[0].ToLower();

            switch (arguments.Count)
            {
                case 3:
                    HandleThreeArguments(command, arguments[1], arguments[2]);
                    break;
                case 4:
                    HandleFourArguments(command, arguments[1], arguments[2], arguments[3]);
                    break;
                case 5:
                    HandleFiveArguments(command, arguments[1], arguments[2], arguments[3], arguments[4]);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
           
        }

        private void HandleThreeArguments(string command, string path, string option)
        {
            switch (command)
            {
                case "tasksch":
                    HandleTaskSchedulerMode(path, option);
                    break;
                case "search":
                    HandleSearchMode(path, option);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleTaskSchedulerMode(string path, string option)
        {
            var (inprocServer, localServer) = Build.BuildRegistryKey(TaskSchClsid);
            Console.WriteLine("[*] Starting Task Scheduler Mode...\n");

            switch (option.ToLower())
            {
                case "-i":
                case "--inprocserver32":
                    HandleServerSetting(path, "InprocServer32", inprocServer);
                    break;
                case "-l":
                case "--localserver32":
                    HandleServerSetting(path, "LocalServer32", localServer);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleServerSetting(string path, string serverType, string serverKey)
        {
            Check.CheckExtension(path, serverType);
            Build.CreateRegistryCU(serverKey, path);
        }

        private void HandleSearchMode(string clsid, string option)
        {
            var (inprocsrv32, localsrv32) = Build.BuildRegistryKey(clsid);
            Console.WriteLine("[*] Starting Search Mode...\n");

            switch (option.ToLower())
            {
                case "-a":
                case "--all":
                    SearchBoth(inprocsrv32, localsrv32, true, true);
                    break;
                case "-i":
                case "--inprocserver32":
                    SearchBoth(inprocsrv32, null, true, true);
                    break;
                case "-l":
                case "--localserver32":
                    SearchBoth(null, localsrv32, true, true);
                    break;
                case "-m":
                case "--machine":
                    SearchBoth(inprocsrv32, localsrv32, true, false);
                    break;
                case "-u":
                case "--user":
                    SearchBoth(inprocsrv32, localsrv32, false, true);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void SearchBoth(string inprocServer, string localServer, bool searchMachine, bool searchUser)
        {
            if (inprocServer != null)
            {
                if (searchMachine)
                {
                    Search.SearchRegistryLocalMachine(inprocServer);
                }
                if (searchUser)
                {
                    Search.SearchRegistryCurrentUser(inprocServer);
                }
            }

            if (localServer != null)
            {
                if (searchMachine)
                {
                    Search.SearchRegistryLocalMachine(localServer);
                }
                if (searchUser)
                {
                    Search.SearchRegistryCurrentUser(localServer);
                }
            }
        }

        private void HandleFourArguments(string command, string clsid, string path, string option)
        {
            var (inprocServer32, localServer32) = Build.BuildRegistryKey(clsid);

            if (command == "persist")
            {
                HandlePersistMode(inprocServer32, localServer32, path, option);
            }
            else if (command == "search")
            {
                Console.WriteLine("[*] Starting Advanced Search Mode...\n");
                HandleAdvancedSearchMode(inprocServer32, localServer32, path, option);
            }
            else
            {
                Info.ShowUsage();
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        private void HandlePersistMode(string inprocServer32, string localServer32, string path, string option)
        {
            Console.WriteLine("[*] Starting Classic Persist Mode...\n");

            switch (option.ToLower())
            {
                case "-i":
                case "--inprocserver32":
                    HandleServerSetting(path, "InprocServer32", inprocServer32);
                    break;
                case "-l":
                case "--localserver32":
                    HandleServerSetting(path, "LocalServer32", localServer32);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleAdvancedSearchMode(string inprocServer32, string localServer32, string path, string option)
        {
            // Maps options to their normalized values and handlers
            Dictionary<string, (string normalized, Action action)> optionMap = new Dictionary<string, (string, Action)>
            {
                {"-l", ("localserver", () => HandleServerOption(localServer32, option))},
                {"--localserver32", ("localserver", () => HandleServerOption(localServer32, option))},
                {"-i", ("inprocserver", () => HandleServerOption(inprocServer32, option))},
                {"--inprocserver32", ("inprocserver", () => HandleServerOption(inprocServer32, option))},
                {"-m", ("machine", () => HandleMachineOption(inprocServer32, localServer32, option))},
                {"--machine", ("machine", () => HandleMachineOption(inprocServer32, localServer32, option))},
                {"-u", ("user", () => HandleUserOption(inprocServer32, localServer32, option))},
                {"--user", ("user", () => HandleUserOption(inprocServer32, localServer32, option))}
            };

            // Get the action based on the path option
            if (optionMap.TryGetValue(path.ToLower(), out var pathAction))
            {
                pathAction.action();
            }
            else
            {
                Info.ShowUsage();
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        private void HandleServerOption(string serverKey, string locationOption)
        {
            switch (locationOption.ToLower())
            {
                case "-m":
                case "--machine":
                    Search.SearchRegistryLocalMachine(serverKey);
                    break;
                case "-u":
                case "--user":
                    Search.SearchRegistryCurrentUser(serverKey);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleMachineOption(string inprocServer, string localServer, string serverOption)
        {
            switch (serverOption.ToLower())
            {
                case "-i":
                case "--inprocserver32":
                    Search.SearchRegistryLocalMachine(inprocServer);
                    break;
                case "-l":
                case "--localserver32":
                    Search.SearchRegistryLocalMachine(localServer);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleUserOption(string inprocServer, string localServer, string serverOption)
        {
            switch (serverOption.ToLower())
            {
                case "-i":
                case "--inprocserver32":
                    Search.SearchRegistryCurrentUser(inprocServer);
                    break;
                case "-l":
                case "--localserver32":
                    Search.SearchRegistryCurrentUser(localServer);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleFiveArguments(string command, string clsid, string fakeClsid, string path, string option)
        {
            var treatAs = Build.BuildTreatAsKey(clsid);
            var (inprocServer32, localServer32) = Build.BuildRegistryKey(fakeClsid);
            var trimmeddClsid = Build.TrimClsid(fakeClsid);

            switch (command)
            {
                case "treatas":
                    HandleTreatAsPersistMode(inprocServer32, localServer32, treatAs, trimmeddClsid, path, option);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }

        private void HandleTreatAsPersistMode(string inprocServer32, string localServer32, string treatAs, string trimmedClsid, string path, string option)
        {
            Console.WriteLine("[*] Starting TreatAs Persist Mode...\n");

            switch (option.ToLower())
            {
                case "-i":
                case "--inprocserver32":
                    HandleServerSetting(path, "InprocServer32", inprocServer32);
                    Build.CreateTreatAsRegistryCU(treatAs, trimmedClsid);
                    break;
                case "-l":
                case "--localserver32":
                    HandleServerSetting(path, "LocalServer32", localServer32);
                    Build.CreateTreatAsRegistryCU(treatAs, trimmedClsid);
                    break;
                default:
                    Info.ShowUsage();
                    Settings.ExitCodeMethod(Settings.exitCodeError);
                    break;
            }
        }
    }
}