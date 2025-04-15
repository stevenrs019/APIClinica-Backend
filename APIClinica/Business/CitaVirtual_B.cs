using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class CitaVirtual_B
    {
        private readonly ClinicaDbContext _context;

        public CitaVirtual_B(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response AgendarCitaVirtual(CitaDto cita)
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

                CitaVirtualDB citaRef = new CitaVirtualDB(_context);
                return citaRef.AgendarCitaVirtual(cita);
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

        

        
    }
}
