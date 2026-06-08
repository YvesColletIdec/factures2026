using FactureEntities.Entities;
using FactureWeb.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FactureWeb.Controllers
{
    public class RootController : Controller
    {
        protected SqlServerContext _context;

        public RootController(SqlServerContext context)
        {
            _context = context;
        }

        public void LogInfo(string message)
        {
            Log(message, ETypeLog.INFO);
        }

        public void LogErreur(string message)
        {
            Log(message, ETypeLog.ERREUR);
        }

        private void Log(string message, ETypeLog typelog)
        {
            Log l = new Log();
            l.Message = message;
            l.Typelog = typelog.ToString();
            l.Login = User.Identity.Name;
            _context.Logs.Add(l);
            _context.SaveChanges();
        }
    }
}
