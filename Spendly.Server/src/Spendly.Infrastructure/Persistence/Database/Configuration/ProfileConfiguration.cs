using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spendly.Domain.Entities;

namespace Spendly.Infrastructure.Persistence.Database.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable(nameof(Profile));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.MiddleName)
                .HasMaxLength(50);

            builder.Property(x => x.BirthDate)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
