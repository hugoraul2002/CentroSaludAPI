namespace CentroSaludAPI.Services.MunicipioService
{
    public class MunicipioService : IMunicipioService
    {
        private readonly DataContext _context;

        public MunicipioService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Municipio>> GetMunicipios()
        {
            var municipios = await _context.Municipio.ToListAsync(); 
            return municipios;
        }

        public async Task<Municipio?> GetMunicipio(int id)
        {
            var municipio = await _context.Municipio.FindAsync(id);
            if (municipio == null)
                return null;
            return municipio;
        }

        public async Task<Municipio?> AddMunicipio(Municipio municipio)
        {
            _context.Municipio.Add(municipio);
            await _context.SaveChangesAsync();
            return municipio;
        }

        public async Task<Municipio> UpdateMunicipio(int id, Municipio municipio)
        {
            var municipioToUpdate = await _context.Municipio.FindAsync(id);
            if (municipioToUpdate == null)
                throw new Exception("Municipio no encontrado");
            municipioToUpdate.Nombre = municipio.Nombre;
            await _context.SaveChangesAsync();
            return municipioToUpdate;
        }

        public async Task<List<Municipio>> DeleteMunicipios(int id)
        {
            var municipio = await _context.Municipio.FindAsync(id);
            if (municipio == null)
                throw new Exception("Municipio no encontrado");
            _context.Municipio.Remove(municipio);
            await _context.SaveChangesAsync();
            return await GetMunicipios();
        }   


    }
}
