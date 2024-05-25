namespace CentroSaludAPI.Services.CitaService
{
    public interface ICitaService
    {
        Task<List<Cita>> GetCitas();
        Task<Cita> GetCitaById(int id);
        Task<Cita> AddCita(Cita cita);
        Task<Cita> UpdateCita(int id, Cita cita);
        Task<bool> DeleteCita(int id);
    }
}
