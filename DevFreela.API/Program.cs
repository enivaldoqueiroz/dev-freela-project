using DevFreela.API.Filters;
using DevFreela.API.Model;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Consumers;
using DevFreela.Application.Validators;
using DevFreela.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DevFreela.Infrastructure;
using DevFreela.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,

//                        ValidIssuer = configuration["Jwt:Issuer"],
//                        ValidAudience = configuration["Jwt:Audience"],
//                        IssuerSigningKey = new SymmetricSecurityKey
//                            (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
//                    };
//                });

//builder.Services.AddDbContext<DevFreelaDbContext>( 
//    options => options.UseNpgsql(configuration.GetConnectionString("DevFreelaCs")));

builder.Services.AddHostedService<PaymentApprovedConsumer>();

builder.Services.AddHttpClient();

builder.Services.Configure<OpenigTimeOption>(configuration.GetSection("OpenigTime"));

builder.Services.AddInfrastructureApi();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

#region Ciclo de vida de uma instancia no projeto
// AddSingleton - uma inst�ncia por aplica��o
//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage"});

// AddScoped - uma inst�ncia por requisi��o
builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage"});

// AddTransient - uma inst�ncia por classe
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
