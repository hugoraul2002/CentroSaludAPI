using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace CentroSaludAPI.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;
        public UsuarioService(DataContext context)
        {
            _context = context;
        }

        //listar todos los usuarios
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuario
                            .ToListAsync();
        }

        //listar un usuario por id
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuario
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        //agregar un usuario
        public async Task<Usuario> AddUsuario(Usuario usuario)
        {
            if (await _context.Usuario.AnyAsync(u => u.username == usuario.username))
            {
                throw new ArgumentException("El nombre de usuario ya existe.");
            }
            usuario.password = BCrypt.Net.BCrypt.HashPassword(usuario.password);

            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        //actualizar un usuario por id
        public async Task<Usuario> UpdateUsuario(int id, Usuario usuario)
        {
            try
            {
                var usuarioToUpdate = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
                if (usuarioToUpdate == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                // Actualizar los datos del usuario
                usuarioToUpdate.username = usuario.username;
                usuarioToUpdate.password = usuario.password;
                usuarioToUpdate.nombre = usuario.nombre;
                usuarioToUpdate.apellido = usuario.apellido;
                usuarioToUpdate.telefono = usuario.telefono;
                usuarioToUpdate.direccion = usuario.direccion;

                await _context.SaveChangesAsync();
                return usuarioToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //eliminar un usuario por id
        public async Task<bool> DeleteUsuario(int id)
        {
            var usuarioToDelete = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
            if (usuarioToDelete == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            _context.Usuario.Remove(usuarioToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
