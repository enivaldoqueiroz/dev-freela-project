using DevFreela.API.Model;
using DevFreela.Application.Services.Implamentations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OpenigTimeOption>(configuration.GetSection("OpenigTime"));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<DevFreelaDbContext>( 
    options => options.UseNpgsql(configuration.GetConnectionString("DevFreelaCs")));

#region Ciclo de vida de uma instancia no projeto
// AddSingleton - uma instância por aplicação
//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage"});

// AddScoped - uma instância por requisição
builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage"});

// AddTransient - uma instância por classe
//builder.Services.AddTransient<ExampleClass>();
#endregion

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
