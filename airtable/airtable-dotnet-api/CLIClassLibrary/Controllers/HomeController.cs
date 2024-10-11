using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLIClassLibrary.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            var actions = new List<Endpoint>(new Endpoint[]
            {
                new Endpoint()
                {
                    Name = "Invoices",
                    Method = "GET",
                    Url = "/Invoices",
                    Description = "Get's a list of invoices from the data source."
                },
                new Endpoint()
                {
                    Name = "Games",
                    Method = "GET",
                    Url = "/Games",
                    Description = "Get's a list of Games from the data source."
                }
            ,
                new Endpoint()
                {
                    Name = "GameDetails",
                    Method = "GET",
                    Url = "/GameDetails",
                    Description = "Get's a list of GameDetails from the data source."
                }
            ,
                new Endpoint()
                {
                    Name = "Characters",
                    Method = "GET",
                    Url = "/Characters",
                    Description = "Get's a list of Characters from the data source."
                }
            ,
                new Endpoint()
                {
                    Name = "AppUsers",
                    Method = "GET",
                    Url = "/AppUsers",
                    Description = "Get's a list of AppUsers from the data source."
                }
            ,
                new Endpoint()
                {
                    Name = "Weapons",
                    Method = "GET",
                    Url = "/Weapons",
                    Description = "Get's a list of Weapons from the data source."
                }
            ,
                new Endpoint()
                {
                    Name = "Levels",
                    Method = "GET",
                    Url = "/Levels",
                    Description = "Get's a list of Levels from the data source."
                }
            
            });
            return Json(actions);
        }
    }
}