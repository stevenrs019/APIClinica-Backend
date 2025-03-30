using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class EspecialidadDB
    {
        private readonly ClinicaDbContext _context;

        public EspecialidadDB(ClinicaDbContext context)
        {
            _context = context;
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
                    command.CommandText = "SP_OBTENER_ESPECIALIDADES";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        List<EspecialidadDto> especialidades = new List<EspecialidadDto>();

                        while (reader.Read())
                        {
                            EspecialidadDto e = new EspecialidadDto
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                NOMBRE = reader["NOMBRE"].ToString(),
                                SUBTITLE = reader["SUBTITLE"].ToString(),
                                DESCRIPCION = reader["DESCRIPCION"].ToString(),
                                ICONO = reader["ICONO"].ToString()
                            };

                            especialidades.Add(e);
                        }

                        res.Code = (int)ResultCode.Exito;
                        res.Message = "Especialidades obtenidas correctamente";
                        res.Content = especialidades;
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
