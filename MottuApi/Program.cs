using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MottuBusiness;
using MottuData;

var builder = WebApplication.CreateBuilder(args);

// Serviços do domínio (injeção de dependência)
builder.Services.AddScoped<IMottuService, MottuService>();
builder.Services.AddScoped<IZonaService, ZonaService>();
builder.Services.AddScoped<IPatioService, PatioService>();

// Configuração do contexto do banco Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do Swagger com documentação XML
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu API",
        Version = "v1",
        Description = "API para gerenciamento de motos, zonas e pátios da Mottu"
    });

    // Caminho do arquivo XML da documentação gerada
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddControllers();

var app = builder.Build();

// Middleware da pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1");
        c.RoutePrefix = string.Empty; // Swagger em http://localhost:5026/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
