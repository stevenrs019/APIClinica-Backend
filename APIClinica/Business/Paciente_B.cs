using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class Paciente_B
    {
        private readonly PacienteDB _pacienteDB;

        public Paciente_B(PacienteDB pacienteDB)
        {
            _pacienteDB = pacienteDB;
        }

        public Response InsertarPaciente(PacienteInsertarDto paciente)
        {
            return _pacienteDB.InsertarPaciente(paciente);
        }

        public Response ModificarPaciente(PacienteDto paciente)
        {
            return _pacienteDB.ModificarPaciente(paciente);
        }

        public Response EliminarPaciente(int id)
        {
            return _pacienteDB.EliminarPaciente(id);
        }
    }
}

