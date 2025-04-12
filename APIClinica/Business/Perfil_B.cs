using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class Perfil_B
    {
        private readonly ClinicaDbContext _context;

        public Perfil_B(ClinicaDbContext context)
        {
            _context = context;
        }

        public Response ObtenerPerfil(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return new Response
                {
                    Code = (int)ResultCode.DatosIncompletos,
                    Message = "El correo es obligatorio para obtener el perfil."
                };
            }

            PerfilDB perfilDb = new PerfilDB(_context);
            return perfilDb.ObtenerPerfilPorCorreo(correo);
        }

        public Response ActualizarPerfil(PerfilDto perfil)
        {
            if (string.IsNullOrWhiteSpace(perfil.EMAIL) ||
                string.IsNullOrWhiteSpace(perfil.NOMBRE) ||
                string.IsNullOrWhiteSpace(perfil.APELLIDO1) ||
                string.IsNullOrWhiteSpace(perfil.APELLIDO2) ||
                string.IsNullOrWhiteSpace(perfil.EDAD) ||
                string.IsNullOrWhiteSpace(perfil.TELEFONO) ||
                perfil.FECHA_NACIMIENTO == default)
            {
                return new Response
                {
                    Code = (int)ResultCode.DatosIncompletos,
                    Message = "Todos los campos son obligatorios para actualizar el perfil."
                };
            }

            PerfilDB perfilDb = new PerfilDB(_context);
            return perfilDb.ActualizarPerfilPorCorreo(perfil);
        }
    }
}
