using Microsoft.AspNetCore.Mvc;
using Readit.Models.Entities;
using Newtonsoft.Json;
using MySqlConnector;
using Readit.Models;

namespace Readit.Controllers
{
    public class SignUpController : Controller {
        private readonly ISession _session;
        private readonly mproulx_5w6_readitContext _context;
        public SignUpController(IHttpContextAccessor httpContextAccessor) {
            _context = new mproulx_5w6_readitContext();
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index() {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(Member member) {
            try {
                _context.Members.Add(member);
                _context.SaveChanges();
            } catch (MySqlException ex) {
                ViewBag.Alert = $"Exception: {ex.InnerException}, Message: {ex.Message}";
                return View("Index");
            }

            _session.SetString("user", JsonConvert.SerializeObject(member));
            return RedirectToAction("Index", "Home");
        }
    }
}
