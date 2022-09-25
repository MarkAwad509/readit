using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Readit.Models;
using System.Diagnostics;
namespace Readit.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            if (ViewBag.connectedUser.Username != null) {
                ViewBag.connectedUser = ViewBag.connectedUser;
                return View();
            }
            else {

                return RedirectToAction("Index", "Login");
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult isLoggedIn() {
            return View("Index");
        }
    }
}