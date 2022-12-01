using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Readit.Models;
using Readit.Models.Entities;
using System.Linq;

namespace Readit.Controllers {
    public class HomeController : Controller {
        private readonly mproulx_5w6_readitContext _context;
        private readonly ISession _session;

        public HomeController(IHttpContextAccessor httpContextAccessor) {
            _context = new mproulx_5w6_readitContext();
            _session = httpContextAccessor.HttpContext.Session;
        }

        public ActionResult Index() {
            var member = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
            if (member.Email != null) {
                ViewBag.connectedUser = member;
                return View("Index", _context.Links.ToList());
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public IActionResult Delete(int Id) {
            var link = _context.Links.Where(l => l.Id.Equals(Id))
                .Include(l => l.Comments)
                .Include(l => l.Votes).First();
            _context.Links.Remove(link);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ViewLink(int Id) {
            ViewBag.connectedUser = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
            return View(_context.Links.Where(l => l.Id == Id).FirstOrDefault());
            ViewBag.connectedUser = JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Id;
            return View(_context.Links.Find(Id));
        }

        public IActionResult Create() {
            return View();
        }

        public IActionResult AjouterUnLien(string Title, string Description) {
            Link link = new Link() {
                Title = Title,
                Description = Description,
                MemberId = JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Id,
                UpVote = 0,
                DownVote = 0,
                PublicationDate = DateTime.Now
            };
            _context.Links.Add(link);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PublierCommentaire(int linkid, int memberid, string comment) {
            if (comment != null) {
                var verification = comment.Split(' ');
                if (verification.Count() > 0) {
                    Comment commentaire = new Comment() {
                        LinkId = linkid,
                        MemberId = memberid,
                        Content = comment,
                        PublicationDate = DateTime.Now
                    };
                    _context.Comments.Add(commentaire);
                    _context.SaveChanges();
                }
            }
            else {
                ViewBag.Alert = String.Format("Votre commentaire doit contenir au minimum un charactère");
            }
            return RedirectToAction("ViewLink", new { Id = linkid });
        }

        public IActionResult Logout() {
            _session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult isLoggedIn() {
            return View("Index");
        }
    }
}