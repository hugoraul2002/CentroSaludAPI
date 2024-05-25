using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CentroSaludAPI.DTO
{
    public class CitaDTO
    {
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;

    }
}
