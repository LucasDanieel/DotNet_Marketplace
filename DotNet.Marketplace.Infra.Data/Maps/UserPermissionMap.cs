
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Marketplace.Infra.Data.Maps
{
    public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("User_Permission");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("User_Id");

            builder.Property(x => x.PermissionId)
                .IsRequired()
                .HasColumnName("Permission_Id");

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserPermissions);
            
            builder.HasOne(x => x.Permission)
                .WithMany(x => x.UserPermissions);
        }
    }
}
