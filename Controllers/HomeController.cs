using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Readit.Models;
using Readit.Models.DAO;
using System.Diagnostics;
namespace Readit.Controllers
{
    public class HomeController : Controller
    {
        MemberDAO memberDAO;
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration)
        {
            _logger = logger;
            memberDAO = new MemberDAO(configuration);
        }

        public ActionResult Index(string Member) {

            if (Member != null)
            {
                ViewBag.connectedUser = memberDAO.GetMemberByUsername(Member);
                return View();
            }
            else {
             
                return RedirectToAction("Index","Login");
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}