using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class HistorialClinico_B
    {
        private readonly ClinicaDbContext _context;

        public HistorialClinico_B(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response RegistrarHistorial(HistorialClinicoDto historial)
        {
            try
            {
                if (historial.IdCita <= 0 ||
                    string.IsNullOrWhiteSpace(historial.Prescripcion) ||
                    string.IsNullOrWhiteSpace(historial.Diagnostico) ||
                    string.IsNullOrWhiteSpace(historial.Receta))
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios. Verificá que ningún valor esté vacío o inválido."
                    };
                }

                // Convertimos al DTO completo si es necesario para la capa DB
                var dto = new HistorialClinicoDto
                {
                    IdCita = historial.IdCita,
                    Prescripcion = historial.Prescripcion,
                    Diagnostico = historial.Diagnostico,
                    Receta = historial.Receta
                };

                HistorialClinicoDB historialRef = new HistorialClinicoDB(_context);
                return historialRef.RegistrarHistorialClinico(dto);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = (int)ResultCode.ErrorInterno,
                    Message = "Error al registrar el historial clínico.",
                    Content = ex.Message
                };
            }
        }

        public Response ObtenerHistorialPorPaciente(int idPaciente)
        {
            if (idPaciente <= 0)
            {
                return new Response
                {
                    Code = (int)ResultCode.DatosIncompletos,
                    Message = "El ID del paciente no es válido."
                };
            }

            HistorialClinicoDB historialRef = new HistorialClinicoDB(_context);
            return historialRef.ObtenerHistorialPorPaciente(idPaciente);
        }
        public Response ObtenerHistorialPorCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return new Response
                {
                    Code = (int)ResultCode.DatosIncompletos,
                    Message = "El correo del usuario es requerido."
                };
            }

            HistorialClinicoDB historialRef = new HistorialClinicoDB(_context);
            return historialRef.ObtenerHistorialPorCorreo(correo);
        }

    }
}
