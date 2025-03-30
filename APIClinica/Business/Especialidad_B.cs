using APIClinica.Data.Entidades;
using APIClinica.Data;
using APIClinica.Models.DTO;
using APIClinica.Services;

namespace APIClinica.Business
{
    public class Especialidad_B
    {
        private readonly ClinicaDbContext _context;

        public Especialidad_B(ClinicaDbContext context)
        {
            _context = context;
        }
        public Response ObtenerEspecialidades()
        {
            try
            {
                
                EspecialidadDB especialidadref = new EspecialidadDB(_context);
                return especialidadref.Obtener();

            }
            catch (Exception ex)
            {
                return new Response { Code = (int)ResultCode.ErrorInterno, Message = ex.Message };
            }
        }
    }
}
