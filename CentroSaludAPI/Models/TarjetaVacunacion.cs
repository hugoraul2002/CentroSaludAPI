using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CentroSaludAPI.Models
{
    public class TarjetaVacunacion
    {
        public int Id { get; set; }
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }
        [Required]
        public required Paciente Paciente { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        [Required]
        public required Usuario Usuario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
