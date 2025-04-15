using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data.Entidades
{
    public class CitaVirtualDB
    {
        private readonly ClinicaDbContext _context;

        public CitaVirtualDB(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response AgendarCitaVirtual(CitaDto cita)
        {
            Response res = new Response();
            var connection = _context.Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_AGENDAR_CITA_VIRTUAL";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ID_MEDICO", cita.ID_MEDICO));
                    command.Parameters.Add(new SqlParameter("@ID_USUARIO", cita.ID_USUARIO));
                    command.Parameters.Add(new SqlParameter("@ID_HORARIO", cita.ID_HORARIO));
                    command.Parameters.Add(new SqlParameter("@TIPO_CITA", cita.TIPO_CITA));
                    command.Parameters.Add(new SqlParameter("@DIA", cita.DIA));
                    command.Parameters.Add(new SqlParameter("@MES", cita.MES));
                    command.Parameters.Add(new SqlParameter("@ANIO", cita.ANIO));
                    command.Parameters.Add(new SqlParameter("@LINK_VIDEO", cita.LINK_VIDEO));

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

        

        


    }
}
