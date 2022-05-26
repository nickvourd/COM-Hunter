using System;

namespace COM_Hunter
{
    public class Settings
    {
        public const int ExitCode = 0, ExitCodeError = 1;
        public const string Version = "1.1.4", Author = "@nickvourd", Inspiration = "~ Inspired during RTO course of @zeropointsecltd ~", KeyValue = "(Default)", Entry = "entry", Libre = "libre", TaskSch = "tasksch", FindPersist = "fpersist", FindTaskSch = "ftasksch", HelpArgFull = "--help", HelpArgSort = "-h", VersionArgFull = "--version", VersionArgSort = "-v", AboutArgFull = "--about", AboutArgSort = "-a", SearchArg = "search", PersistArg = "persist", EntryArg = "get-entry", LibreArg = "get-library", TaskSchArg = "get-tasksch", FindPersistArg = "find-persist", FindTaskSchArg = "find-tasksch", GeneralArg = "general", TaskschArg = "tasksch", TreatAsArg = "treatas";
        public static void ExitCodeMethodNormal()
        {
            Environment.Exit(ExitCode);
        }

        public static void ExitCodeMethodError()
        {
            Environment.Exit(ExitCodeError);
        }
    }
}
