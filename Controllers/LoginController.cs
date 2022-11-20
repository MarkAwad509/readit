using Microsoft.AspNetCore.Mvc;
using Readit.Models.DAO;
using Readit.Models.Entities;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace Readit.Controllers {
    
    public class LoginController : Controller {
        MemberDAO memberDAO;
        private readonly ISession _session;
        public LoginController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
            memberDAO = new MemberDAO(configuration);
            _session = httpContextAccessor.HttpContext.Session;
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
            else
            {
                _session.SetString("user", JsonConvert.SerializeObject(currentUser));   
                return RedirectToAction("Index", "Home");

            }

        }
    }
}

