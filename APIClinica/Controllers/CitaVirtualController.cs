using APIClinica.Business;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaVirtualController : ControllerBase
    {
        private readonly CitaVirtual_B _citaNegocio;

        public CitaVirtualController(CitaVirtual_B citaNegocio)
        {
            _citaNegocio = citaNegocio;
        }

        [HttpPost("agendar_virtual")]
        public IActionResult AgendarCitaVirtual([FromBody] CitaDto cita)
        {
            try
            {
                var resultado = _citaNegocio.AgendarCitaVirtual(cita);
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

        
    }
}
