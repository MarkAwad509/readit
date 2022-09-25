using Microsoft.AspNetCore.Mvc;
using Readit.Models.DAO;
using Readit.Models.Entities;

namespace Readit.Controllers {
    public class LoginController : Controller {
        MemberDAO memberDAO;
        public LoginController(IConfiguration configuration) {
            memberDAO = new MemberDAO(configuration);
        }

        public IActionResult Index() {

            return View();
        }

        public IActionResult Login(string Email, string password) {
            Member currentUser = memberDAO.GetMemberByEmail(Email);
            if (currentUser == null || currentUser.Password != password) {
                ViewBag.Alert = String.Format("Wrong Email or Password please try again");
                return View("Index");
            }
            else {
                ViewBag.connectedUser = currentUser;
                return View("..\\..\\Views\\Home\\Index");
            }

        }
    }
}

