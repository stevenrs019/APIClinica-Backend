using APIClinica.Data;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.EntityFrameworkCore;

public class Medico_B
{
    private readonly MedicoDB _medicoDB;
    private readonly ClinicaDbContext _context;

    public Medico_B(MedicoDB medicoDB, ClinicaDbContext context)
    {
        _medicoDB = medicoDB;
        _context = context;
    }
    public Response ObtenerPorEspecialidad(int idEspecialidad)
    {
        if (idEspecialidad <= 0)
        {
            return new Response
            {
                Code = (int)ResultCode.DatosIncompletos,
                Message = "El ID de la especialidad no es válido."
            };
        }

        MedicoDB medicoRef = new MedicoDB(_context);
        return medicoRef.ObtenerMedicosPorEspecialidad(idEspecialidad);
    }
    public Response InsertarMedico(MedicoInsertarDto medico) => _medicoDB.Insertar(medico);

    public Response ModificarMedico(MedicoDto medico) => _medicoDB.Modificar(medico);

    public Response EliminarMedico(int idMedico) => _medicoDB.Eliminar(idMedico);
}
