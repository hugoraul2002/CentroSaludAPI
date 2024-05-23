using System.ComponentModel.DataAnnotations.Schema;

namespace CentroSaludAPI.Models
{
    public class UsuarioRol
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; }



    }
}
