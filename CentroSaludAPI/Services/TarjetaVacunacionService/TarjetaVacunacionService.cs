namespace CentroSaludAPI.Services.TarjetaVacunacionService
{
    public class TarjetaVacunacionService : ITarjetaVacunacionService
    {
        private readonly DataContext _context;
        public TarjetaVacunacionService(DataContext context)
        {
            _context = context;
        }

        // Listar todas las tarjetas de vacunación
        public async Task<List<TarjetaVacunacion>> GetTarjetasVacunacion()
        {
            return await _context.TarjetasVacunacion
                .Include(tv => tv.Paciente)
                .Include(tv => tv.Usuario)
                .ToListAsync();
        }

        // Listar una tarjeta de vacunación por id
        public async Task<TarjetaVacunacion> GetTarjetaVacunacionById(int id)
        {
            return await _context.TarjetasVacunacion
                .Include(tv => tv.Paciente)
                .Include(tv => tv.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Agregar una tarjeta de vacunación
        public async Task<TarjetaVacunacion> AddTarjetaVacunacion(TarjetaVacunacion tarjetaVacunacion)
        {
            await _context.TarjetasVacunacion.AddAsync(tarjetaVacunacion);
            await _context.SaveChangesAsync();
            return tarjetaVacunacion;
        }

        // Actualizar una tarjeta de vacunación por id
        public async Task<TarjetaVacunacion> UpdateTarjetaVacunacion(int id, TarjetaVacunacion tarjetaVacunacion)
        {
            try
            {
                var tarjetaToUpdate = await _context.TarjetasVacunacion.FirstOrDefaultAsync(x => x.Id == id);
                if (tarjetaToUpdate == null)
                {
                    throw new Exception("Tarjeta de vacunación no encontrada");
                }

                // Actualizar los datos de la tarjeta de vacunación
                tarjetaToUpdate.PacienteId = tarjetaVacunacion.PacienteId;
                tarjetaToUpdate.Paciente = tarjetaVacunacion.Paciente;
                tarjetaToUpdate.UsuarioId = tarjetaVacunacion.UsuarioId;
                tarjetaToUpdate.Usuario = tarjetaVacunacion.Usuario;
                tarjetaToUpdate.FechaRegistro = tarjetaVacunacion.FechaRegistro;

                await _context.SaveChangesAsync();
                return tarjetaToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar una tarjeta de vacunación por id
        public async Task<bool> DeleteTarjetaVacunacion(int id)
        {
            var tarjetaToDelete = await _context.TarjetasVacunacion.FirstOrDefaultAsync(x => x.Id == id);
            if (tarjetaToDelete == null)
            {
                throw new Exception("Tarjeta de vacunación no encontrada");
            }
            _context.TarjetasVacunacion.Remove(tarjetaToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
