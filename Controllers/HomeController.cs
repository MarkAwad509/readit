using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Readit.Models.DAO;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Readit.Models.Entities;

namespace Readit.Controllers
{
    public class HomeController : Controller
    {
        MemberDAO memberDAO;
        private readonly ISession _session;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            memberDAO = new MemberDAO(configuration);
            _session = httpContextAccessor.HttpContext.Session;
        }

        public ActionResult Index() {
            
            if (JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Email!=null)
            {
                ViewBag.connectedUser = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
                return View("Index");
            }
            else {
             
                return RedirectToAction("Index","Login");
            }
            
        }
        public IActionResult Logout()
        {
            _session.SetString("user", null);
            return View("Index", "Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult isLoggedIn() {
            return View("Index");
        }
    }
}