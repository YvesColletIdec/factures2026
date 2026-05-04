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
            List<Article> liste = _context.Articles.Where(a => a.Actif).ToList();
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //retrouver avec la PK
            Article art = _context.Articles.Find(id);
            //retrouver le premier avec n'importe quel champ
            Article art2 = _context.Articles.FirstOrDefault(a => a.Id == id);
            //retrouver tous et prendre le premier
            Article art3 = _context.Articles.Where(a => a.Id == id).FirstOrDefault();
            return View(art);
        }

        [HttpPost]
        public IActionResult Edit(Article a)
        {
            if (ModelState.IsValid)
            {
                _context.Articles.Update(a);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View(a);
            }

        }
    }
}
