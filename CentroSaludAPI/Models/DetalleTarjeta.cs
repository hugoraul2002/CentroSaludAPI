using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CentroSaludAPI.Models
{
    public class DetalleTarjeta
    {
        public int Id { get; set; }
        [ForeignKey("Tarjeta")]
        public int TarjetaId { get; set; }
        [Required]
        public required TarjetaVacunacion Tarjeta { get; set; }

        [ForeignKey("Vacuna")]
        public int VacunaId { get; set; }
        [Required]
        public required Vacuna Vacuna { get; set; }

        public DateTime FechaRegistro { get; set; }
        public DateTime FechaProximaVacuna { get; set; }
        public string Observaciones { get; set; } = string.Empty;

    }
}
