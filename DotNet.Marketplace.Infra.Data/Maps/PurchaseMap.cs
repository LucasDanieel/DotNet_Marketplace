
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Marketplace.Infra.Data.Maps
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(x => x.PersonId)
                .IsRequired()
                .HasColumnName("Person_Id");

            builder.Property(x => x.ProductId)
                .IsRequired()
                .HasColumnName("Product_Id");

            builder.Property(x => x.DateTime)
                .HasColumnName("Date_Time");

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Purchases);

            builder.HasOne(x => x.Person)
                .WithMany(x => x.Purchases);
        }
    }
}
