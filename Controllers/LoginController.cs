using Microsoft.AspNetCore.Mvc;
using Readit.Models.Entities;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Readit.Models;
using Microsoft.EntityFrameworkCore;

namespace Readit.Controllers {

    public class LoginController : Controller {
        private readonly mproulx_5w6_readitContext _context;
        private readonly ISession _session;
        public LoginController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
            _context = new mproulx_5w6_readitContext();
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Login(string Email, string password) {
            var currentUser = _context.Members.Where(e => e.Email == Email).FirstOrDefault();
            if (currentUser == null || currentUser.Password != password) {
                ViewBag.Alert = String.Format("Wrong email or password, please try again.");
                return View("Index");
            }
            else {
                _session.SetString("user", JsonConvert.SerializeObject(currentUser, Formatting.None, new JsonSerializerSettings() {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
                return RedirectToAction("Index", "Home");

            }
        }
    }
}

