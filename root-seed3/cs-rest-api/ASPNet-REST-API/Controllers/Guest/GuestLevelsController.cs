using Auth0.AuthenticationApi.Models;
using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Mvc;

using CLIClassLibrary.ATDMQ;
using AirtableDirect.CLI.Lib.DataClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using Swan.Formatters;
using YP.SassyMQ.Lib.RabbitMQ;

namespace ASPNet_REST_API.Controllers.Admin
{
    [ApiController]
    [Route("/Guest")]
    public class GuestLevelsController : Controller
    {
        

        [HttpGet("Levels")]
        public IActionResult Index(string airtableWhere = null, string view = null, int maxPages = 5)
        {
            var results = new List<Level>();
            try
            {
                ATDGuest atdGuest = new ATDGuest();
                atdGuest.EmailAddress = this.User.Identity.Name;
                atdGuest.UserIdentity = this.User.Identities.FirstOrDefault();
                var payload = atdGuest.CreatePayload("{}", airtableWhere, view, maxPages);
                results = atdGuest.GetLevels(payload)?.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Levels: {ex.Message}", ex);
            }
            var json = JsonConvert.SerializeObject(results, Formatting.Indented);

            return Content(json, "application/json");

        }
                        

                        

                        
                        
    }
}
                    