using FactureWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FactureWeb.Controllers
{
    public class HomeController : Controller
    {
        //https://localhost:1234/Home/Index
        public IActionResult Index()
        {
            //Views/Home/Index.cshtml
            return View();
        }
        //https://localhost:1234/Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
