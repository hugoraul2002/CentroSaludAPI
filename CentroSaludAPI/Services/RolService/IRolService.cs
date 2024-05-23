namespace CentroSaludAPI.Services.RolService
{
    public interface IRolService
    {
        Task<List<Rol>> ListarRoles();
        Task<Rol?> ObtenerRol(int id);
        Task<Rol?> CrearRol(Rol rol);
        Task<Rol> ActualizarRol(int id, Rol rol);

        //elimar rool con id boolean
        Task<bool> EliminarRol(int id);
    }
}
