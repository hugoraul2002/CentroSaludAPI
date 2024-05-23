global using Microsoft.EntityFrameworkCore;

namespace CentroSaludAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=centro_salud;Trusted_Connection=true;TrustServerCertificate=true;");

        }

        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }   
    }
}
