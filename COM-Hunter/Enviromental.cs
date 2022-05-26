using System;


namespace COM_Hunter
{
    public class Enviromental
    {
        public void EnviromertArch()
        {
            bool archFlag = Environment.Is64BitOperatingSystem;

            if (archFlag != true)
            {
                Info.ErrorMsgArch();
                Settings.ExitCodeMethodError();
            }
        }

    }
}
