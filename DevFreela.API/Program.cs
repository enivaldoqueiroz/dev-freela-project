using DevFreela.API.Model;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OpenigTimeOption>(configuration.GetSection("OpenigTime"));

// AddSingleton - uma inst�ncia por aplica��o
//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage"});

// AddScoped - uma inst�ncia por requisi��o
builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage"});

// AddTransient - uma inst�ncia por classe
//builder.Services.AddTransient<ExampleClass>();

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
