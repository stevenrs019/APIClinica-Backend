using APIClinica.Business;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly Paciente_B _pacienteNegocio;

        public PacienteController(Paciente_B pacienteNegocio)
        {
            _pacienteNegocio = pacienteNegocio;
        }

        [HttpPost("insertar")]
        public IActionResult Insertar([FromBody] PacienteInsertarDto paciente)
        {
            try
            {
                var resultado = _pacienteNegocio.InsertarPaciente(paciente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado", detalle = ex.Message });
            }
        }

        [HttpPut("modificar")]
        public IActionResult Modificar([FromBody] PacienteDto paciente)
        {
            try
            {
                var resultado = _pacienteNegocio.ModificarPaciente(paciente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado", detalle = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var resultado = _pacienteNegocio.EliminarPaciente(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado", detalle = ex.Message });
            }
        }
    }
}
