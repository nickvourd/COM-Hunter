using System.Collections.Generic;
using System.Linq;


namespace COM_Hunter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Show ascii art
            Info.ShowLogo();

            //Try to find out Architecture of System
            var enviromentArchChecker = new Enviromental();
            enviromentArchChecker.EnviromertArch();

            //If no arguments show usage
            if (args.Length < 1)
            {
                Info.HelpMsg();
                Settings.ExitCodeMethodError();
            }
            //check arguments
            else
            {
                List<string> arguments = args.ToList();
                //Argument constructor
                Arguments arg = new Arguments(arguments);
            }
        }
    }
}
