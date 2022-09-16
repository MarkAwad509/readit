using Microsoft.AspNetCore.Mvc;
using Readit.DAL;

namespace ProjetWebServer_.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
