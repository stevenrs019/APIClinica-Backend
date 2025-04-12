using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class HistorialNotificacion_B
    {
        private readonly ClinicaDbContext _context;

        public HistorialNotificacion_B(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response InsertarHistorial(HistorialNotificacionDto historial)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(historial.EMAIL) ||
                    historial.ID_TIPO_NOTIFICACION <= 0 ||
                    historial.ID_NOTIFICACION <= 0)
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios. Verificá que ningún valor esté vacío o inválido."
                    };
                }

                HistorialNotificacionDB historialRef = new HistorialNotificacionDB(_context);
                return historialRef.Insertar(historial);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = (int)ResultCode.ErrorInterno,
                    Message = "Error al insertar el historial de notificación.",
                    Content = ex.Message
                };
            }
        }

        public Response ObtenerHistorial()
        {
            try
            {
                HistorialNotificacionDB historialRef = new HistorialNotificacionDB(_context);
                return historialRef.Obtener();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = (int)ResultCode.ErrorInterno,
                    Message = "Error al obtener el historial de notificaciones.",
                    Content = ex.Message
                };
            }
        }
    }
}
