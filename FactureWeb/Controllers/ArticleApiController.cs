using FactureEntities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FactureWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleApiController : ControllerBase
    {
        private SqlServerContext _context;
        public ArticleApiController(SqlServerContext context)
        {
            _context = context;
        }

        [HttpGet("Toto")]
        public IActionResult GetArticleById(int id)
        {
            Article art = _context.Articles.FirstOrDefault(a => a.Id == id);
            //art = _context.Articles.Find(id);
            if (art == null)
            {
                return NotFound($"pas trouvé l'article n° {id}");//204
            } else
            {
                return Ok(art);//200
            }
        }
    }
}
