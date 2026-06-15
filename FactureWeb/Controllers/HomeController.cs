using FactureEntities.Entities;
using FactureSecurity;
using FactureWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FactureWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlServerContext _context;
        public HomeController(SqlServerContext context)
        {
            this._context = context;
        }
        //https://localhost:1234/Home/Index
        [Authorize]
        public IActionResult Index()
        {
            //Views/Home/Index.cshtml
            return View();
        }
        //https://localhost:1234/Home/Privacy
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModifyPassword(Pwd pwd)
        {
            //check password actuel
            string login = User.Identity.Name;
            Vendeur v = _context.Vendeurs.FirstOrDefault(vx => vx.Identifiant == login);
            if (!ModelState.IsValid)
            {
                return View("Index", pwd);
            } else if (!Security.Verify(pwd.OldPassword, v.MotDePasse))
            {
                TempData["ko"] = "le mot de passe actuel n'est pas le bon.";
                return View("Index", pwd);
            }
            else
            {
                v.MotDePasse = Security.Hash(pwd.NewPassword);
                _context.Update(v);
                _context.SaveChanges();
                TempData["ok"] = "le mot de passe a été changé.";
                return View("Index");
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            TempData["ko"] = "Vous n'avez pas le droit d'accéder à cette page";
            return Redirect("~/Home/Index");
        }

    }
}
