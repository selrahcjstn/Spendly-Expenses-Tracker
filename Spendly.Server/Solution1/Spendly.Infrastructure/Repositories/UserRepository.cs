using Microsoft.EntityFrameworkCore;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Domain.Entities;
using Spendly.Infrastructure.Persistence;

namespace Spendly.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _context = dbContext;

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);   
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
           return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUserAsync(string user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == user);
        }

        public Task AddAsync(User user)
        {
            _context.Users.Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}
