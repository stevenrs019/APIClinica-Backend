using APIClinica.Models.DTO;
using APIClinica.Services;

public class Medico_B
{
    private readonly MedicoDB _medicoDB;

    public Medico_B(MedicoDB medicoDB)
    {
        _medicoDB = medicoDB;
    }

    public Response InsertarMedico(MedicoInsertarDto medico) => _medicoDB.Insertar(medico);

    public Response ModificarMedico(MedicoDto medico) => _medicoDB.Modificar(medico);

    public Response EliminarMedico(int idMedico) => _medicoDB.Eliminar(idMedico);
}
