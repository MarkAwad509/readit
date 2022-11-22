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
        LinkDAO linkDAO;
        private readonly ISession _session;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            memberDAO = new MemberDAO(configuration);
            linkDAO = new LinkDAO(configuration);
            _session = httpContextAccessor.HttpContext.Session;
        }

        public ActionResult Index() {
            
            if (JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Email!=null)
            {
                ViewBag.connectedUser = JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Id;
                return View("Index",linkDAO.getLinks());
            }
            else {
             
                return RedirectToAction("Index","Login");
            }
            
        }
        public IActionResult Delete(int Id)
        {
            linkDAO.DeleteLink(linkDAO.GetLinkByID(Id));
            return View("Index");
        }
        public IActionResult AjoutLien()
        {
            return View();
        }
        public IActionResult AjouterUnLien(string Title,string Description)
        {
            
            Link link = new Link()
            {
                Title = Title,
                Description = Description,
                MemberId = JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Id,
                UpVote = 0,
                DownVote = 0,
                PublicationDate = DateTime.Now
            };
            linkDAO.AddLink(link);            
            return View("Index", linkDAO.getLinks());
        }

        public IActionResult Logout(){
            _session.Clear();
            return RedirectToAction("Index", "Login");
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