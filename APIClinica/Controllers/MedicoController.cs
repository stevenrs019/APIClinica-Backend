using APIClinica.Models.DTO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : ControllerBase
{
    private readonly Medico_B _medicoNegocio;

    public MedicoController(Medico_B medicoNegocio)
    {
        _medicoNegocio = medicoNegocio;
    }

    [HttpGet("obtener-por-especialidad/{idEspecialidad}")]
    public IActionResult ObtenerMedicosPorEspecialidad(int idEspecialidad)
    {
        var response = _medicoNegocio.ObtenerPorEspecialidad(idEspecialidad);
        return Ok(response);
    }
    [HttpPost("insertar")]
    public IActionResult Insertar([FromBody] MedicoInsertarDto medico)
    {
        var res = _medicoNegocio.InsertarMedico(medico);
        return Ok(res);
    }

    [HttpPut("modificar")]
    public IActionResult Modificar([FromBody] MedicoDto medico)
    {
        var res = _medicoNegocio.ModificarMedico(medico);
        return Ok(res);
    }

    [HttpDelete("eliminar/{id}")]
    public IActionResult Eliminar(int id)
    {
        var res = _medicoNegocio.EliminarMedico(id);
        return Ok(res);
    }
}
