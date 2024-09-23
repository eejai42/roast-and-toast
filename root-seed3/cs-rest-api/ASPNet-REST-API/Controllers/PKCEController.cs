using ASPNet_REST_API;
using Auth0.AuthenticationApi.Models;
using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Azure.Core;
using System.Security.Authentication;
using CLIClassLibrary;
using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient;
using SSoTme.Default.Lib.CLIHandler;
using static IdentityModel.ClaimComparer;
using System.Web;
using CLIClassLibrary.ATDMQ;
using Swan.Logging;
using AirtableDirect.CLI.Lib.DataClasses;

namespace ASPNet_REST_API.Controllers
{
    [Route("/PKCE")]
    public class PKCEController : AccountController
    {

        public PKCEController(IOptions<Auth0Settings> auth0Settings)
            : base(auth0Settings)
        {
        }

        public static Dictionary<string, (string CodeVerifier, DateTime Expiry, string ReturnUrl)> StateStore = new Dictionary<string, (string, DateTime, string)>();

        [HttpGet("AllFiles2")]
        public async Task<string> AllFiles()
        {
            // Using Environment.CurrentDirectory to get the current working directory
            var directoryInfo = new DirectoryInfo("/app");

            // GetFiles method with SearchOption.AllDirectories to include all subdirectories
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

            // Select to transform FileInfo objects into their full path strings
            var filePaths = files.Select(file => file.FullName);

            var json = JsonConvert.SerializeObject(filePaths, Formatting.Indented);

            // Asynchronously return the file paths as a JSON result
            return json;
        }

        [HttpGet("LoginUrl")]
        public async Task<IActionResult> PKCELoginUrl(string returnUrl = null, string callbackUrl = null)
        {
            var auth0SEttings = ConfigurationHelper.GetAuth0Settings();
            var options = new OidcClientOptions
            {
                Authority = $"https://{auth0SEttings.Domain}",
                ClientId = auth0SEttings.ClientId,
                RedirectUri = $"{callbackUrl ?? auth0SEttings.PKCECallbackUrl}",
                Scope = "openid email profile",
                FilterClaims = false,
            };
            var oidcClient = new OidcClient(options);
            var state = await oidcClient.PrepareLoginAsync();

            // Store the state, code verifier, and return URL
            StateStore[state.State] = (state.CodeVerifier, DateTime.UtcNow.AddMinutes(10), returnUrl);

            return Ok(new { StartUrl = state.StartUrl });
        }


        [HttpGet("MobileLoginUrl")]
        public async Task<IActionResult> MobileLoginUrl(string returnUrl = null)
        {
            var auth0SEttings = ConfigurationHelper.GetAuth0Settings();
            var options = new OidcClientOptions
            {
                Authority = $"https://{auth0SEttings.Domain}",
                ClientId = auth0SEttings.ClientId,
                RedirectUri = returnUrl ?? $"https://localhost:42123/pkce/callback",
                Scope = "openid email profile",
                FilterClaims = false,
            };
            var oidcClient = new OidcClient(options);
            var state = await oidcClient.PrepareLoginAsync();

            // Store the state, code verifier, and return URL
            StateStore[state.State] = (state.CodeVerifier, DateTime.UtcNow.AddMinutes(10), returnUrl);

            return Ok(new { StartUrl = state.StartUrl });
        }


        [HttpPost("exchange")]
        public async Task<IActionResult> ExchangeToken([FromBody] ExchangeTokenRequest request)
        {

            try
            {
                string emailAddress = await GetEmailAddress(request, "http://localhost:3000/callback");
                var atdAdmin = new ATDAdmin();
                var payload = atdAdmin.CreatePayload();
                payload.AirtableWhere = $"EmailAddress='{emailAddress}'";
                var appUser = atdAdmin.GetAppUsers(payload).FirstOrDefault();
                if (appUser is null)
                {
                    payload.AppUser = new AirtableDirect.CLI.Lib.DataClasses.AppUser()
                    {
                        EmailAddress = emailAddress,
                        Name = emailAddress,
                        Roles = "Influencer"
                    };

                    appUser = atdAdmin.AddAppUser(payload);
                }

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.AppUserId),
            new Claim(ClaimTypes.Name, appUser.EmailAddress),
            new Claim(ClaimTypes.Email, appUser.EmailAddress),
            //... other claims
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_auth0Settings.ClientSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _auth0Settings.Domain,
                    audience: _auth0Settings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var serializedToken = tokenHandler.WriteToken(token);
                Console.WriteLine($"SERIALIZED TOKEN: {serializedToken}");
                return Ok(new { AccessToken = serializedToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private async Task<string> GetEmailAddress(ExchangeTokenRequest request, string redirect_uri = "ejtictactoedemo://callback")
        {
            if (!string.IsNullOrEmpty(request.Code) && !string.IsNullOrEmpty(request.State))
            {
                // Exchange code and state for an access token
                var codeVerifier = StateStore[request.State].CodeVerifier;
                var tokenClient = new HttpClient();
                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Settings.Domain}/oauth/token")
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"grant_type", "authorization_code"},
                        {"client_id", _auth0Settings.ClientId},
                        {"code_verifier", codeVerifier},
                        {"code", request.Code},
                        {"redirect_uri", redirect_uri}
                    })
                };

                var tokenResponse = await tokenClient.SendAsync(tokenRequest);

                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                if (!tokenResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to exchange code for access token.\n{tokenResponse.ReasonPhrase}\n{JsonConvert.DeserializeObject<dynamic>(tokenContent)?.error_description}");
                }

                var tokenData = JsonConvert.DeserializeObject<TokenResponse>(tokenContent);
                if (tokenData == null || string.IsNullOrEmpty(tokenData.AccessToken))
                {
                    throw new Exception("Invalid access token received.");
                }

                // Now retrieve user info using the new access token
                var client = new AuthenticationApiClient(new Uri($"https://{_auth0Settings.Domain}"));
                var userInfo = await client.GetUserInfoAsync(tokenData.AccessToken);
                return userInfo.Email;
            }

            if (!string.IsNullOrEmpty(request.Auth0Token))
            {
                // Using the token to get user info directly
                var client = new AuthenticationApiClient(new Uri($"https://{_auth0Settings.Domain}"));
                var userInfo = await client.GetUserInfoAsync(request.Auth0Token);
                return userInfo.Email;
            }
            else
            {
                throw new ArgumentException("Invalid request parameters.");
            }
        }

        private class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("id_token")]
            public string IdToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
        }

        public class ExchangeTokenRequest
        {
            public string Auth0Token { get; set; }
            public string Code { get; set; }
            public string State { get; set; }
        }

        [HttpGet("Callback")]
        public async Task<IActionResult> PKCECallback(string code, string state)
        {
            StateStore.TryGetValue(state, out var storedData);
            var codeVerifier = storedData.CodeVerifier;
            var returnUrl = storedData.ReturnUrl;

            var client = new AuthenticationApiClient(_auth0Settings.Domain);

            try
            {
                var tokenRequest = new AuthorizationCodePkceTokenRequest
                {
                    ClientId = _auth0Settings.ClientId,
                    ClientSecret = _auth0Settings.ClientSecret,
                    Code = code,
                    CodeVerifier = codeVerifier,
                    RedirectUri = _auth0Settings.PKCECallbackUrl
                };

                var tokenResponse = await client.GetTokenAsync(tokenRequest);
                var accessToken = tokenResponse.AccessToken;
                var userInfo = await client.GetUserInfoAsync(accessToken);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.UserId),
                    new Claim(ClaimTypes.Name, userInfo.FullName),
                    new Claim(ClaimTypes.Email, userInfo.Email),
                    new Claim("picture", userInfo.Picture),
                    new Claim("email_verified", $"{userInfo.EmailVerified}")
                    //... other claims
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_auth0Settings.ClientSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _auth0Settings.Domain,
                    audience: _auth0Settings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var serializedToken = tokenHandler.WriteToken(token);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    var encodedToken = HttpUtility.UrlEncode(serializedToken);
                    var redirectUrl = $"{returnUrl}?AccessToken={encodedToken}&UserInfo={HttpUtility.UrlEncode(JsonConvert.SerializeObject(userInfo))}";
                    return Redirect(redirectUrl);
                }
                else
                {
                    return Ok(new { AccessToken = serializedToken, UserInfo = userInfo });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}