using APIClinica.Data;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class MedicoDB
{
    private readonly ClinicaDbContext _context;

    public MedicoDB(ClinicaDbContext context)
    {
        _context = context;
    }

    public Response Insertar(MedicoInsertarDto medico)
    {
        Response res = new Response();
        var connection = _context.Database.GetDbConnection();

        try
        {
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SP_CREAR_MEDICO";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@NOMBRE", medico.NOMBRE));
            command.Parameters.Add(new SqlParameter("@APELLIDO1", medico.APELLIDO1));
            command.Parameters.Add(new SqlParameter("@APELLIDO2", medico.APELLIDO2));
            command.Parameters.Add(new SqlParameter("@EDAD", medico.EDAD));
            command.Parameters.Add(new SqlParameter("@TELEFONO", medico.TELEFONO));
            command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", medico.FECHA_NACIMIENTO));
            command.Parameters.Add(new SqlParameter("@ID_ESPECIALIDAD", medico.ID_ESPECIALIDAD));

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                int messageId = Convert.ToInt32(reader["message_id"]);
                string message = reader["message"]?.ToString() ?? "";

                res.Code = messageId < 0 ? (int)ResultCode.ErrorBaseDatos : (int)ResultCode.Exito;
                res.Message = message;
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

    public Response Modificar(MedicoDto medico)
    {
        Response res = new Response();
        var connection = _context.Database.GetDbConnection();

        try
        {
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SP_MODIFICAR_MEDICO";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ID_MEDICO", medico.ID_MEDICO));
            command.Parameters.Add(new SqlParameter("@NOMBRE", medico.NOMBRE));
            command.Parameters.Add(new SqlParameter("@APELLIDO1", medico.APELLIDO1));
            command.Parameters.Add(new SqlParameter("@APELLIDO2", medico.APELLIDO2));
            command.Parameters.Add(new SqlParameter("@EDAD", medico.EDAD));
            command.Parameters.Add(new SqlParameter("@TELEFONO", medico.TELEFONO));
            command.Parameters.Add(new SqlParameter("@FECHA_NACIMIENTO", medico.FECHA_NACIMIENTO));
            command.Parameters.Add(new SqlParameter("@ID_ESPECIALIDAD", medico.ID_ESPECIALIDAD));

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                int messageId = Convert.ToInt32(reader["message_id"]);
                string message = reader["message"]?.ToString() ?? "";

                res.Code = messageId < 0 ? (int)ResultCode.ErrorBaseDatos : (int)ResultCode.Exito;
                res.Message = message;
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

    public Response Eliminar(int idMedico)
    {
        Response res = new Response();
        var connection = _context.Database.GetDbConnection();

        try
        {
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SP_ELIMINAR_MEDICO";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ID_MEDICO", idMedico));

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                int messageId = Convert.ToInt32(reader["message_id"]);
                string message = reader["message"]?.ToString() ?? "";

                res.Code = messageId < 0 ? (int)ResultCode.ErrorBaseDatos : (int)ResultCode.Exito;
                res.Message = message;
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
