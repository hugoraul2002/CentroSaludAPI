
using CentroSaludAPI.Services.DoctorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentroSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctors()
        {
            var doctors = await _doctorService.GetDoctors();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor?>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctor(id);
            if (doctor == null)
                return NotFound(value:"No se encontro un doctor con id " + id); 
            return Ok(doctor);
        }


        [HttpPost]
        public async Task<ActionResult<Doctor?>> AddDoctor(Doctor doctor)
        {
            var newDoctor = await _doctorService.AddDoctor(doctor);
            return Ok(newDoctor);
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<Doctor>>> GetDoctor(int id)
        //{
        //    var doctor = doctors.Find(x => x.Id == id);
        //    if (doctor is null) {
        //        return NotFound("No existe el doctor.");
        //    }
        //    return Ok(doctor);
        //}
    }
}
