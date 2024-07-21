using DevFreela.Application.Services.Implamentations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.AuthServices;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Payment;
using DevFreela.Infrastructure.Repositories;

namespace DevFreela.API.Extensions
{
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection AddInfrastruture(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMessageBusService, MessageBusService>();
            
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            return services;
        }
    }
}
