using Microsoft.EntityFrameworkCore;
using Spendly.Domain.Entities;

namespace Spendly.Infrastructure.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
