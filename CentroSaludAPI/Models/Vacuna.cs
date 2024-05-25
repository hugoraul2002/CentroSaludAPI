namespace CentroSaludAPI.Models
{
    public class Vacuna
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Dosis { get; set; } = string.Empty;
        public int edad_minima { get; set; }
        public int edad_maxima { get; set; }

    }
}
