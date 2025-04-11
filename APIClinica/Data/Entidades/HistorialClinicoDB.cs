using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class HistorialClinicoDB
    {
        private readonly ClinicaDbContext _context;

        public HistorialClinicoDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response RegistrarHistorialClinico(HistorialClinicoDto historial)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_REGISTRAR_HISTORIAL_CLINICO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_CITA", historial.IdCita ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@PRESCRIPCION", historial.Prescripcion));
                    command.Parameters.Add(new SqlParameter("@DIAGNOSTICO", historial.Diagnostico));
                    command.Parameters.Add(new SqlParameter("@RECETA", historial.Receta)); 

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
                res.Message = "Se ha presentado un inconveniente";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public Response ObtenerHistorialPorPaciente(int idPaciente)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_OBTENER_HISTORIAL_CLINICO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_PACIENTE", idPaciente));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int code = Convert.ToInt32(reader["code"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = code == 0 ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
                            res.Message = message;

                            if (code == 0 && reader.NextResult())
                            {
                                var historial = new List<object>();

                                while (reader.Read())
                                {
                                    historial.Add(new
                                    {
                                        ID_HISTORIAL = reader["ID_HISTORIAL"],
                                        PRESCRIPCION = reader["PRESCRIPCION"],
                                        DIAGNOSTICO = reader["DIAGNOSTICO"],
                                        RECETA = reader["RECETA"],
                                        DIA = reader["DIA"],
                                        MES = reader["MES"],
                                        ANIO = reader["ANIO"],
                                        NOMBRE_MEDICO = reader["NOMBRE_MEDICO"]
                                    });
                                }

                                res.Content = historial;
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

        public Response ObtenerHistorialPorCorreo(string correo)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_OBTENER_HISTORIAL_CLINICO_POR_CORREO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@CORREO", correo));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int code = Convert.ToInt32(reader["code"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = code == 0 ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
                            res.Message = message;

                            if (code == 0 && reader.NextResult())
                            {
                                var historial = new List<object>();

                                while (reader.Read())
                                {
                                    historial.Add(new
                                    {
                                        ID_HISTORIAL = reader["ID_HISTORIAL"],
                                        PRESCRIPCION = reader["PRESCRIPCION"],
                                        DIAGNOSTICO = reader["DIAGNOSTICO"],
                                        RECETA = reader["RECETA"],
                                        DIA = reader["DIA"],
                                        MES = reader["MES"],
                                        ANIO = reader["ANIO"],
                                        HORA_INICIO = reader["HORA_INICIO"],
                                        HORA_FIN = reader["HORA_FIN"],
                                        NOMBRE_MEDICO = reader["NOMBRE_MEDICO"]
                                    });
                                }

                                res.Content = historial;
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
