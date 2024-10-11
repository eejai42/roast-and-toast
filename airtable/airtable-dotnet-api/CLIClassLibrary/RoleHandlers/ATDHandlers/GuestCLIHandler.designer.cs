using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using CLIClassLibrary.AirtableAPI;
using static SSoT.me.AirtableToDotNetLib.AirtableExtensions;

namespace CLIClassLibrary.RoleHandlers.ATDHandlers
{
    public partial class GuestCLIHandler
    {
        public override void AddHelp(StringBuilder sb, string helpTerm)
        {
            sb.AppendLine($"Help for Guest.");
            
            helpTerm = helpTerm.ToLower();
            var found = helpTerm == "general";
            
            if (helpTerm == "general")
            {
                sb.AppendLine();
                
                sb.AppendLine($"void: RequestToken");
                sb.AppendLine($"void: ValidateTemporaryAccessToken");
                sb.AppendLine($"void: WhoAmI");
                sb.AppendLine($"void: WhoAreYou");                                            
            }
            
            sb.AppendLine($"{Environment.NewLine}Available Actions Matching: {helpTerm}");
            
            if ("requesttoken".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - RequestToken");
                if ("requesttoken".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintRequestTokenHelp(sb);
                }
                found = true;
            }
            if ("validatetemporaryaccesstoken".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - ValidateTemporaryAccessToken");
                if ("validatetemporaryaccesstoken".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintValidateTemporaryAccessTokenHelp(sb);
                }
                found = true;
            }
            if ("whoami".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - WhoAmI");
                if ("whoami".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintWhoAmIHelp(sb);
                }
                found = true;
            }
            if ("whoareyou".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - WhoAreYou");
                if ("whoareyou".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintWhoAreYouHelp(sb);
                }
                found = true;
            }
                       
            if (!found)
            {
                sb.AppendLine();
                sb.AppendLine($"{Environment.NewLine}UNABLE TO FIND COMMAND: {helpTerm} not found.");
            }
        }

        private string HandlerFactory(string invokeRequest, string payloadString, string where, Int32 maxPages)
        {
            var result = "";
            var payload = JsonConvert.DeserializeObject<StandardPayload>(payloadString, new JsonSerializerSettings() { 
                    FloatParseHandling = FloatParseHandling.Decimal, 
                    ContractResolver = new CustomContractResolver<StandardPayload>()
            });
            payload.SetActor(this.ATDActor);
            payload.AccessToken = this.ATDActor.AccessToken;
            payload.ApiKey = AirtableKey.CurrentKey.APIKey;
            payload.BaseId = StageInfo.CurrentStage.BaseID;
            payload.AirtableWhere = where;
            payload.MaxPages = maxPages < 1 ? 5 : maxPages;
            Object reply = new Object();
			
            switch (invokeRequest.ToLower())
            {
                case "requesttoken":
				    this.ATDActor.RequestToken(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                case "validatetemporaryaccesstoken":
				    this.ATDActor.ValidateTemporaryAccessToken(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                case "whoami":
				    this.ATDActor.WhoAmI(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                case "whoareyou":
				    this.ATDActor.WhoAreYou(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                default:
                    throw new Exception($"Invalid request: {invokeRequest}");
            }

            return result;
        }
        
        
        public void PrintRequestTokenHelp(StringBuilder sb)
        {
            
        }
        
        public void PrintValidateTemporaryAccessTokenHelp(StringBuilder sb)
        {
            
        }
        
        public void PrintWhoAmIHelp(StringBuilder sb)
        {
            
        }
        
        public void PrintWhoAreYouHelp(StringBuilder sb)
        {
            
        }
        

    }
}
