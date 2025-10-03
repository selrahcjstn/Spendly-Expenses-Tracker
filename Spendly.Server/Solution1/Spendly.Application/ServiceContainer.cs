using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Spendly.Application.Interfaces.IServices;
using Spendly.Application.Services;

namespace Spendly.Application
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ServiceContainer).Assembly);

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
