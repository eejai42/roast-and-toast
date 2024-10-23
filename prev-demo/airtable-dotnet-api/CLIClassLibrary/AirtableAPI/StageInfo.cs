using Newtonsoft.Json;
using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CLIClassLibrary.AirtableAPI
{
    public class StageInfo
    {
        public string Stage { get; set; }
        public string BaseID { get; set; }
        public string ApiKeyName { get; set; }
        public static StageInfo CurrentStage
        {
            get { return GetStageInfo(""); }
            set { SetStageInfo(value); }
        }

        public static string GetApiKey(string emailAddress = "")
        {
            return AirtableKey.GetAirtableKey(emailAddress).APIKey;
        }

        public static void SetStageInfo(StageInfo value, bool defaultKey = false, bool overwriteStage = true)
        {
            try
            {
                if (defaultKey || !File.Exists(Path.Combine(StageInfoDefaultRootDir.FullName, "default_stageinfo.json")))
                {
                    var fileName = "default_stageinfo.json";
                    var rootDirFullName = StageInfoDefaultRootDir.FullName;
                    var siFI = GetStageFIFromPath(fileName, rootDirFullName);
                    String siJson = JsonConvert.SerializeObject(value, Formatting.Indented);
                    File.WriteAllText(siFI.FullName, siJson);
                }
                if (!defaultKey || (defaultKey && !string.IsNullOrEmpty(value.BaseID)) && overwriteStage)
                {
                    var fileName = "stageinfo.json";
                    var rootDirPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), string.Format(".eapi\\atdcli\\{0}\\{1}\\", EAPICLIHandler.C_PROJECT_NAME, value.Stage)));
                    if (!rootDirPath.Exists) rootDirPath.Create();
                    var rootDirFullName = rootDirPath.FullName;
                    var siFI = GetStageFIFromPath(fileName, rootDirFullName);
                    String siJson = JsonConvert.SerializeObject(value, Formatting.Indented);
                    File.WriteAllText(siFI.FullName, siJson);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public static StageInfo GetStageInfo(string stage = "")
        {
            FileInfo stageInfoFI = GetStageInfoFIByStage(stage);
            var si = default(StageInfo);

            if (!stageInfoFI.Exists)
            {
                CollectDefaultStage();
            }

            si = JsonConvert.DeserializeObject<StageInfo>(File.ReadAllText(stageInfoFI.FullName));
            return si;
        }

        private static void CollectDefaultStage()
        {
            Console.WriteLine("Which stage do you want to set as your default?");
            var stages = GetProjectStageInfo();
            Console.WriteLine(JsonConvert.SerializeObject(stages, Formatting.Indented));
            var chosenStage = Console.ReadLine();
            var noStagePassedIn = false;
            if (String.IsNullOrEmpty(chosenStage)) noStagePassedIn = true;
            var stage = new StageInfo();
            if (noStagePassedIn)
            {
                Console.WriteLine(String.Format("Defaulting to {0}", stages[0].Stage));
                stages[0].ApiKeyName = AirtableKey.CurrentKey.EmailAddress;
                stage = stages[0];
                SetStageInfo(stage);
            }
            else
            {
                var foundMatch = false;
                foreach (var s in stages)
                {
                    if (s.Stage.ToLower() == chosenStage.ToLower())
                    {
                        s.ApiKeyName = AirtableKey.CurrentKey.EmailAddress;
                        stage = s;
                        foundMatch = true;
                        break;
                    }
                }
                if (foundMatch == false)
                {
                    throw new Exception("Invalid stage name");
                }
            }
            SetStageInfo(stage);
        }

        private static DirectoryInfo StageInfoDefaultRootDir
        {
            get
            {
                var stageInfoRootDir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), String.Format(".eapi\\atdcli\\{0}\\", EAPICLIHandler.C_PROJECT_NAME)));
                if (!stageInfoRootDir.Exists) stageInfoRootDir.Create();
                // Console.WriteLine($"GOT STAGE INFO DEFAULT ROOT DIR: {stageInfoRootDir.FullName}");
                return stageInfoRootDir;
            }
        }


        private static FileInfo GetStageInfoFIByStage(string stage = "", bool defaultStage = false)
        {
            if (stage == "") defaultStage = true;
            var fileName = "stageinfo.json";
            string rootDirFullName;
            if (defaultStage)
            {
                fileName = "default_stageinfo.json";
                rootDirFullName = StageInfoDefaultRootDir.FullName;
            }
            else
            {
                var stageInfoRootDir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), string.Format(".eapi\\atdcli\\{0}\\{1}\\", EAPICLIHandler.C_PROJECT_NAME, stage)));
                if (!stageInfoRootDir.Exists) stageInfoRootDir.Create();
                rootDirFullName = stageInfoRootDir.FullName;
            }

            var stageInfoFI = GetStageFIFromPath(fileName, rootDirFullName);

            return stageInfoFI;
        }

        private static FileInfo GetStageFIFromPath(string fileName, string directoryName)
        {
            var fileInfo = new FileInfo(Path.Combine(directoryName, fileName));
            // Console.WriteLine($"Trying to get stage from path: {fileInfo.FullName}");
            return fileInfo;
        }

        public static void CheckProjectStagesExist()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var stagesInfoFileName = Path.Combine(dir, "stagesInfo.json");
            var output = JsonConvert.DeserializeObject<StageInfo[]>(File.ReadAllText(stagesInfoFileName));
            foreach (var stage in output)
            {
                var fi = GetStageInfoFIByStage(stage.Stage);
                if (!fi.Exists)
                {
                    var si = new StageInfo { Stage = stage.Stage, BaseID = stage.BaseID, ApiKeyName = AirtableKey.CurrentKey.EmailAddress };
                    SetStageInfo(si);
                }
            }
        }

        public static StageInfo[] GetProjectStageInfo()
        {
            CheckProjectStagesExist();
            var rawSI = JsonConvert.DeserializeObject<StageInfo[]>(File.ReadAllText("stagesInfo.json"));
            List<StageInfo> stages = new List<StageInfo>();
            foreach (var stage in rawSI)
            {
                stages.Add(GetStageInfo(stage.Stage));
            }
            return stages.ToArray();
        }

        public static StageInfo GetStageInfoByStage(string stage)
        {
            var stages = GetProjectStageInfo();
            foreach (var si in stages)
            {
                if (si.Stage == stage) { return si; }
            }
            return null;
        }

        public static StageInfo GetStageInfoByBaseID(string baseID)
        {
            var stages = GetProjectStageInfo();
            foreach (var si in stages)
            {
                if (si.BaseID == baseID) { return si; }
            }
            throw new Exception("BaseID doesn't match any stage");
        }

        public static string AddApiKey(string stage, string apiKeyEmail)
        {
            var si = GetStageInfoByStage(stage);
            si.ApiKeyName = apiKeyEmail;
            SetStageInfo(si, true);
            return JsonConvert.SerializeObject(si, Formatting.Indented);
        }

    }
}
