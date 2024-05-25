namespace CentroSaludAPI.Services.DetalleTarjetaService
{
    public interface IDetalleTarjetaService
    {
        Task<List<DetalleTarjeta>> GetDetallesTarjetas();
        Task<DetalleTarjeta> GetDetalleTarjetaById(int id);
        Task<DetalleTarjeta> AddDetalleTarjeta(DetalleTarjeta detalleTarjeta);
        Task<DetalleTarjeta> UpdateDetalleTarjeta(int id, DetalleTarjeta detalleTarjeta);
        Task<bool> DeleteDetalleTarjeta(int id);
    }
}
