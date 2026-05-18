using FactureEntities.Entities;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FactureWeb.Controllers
{
    public class TestController : Controller
    {
        private SqlServerContext _context;
        public TestController(SqlServerContext context)
        {
            _context  = context;
        }
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

        //public IActionResult ChiffreTousLesMotsDePasse()
        //{
        //    List<Vendeur> liste = _context.Vendeurs.ToList();
        //    foreach (Vendeur v in liste)
        //    {
        //        v.MotDePasse = Security.Hash(v.MotDePasse);
        //    }
        //    _context.SaveChanges();
        //    TempData["ok"] = "mots de passe changés";
        //    return View("~/Home/Index");
        //}
    }
}
