global using CentroSaludAPI.Models;
global using CentroSaludAPI.Data;
using CentroSaludAPI.Services.DoctorService;
using CentroSaludAPI.Services.MunicipioService;
using CentroSaludAPI.Services.PacienteService;
using CentroSaludAPI.Services.UsuarioService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Se agrego para usar los services
builder.Services.AddScoped<IDoctorService,DoctorService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
// se agrego para el contexto de la base de datos a usar
builder.Services.AddDbContext<DataContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
