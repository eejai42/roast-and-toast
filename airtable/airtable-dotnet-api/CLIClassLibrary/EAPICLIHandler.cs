using CLIClassLibrary.RoleHandlers;
using AirtableDirect.CLI.Lib.DataClasses;
using Newtonsoft.Json;
using Plossum.CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YP.SassyMQ.Lib.RabbitMQ;
using CLIClassLibrary.AirtableAPI;
using SSoT.me.AirtableToDotNetLib;

namespace SSoTme.Default.Lib.CLIHandler
{

    public partial class EAPICLIHandler : EAPICLIHandlerBase<FAKE_UserAccount>
    {
        public static string C_PROJECT_NAME = "roast-and-toast.xlsx";

        public EAPICLIHandler(string[] args)
            : base(args)
        {
            
        }

        protected override string HandleCustomRequest()
        {
            if (!String.IsNullOrEmpty(this.action)) return this.HandleAction();
            else throw new Exception($"Sytnax error: cli -action XYZ -bodyData {{...}} -as Admin");

        }

        protected override string HandleAction()
        {
            if (!String.IsNullOrEmpty(this.bodyFile))
            {
                var fileInfo = new FileInfo(this.bodyFile);
                if (!fileInfo.Exists) throw new Exception($"-bodyFile {fileInfo.FullName} does not exists.");
                else if (String.IsNullOrEmpty(this.bodyData)) this.bodyData = File.ReadAllText(fileInfo.FullName);
            }
            var result = this.RoleHandler.Handle(this.action, this.bodyData, this.where, this.maxpages, this.view);
            if (!String.IsNullOrEmpty(this.output)) File.WriteAllText(this.output, result);
            return result;
        }

        protected override string GetCustomHelp()
        {
            return this.GetStandardHelp();
        }

        protected override void CheckDefaultCLIParameters(string firstArgument)
        {
            if (!this.help &&
                !this.whoami &&
                !this.authenticate &&
                String.IsNullOrEmpty(this.action) &&
                !String.IsNullOrEmpty(firstArgument))
            {
                if (String.IsNullOrEmpty(firstArgument))
                {
                    this.help = true;
                }
                else if (!String.IsNullOrEmpty(this.apikey) && !String.IsNullOrEmpty(this.baseid))
                {
                    var key = ATDKey.GetATDKey(this.runas);
                    if (ReferenceEquals(key.APIKeys, null)) key.APIKeys = new Dictionary<String, String>();
                    key.BaseID = this.baseid;
                    key.APIKeys[this.runas] = this.apikey;
                    ATDKey.SetATDKey(key, this.runas);
                }
                else if (!String.IsNullOrEmpty(this.apikey) && !String.IsNullOrEmpty(this.emailAddress))
                {
                    var key = AirtableKey.GetAirtableKey(this.emailAddress);
                    if (ReferenceEquals(key.APIKey, null)) key.APIKey = this.apikey;
                    AirtableKey.SetAirtableKey(key, this.makeDefault);
                }
                else
                {
                    this.action = firstArgument;
                    
                }
            }
        }
    }
}

namespace CoreLibrary.Extensions
{
    public class Foo { }
}