using System;
using System.Security.Policy;

namespace ASPNet_REST_API
{
    public class Auth0Settings
    {
        public static Auth0Settings Instance { get; set; } = new Auth0Settings();

        private string _pkceRootUrl = "";
        public string PKCERootUrl
        {
            get { return _pkceRootUrl; }
            set
            {
                _pkceRootUrl = value;
                if (!string.IsNullOrEmpty(value))
                {
                    PKCECallbackUrl = _pkceRootUrl + "/pkce/callback";
                    PKCELoginUrl = _pkceRootUrl + "/pkce/loginurl";
                }
                
            }
        }

        private string _domain = "";
        public string Domain
        {
            get { return _domain; }
            set
            {
                _domain = value;
                Audience = $"https://{_domain}/api/v2";
            }
        }

        public string Audience { get; private set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string PKCECallbackUrl { get; private set; } = "";
        public string PKCELoginUrl { get; private set; } = "";
    }
}
