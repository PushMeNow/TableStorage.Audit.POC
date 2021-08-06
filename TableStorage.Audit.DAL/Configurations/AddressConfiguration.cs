using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableStorage.Audit.DAL.Entities;

namespace TableStorage.Audit.DAL.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(q => q.AddressId);

            builder.Property(q => q.City)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(q => q.Country)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(q => q.User)
                   .WithOne(q => q.Address)
                   .HasForeignKey<Address>(q => q.UserId);
        }
    }
}