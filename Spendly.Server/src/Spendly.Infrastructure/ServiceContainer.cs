using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Infrastructure.Persistence.Database;
using Spendly.Infrastructure.Persistence.Repositories;

namespace Spendly.Infrastructure
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("Connection")));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IProfileRepository, ProfileRepostory>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
