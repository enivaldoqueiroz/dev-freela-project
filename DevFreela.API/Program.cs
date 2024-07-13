using DevFreela.API.Filters;
using DevFreela.API.Model;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Services.Implamentations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.AuthServices;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DevFreelaDbContext>( 
    options => options.UseNpgsql(configuration.GetConnectionString("DevFreelaCs")));

builder.Services.Configure<OpenigTimeOption>(configuration.GetSection("OpenigTime"));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

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
