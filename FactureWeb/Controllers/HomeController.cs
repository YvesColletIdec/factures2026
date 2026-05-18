using FactureWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FactureWeb.Controllers
{
    public class HomeController : Controller
    {
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
