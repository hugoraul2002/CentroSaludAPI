using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentroSaludAPI.Models;
using Microsoft.Extensions.Configuration;
using CentroSaludAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Azure.Identity;

namespace CentroSaludAPI.Services.Jwt
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        private string GenerarToken(string idUsuario, string userName,string rol)
        {
            var key = _configuration.GetValue<string>("JwtSetting:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            Console.WriteLine($"idUsuario: {idUsuario ?? "null"}");
            Console.WriteLine($"userName: {userName ?? "null"}");
            Console.WriteLine($"rol: {rol ?? "null"}");
            var claims = new ClaimsIdentity(new[]
            {
        new Claim(ClaimTypes.NameIdentifier, idUsuario),
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.Role, rol)
         });

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }


        //public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        //{
        //    var usuario_encontrado = _context.Usuario.FirstOrDefault(x =>
        //        x.username == autorizacion.NombreUsuario &&
        //        x.password == autorizacion.Clave
        //    );

        //    if (usuario_encontrado == null)
        //    {
        //        return await Task.FromResult<AutorizacionResponse>(null);

        //    }

        //    string tokenCreado = GenerarToken(usuario_encontrado.Id.ToString(),usuario_encontrado.username);

        //    return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Msg = "ok" };

        //}

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuario
                .Include(u => u.UsuarioRoles) // Incluye la relación con UsuarioRoles
                .ThenInclude(ur => ur.Rol) // Incluye la relación con Rol a través de UsuarioRol
                .FirstOrDefault(x =>
                    x.username == autorizacion.NombreUsuario
                );
            Console.WriteLine("HOLA VERIFICA PASSWORD:");
            Console.WriteLine(usuario_encontrado);
            Console.WriteLine(BCrypt.Net.BCrypt.Verify(autorizacion.Clave, usuario_encontrado.password));
            if (usuario_encontrado == null || !BCrypt.Net.BCrypt.Verify(autorizacion.Clave, usuario_encontrado.password))
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }

            var rol = usuario_encontrado.UsuarioRoles.FirstOrDefault()?.Rol; // Obtén el primer rol asociado al usuario
            Console.WriteLine(rol);
            Console.WriteLine(usuario_encontrado);
            string tokenCreado = GenerarToken(usuario_encontrado.Id.ToString(), usuario_encontrado.username, rol?.Nombre);

            return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Msg = "ok" };
        }

    }
}
