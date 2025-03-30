using APIClinica.Data;
using APIClinica.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public PersonaController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: api/persona
        [HttpGet]
        public IActionResult GetPersonas()
        {
            var personas = _context.Personas.ToList();
            return Ok(personas);
        }

        // POST: api/persona
        [HttpPost]
        public IActionResult CrearPersona([FromBody] Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
            return Ok(new { message = "Persona creada correctamente", persona });
        }
    }
}
