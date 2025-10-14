using Microsoft.EntityFrameworkCore;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Domain.Entities;
using Spendly.Infrastructure.Persistence.Database;

namespace Spendly.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(RefreshToken entity)
        {
            await _context.RefreshTokens.AddAsync(entity);
        }

        public void Update(RefreshToken entity)
        {
            _context.RefreshTokens.Update(entity);
        }

        public void Remove(RefreshToken entity)
        {
            _context.RefreshTokens.Remove(entity);
        }

        public async Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            return await _context.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
