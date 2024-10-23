using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CLIClassLibrary.RoleHandlers.RESTHandlers
{
    public abstract class RESTRoleHandlerBase : RoleHandlerBase
    {
        public HttpClient Client { get; set; }
        public RESTRoleHandlerBase(string restEndpoint)
        {
            RestEndpoint = restEndpoint;
            this.Client = new HttpClient();
            this.AddBearerToken();
        }

        protected void AddBearerToken()
        {
            var bearerHeader = $"Bearer {EAPICLIHandlerBase.EAPICLISettings.AccessToken}";
            if (!this.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                this.Client.DefaultRequestHeaders.Add("Authorization", bearerHeader);
            }
        }

        public string RestEndpoint { get; }
    }
}
