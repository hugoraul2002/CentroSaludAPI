namespace CentroSaludAPI.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetDoctors();
        Task<Doctor?> GetDoctor(int id);
        Task<Doctor?> AddDoctor(Doctor doctor);
        Task<Doctor> UpdateDoctor(int id, Doctor doctor);
        Task<List<Doctor>> DeleteDoctors(int id);

    }
}
