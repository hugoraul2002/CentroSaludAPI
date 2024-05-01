
namespace CentroSaludAPI.Services.PacienteService
{
    public class PacienteService : IPacienteService
    {
        private readonly DataContext _context;

        public PacienteService(DataContext context)
        {
            _context = context;
        }

        //listar todos los pacientes
        public async Task<List<Paciente>> GetPacientes()
        {
            return await _context.Paciente
                            .Include(p => p.Municipio) // Incluyendo la relación con Municipio
                            .ToListAsync();
        }

        //listar un paciente por id
        public async Task<Paciente> GetPacienteById(int id)
        {
            //listar un paciente por id incluyendo la relación con Municipio
            return await _context.Paciente
                            .Include(p => p.Municipio)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        //agregar un paciente
        public async Task<Paciente> AddPaciente(Paciente paciente)
        {
            //eviar guardar en la tabla de muncipio a la hora de guardar un paciente
            paciente.Municipio = await _context.Municipio.FirstOrDefaultAsync(x => x.Id == paciente.MunicipioId);
            await _context.Paciente.AddAsync(paciente);
            await _context.SaveChangesAsync();
            return paciente;    
        }

        //actualizar un paciente por id
        public async Task<Paciente> UpdatePaciente(int id, Paciente paciente)
        {
            try
            {
                var pacienteToUpdate = await _context.Paciente.FirstOrDefaultAsync(x => x.Id == id);
                if (pacienteToUpdate == null)
                {
                    throw new Exception("Paciente no encontrado");
                }
                // Actualizar los datos del paciente
                pacienteToUpdate.Nombre = paciente.Nombre;
                pacienteToUpdate.Apellido = paciente.Apellido;
                pacienteToUpdate.Direccion = paciente.Direccion;
                pacienteToUpdate.Telefono = paciente.Telefono;
                pacienteToUpdate.FechaNacimiento = paciente.FechaNacimiento;
                pacienteToUpdate.MunicipioId = paciente.MunicipioId;
                pacienteToUpdate.Municipio = await _context.Municipio.FirstOrDefaultAsync(x => x.Id == paciente.MunicipioId);

                await _context.SaveChangesAsync();
                return pacienteToUpdate;

            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar el paciente");
            }

            
        }


        //eliminar un paciente por id
        public async Task<bool> DeletePaciente(int id)
        {
            var pacienteToDelete = await _context.Paciente.FirstOrDefaultAsync(x => x.Id == id);
            if (pacienteToDelete == null)
                throw new Exception("Paciente no encontrado");

            _context.Paciente.Remove(pacienteToDelete);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
