using FactureEntities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FactureWeb.Controllers
{
    [Authorize]
    public class FactureController : Controller
    {
        private SqlServerContext _context;

        public FactureController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            List<Facture> liste = _context.Factures.Include(f => f.Client)
                .Include(f => f.LigneFactures).ThenInclude(lf => lf.Article).ToList();
            return View(liste);
        }

        [HttpPost]
        public IActionResult Edit(Facture f)
        {
            Facture f1 = _context.Factures.Find(f.Id);
            f1.ClientId = f.ClientId;
            _context.Factures.Update(f1);
            _context.SaveChanges();
            TempData["message"] = "facture sauvée";
            return RedirectToAction("List");
        }

        public IActionResult Modifier(int id)
        {
            Facture f = _context.Factures.Include(f => f.Client).FirstOrDefault(f => f.Id == id);
            List<Client> listeClients = _context.Clients.OrderBy(c => c.Nom).ThenBy(c => c.Prenom).ToList();
            List<SelectListItem> liste = new List<SelectListItem>();
            foreach(Client c in listeClients)
            {
                liste.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Nom + " " + c.Prenom});
            }
            ViewBag.clients = liste;
            return View(f);
        }
    }
}
