using FactureEntities.Entities;
using FactureWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace FactureWeb.Controllers
{
    
    public class ArticleController : RootController
    {
        public ArticleController(SqlServerContext context) : base(context) { }

        //https://localhost/Article/List
        public IActionResult List()
        {
            //Views/Article/List.cshtml
            //select * from article
            List<Article> liste = _context.Articles.Where(a => a.Actif).ToList();
            return View(liste);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
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

        
        [Authorize(Roles = "admin, user")]
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

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Article a)
        {
            if (ModelState.IsValid)
            {
                _context.Articles.Update(a);
                _context.SaveChanges();
                //TODO loguer la modif
                LogInfo($"l'article {a.Id} a été modifié");
                return RedirectToAction("List");
            }
            else
            {
                return View(a);
            }
        
        }
    }
}
