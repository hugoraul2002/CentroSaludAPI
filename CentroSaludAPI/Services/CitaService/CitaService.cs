namespace CentroSaludAPI.Services.CitaService
{
    public class CitaService : ICitaService
    {
        private readonly DataContext _context;

        public CitaService(DataContext context)
        {
            _context = context;
        }

        // Listar todas las citas
        public async Task<List<Cita>> GetCitas()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Usuario)
                .ToListAsync();
        }

        // Listar una cita por ID
        public async Task<Cita> GetCitaById(int id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Agregar una nueva cita
        public async Task<Cita> AddCita(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        // Actualizar una cita existente
        public async Task<Cita> UpdateCita(int id, Cita cita)
        {
            try
            {
                var citaToUpdate = await _context.Citas.FirstOrDefaultAsync(x => x.Id == id);
                if (citaToUpdate == null)
                {
                    throw new Exception("Cita no encontrada");
                }

                // Actualizar los datos de la cita
                citaToUpdate.PacienteId = cita.PacienteId;
                citaToUpdate.Paciente = cita.Paciente;
                citaToUpdate.DoctorId = cita.DoctorId;
                citaToUpdate.Doctor = cita.Doctor;
                citaToUpdate.UsuarioId = cita.UsuarioId;
                citaToUpdate.Usuario = cita.Usuario;
                citaToUpdate.FechaHora = cita.FechaHora;
                citaToUpdate.FechaRegistro = cita.FechaRegistro;
                citaToUpdate.Observaciones = cita.Observaciones;
                citaToUpdate.Estado = cita.Estado;

                await _context.SaveChangesAsync();
                return citaToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar una cita por ID
        public async Task<bool> DeleteCita(int id)
        {
            var citaToDelete = await _context.Citas.FirstOrDefaultAsync(x => x.Id == id);
            if (citaToDelete == null)
            {
                throw new Exception("Cita no encontrada");
            }
            _context.Citas.Remove(citaToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
