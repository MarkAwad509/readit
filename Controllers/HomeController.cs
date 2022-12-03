using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Readit.Models;
using Readit.Models.Entities;
using System.Diagnostics.Metrics;

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
                ViewBag.votes = _context.Votes.Where(v => v.MemberId == member.Id).ToList();
                return View("Index", _context.Links.OrderByDescending(w => w.PublicationDate).ToList());
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public IActionResult Thumbs(int LinkId, int MemberId, bool updown)
        {

            Vote vote = new Vote()
            {
                LinkId = LinkId,
                MemberId = MemberId,
                IsUpVote = updown
            };
            _context.Add<Vote>(vote);
            
            _context.SaveChanges();
            _context.Entry<Vote>(vote).Reload();

            return RedirectToAction("Index");

        }
        
        public IActionResult ThumbsUpdate(int LinkId)
        {
            Vote vote = _context.Votes.Where(v => v.LinkId == LinkId && v.MemberId == JsonConvert.DeserializeObject<Member>(_session.GetString("user")).Id).First();
            _context.Votes.Remove(vote);
            _context.SaveChanges();
            if (vote.IsUpVote == false)
            {
                vote.IsUpVote = true;
            }
            else
            {
                vote.IsUpVote = false;
            }
            _context.Add<Vote>(vote);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int Id) {
             foreach(var item in _context.Comments)
            {
                if (item.LinkId == Id)
                {
                    _context.Comments.Remove(item);
                }
                }
            foreach (var item in _context.Votes)
            {
                if (item.LinkId == Id)
                {
                    _context.Votes.Remove(item);
                }
            }
            _context.Links.Remove(_context.Links.Where(l=>l.Id==Id).First());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ViewLink(int Id) {
            ViewBag.connectedUser = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
            return View(_context.Links.Where(l => l.Id == Id).FirstOrDefault());
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