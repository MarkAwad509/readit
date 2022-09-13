using Microsoft.AspNetCore.Mvc;
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
            dao.ConfirmPerson(user,pass);
            
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
