using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableStorage.Audit.DAL.Entities;

namespace TableStorage.Audit.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(q => q.UserId);

            builder.Property(q => q.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(q => q.LastName)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}