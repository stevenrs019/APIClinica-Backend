using APIClinica.Business;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Usuario_B _usuarioNegocio;

        public UsuarioController(Usuario_B usuarioNegocio)
        {
            _usuarioNegocio = usuarioNegocio;
        }

        [HttpPost("insertar")]
        public IActionResult InsertarUsuario([FromBody] UsuarioDto usuario)
        {
            try
            {
                var resultado = _usuarioNegocio.InsertarUsuario(usuario);
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            try
            {
                var resultado = _usuarioNegocio.LoginUsuario(login);
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

        [HttpPut("modificar/{id}")]
        public IActionResult ModificarUsuario(int id, [FromBody] UsuarioDto usuario)
        {
            try
            {
                var resultado = _usuarioNegocio.ModificarUsuario(id, usuario);
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

        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                var resultado = _usuarioNegocio.EliminarUsuario(id);
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
