using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class Cita_B
    {
        private readonly ClinicaDbContext _context;

        public Cita_B(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response AgendarCita(CitaDto cita)
        {
            try
            {
                // Validación
                if (cita.ID_MEDICO <= 0 ||
                    cita.ID_USUARIO <= 0 ||
                    cita.ID_HORARIO <= 0 ||
                    cita.DIA <= 0 ||
                    cita.MES <= 0 ||
                    cita.ANIO <= 0)
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios. Verificá que ningún valor esté vacío o inválido."
                    };
                }

                CitaDB citaRef = new CitaDB(_context);
                return citaRef.AgendarCita(cita);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = (int)ResultCode.ErrorInterno,
                    Message = "Error al agendar la cita.",
                    Content = ex.Message
                };
            }
        }

        public Response ObtenerDisponibilidad(DisponibilidadRequestDto dto)
        {
            try
            {
                // Validación
                if (dto.ID_MEDICO <= 0 || dto.DIA <= 0 || dto.MES <= 0 || dto.ANIO <= 0)
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios. Verificá que ningún valor esté vacío o inválido."
                    };
                }

                CitaDB citaRef = new CitaDB(_context);
                return citaRef.ObtenerDisponibilidad(dto);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = (int)ResultCode.ErrorInterno,
                    Message = "Error al consultar disponibilidad.",
                    Content = ex.Message
                };
            }
        }

        public Response ObtenerCitasPorDia(int idUsuario, int dia, int mes, int anio)
        {
            if (idUsuario <= 0 || dia <= 0 || mes <= 0 || anio <= 0)
            {
                return new Response
                {
                    Code = (int)ResultCode.DatosIncompletos,
                    Message = "Todos los campos son obligatorios para consultar las citas."
                };
            }

            CitaDB citaRef = new CitaDB(_context);
            return citaRef.ObtenerCitasPorDia(idUsuario, dia, mes, anio);
        }
    }
}
