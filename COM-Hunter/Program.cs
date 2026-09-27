using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    class Program
    {
        static void Main(string[] args)
        {
            #if DEBUG
                Console.WriteLine("[DEBUG] COM-Hunter compiled in Debug mode.");
            #endif
            
            //Show Ascii Art
            Info.ShowLogo();

            // Get arguments
            List<string> arguments = args.ToList();
            Arguments arg = new Arguments(arguments);
        }
    }
}
