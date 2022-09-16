using Microsoft.AspNetCore.Mvc;
using Readit.Models;

namespace Readit.Controllers {
    public class SignUpController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult CreateUser() {
            return View("../Home/Index");
        }
    }
}
