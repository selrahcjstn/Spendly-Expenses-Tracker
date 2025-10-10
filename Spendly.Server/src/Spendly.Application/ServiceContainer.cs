using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Spendly.Application.Interfaces.IServices;
using Spendly.Application.Services;
using Spendly.Domain.Entities;

namespace Spendly.Application
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            // password hashing
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            // JWT Service
            services.AddSingleton<IJwtService, JwtService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
