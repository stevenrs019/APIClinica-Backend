using APIClinica.Business;
using APIClinica.Data;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialClinicoController : ControllerBase
    {
        private readonly HistorialClinico_B _historialService;

        public HistorialClinicoController(ClinicaDbContext context)
        {
            _historialService = new HistorialClinico_B(context);
        }

        /// <summary>
        /// Registra un nuevo historial clínico con prescripción, diagnóstico y receta.
        /// </summary>
        [HttpPost("registrar")]
        public IActionResult RegistrarHistorial([FromBody] HistorialClinicoDto historial)
        {
            var response = _historialService.RegistrarHistorial(historial);
            return Ok(response);
        }
        [HttpGet("obtener-hitorial-paciente")]
        public IActionResult ObtenerHistorialPorPaciente([FromQuery] int idPaciente)
        {
            var response = _historialService.ObtenerHistorialPorPaciente(idPaciente);
            return Ok(response);
        }

        [HttpGet("obtener-hitorial-email")]
        public IActionResult ObtenerHistorialPorCorreo([FromQuery] string correo)
        {
            var response = _historialService.ObtenerHistorialPorCorreo(correo);
            return Ok(response);
        }
    }
}
