using System.Collections.Generic;


namespace COM_Hunter
{
    public static class Storage
    {
        public static List<string> DllHost = new List<string>()
        {
            "SOFTWARE\\Classes\\CLSID\\{AB8902B4-09CA-4bb6-B78D-A8F59079A8D5}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{AB8902B4-09CA-4bb6-B78D-A8F59079A8D5}\\LocalServer32"
        };

        public static List<string> Firefox = new List<string>()
        {
            "SOFTWARE\\Classes\\CLSID\\{BCDE0395-E52F-467C-8E3D-C4579291692E}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{BCDE0395-E52F-467C-8E3D-C4579291692E}\\LocalServer32"
        };

        public static List<string> Pwsh = new List<string>()
        {
            "SOFTWARE\\Classes\\CLSID\\{660b90c8-73a9-4b58-8cae-355b7f55341b}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{660b90c8-73a9-4b58-8cae-355b7f55341b}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{00021401-0000-0000-C000-000000000046}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{00021401-0000-0000-C000-000000000046}\\LocalServer32"
        };

        public static List<string> Explorer = new List<string>()
        {
            "SOFTWARE\\Classes\\CLSID\\{b03c2205-f02e-4d77-80df-e1747afdd39c}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{b03c2205-f02e-4d77-80df-e1747afdd39c}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{35786D3C-B075-49B9-88DD-029876E11C01}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{35786D3C-B075-49B9-88DD-029876E11C01}\\InprocServer32"
        };

        public static List<string> Chrome = new List<string>
        {
            "SOFTWARE\\Classes\\CLSID\\{BCDE0395-E52F-467C-8E3D-C4579291692E}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{BCDE0395-E52F-467C-8E3D-C4579291692E}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{591209c7-767b-42b2-9fba-44ee4615f2c7}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{591209c7-767b-42b2-9fba-44ee4615f2c7}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{9FC8E510-A27C-4B3B-B9A3-BF65F00256A8}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{9FC8E510-A27C-4B3B-B9A3-BF65F00256A8}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{00021401-0000-0000-C000-000000000046}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{00021401-0000-0000-C000-000000000046}\\LocalServer32",
        };

        public static List<string> IExplore = new List<string>
        {
            "SOFTWARE\\Classes\\CLSID\\{660b90c8-73a9-4b58-8cae-355b7f55341b}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{660b90c8-73a9-4b58-8cae-355b7f55341b}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{317D06E8-5F24-433D-BDF7-79CE68D8ABC2}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{317D06E8-5F24-433D-BDF7-79CE68D8ABC2}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{b5f8350b-0548-48b1-a6ee-88bd00b4a5e7}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{b5f8350b-0548-48b1-a6ee-88bd00b4a5e7}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{9FC8E510-A27C-4B3B-B9A3-BF65F00256A8}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{9FC8E510-A27C-4B3B-B9A3-BF65F00256A8}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{591209c7-767b-42b2-9fba-44ee4615f2c7}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{591209c7-767b-42b2-9fba-44ee4615f2c7}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{00020420-0000-0000-C000-000000000046}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{00020420-0000-0000-C000-000000000046}\\LocalServer32",
        };

        public static List<string> SvcHost = new List<string>
        {
            "SOFTWARE\\Classes\\CLSID\\{660b90c8-73a9-4b58-8cae-355b7f55341b}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{660b90c8-73a9-4b58-8cae-355b7f55341b}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{BCDE0395-E52F-467C-8E3D-C4579291692E}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{BCDE0395-E52F-467C-8E3D-C4579291692E}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{9FC8E510-A27C-4B3B-B9A3-BF65F00256A8}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{9FC8E510-A27C-4B3B-B9A3-BF65F00256A8}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{591209c7-767b-42b2-9fba-44ee4615f2c7}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{591209c7-767b-42b2-9fba-44ee4615f2c7}\\LocalServer32",
            "SOFTWARE\\Classes\\CLSID\\{00021401-0000-0000-C000-000000000046}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{00021401-0000-0000-C000-000000000046}\\LocalServer32"
        };

        public static List<string> Tasksch = new List<string>
        {
            "SOFTWARE\\Classes\\CLSID\\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}"
        };

        public static List<string> TaskschUsed = new List<string>
        {
            "SOFTWARE\\Classes\\CLSID\\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}\\InprocServer32",
            "SOFTWARE\\Classes\\CLSID\\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}\\LocalServer32"
        }; 
    }
}
