namespace CentroSaludAPI.Services.UsuarioRolService
{
    public class UsuarioRolService : IUsuarioRolService
    {
        private readonly DataContext _context;
        public UsuarioRolService(DataContext context)
        {
            _context = context;
        }

        //listar todos los roles de usuario
        public async Task<List<UsuarioRol>> ListarUsuarioRoles()
        {
            return await _context.UsuarioRol
                            .Include(p => p.Usuario) // Incluyendo la relación con Usuario
                            .Include(p => p.Rol) // Incluyendo la relación con Rol
                            .ToListAsync();
        }

        //listar un rol de usuario por id
        public async Task<UsuarioRol> ObtenerUsuarioRol(int id)
        {
            //listar un rol de usuario por id incluyendo la relación con Usuario y Rol
            return await _context.UsuarioRol
                            .Include(p => p.Usuario)
                            .Include(p => p.Rol)
                            .FirstOrDefaultAsync(x => x.UsuarioId == id);
        }

        //agregar un rol de usuario
        public async Task<UsuarioRol> CrearUsuarioRol(UsuarioRol usuarioRol)
        {
            //enviar guardar en la tabla de usuario y rol a la hora de guardar un rol de usuario
            usuarioRol.Usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == usuarioRol.UsuarioId);
            usuarioRol.Rol = await _context.Rol.FirstOrDefaultAsync(x => x.Id == usuarioRol.RolId);
            await _context.UsuarioRol.AddAsync(usuarioRol);
            await _context.SaveChangesAsync();
            return usuarioRol;
        }

        //eliminar un rol de usuario por id
        public async Task<bool> EliminarUsuarioRol(int id)
        {
            var usuarioRol = await _context.UsuarioRol.FirstOrDefaultAsync(x => x.UsuarioId == id);
            if (usuarioRol == null)
            {
                return false;
            }
            _context.UsuarioRol.Remove(usuarioRol);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<UsuarioRol> ActualizarUsuarioRol(int id, UsuarioRol usuarioRol)
        {
            throw new System.NotImplementedException();
        }
    }
}
