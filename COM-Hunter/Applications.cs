using Microsoft.Win32;

namespace COM_Hunter
{
    public class Applications
    {
        public static bool BrowserInstalled(string browserName)
        {
            bool isInstalled = false;
            RegistryKey browsersNode = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            foreach (string browser in browsersNode.GetSubKeyNames())
            {
                if (browser.ToLower().Contains(browserName.ToLower()))
                {
                    isInstalled = true;
                    break;
                }
            }
            return isInstalled;
        }
    }
}
