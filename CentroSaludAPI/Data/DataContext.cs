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
            optionsBuilder.UseSqlServer("Server=.\\SQL2014;Database=centroSalud;Trusted_Connection=true;TrustServerCertificate=true;");

        }

        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }   
        public DbSet<Vacuna> Vacunas { get; set; }   
        public DbSet<TarjetaVacunacion> TarjetasVacunacion { get; set; }   
        public DbSet<DetalleTarjeta> DetallesTarjeta { get; set; }   
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DetalleTarjeta>()
                .HasOne(d => d.Tarjeta)
                .WithMany()
                .HasForeignKey(d => d.TarjetaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleTarjeta>()
                .HasOne(d => d.Vacuna)
                .WithMany()
                .HasForeignKey(d => d.VacunaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TarjetaVacunacion>()
                .HasOne(t => t.Paciente)
                .WithMany()
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TarjetaVacunacion>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
