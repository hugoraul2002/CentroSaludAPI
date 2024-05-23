using CentroSaludAPI.Services.PacienteService;
using CentroSaludAPI.Services.RolService;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        //Listar todos los roles
        [HttpGet]
        public async Task<IActionResult> ListarRoles()
        {
            var roles = await _rolService.ListarRoles();
            return Ok(roles);
        }

        //Obtener un rol por su id
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRol(int id)
        {
            var rol = await _rolService.ObtenerRol(id);
            if (rol == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }

        //Crear un rol
        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdRol = await _rolService.CrearRol(rol);
            return CreatedAtAction(nameof(ObtenerRol), new { id = createdRol.Id }, createdRol);
        }

        //Actualizar un rol
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo del rol.");
            }
            try
            {
                var updatedRol = await _rolService.ActualizarRol(id, rol);
                return Ok(updatedRol);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Eliminar un rol
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                var deleted = await _rolService.EliminarRol(id);
                if (deleted)
                {
                    return Ok("Rol eliminado");
                }
                return NotFound("Rol no encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
