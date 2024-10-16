using AirtableDirect.CLI.Lib.DataClasses;
using CLIClassLibrary.Auth0;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace SSoTme.Default.Lib.CLIHandler
{
    public class EAPICLISettings
    {
        private string _accessToken;
        private string _restEndpoint;

        public bool InterpretAccessToken { get; set; }
        public string RESTEndpoint
        {
            get => _restEndpoint;
            set
            {
                if (_restEndpoint == value) return;

                Console.WriteLine($"Setting REST Endpoint: '{_restEndpoint}' => '{value}'");
                _restEndpoint = value;
            }
        }

        public string AuthTokenJson { get; set; }
        public string AccessToken
        {
            get => _accessToken; set
            {
                _accessToken = value;
                if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(this.RESTEndpoint)) return;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {value}");
                if (this.InterpretAccessToken)
                {
                    var whoami = client.GetStringAsync($"{this.RESTEndpoint}/account/whoami").GetAwaiter().GetResult();
                    this.UserInfoJson = whoami;
                }
            }
        }
        public string UserInfoJson { get; set; }
        public ASPNetUserInfo UserInfo
        {
            get
            {
                if (string.IsNullOrEmpty(this.UserInfoJson))
                {
                    return new ASPNetUserInfo()
                    {
                        Name = "anonymous",
                        EmailAddress = "anonymous@ssot.me"
                    };
                }

                var aspNetUser = JsonConvert.DeserializeObject<ASPNetUserInfo>(this.UserInfoJson);
                return aspNetUser;
            }
        }
        public string Auth0UserInfoJson { get; set; }
        public Auth0UserInfo Auth0UserInfo
        {
            get
            {
                if (string.IsNullOrEmpty(this.Auth0UserInfoJson))
                {
                    return new Auth0UserInfo()
                    {
                        Name = "anonymous",
                        Email = "anonymous@ssot.me",
                        Email_Verified = false
                    };
                }

                return JsonConvert.DeserializeObject<Auth0UserInfo>(this.Auth0UserInfoJson);
            }
        }

        internal void ImportAccessToken(string accessToken)
        {
            this.InterpretAccessToken = true;
            this.AccessToken = accessToken;
            this.InterpretAccessToken = false;
        }
    }
}