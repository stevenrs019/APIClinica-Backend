using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class PacienteDB
    {
        private readonly ClinicaDbContext _context;

        public PacienteDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response InsertarPaciente(PacienteInsertarDto paciente)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_CREAR_PACIENTE";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NOMBRE", paciente.NOMBRE));
                    command.Parameters.Add(new SqlParameter("@APELLIDO1", paciente.APELLIDO1));
                    command.Parameters.Add(new SqlParameter("@APELLIDO2", paciente.APELLIDO2));
                    command.Parameters.Add(new SqlParameter("@EDAD", paciente.EDAD));
                    command.Parameters.Add(new SqlParameter("@TELEFONO", paciente.TELEFONO));
                    command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", paciente.FECHA_NACIMIENTO));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            res.Code = Convert.ToInt32(reader["message_id"]);
                            res.Message = reader["message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Error en base de datos";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public Response ModificarPaciente(PacienteDto paciente)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_MODIFICAR_PACIENTE";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_PACIENTE", paciente.ID_PACIENTE));
                    command.Parameters.Add(new SqlParameter("@NOMBRE", paciente.NOMBRE));
                    command.Parameters.Add(new SqlParameter("@APELLIDO1", paciente.APELLIDO1));
                    command.Parameters.Add(new SqlParameter("@APELLIDO2", paciente.APELLIDO2));
                    command.Parameters.Add(new SqlParameter("@EDAD", paciente.EDAD));
                    command.Parameters.Add(new SqlParameter("@TELEFONO", paciente.TELEFONO));
                    command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", paciente.FECHA_NACIMIENTO));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            res.Code = Convert.ToInt32(reader["message_id"]);
                            res.Message = reader["message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Error en base de datos";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public Response EliminarPaciente(int idPaciente)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_ELIMINAR_PACIENTE";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_PACIENTE", idPaciente));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            res.Code = Convert.ToInt32(reader["message_id"]);
                            res.Message = reader["message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Error en base de datos";
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

