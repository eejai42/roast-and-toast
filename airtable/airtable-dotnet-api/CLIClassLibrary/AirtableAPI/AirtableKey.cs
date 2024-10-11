using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CLIClassLibrary.AirtableAPI
{
    public class AirtableKey
    {
        public string APIKey { get; set; }
        public string EmailAddress { get; set; }

        public static AirtableKey CurrentKey
        {
            get { return GetAirtableKey(""); }
            set { SetAirtableKey(value); }
        }

        public static void SetAirtableKey(AirtableKey value, bool defaultKey = false, bool overwriteEmailAddressLink = true)
        {
            try
            {
                var emailAddress = value.EmailAddress;
                if (defaultKey || !File.Exists(Path.Combine(AirtableCLIDefaultRootDir.FullName, "airtable.key")))
                {
                    var fileName = "airtable.key";
                    var rootDirFullName = AirtableCLIDefaultRootDir.FullName;
                    var airtableKeyFI = GetKeyFIFromPath(fileName, rootDirFullName);
                    String akJson = JsonConvert.SerializeObject(value, Formatting.Indented);
                    File.WriteAllText(airtableKeyFI.FullName, akJson);
                }
                if (!defaultKey || (defaultKey && !string.IsNullOrEmpty(emailAddress)) && overwriteEmailAddressLink)
                {
                    var fileName = "airtable.key";
                    var rootDirPath  = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), string.Format(".eapi\\atdcli\\AirtableAPIKeys\\{0}\\", emailAddress)));
                    if (!rootDirPath.Exists) rootDirPath.Create();
                    var rootDirFullName = rootDirPath.FullName;
                    var airtableKeyFI = GetKeyFIFromPath(fileName, rootDirFullName);
                    String akJson = JsonConvert.SerializeObject(value, Formatting.Indented);
                    File.WriteAllText(airtableKeyFI.FullName, akJson);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public static AirtableKey GetAirtableKey(string emailAddress = "")
        {
            FileInfo ssotmeKeyFI = GetKeyForAccount(emailAddress);
            var airtableKey = default(AirtableKey);

            if (!ssotmeKeyFI.Exists)
            {
                CollectDefaultKey();
            }
            airtableKey = JsonConvert.DeserializeObject<AirtableKey>(File.ReadAllText(ssotmeKeyFI.FullName));

            return airtableKey;
        }

        private static void CollectDefaultKey()
        {
            Console.WriteLine("What email addresss do you use to connect to airtable?");
            var emailAddress = Console.ReadLine();
            if (String.IsNullOrEmpty(emailAddress)) throw new Exception("Can't configure CLI without Airtable Credentials");
            else
            {
                Console.WriteLine("What is the API Key associated with that email address?");
                var apiKEY = Console.ReadLine();
                if (String.IsNullOrEmpty(apiKEY)) throw new Exception("API Key required for CLI Access");
                else SetAirtableKey(new AirtableKey()
                {
                    EmailAddress = emailAddress,
                    APIKey = apiKEY
                });
            }
        }

        private static DirectoryInfo AirtableCLIDefaultRootDir
        {
            get
            {
                var atdcliRootDir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".eapi", "atdcli", "AirtableAPIKeys"));
                if (!atdcliRootDir.Exists) atdcliRootDir.Create();
                return atdcliRootDir;
            }
        }

        private static FileInfo GetKeyForAccount(string emailAddress="")
        {
            var fileName = "airtable.key";
            var userRoot = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).FullName;
            string rootDirFullName;
            if (String.IsNullOrEmpty(emailAddress))
            {
                fileName = "airtable.key";
                rootDirFullName = AirtableCLIDefaultRootDir.FullName;
            }
            else
            {
                var atdcliRootDir = new DirectoryInfo(Path.Combine(userRoot, ".eapi", "atdcli", "AirtableAPIKeys", $"{emailAddress}"));
                if (!atdcliRootDir.Exists) atdcliRootDir.Create();
                rootDirFullName = atdcliRootDir.FullName;
            }
            var ssotmeKeyFI = GetKeyFIFromPath(fileName, "/app"); 

            if (!ssotmeKeyFI.Exists) ssotmeKeyFI = GetKeyFIFromPath(fileName, userRoot);
            if (!ssotmeKeyFI.Exists) ssotmeKeyFI = GetKeyFIFromPath(fileName, rootDirFullName);

            return ssotmeKeyFI;
        }

        private static FileInfo GetKeyFIFromPath(string fileName, string directoryName)
        {
            var fileInfo = new FileInfo(Path.Combine(directoryName, fileName));
            return fileInfo;
        }
    }
}
