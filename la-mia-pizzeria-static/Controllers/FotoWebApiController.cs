using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FotoWebApiController : ControllerBase
    {
        private readonly FotosContext _context;

        public FotoWebApiController(FotosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllFotos()
        {
            var fotos = _context.Fotos
                .Where(f => f.Visible)  // Aggiungi filtro per visibilità
                .ToList();
            return Ok(fotos);
        }

        [HttpGet]
        public IActionResult GetFotoById(int id)
        {
            var foto = _context.Fotos
                .Where(f => f.Visible)  // Aggiungi filtro per visibilità
                .FirstOrDefault(f => f.Id == id);
            if (foto == null)
                return NotFound();
            return Ok(foto);
        }

        [HttpGet("{name?}")]
        public IActionResult GetFotoByName(string? name)
        {
            var fotos = string.IsNullOrEmpty(name)
                ? _context.Fotos.Include(f => f.Categories).Where(f => f.Visible).ToList()
                : _context.Fotos.Include(f => f.Categories).Where(f => f.Visible && f.Name.Contains(name)).ToList();

            var fotosWithCategories = fotos.Select(f => new
            {
                f.Id,
                f.Name,
                f.Description,
                f.Image,
                Categories = f.Categories.Select(c => c.Name).ToList()
            });

            return Ok(fotosWithCategories);
        }
    }
}
