using MottuBusiness;
using MottuData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger configurado com título, versão, e contato
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu API",
        Version = "v1",
        Description = "API para gerenciamento de motos, zonas e pátios da Mottu",
    });
});

// Serviços do domínio
builder.Services.AddScoped<IMottuService, MottuService>();
builder.Services.AddScoped<IZonaService, ZonaService>();
builder.Services.AddScoped<IPatioService, PatioService>();

// Banco de dados Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
