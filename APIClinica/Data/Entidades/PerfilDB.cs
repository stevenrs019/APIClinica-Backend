using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class PerfilDB
    {
        private readonly ClinicaDbContext _context;

        public PerfilDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response ObtenerPerfilPorCorreo(string email)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_OBTENER_PERFIL_POR_CORREO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EMAIL", email));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var perfil = new PerfilDto
                            {
                                EMAIL = email,
                                NOMBRE = reader["NOMBRE"].ToString() ?? "",
                                APELLIDO1 = reader["APELLIDO1"].ToString() ?? "",
                                APELLIDO2 = reader["APELLIDO2"].ToString() ?? "",
                                EDAD = reader["EDAD"].ToString() ?? "",
                                TELEFONO = reader["TELEFONO"].ToString() ?? "",
                                FECHA_NACIMIENTO = Convert.ToDateTime(reader["FECHA_NACIMIENTO"])
                            };

                            res.Code = (int)ResultCode.Exito;
                            res.Message = "Perfil obtenido correctamente";
                            res.Content = perfil;
                        }
                        else
                        {
                            res.Code = (int)ResultCode.SP_SinRespuesta;
                            res.Message = "No se encontró información de perfil para este correo.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Ocurrió un error al obtener el perfil.";
                res.Content = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public Response ActualizarPerfilPorCorreo(PerfilDto perfil)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_MODIFICAR_PERFIL_POR_CORREO";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@EMAIL", perfil.EMAIL));
                    command.Parameters.Add(new SqlParameter("@NOMBRE", perfil.NOMBRE));
                    command.Parameters.Add(new SqlParameter("@APELLIDO1", perfil.APELLIDO1));
                    command.Parameters.Add(new SqlParameter("@APELLIDO2", perfil.APELLIDO2));
                    command.Parameters.Add(new SqlParameter("@EDAD", perfil.EDAD));
                    command.Parameters.Add(new SqlParameter("@TELEFONO", perfil.TELEFONO));
                    command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", perfil.FECHA_NACIMIENTO));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        res.Code = (int)ResultCode.Exito;
                        res.Message = "Perfil actualizado correctamente.";
                    }
                    else
                    {
                        res.Code = (int)ResultCode.ErrorBaseDatos;
                        res.Message = "No se pudo actualizar el perfil.";
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ResultCode.ErrorDesconocidoBaseDatos;
                res.Message = "Ocurrió un error al actualizar el perfil.";
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
