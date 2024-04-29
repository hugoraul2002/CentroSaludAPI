using Microsoft.AspNetCore.Http.HttpResults;

namespace CentroSaludAPI.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly DataContext _context;
        public DoctorService(DataContext context)
        {
            _context = context;
        }
        private static readonly List<Doctor> doctors = new List<Doctor> {
                new Doctor { Id = 1,Nombre="Hugo", Apellido="Lopez", Telefono="41108778"},
                new Doctor { Id = 2,Nombre="Raul", Apellido="Enriquez", Telefono="3215462"},
                new Doctor { Id = 3,Nombre="Dimtri", Apellido="Lopez", Telefono="8475125"}
            };

        public async Task<Doctor?> AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public Task<List<Doctor>> DeleteDoctors(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor?> GetDoctor(int id)
        {
            var doctor= await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return null;
            return doctor;
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            var doctores = await _context.Doctors.ToListAsync();
            return doctores;
        }

        public Task<Doctor> UpdateDoctor(int id, Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
