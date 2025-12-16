using Business;
using Business.Abstractions;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Kestrel settings for Docker
// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(8080); // HTTP only
// });

builder.Services.AddDbContext<UniprDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MioPrimoMicroservizio")
                      ?? throw new InvalidOperationException("Connection string 'MioPrimoMicroservizio' not found.")));

builder.Services.AddScoped<IBusinessApp, BusinessApp>();
builder.Services.AddScoped<IRepositoryApp, RepositoryApp>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mio Primo Microservizio V1");
    c.RoutePrefix = "swagger"; // <-- così /swagger punta direttamente alla UI
});

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();