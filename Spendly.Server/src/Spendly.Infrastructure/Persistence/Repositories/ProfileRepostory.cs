using Spendly.Application.Interfaces.IRepositories;
using Spendly.Domain.Entities;
using Spendly.Infrastructure.Persistence.Database;

namespace Spendly.Infrastructure.Persistence.Repositories
{
    public class ProfileRepostory(ApplicationDbContext context) : IProfileRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _context.Profiles.FindAsync(id);
        }

        public Task Add(Profile profile)
        {
            _context.Profiles.Add(profile);
            return Task.CompletedTask;
        }

        public Task Update(Profile profile)
        {
            _context.Profiles.Update(profile);
            return Task.CompletedTask;
        }
    }
}
