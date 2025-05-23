using BibliotecaApi.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Area de servicios
builder.Services.AddControllers().AddJsonOptions(optiones => optiones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Area de middlewares
app.MapControllers();


app.Run();
