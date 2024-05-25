namespace CentroSaludAPI.Services.TarjetaVacunacionService
{
    public interface ITarjetaVacunacionService
    {
        Task<List<TarjetaVacunacion>> GetTarjetasVacunacion();
        Task<TarjetaVacunacion> GetTarjetaVacunacionById(int id);
        Task<TarjetaVacunacion> AddTarjetaVacunacion(TarjetaVacunacion vacuna);
        Task<TarjetaVacunacion> UpdateTarjetaVacunacion(int id, TarjetaVacunacion vacuna);
        Task<bool> DeleteTarjetaVacunacion(int id);
    }
}
