namespace CentroSaludAPI.Services.MunicipioService
{
    public interface IMunicipioService
    {
        Task<List<Municipio>> GetMunicipios();
        Task<Municipio?> GetMunicipio(int id);
        Task<Municipio?> AddMunicipio(Municipio municipio);
        Task<Municipio> UpdateMunicipio(int id, Municipio municipio);
        Task<List<Municipio>> DeleteMunicipios(int id);

    }
}
