
using app.projectCholcaByron.services.EventMQ;
using app.projectRandyPenaherrera.common.EventMQ;
using app.projectRandyPenaherrera.DataAccess.context;
using app.projectRandyPenaherrera.DataAccess.repositories;
using app.projectRandyPenaherrera.services.EventMQ;
using app.projectRandyPenaherrera.services.Implementations;
using app.projectRandyPenaherrera.services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Leer la configuración de RabbitMQ desde appsettings.json
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("rabbitmq"));



//LA CADENA DE CONEXION ESTA EN EL appsettings.json
//CON EL SIGUIENTA LINEA OBTENEMOS LA CADENA DE CONEXIONA SQL SERVER
var conSqlServer = builder.Configuration.GetConnectionString("BDDSqlServer")!;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conSqlServer);
    options.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
});

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IVentaDetalleRepository, VentaDetalleRepository>();
builder.Services.AddScoped<IVentaDetalleService, VentaDetalleService>();

builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();


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
