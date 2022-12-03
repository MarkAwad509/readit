using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Readit.Models;
using Readit.Models.Entities;

namespace Readit.Controllers {
    public class ProfileController : Controller {
        private readonly mproulx_5w6_readitContext _context;
        private readonly ISession _session;

        public ProfileController(IHttpContextAccessor httpContextAccessor) {
            _context = new mproulx_5w6_readitContext();
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult ProfileView() {
            var user = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
            var currentUser = _context.Members.Find(user.Id);
            if (user.Comments != null || user.Links != null || user.Votes != null){
                ViewBag.CommentCount = currentUser.Comments.Count;
                ViewBag.PostCount = currentUser.Links.Count;
                ViewBag.LikesCount = countScore(currentUser);
            } else {
                ViewBag.CommentCount = 0;
                ViewBag.PostCount = 0;
                ViewBag.LikesCount = 0;
            }
            ViewBag.user = currentUser;
            ViewBag.links = _context.Links.Where(l => l.MemberId == user.Id).OrderByDescending(l => l.PublicationDate).ToList();
            return View("ProfileView");
        }

        private int countScore(Member member) {
            int count = 0;
            foreach(var link in member.Links)
                count = count + (int)(link.UpVote - link.DownVote);
            return count;
        }

        public IActionResult EditProfile() {
            ViewBag.CurrentMember = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
            return View("ProfileEdit");
        }
    }
}
