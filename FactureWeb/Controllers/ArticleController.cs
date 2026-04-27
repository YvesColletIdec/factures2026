using FactureEntities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FactureWeb.Controllers
{
    public class ArticleController : Controller
    {
        private SqlServerContext _context;
        public ArticleController(SqlServerContext context) 
        { 
            _context = context;
        }

        //https://localhost/Article/List
        public IActionResult List()
        {
            //Views/Article/List.cshtml
            //select * from article
            List<Article> liste = _context.Articles.ToList();
            return View(liste);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article a)
        {
            if (ModelState.IsValid)
            {
                _context.Articles.Add(a);
                _context.SaveChanges();
                return RedirectToAction("List");
            } else
            {
                return View(a);
            }
            
        }
    }
}
