using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Readit.Models.Entities;

namespace Readit.Controllers {
    public class ProfileController : Controller {
        private readonly ISession _session;

        public ProfileController(IHttpContextAccessor httpContextAccessor) {
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult ProfileView() {
            var user = JsonConvert.DeserializeObject<Member>(_session.GetString("user"));
            ViewBag.CommentCount = 0;
            ViewBag.PostCount = 0;
            if (user.Comments != null | user.Links != null)
            {
                ViewBag.CommentCount = user.Comments.Count;
                ViewBag.PostCount = user.Links.Count;
            }
            ViewBag.user=user;
            return View("ProfileView", user);
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
