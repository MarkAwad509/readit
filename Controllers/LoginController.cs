using Microsoft.AspNetCore.Mvc;
using Readit.Models.Entities;

namespace ProjetWebServer_.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult data(string user,string pass)
        {
            return View();
        }
    }
}
