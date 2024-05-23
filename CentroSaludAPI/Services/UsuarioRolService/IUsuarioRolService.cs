namespace CentroSaludAPI.Services.UsuarioRolService
{
    public interface IUsuarioRolService
    {
        Task<List<UsuarioRol>> ListarUsuarioRoles();
        Task<UsuarioRol?> ObtenerUsuarioRol(int id);
        Task<UsuarioRol?> CrearUsuarioRol(UsuarioRol usuarioRol);
        Task<UsuarioRol> ActualizarUsuarioRol(int id, UsuarioRol usuarioRol);

        //elimar rool con id boolean
        Task<bool> EliminarUsuarioRol(int id);
    }
}
