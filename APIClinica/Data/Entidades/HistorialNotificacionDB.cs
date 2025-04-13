using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class HistorialNotificacionDB
    {
        private readonly ClinicaDbContext _context;

        public HistorialNotificacionDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response Insertar(HistorialNotificacionDto dto)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_INSERTAR_HISTORIAL_NOTIFICACION";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_TIPO_NOTIFICACION", dto.ID_TIPO_NOTIFICACION));
                    command.Parameters.Add(new SqlParameter("@EMAIL", dto.EMAIL));
                    command.Parameters.Add(new SqlParameter("@MENSAJE", dto.MENSAJE)); // 💬 Nuevo parámetro

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int code = Convert.ToInt32(reader["code"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = code == 0 ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
                            res.Message = message;
                        }
                        else
                        {
                            res.Code = (int)ResultCode.SP_SinRespuesta;
                            res.Message = "El procedimiento no devolvió respuesta.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Error al insertar historial de notificación.";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }


        public Response Obtener()
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_OBTENER_HISTORIAL_NOTIFICACIONES";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        var historial = new List<object>();

                        while (reader.Read())
                        {
                            historial.Add(new
                            {
                                ID_HISTORIAL_NOTIFICACION = reader["ID_HISTORIAL_NOTIFICACION"],
                                ID_TIPO_NOTIFICACION = reader["ID_TIPO_NOTIFICACION"],
                                ID_NOTIFICACION = reader["ID_NOTIFICACION"],
                                EMAIL = reader["EMAIL"],
                                FECHA_ENVIO = reader["FECHA_ENVIO"]
                            });
                        }

                        res.Code = (int)ResultCode.Exito;
                        res.Message = "Historial obtenido correctamente";
                        res.Content = historial;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Error al obtener historial de notificaciones.";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }
    }
}
