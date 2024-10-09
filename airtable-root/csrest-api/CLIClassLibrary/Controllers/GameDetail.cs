using CLIClassLibrary.ATDMQ;
using CLIClassLibrary.RoleHandlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; // changed.
using System.Text;

namespace CLIClassLibrary.Controllers
{
    public class GameDetailsController : Controller
    {
        public IActionResult Index()
        {
            var admin = new ATDAdmin();
            var payload = admin.CreatePayload();
            return Json(admin.GetGameDetails(payload));

        }
    }
}
