using Microsoft.AspNetCore.Mvc;

namespace ProjetWebServer_.Controllers
{
    public class InscriptionController : Controller
    {
        public IActionResult Index()
        {
            return View("Inscription");
        }

        public IActionResult data()
        {
            return View("~/Views/Shared/Login.cshtml");
        }
    }
}
