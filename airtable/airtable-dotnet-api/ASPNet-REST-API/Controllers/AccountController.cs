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

namespace ASPNet_REST_API.Controllers
{
    [Route("/Account")]
    public class AccountController : Controller
    {
        protected readonly Auth0Settings _auth0Settings;

        public AccountController(IOptions<Auth0Settings> auth0Settings)
        {
            _auth0Settings = auth0Settings.Value;
        }

        [HttpGet("Login")]
        public async Task Login(string returnUrl = "/app")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                       // Indicate here where Auth0 should redirect the user after a login.
                       // Note that the resulting absolute Uri must be added to the
                       // **Allowed Callback URLs** settings for the app.
                       .WithRedirectUri(returnUrl)
                       .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("WhoAmI")]
        public IActionResult Profile()
        {
            return GetProfile();
        }

        private IActionResult GetProfile()
        {
            var claims = User.Claims.ToList();
            var user = new
            {
                User.Identity?.Name,
                IsValidated = User.Claims.FirstOrDefault(c => c.Type == "email_verified")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                LogoutUrl = $"http://localhost:3000/"
            };
            var json = JsonConvert.SerializeObject(user);
            return Json(user);
        }

        public static bool LoggingOut = false;
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (LoggingOut)
            {
                LoggingOut = false;
                return Redirect("/account/login");
            }
            LoggingOut = true;
            
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .Build();

            
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            var logoutUrl = $"https://{_auth0Settings.Domain}/v2/logout?client_id={_auth0Settings.ClientId}";
            return Redirect(logoutUrl);
        }
    }

}