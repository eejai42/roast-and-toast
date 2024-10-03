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
using System.Net.Http;

namespace CLIClassLibrary.RoleHandlers.RESTHandlers
{
    public partial class GuestRESTCLIHandler
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

        private async Task<string> HandlerFactory(string invokeRequest, string payloadString, string where, Int32 maxPages)
        {
            string result = null;
            var payload = JsonConvert.DeserializeObject<StandardPayload>(payloadString, new JsonSerializerSettings() { 
                    FloatParseHandling = FloatParseHandling.Decimal, 
                    ContractResolver = new CustomContractResolver<StandardPayload>()
            });
            payload.AirtableWhere = where;
            payload.MaxPages = maxPages < 1 ? 5 : maxPages;
			
            switch (invokeRequest.ToLower())
            {
                
                
                case "requesttoken":
                    break;

                    
                
                
                case "validatetemporaryaccesstoken":
                    break;

                    
                
                
                case "whoami":
                    break;

                    
                
                
                case "whoareyou":
                    break;

                    
                default:
                    throw new Exception($"Invalid request: {invokeRequest}");
            }

            return result;
        }
        
        private async Task<string> POSTRequest(string methodName, StandardPayload payload)
        {
            var role = "Guest";
            var client = new HttpClient();
            var requestUrl = $"{this.RestEndpoint}/{role}/{methodName}";

            // Serialize the payload to JSON
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await Client.PostAsync(requestUrl, httpContent);

            return await response.Content.ReadAsStringAsync();
        }
        
        private async Task<string> GETRequest(string pluralName, StandardPayload payload)
        {
            var role = "Guest";
            var requestUrl = $"{this.RestEndpoint}/{role}/{pluralName}";
            this.AddBearerToken();
            var results = await Client.GetStringAsync(requestUrl);

            return results;
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
