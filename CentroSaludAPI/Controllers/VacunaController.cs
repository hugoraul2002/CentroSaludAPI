using CentroSaludAPI.Services.VacunaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacunaController : ControllerBase
    {
        private readonly IVacunaService _vacunaService;

        public VacunaController(IVacunaService vacunaService)
        {
            _vacunaService = vacunaService;
        }

        // Obtener todas las vacunas
        [HttpGet]
        public async Task<IActionResult> GetVacunas()
        {
            var vacunas = await _vacunaService.GetVacunas();
            return Ok(vacunas);
        }

        // Obtener una vacuna por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacunaById(int id)
        {
            var vacuna = await _vacunaService.GetVacunaById(id);
            if (vacuna == null)
            {
                return NotFound();
            }
            return Ok(vacuna);
        }

        // Agregar una nueva vacuna
        [HttpPost]
        public async Task<IActionResult> AddVacuna([FromBody] Vacuna vacuna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdVacuna = await _vacunaService.AddVacuna(vacuna);
            return CreatedAtAction(nameof(GetVacunaById), new { id = createdVacuna.Id }, createdVacuna);
        }

        // Actualizar una vacuna existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacuna(int id, [FromBody] Vacuna vacuna)
        {
            if (id != vacuna.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo de la vacuna.");
            }
            try
            {
                var updatedVacuna = await _vacunaService.UpdateVacuna(id, vacuna);
                return Ok(updatedVacuna);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Eliminar una vacuna
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacuna(int id)
        {
            bool isDeleted = await _vacunaService.DeleteVacuna(id);
            if (!isDeleted)
            {
                return NotFound("Vacuna no encontrada.");
            }
            return NoContent();
        }
    }

}
