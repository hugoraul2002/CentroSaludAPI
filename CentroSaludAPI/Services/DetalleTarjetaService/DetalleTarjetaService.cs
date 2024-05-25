namespace CentroSaludAPI.Services.DetalleTarjetaService
{
    public class DetalleTarjetaService : IDetalleTarjetaService
    {
        
            private readonly DataContext _context;
            public DetalleTarjetaService(DataContext context)
            {
                _context = context;
            }

            // Listar todos los detalles de tarjeta
            public async Task<List<DetalleTarjeta>> GetDetallesTarjetas()
            {
                return await _context.DetallesTarjeta
                    .Include(dt => dt.Tarjeta)
                    .Include(dt => dt.Vacuna)
                    .ToListAsync();
            }

            // Listar un detalle de tarjeta por id
            public async Task<DetalleTarjeta> GetDetalleTarjetaById(int id)
            {
                return await _context.DetallesTarjeta
                    .Include(dt => dt.Tarjeta)
                    .Include(dt => dt.Vacuna)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }

            // Agregar un detalle de tarjeta
            public async Task<DetalleTarjeta> AddDetalleTarjeta(DetalleTarjeta detalleTarjeta)
            {
                await _context.DetallesTarjeta.AddAsync(detalleTarjeta);
                await _context.SaveChangesAsync();
                return detalleTarjeta;
            }

            // Actualizar un detalle de tarjeta por id
            public async Task<DetalleTarjeta> UpdateDetalleTarjeta(int id, DetalleTarjeta detalleTarjeta)
            {
                try
                {
                    var detalleToUpdate = await _context.DetallesTarjeta.FirstOrDefaultAsync(x => x.Id == id);
                    if (detalleToUpdate == null)
                    {
                        throw new Exception("Detalle de tarjeta no encontrado");
                    }

                    // Actualizar los datos del detalle de tarjeta
                    detalleToUpdate.TarjetaId = detalleTarjeta.TarjetaId;
                    detalleToUpdate.Tarjeta = detalleTarjeta.Tarjeta;
                    detalleToUpdate.VacunaId = detalleTarjeta.VacunaId;
                    detalleToUpdate.Vacuna = detalleTarjeta.Vacuna;
                    detalleToUpdate.FechaRegistro = detalleTarjeta.FechaRegistro;
                    detalleToUpdate.FechaProximaVacuna = detalleTarjeta.FechaProximaVacuna;
                    detalleToUpdate.Observaciones = detalleTarjeta.Observaciones;

                    await _context.SaveChangesAsync();
                    return detalleToUpdate;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            // Eliminar un detalle de tarjeta por id
            public async Task<bool> DeleteDetalleTarjeta(int id)
            {
                var detalleToDelete = await _context.DetallesTarjeta.FirstOrDefaultAsync(x => x.Id == id);
                if (detalleToDelete == null)
                {
                    throw new Exception("Detalle de tarjeta no encontrado");
                }
                _context.DetallesTarjeta.Remove(detalleToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
        }

    
}
