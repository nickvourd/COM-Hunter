using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    internal class Settings
    {
        public const string version = "2.0", quote = "~ Inspired during the RTO course by @zeropointsecltd ~";
        public static string[] authors = new string[] { "@nickvourd", "@S1ckB0y1337" };
        public const int exitCode = 0, exitCodeError = 1;

        // Exit code method
        public static void ExitCodeMethod(int code)
        {
            Environment.Exit(code);
        }
    }
}
