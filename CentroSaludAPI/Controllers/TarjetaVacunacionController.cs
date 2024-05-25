using CentroSaludAPI.Services.TarjetaVacunacionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaVacunacionController : ControllerBase
    {
        private readonly ITarjetaVacunacionService _tarjetaVacunacionService;

        public TarjetaVacunacionController(ITarjetaVacunacionService tarjetaVacunacionService)
        {
            _tarjetaVacunacionService = tarjetaVacunacionService;
        }

        // Obtener todas las tarjetas de vacunación
        [HttpGet]
        public async Task<IActionResult> GetTarjetasVacunacion()
        {
            var tarjetas = await _tarjetaVacunacionService.GetTarjetasVacunacion();
            return Ok(tarjetas);
        }

        // Obtener una tarjeta de vacunación por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarjetaVacunacionById(int id)
        {
            var tarjeta = await _tarjetaVacunacionService.GetTarjetaVacunacionById(id);
            if (tarjeta == null)
            {
                return NotFound();
            }
            return Ok(tarjeta);
        }

        // Agregar una nueva tarjeta de vacunación
        [HttpPost]
        public async Task<IActionResult> AddTarjetaVacunacion([FromBody] TarjetaVacunacion tarjetaVacunacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTarjeta = await _tarjetaVacunacionService.AddTarjetaVacunacion(tarjetaVacunacion);
            return CreatedAtAction(nameof(GetTarjetaVacunacionById), new { id = createdTarjeta.Id }, createdTarjeta);
        }

        // Actualizar una tarjeta de vacunación existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarjetaVacunacion(int id, [FromBody] TarjetaVacunacion tarjetaVacunacion)
        {
            if (id != tarjetaVacunacion.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo de la tarjeta de vacunación.");
            }
            try
            {
                var updatedTarjeta = await _tarjetaVacunacionService.UpdateTarjetaVacunacion(id, tarjetaVacunacion);
                return Ok(updatedTarjeta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Eliminar una tarjeta de vacunación
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjetaVacunacion(int id)
        {
            bool isDeleted = await _tarjetaVacunacionService.DeleteTarjetaVacunacion(id);
            if (!isDeleted)
            {
                return NotFound("Tarjeta de vacunación no encontrada.");
            }
            return NoContent();
        }
    }

}
