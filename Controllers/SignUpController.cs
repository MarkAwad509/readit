using Microsoft.AspNetCore.Mvc;
using Readit.Models.DAO;
using Readit.Models.Entities;
using Microsoft.AspNetCore.Http;
namespace Readit.Controllers {
    public class SignUpController : Controller {
        MemberDAO mDAO;
        private readonly ISession _session;
        public SignUpController(IConfiguration configuration,IHttpContextAccessor httpContextAccessor) {
            this.mDAO = new MemberDAO(configuration);
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index() {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(string Username, string Email, string Password) {
            var member = new Member(Username, Email, Password);

            return mDAO.AddMember(member) ? RedirectToAction("Index","Home",new {Member = member.Username}) : View();

        }
    }
}
