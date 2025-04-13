using APIClinica.Data;
using Microsoft.EntityFrameworkCore;
using APIClinica.Configuration;
using APIClinica.Business;
using APIClinica.Data.Entidades;
using APIClinica.Services;
using Resend;
using QuestPDF.Infrastructure;

var myAllowSpecificOrigins = "myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5173", // frontend en React
                "https://wonderful-otter-e630f8.netlify.app" // si usás dominio productivo
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
builder.Services.AddProjectServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<Usuario_B>();
builder.Services.AddScoped<UsuarioDB>();

builder.Services.AddScoped<PacienteDB>();
builder.Services.AddScoped<Paciente_B>();

builder.Services.AddScoped<MedicoDB>();
builder.Services.AddScoped<Medico_B>();

builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<PdfService>();




var app = builder.Build();

QuestPDF.Settings.License = LicenseType.Community;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usá solo una política de CORS
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
