
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Marketplace.Infra.Data.Maps
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name");
            
            builder.Property(x => x.Document)
                .IsRequired()
                .HasColumnName("Document");

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnName("Phone");

            builder.HasMany(x => x.Purchases)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.PersonImages)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);
        }
    }
}
