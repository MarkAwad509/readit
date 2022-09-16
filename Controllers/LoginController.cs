using Microsoft.AspNetCore.Mvc;
using projetwebserver.Models;
using ProjetWebServer_.DAL;

namespace ProjetWebServer_.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult data(string user,string pass)
        {
            //needs logic for login if else and confirm with  dao.confirmPerson
            ConnexionDAO dao = new ConnexionDAO();
            if(true)
            {
                User testuser = new projetwebserver.Models.User("jeremy", "jeremy", "jeremy");
                ViewBag.ConnectedUser = testuser;
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
             return View("~/Views/Home/Login");
            }
        }
    }
}
