
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Marketplace.Infra.Data.Maps
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(x => x.PermissionName)
                .IsRequired()
                .HasColumnName("Permission_Name");

            builder.Property(x => x.PermissionVisual)
                .IsRequired()
                .HasColumnName("Permission_Visual");

            builder.HasMany(x => x.UserPermissions)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId);
        }
    }
}
