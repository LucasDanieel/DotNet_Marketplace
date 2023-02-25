
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Marketplace.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email");

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("Password");

            builder.HasMany(x => x.UserPermissions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
