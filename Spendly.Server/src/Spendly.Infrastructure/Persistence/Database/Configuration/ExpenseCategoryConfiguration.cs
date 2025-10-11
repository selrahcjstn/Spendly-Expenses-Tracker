using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spendly.Domain.Entities;

namespace Spendly.Infrastructure.Persistence.Database.Configuration
{
    public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpensesCategory>
    {
        public void Configure(EntityTypeBuilder<ExpensesCategory> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Category)
                .IsRequired();

            builder
            .HasMany(e => e.Expenses)          
            .WithMany(c => c.Category)       
            .UsingEntity<Dictionary<string, object>>(
                "ExpenseCategoryLink",
                j => j
                    .HasOne<Expense>()
                    .WithMany()
                    .HasForeignKey("ExpenseId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<ExpensesCategory>()
                    .WithMany()
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
        }
    }
}
