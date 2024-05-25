using CentroSaludAPI.Models;

namespace CentroSaludAPI.Services.VacunaService
{
    public interface IVacunaService
    {        
        Task<List<Vacuna>> GetVacunas();
        Task<Vacuna> GetVacunaById(int id);
        Task<Vacuna> AddVacuna(Vacuna vacuna);
        Task<Vacuna> UpdateVacuna(int id, Vacuna vacuna);
        Task<bool> DeleteVacuna(int id);
    }
}
