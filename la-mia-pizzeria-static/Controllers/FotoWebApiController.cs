using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var fotos = _context.Fotos
                .Where(f => f.Visible)  // Aggiungi filtro per visibilità
                .Where(f => string.IsNullOrEmpty(name) || f.Name.Contains(name))
                .ToList();
            return Ok(fotos);
        }

        [HttpPost]
        public IActionResult CreateFoto([FromBody] Fotos fotos)
        {
            _context.Fotos.Add(fotos);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFoto(int id, [FromBody] Fotos fotos)
        {
            var oldFoto = _context.Fotos.Find(id);
            if (oldFoto == null)
                return NotFound();

            oldFoto.Name = fotos.Name;
            oldFoto.Description = fotos.Description;
            oldFoto.Image = fotos.Image;
            oldFoto.Visible = fotos.Visible;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFoto(int id)
        {
            var foto = _context.Fotos.Find(id);
            if (foto == null)
                return NotFound();

            _context.Fotos.Remove(foto);
            _context.SaveChanges();

            return Ok();
        }
    }
}
