namespace CentroSaludAPI.Services.UsuarioService
{
    public interface IUsuarioService
    {
        //Listar los usuarios
        Task<List<Usuario>> GetUsuarios();
        //Listar un usuario por id
        Task<Usuario> GetUsuarioById(int id);
        //Agregar un usuario
        Task<Usuario> AddUsuario(Usuario usuario);
        //Actualizar un usuario por id
        Task<Usuario> UpdateUsuario(int id, Usuario usuario);
        //Eliminar un usuario por id
        Task<bool> DeleteUsuario(int id);
    }
}
