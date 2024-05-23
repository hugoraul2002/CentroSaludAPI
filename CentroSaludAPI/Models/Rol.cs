namespace CentroSaludAPI.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }
}
