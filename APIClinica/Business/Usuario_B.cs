using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Business
{
    public class Usuario_B
    {
        private readonly ClinicaDbContext _context;

        public Usuario_B(ClinicaDbContext context)
        {
            _context = context;
        }
        public Response InsertarUsuario(UsuarioDto usuario)
        {
            try
            {
                //Validaciones
                if (string.IsNullOrWhiteSpace(usuario.NOMBRE) ||
                    string.IsNullOrWhiteSpace(usuario.APELLIDO1) ||
                    string.IsNullOrWhiteSpace(usuario.APELLIDO2) ||
                    string.IsNullOrWhiteSpace(usuario.EDAD) ||
                    string.IsNullOrWhiteSpace(usuario.TELEFONO) ||
                    string.IsNullOrWhiteSpace(usuario.EMAIL) ||
                    string.IsNullOrWhiteSpace(usuario.CONTRASENA) ||
                    usuario.FECHA_NACIMIENTO == default ||
                    usuario.ID_ROL <= 0)
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios. Verificá que ningún valor esté vacío o inválido."
                    };
                }

                UsuarioDB usuarioref = new UsuarioDB(_context);
                return usuarioref.Insertar(usuario);
                
            }
            catch (Exception ex)
            {
                return new Response { Code = (int)ResultCode.ErrorInterno, Message = ex.Message };
            }
        }

        public Response LoginUsuario(LoginDto login)
        {
            try
            {
                //Validaciones
                if (string.IsNullOrWhiteSpace(login.EMAIL) ||
                    string.IsNullOrWhiteSpace(login.CONTRASENA))
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios. Verificá que ningún valor esté vacío o inválido."
                    };
                }

                UsuarioDB usuarioref = new UsuarioDB(_context);
                return usuarioref.Login(login);

            }
            catch (Exception ex)
            {
                return new Response { Code = (int)ResultCode.ErrorInterno, Message = ex.Message };
            }
        }
    }
}
