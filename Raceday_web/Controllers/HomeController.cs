using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Raceday_web.Models;

namespace Raceday_web.Controllers
{
    public class HomeController : Controller
    {
    
        
            public IActionResult Index()
            {
                int? userId = HttpContext.Session.GetInt32("UserId");

                if (userId != null)
                {
                    return RedirectToAction("Dashboard");
                }

                return View();
            }

            public IActionResult Dashboard()
            {
                int? userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    return RedirectToAction("Index");
                }

                string role = HttpContext.Session.GetString("Role") ?? "";

                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.Role = role;

                return View();
            }

            public IActionResult Error()
            {
                return View();
            }
        }
}
