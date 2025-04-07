using APIClinica.Business;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly Cita_B _citaNegocio;

        public CitaController(Cita_B citaNegocio)
        {
            _citaNegocio = citaNegocio;
        }

        [HttpPost("agendar")]
        public IActionResult AgendarCita([FromBody] CitaDto cita)
        {
            try
            {
                var resultado = _citaNegocio.AgendarCita(cita);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = (int)ResultCode.ErrorInterno,
                    message = "Ocurrió un error al agendar la cita.",
                    detail = ex.Message
                });
            }
        }

        [HttpPost("disponibilidad")]
        public IActionResult ObtenerDisponibilidad([FromBody] DisponibilidadRequestDto request)
        {
            try
            {
                var resultado = _citaNegocio.ObtenerDisponibilidad(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = (int)ResultCode.ErrorInterno,
                    message = "Ocurrió un error al consultar disponibilidad.",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("citas-dia")]
        public IActionResult ObtenerCitasPorDia([FromQuery] int idUsuario, [FromQuery] int dia, [FromQuery] int mes, [FromQuery] int anio)
        {
            var result = _citaNegocio.ObtenerCitasPorDia(idUsuario, dia, mes, anio);
            return Ok(result);
        }
    }
}
