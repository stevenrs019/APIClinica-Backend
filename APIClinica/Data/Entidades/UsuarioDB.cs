using APIClinica.Services;
using APIClinica.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class UsuarioDB
    {
        private readonly ClinicaDbContext _context;

        public UsuarioDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response Insertar(UsuarioDto usuario)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_INSERTAR_USUARIO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NOMBRE", usuario.NOMBRE));
                    command.Parameters.Add(new SqlParameter("@APELLIDO1", usuario.APELLIDO1));
                    command.Parameters.Add(new SqlParameter("@APELLIDO2", usuario.APELLIDO2));
                    command.Parameters.Add(new SqlParameter("@EDAD", usuario.EDAD));
                    command.Parameters.Add(new SqlParameter("@TELEFONO", usuario.TELEFONO));
                    command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", usuario.FECHA_NACIMIENTO));
                    command.Parameters.Add(new SqlParameter("@EMAIL", usuario.EMAIL));
                    command.Parameters.Add(new SqlParameter("@CONTRASENA", usuario.CONTRASENA));
                    command.Parameters.Add(new SqlParameter("@ID_ROL", usuario.ID_ROL));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int messageId = Convert.ToInt32(reader["message_id"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = (messageId == 0) ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
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

        public Response Modificar(int idUsuario, UsuarioDto usuario)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_MODIFICAR_USUARIO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    command.Parameters.Add(new SqlParameter("@NOMBRE", usuario.NOMBRE));
                    command.Parameters.Add(new SqlParameter("@APELLIDO1", usuario.APELLIDO1));
                    command.Parameters.Add(new SqlParameter("@APELLIDO2", usuario.APELLIDO2));
                    command.Parameters.Add(new SqlParameter("@EDAD", usuario.EDAD));
                    command.Parameters.Add(new SqlParameter("@TELEFONO", usuario.TELEFONO));
                    command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", usuario.FECHA_NACIMIENTO));
                    command.Parameters.Add(new SqlParameter("@EMAIL", usuario.EMAIL));
                    command.Parameters.Add(new SqlParameter("@CONTRASENA", usuario.CONTRASENA));
                    command.Parameters.Add(new SqlParameter("@ID_ROL", usuario.ID_ROL));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int messageId = Convert.ToInt32(reader["message_id"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = (messageId == 0) ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
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

        public Response Eliminar(int idUsuario)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_ELIMINAR_USUARIO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int messageId = Convert.ToInt32(reader["message_id"]);
                            string message = reader["message"]?.ToString() ?? "";

                            res.Code = (messageId == 0) ? (int)ResultCode.Exito : (int)ResultCode.ErrorBaseDatos;
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

        public Response Login(LoginDto login)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_LOGIN_USUARIO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@EMAIL", login.EMAIL));
                    command.Parameters.Add(new SqlParameter("@CONTRASENA", login.CONTRASENA));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int messageId = Convert.ToInt32(reader["message_id"]);
                            string message = reader["message"]?.ToString() ?? "";

                            if (messageId == 0)
                            {
                                if (reader.NextResult() && reader.Read())
                                {
                                    var usuario = new
                                    {
                                        ID_USUARIO = reader["ID_USUARIO"],
                                        NOMBRE = reader["NOMBRE"],
                                        APELLIDO1 = reader["APELLIDO1"],
                                        APELLIDO2 = reader["APELLIDO2"],
                                        EMAIL = reader["EMAIL"],
                                        ID_ROL = reader["ID_ROL"]
                                    };

                                    res.Code = (int)ResultCode.Exito;
                                    res.Message = message;
                                    res.Content = usuario;
                                }
                                else
                                {
                                    res.Code = (int)ResultCode.SP_SinRespuesta;
                                    res.Message = "No se devolvieron los datos del usuario.";
                                }
                            }
                            else
                            {
                                res.Code = (int)ResultCode.ErrorBaseDatos;
                                res.Message = message;
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
