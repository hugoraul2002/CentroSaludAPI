using CentroSaludAPI.Services.DetalleTarjetaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleTarjetaController : ControllerBase
    {
        private readonly IDetalleTarjetaService _detalleTarjetaService;

        public DetalleTarjetaController(IDetalleTarjetaService detalleTarjetaService)
        {
            _detalleTarjetaService = detalleTarjetaService;
        }

        // Listar todos los detalles de tarjeta
        [HttpGet]
        public async Task<IActionResult> GetDetallesTarjetas()
        {
            var detallesTarjetas = await _detalleTarjetaService.GetDetallesTarjetas();
            return Ok(detallesTarjetas);
        }

        // Obtener un detalle de tarjeta por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetalleTarjetaById(int id)
        {
            var detalleTarjeta = await _detalleTarjetaService.GetDetalleTarjetaById(id);
            if (detalleTarjeta == null)
            {
                return NotFound();
            }
            return Ok(detalleTarjeta);
        }

        // Agregar un nuevo detalle de tarjeta
        [HttpPost]
        public async Task<IActionResult> AddDetalleTarjeta([FromBody] DetalleTarjeta detalleTarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdDetalleTarjeta = await _detalleTarjetaService.AddDetalleTarjeta(detalleTarjeta);
            return CreatedAtAction(nameof(GetDetalleTarjetaById), new { id = createdDetalleTarjeta.Id }, createdDetalleTarjeta);
        }

        // Actualizar un detalle de tarjeta existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetalleTarjeta(int id, [FromBody] DetalleTarjeta detalleTarjeta)
        {
            if (id != detalleTarjeta.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo del detalle de tarjeta.");
            }
            try
            {
                var updatedDetalleTarjeta = await _detalleTarjetaService.UpdateDetalleTarjeta(id, detalleTarjeta);
                return Ok(updatedDetalleTarjeta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Eliminar un detalle de tarjeta
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleTarjeta(int id)
        {
            try
            {
                bool isDeleted = await _detalleTarjetaService.DeleteDetalleTarjeta(id);
                if (!isDeleted)
                {
                    return NotFound("Detalle de tarjeta no encontrado.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
