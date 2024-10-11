using CLIClassLibrary;
using CLIClassLibrary.AirtableAPI;
using CLIClassLibrary.RoleHandlers;
using CLIClassLibrary.RoleHandlers.ATDHandlers;
using ConsoleApp1;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plossum.CommandLine;
using Swan;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YP.SassyMQ.Lib.RabbitMQ;

namespace SSoTme.Default.Lib.CLIHandler
{
    public abstract partial class EAPICLIHandlerBase<T> : EAPICLIHandlerBase
    {
        private RoleHandlerBase _roleHandler;
        private T _loggedInUser;
        private bool addCreds;

        public EAPICLIHandlerBase(string[] args)
        {
            var list = args.ToList();
            list.Insert(0, "cli");
            this.Parser = new CommandLineParser(this);
            this.Parser.Parse(list.ToArray());
        }

        static EAPICLIHandlerBase() {
            ConfigurationHelper.InitializeAppSettings("../../../../../ASPNet-REST-API/appsettings.json");
        }

        internal static string GetCurrentUserRoleName()
        {
            var di = new DirectoryInfo(ProjectRootPath);
            if (!di.Exists) di.Create();
            var cliSettingsFI = di.GetFiles("cli-settings.json").FirstOrDefault();
            if (cliSettingsFI?.Exists == false) return "Guest";
            else
            {
                var settingsJson = File.ReadAllText(cliSettingsFI.FullName); 
                dynamic settings = JsonConvert.DeserializeObject(settingsJson);
                return $"{settings?.UserInfo?.Role}";
            }
        }

        internal static string GetToken(string runas)
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var key = ATDKey.GetATDKey(runas);
            var fileInfo = new FileInfo(Path.Combine(ProjectRootPath, $"{runas}.token"));
            if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();
            if (!fileInfo.Exists) return String.Empty;
            else
            {
                var accessToken = File.ReadAllText(fileInfo.FullName);
                File.WriteAllText(fileInfo.FullName, JsonConvert.SerializeObject(key, Formatting.Indented));
                return accessToken;
            }
        }

        internal static ATDKey GetCurrentKey(string runas)
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var key = ATDKey.GetATDKey(runas);
            return key;
            //var fileInfo = new FileInfo(Path.Combine(ProjectRootPath, $"{runas}.token"));
            //if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();
            //if (!fileInfo.Exists) return new ATDKey{ };
            //else
            //{
            //    File.WriteAllText(fileInfo.FullName, JsonConvert.SerializeObject(key, Formatting.Indented));
            //    return key;
            //}
        }

        public string ProcessRequest()
        {
            this.SetDefaultCLIParameters();
            if (!String.IsNullOrEmpty(this.baseid) && !String.IsNullOrEmpty(this.action)) UseInputBaseID();
            if (!String.IsNullOrEmpty(this.stage) && !String.IsNullOrEmpty(this.action)) UseInputStage();
            if (!String.IsNullOrEmpty(this.apikey) && !String.IsNullOrEmpty(this.action)) UseInputAPIKey();
            if (!String.IsNullOrEmpty(this.baseid) && !String.IsNullOrEmpty(this.stage)) return this.SetStageBaseID();
            if (!String.IsNullOrEmpty(this.apikey) && String.IsNullOrEmpty(this.action)) return this.AddAirtableKey();
            if (!String.IsNullOrEmpty(this.emailAddress) && !String.IsNullOrEmpty(this.stage) && String.IsNullOrEmpty(this.action)) return this.AddApiKeyToStageInfo();

            if (this.help) return this.ShowHelp();
            else if (this.whoami) return this.CheckWhoIAmNow();
            else if (this.addCreds) return this.AddKey(this.apikey, this.baseid);
            else if (this.listStageInfo) return this.ListStageInfo();
            else if (!String.IsNullOrEmpty(this.restEndpoint)) return this.UpdateRestEndpoint();
            else if (!String.IsNullOrEmpty(this.action)) return this.HandleAction();
            else if (this.authenticate) return this.Authenticate().Result;
            else return this.HandleCustomRequest();
        }

        private string StartServer()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "../ASPNet-REST-API/bin/Debug/netcoreapp3.1/ASPNet-REST-API.dll",
                UseShellExecute = false,
            };
            Process.Start(startInfo);

            return "staring http server now...";

        }

        private string UpdateRestEndpoint()
        {
            var lowerEndpoint = $"{this.restEndpoint}".ToLower();
            var nullValues = new string[] { "null", "empty", "nothing", "none" };
            if (nullValues.Contains(lowerEndpoint))
            {
                this.restEndpoint = null;
            }

            if (lowerEndpoint == "default")
            {
                var auth0Settings = ConfigurationHelper.GetAuth0Settings();
                this.restEndpoint = auth0Settings.PKCERootUrl;
            }
            EAPICLISettings.RESTEndpoint = this.restEndpoint;
            SaveEAPICLISettings();
            return this.restEndpoint;
        }

        public static void SaveEAPICLISettings()
        {
            File.WriteAllText(EAPICLISettingsFileInfo.FullName, JsonConvert.SerializeObject(EAPICLISettings));
        }

    

        protected abstract string HandleAction();

        protected abstract string HandleCustomRequest();



        private string CheckWhoIAmNow()
        {
            if (!EAPICLISettingsFileInfo.Exists) throw new Exception($"Must must first authenticate as {runas}");
            else
            {
                var json = File.ReadAllText(EAPICLISettingsFileInfo.FullName);
                return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented);
            }
        }

        public T LoggedInUser
        {
            get
            {
                if (_loggedInUser is null)
                {
                    if (!EAPICLISettingsFileInfo.Exists) throw new Exception("Must authenticate and check who-am-i before this will work.");

                    var accountJson = File.ReadAllText(EAPICLISettingsFileInfo.FullName);
                    _loggedInUser = JsonConvert.DeserializeObject<T>(accountJson);
                }
                return _loggedInUser;
            }
            set => _loggedInUser = value;
        }



        public string ShowHelp()
        {
            var customHelp = this.GetCustomHelp();
            if (!string.IsNullOrEmpty(customHelp)) return customHelp;
            else return GetStandardHelp();
        }

        protected virtual string GetCustomHelp()
        {
            return null;
        }

        public string GetStandardHelp()
        {
            var sbHelpBuilder = new StringBuilder();
            var currentAssembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(currentAssembly.Location);
            sbHelpBuilder.AppendLine($"{EAPICLIHandler.C_PROJECT_NAME} CLI Help: {fvi.FileVersion}.");
            sbHelpBuilder.AppendLine();
            var helpTerm = this.Parser.RemainingArguments.FirstOrDefault();
            if (String.IsNullOrEmpty(helpTerm)) helpTerm = "general";
            this.RoleHandler.AddHelp(sbHelpBuilder, helpTerm);
            if (helpTerm == "general")
            {
                sbHelpBuilder.AppendLine();
                sbHelpBuilder.AppendLine();
                sbHelpBuilder.AppendLine($"Syntax:");
                sbHelpBuilder.AppendLine(this.Parser.UsageInfo.ToString());
                sbHelpBuilder.AppendLine();
                sbHelpBuilder.AppendLine($"Available Roles:");
                RoleHandlerFactory.ListRoles(sbHelpBuilder);
            }
            return sbHelpBuilder.ToString();
        }

        private void SetDefaultCLIParameters()
        {
            var firstArgument = this.Parser.RemainingArguments.FirstOrDefault();

            if (String.Equals(firstArgument, "help", StringComparison.OrdinalIgnoreCase))
            {
                this.help = true;
                this.Parser.RemainingArguments.RemoveAt(0);
            }
            if (string.Equals(firstArgument, "addCredentials", StringComparison.OrdinalIgnoreCase))
            {
                this.addCreds = true;
            }
            if (string.Equals(firstArgument, "listStageInfo", StringComparison.OrdinalIgnoreCase))
            {
                this.listStageInfo = true;
                this.Parser.RemainingArguments.RemoveAt(0);
            }

            if (String.IsNullOrEmpty(this.runas)) this.runas = GetCurrentUserRoleName();
            if (String.IsNullOrEmpty(this.runas)) this.runas = "admin";
            this.runas = this.runas.ToLower();
            this.SaveAuth(String.Empty);
            firstArgument = this.Parser.RemainingArguments.FirstOrDefault();
            this.CheckDefaultCLIParameters(firstArgument);
        }

        protected abstract void CheckDefaultCLIParameters(string firstArgument);

        public async Task<string> Authenticate()
        {
            var auth0Settings = ConfigurationHelper.GetAuth0Settings();
            var callbackHost = "http://localhost:7890";
            var client = new HttpClient();
            var loginUrlJson = await client.GetStringAsync($"{auth0Settings.PKCELoginUrl}?returnUrl={callbackHost}/callback");
            dynamic loginUrlObj = JsonConvert.DeserializeObject(loginUrlJson);
            var browser = new SystemBrowser(7890);
            BrowserOptions browserOptions = new BrowserOptions($"{loginUrlObj.startUrl}", callbackHost);
            var result = await browser.InvokeAsync(browserOptions);

            if (result.ResultType != BrowserResultType.Success)
            {
                Console.WriteLine($"An error occurred: {result.ResultType}");
                return null;
            }


            var tokenJson = result.Response; // This is a call to your server-side component.

            var uri = new Uri(tokenJson);

            // Extract AccessToken and UserInfo from the query parameters
            var accessToken = HttpUtility.ParseQueryString(uri.Query).Get("AccessToken");
            EAPICLIHandler.EAPICLISettings.Auth0UserInfoJson = HttpUtility.ParseQueryString(uri.Query).Get("UserInfo");
            EAPICLIHandler.EAPICLISettings.ImportAccessToken(accessToken);
            EAPICLIHandler.SaveEAPICLISettings();

            return tokenJson;
        }

        private string ExtractAuthorizationCode(string callbackUrl)
        {
            // Extract the code parameter value from the callback URL.
            var uri = new Uri(callbackUrl);
            var code = HttpUtility.ParseQueryString(uri.Query).Get("code");
            return code;
        }

       

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; // Ignore SSL certificate errors
        }
        private void StoreAccessToken(string accessToken, UserInfoResult userInfo)
        {
            Console.WriteLine($"GOT ACCESS TOKEN: {accessToken} - {String.Join(',', userInfo.Claims.Select(claim => $"{claim.Type}: {claim.Value}"))}");
            // do nothing here.
        }

        public string AddKey(string apiKey, string baseId)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            if (baseId == null) throw new ArgumentNullException(nameof(baseId));
            var key = ATDKey.GetATDKey(this.runas);
            key.APIKey = apiKey;
            key.BaseID = baseId;
            ATDKey.SetATDKey(key, this.runas);
            return JsonConvert.SerializeObject(key, Formatting.Indented);
        }

        public string AddAirtableKey()
        {
            if (this.apikey == null) throw new ArgumentNullException(nameof(this.apikey));
            var key = AirtableKey.GetAirtableKey(this.emailAddress);
            key.APIKey = this.apikey;
            AirtableKey.SetAirtableKey(key, this.makeDefault);
            return JsonConvert.SerializeObject(key, Formatting.Indented);
        }

        public string ListStageInfo()
        {
            var stages = StageInfo.GetProjectStageInfo();
            return JsonConvert.SerializeObject(stages, Formatting.Indented);
        }

        public string AddApiKeyToStageInfo()
        {
            return StageInfo.AddApiKey(this.stage, this.emailAddress);
        }

        public void UseInputBaseID()
        {
            var si = StageInfo.GetStageInfoByBaseID(this.baseid);
            StageInfo.SetStageInfo(si, true, false);
        }

        public void UseInputStage()
        {
            var stage = StageInfo.GetStageInfoByStage(this.stage);
            if (stage == null)
            {
                throw new Exception("Invalid stage name");
            }
            StageInfo.SetStageInfo(stage, true, false);
        }

        public string SetStageBaseID()
        {
            var stage = StageInfo.GetStageInfoByStage(this.stage);
            if (stage == null)
            {
                throw new Exception("Invalid stage name");
            }
            stage.BaseID = this.baseid;
            StageInfo.SetStageInfo(stage, true, true);
            return JsonConvert.SerializeObject(stage, Formatting.Indented);
        }

        public void UseInputAPIKey()
        {
            var currentKey = AirtableKey.CurrentKey;
            currentKey.APIKey = this.apikey;
            AirtableKey.SetAirtableKey(currentKey, true, false);
        }

        private void SaveAuth(string accessToken)
        {
            var fileInfo = new FileInfo(Path.Combine(ProjectRootPath, $"{this.runas}.token"));
            File.WriteAllText(fileInfo.FullName, accessToken);
        }

        public CommandLineParser Parser { get; protected set; }
        internal RoleHandlerBase RoleHandler
        {
            get
            {
                if (_roleHandler is null)
                {
                    _roleHandler = RoleHandlerFactory.CreateHandler(this.runas, EAPICLISettings.RESTEndpoint);
                }
                return _roleHandler;
            }
            private set => _roleHandler = value;
        }

        public static string RootPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); } }

        public static string ProjectRootPath
        {
            get
            {
                var di = new DirectoryInfo(Path.Combine(RootPath, ".eapi", $"{EAPICLIHandler.C_PROJECT_NAME}"));
                if (!di.Exists) di.Create();
                return di.FullName;
            }
        }

        public static void HandleRequest(string[] args)
        {
            var handler = new EAPICLIHandler(args);
            var outputText = handler.ProcessRequest();
            Console.WriteLine(outputText);
        }
    }

    public partial class EAPICLIHandlerBase
    {
        private static EAPICLISettings _eapiCLISettings;
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetEntryAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                var fi = new FileInfo(Uri.UnescapeDataString(uri.Path));
                var path = fi.Directory;
                while (!path.GetDirectories().Any(anyDir => anyDir.Name == "docs"))
                {
                    path = path.Parent;
                    if (ReferenceEquals(path, null)) break;
                }

                if (path == null)
                {
                    path = new DirectoryInfo("/docs");
                    if (!path.Exists) path = new DirectoryInfo("/apps/docs");
                    if (!path.Exists) path = fi.Directory;
                }
                else path = new DirectoryInfo(Path.Combine(path.FullName, "docs"));

                return path.FullName;
            }
        }

        public static DirectoryInfo RootFileInfo
        {
            get
            {
                var di = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".eapi"));
                if (!di.Exists) di.Create();
                return di;
            }
        }

        public static FileInfo EAPICLISettingsFileInfo
        {
            get
            {
                var settingsFI = new FileInfo(Path.Combine(RootFileInfo.FullName, "roast-and-toast.xlsx", "cli-settings.json"));
                if (!settingsFI.Exists) File.WriteAllText(settingsFI.FullName, "{}");
                return settingsFI;
            }
        }

        public static EAPICLISettings EAPICLISettings
        {
            get
            {
                if (_eapiCLISettings is null)
                {
                    var settingsText = File.ReadAllText(EAPICLISettingsFileInfo.FullName);
                    _eapiCLISettings = JsonConvert.DeserializeObject<EAPICLISettings>(settingsText);
                }
                return _eapiCLISettings;
            }
        }
    }
}