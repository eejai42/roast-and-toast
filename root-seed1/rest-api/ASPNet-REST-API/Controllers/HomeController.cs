using Microsoft.AspNetCore.Mvc;

namespace ASPNet_REST_API.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Redirect("/app/");
        }
    }
}
