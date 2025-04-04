using APIClinica.Models;
using APIClinica.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Data
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasKey(p => p.ID_PERSONA);
            modelBuilder.Entity<Usuario>().HasKey(u => u.ID_USUARIO);

            base.OnModelCreating(modelBuilder);
        }
    }
}
