using APIClinica.Business;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly Especialidad_B _especialidadNegocio;

        public EspecialidadController(Especialidad_B especialidadNegocio)
        {
            _especialidadNegocio = especialidadNegocio;
        }

        [HttpGet]
        public IActionResult ObtenerEspecialidades()
        {
            try
            {
                var resultado = _especialidadNegocio.ObtenerEspecialidades();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = (int)ResultCode.ErrorInterno,
                    message = "Ocurrió un error inesperado.",
                    detail = ex.Message
                });
            }
        }
    }
}
