using Microsoft.AspNetCore.Mvc;

namespace ASPNet_REST_API.Controllers
{
    [Route("/Swagger")]
    public class SwaggerController : Controller
    {
        public IActionResult Index()
        {
            return Json("Swagger docs only available in development environments.");
        }
    }
}
