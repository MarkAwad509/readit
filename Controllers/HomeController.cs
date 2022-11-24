using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Readit.Models.DAO;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Readit.Models.Entities;
using Readit.Models.DAL;

namespace Readit.Controllers
{
    public class HomeController : Controller
    {
        MemberDAO memberDAO;
        LinkDAO linkDAO;
        CommentDAO commentDAO;
        private readonly ISession _session;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            memberDAO = new MemberDAO(configuration);
            linkDAO = new LinkDAO(configuration);
            commentDAO = new CommentDAO(configuration);
            _session = httpContextAccessor.HttpContext.Session;
        }

        public ActionResult Index() {
            
            if (JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Email!="")
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
            return RedirectToAction("Index");
        }
        public IActionResult ViewLink(int Id)
        {
            ViewBag.connectedUser = JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Id;
            return View(linkDAO.GetLinkByID(Id));
        }
        public IActionResult AjoutLien()
        {
            return View();
        }

        public IActionResult Create()
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
            return RedirectToAction("Index");
        }

        public IActionResult PublierCommentaire(int linkid, int memberid, string comment)
        {
            var verification = comment.Split(" ");
            if (comment != "" | verification.Count() == 0)
            {
                Comment commentaire = new Comment()
                {
                    LinkId = linkid,
                    MemberId = memberid,
                    Content = comment,
                    PublicationDate = DateTime.Now
                };
                commentDAO.AddComment(commentaire);
            }
            else
            {
                ViewBag.Alert = String.Format("Votre commentaire doit contenir au minimum un charactère");
            }
            return RedirectToAction("ViewLink", new { Id = linkid });
        }
        public IActionResult Logout()
        {
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