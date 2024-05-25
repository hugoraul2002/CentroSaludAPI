using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CentroSaludAPI.Models
{
    public class Cita
    {
        public int Id { get; set; }
        [ForeignKey("PacienteCita")]
        public int PacienteId { get; set; }
        
        public Paciente Paciente { get; set; }

        [ForeignKey("DoctorCita")]
        public int DoctorId { get; set; }
   
        public Doctor Doctor { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;

    }
}
