using CentroSaludAPI.DTO;
using CentroSaludAPI.Services.UsuarioService;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuarios();
            return Ok(usuarios);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // Agregar un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuario = new Usuario
            {
                username = usuarioDto.Username,
                password = usuarioDto.Password,
                nombre = usuarioDto.Nombre,
                apellido = usuarioDto.Apellido,
                telefono = usuarioDto.Telefono,
                direccion = usuarioDto.Direccion
            };
            var createdUsuario = await _usuarioService.AddUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = createdUsuario.Id }, createdUsuario);
        }

        // Actualizar un usuario existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo del usuario.");
            }
            try
            {
                var updatedUsuario = await _usuarioService.UpdateUsuario(id, usuario);
                return Ok(updatedUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar un usuario existente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var deleted = await _usuarioService.DeleteUsuario(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}
