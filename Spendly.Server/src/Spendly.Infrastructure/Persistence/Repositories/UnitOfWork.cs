using Spendly.Application.Interfaces.IRepositories;
using Spendly.Infrastructure.Persistence.Database;

namespace Spendly.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository Users { get; }
        public IProfileRepository Profiles { get; }

        public IExpenseRepository Expenses { get; }

        public ICategoryRepository Categories { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Profiles = new ProfileRepostory(_context);
            Expenses =  new ExpenseRepository(_context);
            Categories = new CategoryRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
