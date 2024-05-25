namespace CentroSaludAPI.Services.VacunaService
{
    public class VacunaService : IVacunaService

    {
        private readonly DataContext _context;
        public VacunaService(DataContext context)
        {
            _context = context;
        }

        // Listar todas las vacunas
        public async Task<List<Vacuna>> GetVacunas()
        {
            return await _context.Vacunas.ToListAsync();
        }

        // Listar una vacuna por id
        public async Task<Vacuna> GetVacunaById(int id)
        {
            return await _context.Vacunas.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Agregar una vacuna
        public async Task<Vacuna> AddVacuna(Vacuna vacuna)
        {
            await _context.Vacunas.AddAsync(vacuna);
            await _context.SaveChangesAsync();
            return vacuna;
        }

        // Actualizar una vacuna por id
        public async Task<Vacuna> UpdateVacuna(int id, Vacuna vacuna)
        {
            try
            {
                var vacunaToUpdate = await _context.Vacunas.FirstOrDefaultAsync(x => x.Id == id);
                if (vacunaToUpdate == null)
                {
                    throw new Exception("Vacuna no encontrada");
                }

                // Actualizar los datos de la vacuna
                vacunaToUpdate.Nombre = vacuna.Nombre;
                vacunaToUpdate.Descripcion = vacuna.Descripcion;
                vacunaToUpdate.Dosis = vacuna.Dosis;
                vacunaToUpdate.edad_minima = vacuna.edad_minima;
                vacunaToUpdate.edad_maxima = vacuna.edad_maxima;

                await _context.SaveChangesAsync();
                return vacunaToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar una vacuna por id
        public async Task<bool> DeleteVacuna(int id)
        {
            var vacunaToDelete = await _context.Vacunas.FirstOrDefaultAsync(x => x.Id == id);
            if (vacunaToDelete == null)
            {
                throw new Exception("Vacuna no encontrada");
            }
            _context.Vacunas.Remove(vacunaToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
