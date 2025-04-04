using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class CitaDB
    {
        private readonly ClinicaDbContext _context;

        public CitaDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response AgendarCita(CitaDto cita)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_AGENDAR_CITA";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_MEDICO", cita.ID_MEDICO));
                    command.Parameters.Add(new SqlParameter("@ID_PACIENTE", cita.ID_PACIENTE));
                    command.Parameters.Add(new SqlParameter("@ID_HORARIO", cita.ID_HORARIO));
                    command.Parameters.Add(new SqlParameter("@DIA", cita.DIA));
                    command.Parameters.Add(new SqlParameter("@MES", cita.MES));
                    command.Parameters.Add(new SqlParameter("@ANIO", cita.ANIO));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int messageId = Convert.ToInt32(reader["message_id"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = messageId == 0 ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
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
                res.Message = "Se ha presentado un inconveniente";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public Response ObtenerDisponibilidad(DisponibilidadRequestDto request)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_OBTENER_DISPONIBILIDAD_MEDICO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_MEDICO", request.ID_MEDICO));
                    command.Parameters.Add(new SqlParameter("@DIA", request.DIA));
                    command.Parameters.Add(new SqlParameter("@MES", request.MES));
                    command.Parameters.Add(new SqlParameter("@ANIO", request.ANIO));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int code = Convert.ToInt32(reader["code"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = code == 0 ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
                            res.Message = message;

                            if (code == 0)
                            {
                                var horarios = new List<object>();

                                while (reader.Read())
                                {
                                    horarios.Add(new
                                    {
                                        ID_HORARIO = reader["ID_HORARIO"],
                                        HORA_INICIO = reader["HORA_INICIO"],
                                        HORA_FIN = reader["HORA_FIN"],
                                        ESTADO = reader["ESTADO"]
                                    });
                                }

                                res.Content = horarios;
                            }
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
                res.Message = "Se ha presentado un inconveniente";
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
