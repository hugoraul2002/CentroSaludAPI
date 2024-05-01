using Microsoft.AspNetCore.Mvc;
using CentroSaludAPI.Services.MunicipioService;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Municipio>>> GetMunicipios()
        {
            var municipios = await _municipioService.GetMunicipios();
            return Ok(municipios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipio?>> GetMunicipio(int id)
        {
            var municipio = await _municipioService.GetMunicipio(id);
            if (municipio == null)
                return NotFound(value: "No se encontro un municipio con id " + id);
            return Ok(municipio);
        }

        [HttpPost]
        public async Task<ActionResult<Municipio?>> AddMunicipio(Municipio municipio)
        {
            var newMunicipio = await _municipioService.AddMunicipio(municipio);
            return Ok(newMunicipio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Municipio>> UpdateMunicipio(int id, Municipio municipio)
        {
            var updatedMunicipio = await _municipioService.UpdateMunicipio(id, municipio);
            return Ok(updatedMunicipio);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Municipio>>> DeleteMunicipios(int id)
        {
            var municipio = await _municipioService.DeleteMunicipios(id);
            return Ok(municipio);
        }
    }
}
