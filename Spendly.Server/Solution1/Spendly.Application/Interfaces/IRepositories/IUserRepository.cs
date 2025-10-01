using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserAsync(string user);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
