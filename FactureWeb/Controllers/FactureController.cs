using FactureEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FactureWeb.Controllers
{
    public class FactureController : Controller
    {
        private SqlServerContext _context;

        public FactureController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            List<Facture> liste = _context.Factures.Include(f => f.Client).ToList();
            return View(liste);
        }
    }
}
