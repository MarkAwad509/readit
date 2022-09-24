using Microsoft.AspNetCore.Mvc;
using Readit.Models.DAO;
using Readit.Models.Entities;

namespace Readit.Controllers {
    public class SignUpController : Controller {
        MemberDAO mDAO;

        public SignUpController(IConfiguration configuration) {
            this.mDAO = new MemberDAO(configuration);
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult CreateMember(string Username, string Email, string Password) {
            var member = new Member(Username, Email, Password);
            return mDAO.AddMember(member) ? RedirectToAction("Index","Home") : View();
        }
    }
}
