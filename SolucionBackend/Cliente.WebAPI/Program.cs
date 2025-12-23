using Cliente.Aplicacion.Servicios.Implementacion;
using Cliente.Aplicacion.Servicios.Interfaces;
using Cliente.Dominio.Repositories;
using Cliente.Infraestructura.Persistencia;
using Cliente.Infraestructura.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDBConnection")));
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();

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
