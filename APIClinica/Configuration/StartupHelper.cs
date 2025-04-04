using APIClinica.Business;
using APIClinica.Data.Entidades;

namespace APIClinica.Configuration
{
    public static class StartupHelper
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            // Registrar clases de negocio
            services.AddScoped<Usuario_B>();
            services.AddScoped<Especialidad_B>();
            services.AddScoped<Cita_B>();

            // Registrar clases de datos
            services.AddScoped<UsuarioDB>();
            services.AddScoped<EspecialidadDB>();
            services.AddScoped<CitaDB>();

            return services;
        }
    }
}
