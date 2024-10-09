using Newtonsoft.Json;
using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CLIClassLibrary.AirtableAPI
{
    public class ATDKey
    {
        public string BaseID { get; set; }
        public string APIKey { get; set; }
        public static string ProjectName { get; set; } = "app123abc321";

        public static ATDKey CurrentKey
        {
            get { return GetATDKey(""); }
            set { SetATDKey(value, ""); }
        }

        public static void SetATDKey(ATDKey value, string account = "")
        {
            try
            {
                FileInfo atdKeyFI = GetKeyForAccount(account);
                String atdJson = JsonConvert.SerializeObject(value, Formatting.Indented);
                File.WriteAllText(atdKeyFI.FullName, atdJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public static ATDKey GetATDKey(string runAs = "")
        {
            FileInfo ssotmeKeyFI = GetKeyForAccount(runAs);
            var atdcliKey = default(ATDKey);

            if (ssotmeKeyFI.Exists) atdcliKey = JsonConvert.DeserializeObject<ATDKey>(File.ReadAllText(ssotmeKeyFI.FullName));
            else atdcliKey = new ATDKey();

            if (String.IsNullOrEmpty(atdcliKey.APIKey) && 
                !String.IsNullOrEmpty(atdcliKey.BaseID) && 
                !(atdcliKey.APIKeys is null) &&
                atdcliKey.APIKeys.ContainsKey(runAs))
            {
                atdcliKey.APIKey = atdcliKey.APIKeys[runAs];
            }

            return atdcliKey;
        }

        public static DirectoryInfo ATDCLIRootDir
        {
            get
            {
                var atdcliRootDir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), string.Format(".eapi\\atdcli\\{0}\\", EAPICLIHandler.C_PROJECT_NAME)));
                if (!atdcliRootDir.Exists) atdcliRootDir.Create();
                return atdcliRootDir;
            }

        }

        private Dictionary<string, string> _APIKeys;
        public Dictionary<string, string> APIKeys
        {
            get
            {
                if (ReferenceEquals(_APIKeys, null)) _APIKeys = new Dictionary<string, string>();
                return _APIKeys;
            }
            set { _APIKeys = value; }
        }

        private static FileInfo GetKeyForAccount(string accountUsername)
        {
            var fileName = String.IsNullOrEmpty(accountUsername) ? "atdcli.key" : String.Format("atdcli{0}.key", accountUsername);

            var ssotmeKeyFI = GetKeyFIFromPath(fileName, ATDCLIRootDir.FullName);

            return ssotmeKeyFI;
        }

        private static FileInfo GetKeyFIFromPath(string fileName, string directoryName)
        {
            var fileInfo = new FileInfo(Path.Combine(directoryName, fileName));
            return fileInfo;
        }


    }
}
