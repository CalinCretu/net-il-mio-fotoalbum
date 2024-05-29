using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FotoWebApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllFotos()
        {
            var fotos = FotoManager.GetAllFotos();
            return Ok(fotos);
        }
        

        [HttpGet]
        public IActionResult GetFotoById(int id)
        {
            var foto = FotoManager.VediFoto(id);
            if (foto == null)
                return NotFound();
            return Ok(foto);
        }

        [HttpGet("{name?}")]
        public IActionResult GetFotoByName(string? name)
        {
            if (name == null)
            {
                return Ok(FotoManager.GetAllFotos());
            }
            //var foto = FotoManager.GetFotoByName(name);
            return Ok(FotoManager.GetFotoByName(name));
        }

        [HttpPost]
        public IActionResult CreateFoto([FromBody] Fotos fotos)
        {
            FotoManager.InsertFoto(fotos, null);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateFoto(int id, [FromBody] Fotos fotos)
        {
            var oldFoto = FotoManager.VediFoto(id);
            if (oldFoto == null)
                return NotFound();

            var success = FotoManager.UpdateFoto(
                id,
                fotos.Name,
                fotos.Description,
                null // Passiamo null per le categorie per ora
            );

            if (!success)
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore durante l'aggiornamento della foto.");

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFoto(int id)
        {
            bool success = FotoManager.DeleteFoto(id);
            if (!success)
                return NotFound();
            return Ok();
        }
    }
}
