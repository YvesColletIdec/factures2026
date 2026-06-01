using FactureEntities.Entities;
using FactureWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
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

        public IActionResult CreateNewFacture(List<LigneFacture> LignesFacture, DateTime DateFacture, int ClientId, string Numero)
        {
            Facture facture = new Facture();
            facture.DateFacture = DateOnly.FromDateTime(DateFacture);
            facture.ClientId = ClientId;
            facture.LigneFactures = LignesFacture;
            facture.Numero = Numero;
            facture.VendeurId = Convert.ToInt32(User.FindFirst("id")?.Value);
            _context.Add(facture);
            _context.SaveChanges();
            return View("Liste");
        }

        public IActionResult Create()
        {
            //création de la liste des clients
            List<SelectListItem> listClients = new List<SelectListItem>();
            foreach (Client c in _context.Clients)
            {
                listClients.Add(new SelectListItem() { Text = c.Prenom + " " + c.Nom, Value = c.Id.ToString() });
            }
            ViewData["Clients"] = listClients;
            //transformation de la liste des articles en json pour javascript plus tard
            ViewBag.ArticlesJson = JsonConvert.SerializeObject(_context.Articles);
            return View(new Facture() { DateFacture = DateOnly.FromDateTime(DateTime.Now) });
        }


        public IActionResult Impression(int id)
        {
            Facture f = _context.Factures.Include(x => x.LigneFactures).ThenInclude(x => x.Article).Include(x => x.Client).FirstOrDefault(x => x.Id == id);
            string physicalPath = Print.CreateDocumentFromTemplateWithFormat(f, @"C:\Users\yvesc\Documents\GitHub\factures2026\facture_template.docx");
            byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);
            MemoryStream ms = new MemoryStream(pdfBytes);
            return new FileStreamResult(ms, "application/pdf");
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
