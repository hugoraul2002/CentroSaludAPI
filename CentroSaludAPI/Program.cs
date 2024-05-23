//global using CentroSaludAPI.Models;
//global using CentroSaludAPI.Data;
//using CentroSaludAPI.Services.DoctorService;
//using CentroSaludAPI.Services.MunicipioService;
//using CentroSaludAPI.Services.PacienteService;
//using CentroSaludAPI.Services.UsuarioService;
//using CentroSaludAPI.Services.RolService;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

////Se agrego para usar los services
//builder.Services.AddScoped<IDoctorService,DoctorService>();
//builder.Services.AddScoped<IMunicipioService, MunicipioService>();
//builder.Services.AddScoped<IPacienteService, PacienteService>();
//builder.Services.AddScoped<IUsuarioService, UsuarioService>();
//builder.Services.AddScoped<IRolService, RolService>();
//// se agrego para el contexto de la base de datos a usar
//builder.Services.AddDbContext<DataContext>();
//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


global using CentroSaludAPI.Models;
global using CentroSaludAPI.Data;
using CentroSaludAPI.Services.DoctorService;
using CentroSaludAPI.Services.MunicipioService;
using CentroSaludAPI.Services.PacienteService;
using CentroSaludAPI.Services.UsuarioService;
using CentroSaludAPI.Services.RolService;
using CentroSaludAPI.Services.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CentroSaludAPI.Services.UsuarioRolService;
//using CentroSaludAPI.Services.UsuarioRolService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Se agrego para usar los services
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
builder.Services.AddScoped<IUsuarioRolService, UsuarioRolService>();

var key = builder.Configuration.GetValue<string>("JwtSetting:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config => {
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero 
    };
    
});

// Se agrego para el contexto de la base de datos a usar
builder.Services.AddDbContext<DataContext>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200") // URL de tu aplicación Angular
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Aplicar la política de CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
