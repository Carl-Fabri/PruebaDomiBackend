using DB;
using DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PruebaTecnicaDomiruth.Core.Interfaces;
using PruebaTecnicaDomiruth.Core.Repository;
using PruebaTecnicaDomiruth.Core.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CineDomiruthContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBDomiruth"))
           .EnableSensitiveDataLogging() 
           .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<PeliculaRepository>();
builder.Services.AddScoped<FuncionRepository>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ReservaRepository>();

builder.Services.AddScoped<PeliculaService>();
builder.Services.AddScoped<FuncionService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ReservaService>();

//Implementing CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebPage",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CineDomiruth API",
        Version = "v1",
        Description = "API para el sistema de gestión de cines Domiruth",
        Contact = new OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "soporte@domiruth.com"
        }
    });

    // Incluir comentarios XML para documentación
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineDomiruth API V1");
        c.RoutePrefix = "swagger";
    });
    app.MapOpenApi();
}

// Inicialización de la base de datos y seeders
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CineDomiruthContext>();
    
    // Aplicar migraciones pendientes
    context.Database.Migrate();
    
    // Ejecutar seeders
    context.SeedData();
}

app.UseCors("AllowWebPage");
app.MapControllers();
app.UseHttpsRedirection();
app.Run();