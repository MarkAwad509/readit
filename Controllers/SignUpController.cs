using Microsoft.AspNetCore.Mvc;
using Readit.Models;

namespace Readit.Controllers {
    public class SignUpController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult CreateMember(string Username, string Email, string Password) {
            var member = new Member(Username, Email, Password);
            if (mDAO.AddMember(member)) {
                ViewBag.connectedUser = member;
                return RedirectToAction("isLoggedIn", "Home");
            }
            else {
                return View();
            }
        }
    }
}
