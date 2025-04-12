using APIClinica.Business;
using APIClinica.Data;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialNotificacionController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public HistorialNotificacionController(ClinicaDbContext context)
        {
            _context = context;
        }

        [HttpPost("insertar")]
        public ActionResult<Response> InsertarHistorial([FromBody] HistorialNotificacionDto historial)
        {
            var historialService = new HistorialNotificacion_B(_context);
            var result = historialService.InsertarHistorial(historial);
            return Ok(result);
        }

        [HttpGet("obtener")]
        public ActionResult<Response> ObtenerHistorial()
        {
            var historialService = new HistorialNotificacion_B(_context);
            var result = historialService.ObtenerHistorial();
            return Ok(result);
        }
    }
}
