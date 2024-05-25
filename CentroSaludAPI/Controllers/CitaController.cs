using CentroSaludAPI.DTO;
using CentroSaludAPI.Services.CitaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        // Obtener todas las citas
        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            var citas = await _citaService.GetCitas();
            return Ok(citas);
        }

        // Obtener una cita por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCitaById(int id)
        {
            var cita = await _citaService.GetCitaById(id);
            if (cita == null)
            {
                return NotFound();
            }
            return Ok(cita);
        }

        // Agregar una nueva cita
        [HttpPost]
        public async Task<IActionResult> AddCita([FromBody] CitaDTO citaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cita = new Cita
            {
                PacienteId = citaDTO.PacienteId,
        DoctorId = citaDTO.DoctorId,
       UsuarioId = citaDTO.UsuarioId,
        FechaHora = citaDTO.FechaHora,
        FechaRegistro = citaDTO.FechaRegistro,
        Observaciones = citaDTO.Observaciones,
        Estado= citaDTO.Estado
            };
            var createdCita = await _citaService.AddCita(cita);
            return CreatedAtAction(nameof(GetCitaById), new { id = createdCita.Id }, createdCita);
        }

        // Actualizar una cita existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCita(int id, [FromBody] Cita cita)
        {
            if (id != cita.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo de la cita.");
            }
            try
            {
                var updatedCita = await _citaService.UpdateCita(id, cita);
                return Ok(updatedCita);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Eliminar una cita
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            bool isDeleted = await _citaService.DeleteCita(id);
            if (!isDeleted)
            {
                return NotFound("Cita no encontrada.");
            }
            return NoContent();
        }
    }

}
