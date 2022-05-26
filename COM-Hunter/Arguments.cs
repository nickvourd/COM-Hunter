using System;
using System.Collections.Generic;
using System.Linq;


namespace COM_Hunter
{
    internal class Arguments
    {
        private string flag { get; set; }
        private string hkeyFlag { get; set; }
        public Arguments(List<string> arguments)
        {
            if (arguments.Count == 1)
            {
                //General arguments checks
                foreach (string argument in arguments)
                {
                    switch (argument.ToLower())
                    {
                        case Settings.HelpArgFull:
                        case Settings.HelpArgSort:
                            Info.ShowUsage();
                            break;
                        case Settings.VersionArgFull:
                        case Settings.VersionArgSort:
                            Console.WriteLine("[+] Current Version: " + Settings.Version + "\n");
                            break;
                        case Settings.AboutArgFull:
                        case Settings.AboutArgSort:
                            Info.About();
                            break;
                        case Settings.SearchArg:
                        case Settings.PersistArg:
                            Info.ShowUsage();
                            break;
                        default:
                            Info.HelpMsg();
                            break;
                    }
                }

            }
            else if (arguments.Count == 2)
            {
                string argument = arguments.ElementAt(1);
                var search = new Search();
                if (arguments.Contains("search", StringComparer.CurrentCultureIgnoreCase))
                {
                    switch (argument.ToLower())
                    {
                        case Settings.EntryArg:
                            search.Searcher(flag = Settings.Entry);
                            break;
                        case Settings.TaskSchArg:
                            search.Searcher(flag = Settings.TaskSch);
                            break;
                        case Settings.FindPersistArg:
                            search.Searcher(flag = Settings.FindPersist);
                            break;
                        case Settings.FindTaskSchArg:
                            search.Searcher(flag = Settings.FindTaskSch);
                            break;
                        default:
                            Info.HelpMsg();
                            break;
                    }
                }
                else
                {
                    Info.ShowUsage();
                    Settings.ExitCodeMethodError();
                }
            }
            else if(arguments.Count == 3)
            {
                string argument = arguments.ElementAt(1);
                if (arguments.Contains("persist", StringComparer.CurrentCultureIgnoreCase))
                {
                    var persist = new Persist();
                    argument = arguments.ElementAt(1);
                    switch (argument.ToLower())
                    {
                        case Settings.TaskschArg:
                            argument = arguments.ElementAt(2);
                            persist.PersisterTaskSch(argument);
                            break;
                        default:
                            Info.ShowUsage();
                            break;
                    }
                }
                else
                {
                    Info.ShowUsage();
                    Settings.ExitCodeMethodError();
                }
            }
            else if(arguments.Count == 4)
            {
                string argument = arguments.ElementAt(1);
                if (arguments.Contains("persist", StringComparer.CurrentCultureIgnoreCase))
                {
                    var persist = new Persist();
                    argument = arguments.ElementAt(1);
                    switch (argument.ToLower())
                    {
                        case Settings.GeneralArg:
                            argument = arguments.ElementAt(2);
                            string argument2 = arguments.ElementAt(3);
                            persist.PersisterGeneral(argument, argument2);
                            break;
                        case Settings.TreatAsArg:
                            argument = arguments.ElementAt(2);
                            string arguments2 = arguments.ElementAt(3);
                            persist.PersisterTreatAs(argument, arguments2);
                            break;
                        default:
                            Info.ShowUsage();
                            Settings.ExitCodeMethodError();
                            break;
                    }
                }
                else
                {
                    Info.ShowUsage();
                    Settings.ExitCodeMethodError();
                }
            }
            else
            {
                Info.HelpMsg();
                Settings.ExitCodeMethodError();
            }
        }
    }
}
