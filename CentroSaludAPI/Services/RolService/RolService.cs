namespace CentroSaludAPI.Services.RolService
{
    public class RolService : IRolService
    {
        private readonly DataContext _context;

        public RolService(DataContext context)
        {
            _context = context;
        }

        //listar todos los roles
        public async Task<List<Rol>> ListarRoles()
        {
            return await _context.Rol.ToListAsync();
        }

        //listar un rol por id
        public async Task<Rol> ObtenerRol(int id)
        {
            return await _context.Rol.FirstOrDefaultAsync(x => x.Id == id);
        }

        //agregar un rol
        public async Task<Rol> CrearRol(Rol rol)
        {
            await _context.Rol.AddAsync(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        //actualizar un rol por id
        public async Task<Rol> ActualizarRol(int id, Rol rol)
        {
            try
            {
                var rolToUpdate = await _context.Rol.FirstOrDefaultAsync(x => x.Id == id);
                if (rolToUpdate == null)
                {
                    throw new Exception("Rol no encontrado");
                }
                // Actualizar los datos del rol
                rolToUpdate.Nombre = rol.Nombre;
                await _context.SaveChangesAsync();
                return rolToUpdate;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //eliminar un rol por id
        public async Task<bool> EliminarRol(int id)
        {
            try
            {
                var rolToDelete = await _context.Rol.FirstOrDefaultAsync(x => x.Id == id);
                if (rolToDelete == null)
                {
                    throw new Exception("Rol no encontrado");
                }
                _context.Rol.Remove(rolToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Rol>> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Task<Rol?> GetRol(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rol?> AddRol(Rol rol)
        {
            throw new NotImplementedException();
        }

        public Task<Rol> UpdateRol(int id, Rol rol)
        {
            throw new NotImplementedException();
        }

        public Task<List<Rol>> DeleteRoles(int id)
        {
            throw new NotImplementedException();
        }
    }
}
