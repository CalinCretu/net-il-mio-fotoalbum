using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessageWebApiController : ControllerBase
    {
        private readonly FotosContext _context;

        public ContactMessageWebApiController(FotosContext context)
        {
            _context = context;
        }

        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromBody] ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(contactMessage);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
