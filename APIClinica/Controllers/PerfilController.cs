using APIClinica.Business;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly Perfil_B _perfilNegocio;

        public PerfilController(Perfil_B perfilNegocio)
        {
            _perfilNegocio = perfilNegocio;
        }

        [HttpGet("obtener")]
        public IActionResult ObtenerPerfil([FromQuery] string correo)
        {
            try
            {
                var resultado = _perfilNegocio.ObtenerPerfil(correo);
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

        [HttpPost("actualizar")]
        public IActionResult ActualizarPerfil([FromBody] PerfilDto perfil)
        {
            try
            {
                var resultado = _perfilNegocio.ActualizarPerfil(perfil);
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
