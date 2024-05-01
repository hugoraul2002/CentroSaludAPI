namespace CentroSaludAPI.Services.PacienteService
{
    public interface IPacienteService
    {
        Task<List<Paciente>> GetPacientes();
        Task<Paciente> GetPacienteById(int id);
        Task<Paciente> AddPaciente(Paciente paciente);
        Task<Paciente> UpdatePaciente(int id,Paciente paciente);
        Task<bool> DeletePaciente(int id);
    }
}
