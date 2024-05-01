using CentroSaludAPI.Services.PacienteService;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }
        // Obtener todos los pacientes
        [HttpGet]
        public async Task<IActionResult> GetPacientes()
        {
            var pacientes = await _pacienteService.GetPacientes();
            return Ok(pacientes);
        }

        // Obtener un paciente por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteById(int id)
        {
            var paciente = await _pacienteService.GetPacienteById(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        // Agregar un nuevo paciente
        [HttpPost]
        public async Task<IActionResult> AddPaciente([FromBody] Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdPaciente = await _pacienteService.AddPaciente(paciente);
            return CreatedAtAction(nameof(GetPacienteById), new { id = createdPaciente.Id }, createdPaciente);
        }

        // Actualizar un paciente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest("ID del URL no coincide con el ID del cuerpo del paciente.");
            }
            try
            {
                var updatedPaciente = await _pacienteService.UpdatePaciente(id, paciente);
                return Ok(updatedPaciente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Eliminar un paciente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            bool isDeleted = await _pacienteService.DeletePaciente(id);
            if (!isDeleted)
            {
                return NotFound("Paciente no encontrado.");
            }
            return NoContent();
        }
    }
}
