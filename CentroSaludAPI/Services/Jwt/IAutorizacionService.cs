
using CentroSaludAPI.Models;

namespace CentroSaludAPI.Services.Jwt
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    }
}
