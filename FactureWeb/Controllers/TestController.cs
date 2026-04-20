using Microsoft.AspNetCore.Mvc;

namespace FactureWeb.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Coucou()
        {
            return View();
        }

        public IActionResult Affichage(string prenom)
        {
            string resultat = $"salut {prenom}";
            ViewBag.mavaleur = resultat;
            return View();
        }
    }
}
