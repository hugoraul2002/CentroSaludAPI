using CentroSaludAPI.Services.UsuarioRolService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuarioRolService _usuarioRolService;

        public UsuarioRolController(IUsuarioRolService usuarioRolService)
        {
            _usuarioRolService = usuarioRolService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarioRoles()
        {
            var usuarioRoles = await _usuarioRolService.ListarUsuarioRoles();
            return Ok(usuarioRoles);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioRol([FromBody] UsuarioRol usuarioRol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdUsuarioRol = await _usuarioRolService.CrearUsuarioRol(usuarioRol);
            //Retornar el usuarioRol creado
            return BadRequest(createdUsuarioRol);
        }

    }
}
