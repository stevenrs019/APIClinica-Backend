using APIClinica.Data;
using APIClinica.Data.Entidades;
using APIClinica.Models.DTO;
using APIClinica.Services;

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
                if (CamposInvalidos(usuario))
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

        public Response ModificarUsuario(int idUsuario, UsuarioDto usuario)
        {
            try
            {
                if (idUsuario <= 0 || CamposInvalidos(usuario))
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "Todos los campos son obligatorios y el ID debe ser válido."
                    };
                }

                UsuarioDB usuarioref = new UsuarioDB(_context);
                return usuarioref.Modificar(idUsuario, usuario);
            }
            catch (Exception ex)
            {
                return new Response { Code = (int)ResultCode.ErrorInterno, Message = ex.Message };
            }
        }

        public Response EliminarUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new Response
                    {
                        Code = (int)ResultCode.DatosIncompletos,
                        Message = "El ID del usuario debe ser válido."
                    };
                }

                UsuarioDB usuarioref = new UsuarioDB(_context);
                return usuarioref.Eliminar(idUsuario);
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
                if (string.IsNullOrWhiteSpace(login.EMAIL) || string.IsNullOrWhiteSpace(login.CONTRASENA))
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

        private bool CamposInvalidos(UsuarioDto usuario)
        {
            return string.IsNullOrWhiteSpace(usuario.NOMBRE) ||
                   string.IsNullOrWhiteSpace(usuario.APELLIDO1) ||
                   string.IsNullOrWhiteSpace(usuario.APELLIDO2) ||
                   string.IsNullOrWhiteSpace(usuario.EDAD) ||
                   string.IsNullOrWhiteSpace(usuario.TELEFONO) ||
                   string.IsNullOrWhiteSpace(usuario.EMAIL) ||
                   string.IsNullOrWhiteSpace(usuario.CONTRASENA) ||
                   usuario.FECHA_NACIMIENTO == default ||
                   usuario.ID_ROL <= 0;
        }
    }
}
