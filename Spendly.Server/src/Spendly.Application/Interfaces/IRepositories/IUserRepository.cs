using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> CheckByEmailAsync(string email, Guid id);
        Task<User?> GetByUserAsync(string user);
        Task<User?> GetByEmailOrUserAsync(string user, string email);
        Task Add(User user);
        Task Update(User user);
    }
}
