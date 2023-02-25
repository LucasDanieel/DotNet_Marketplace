
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Marketplace.Infra.Data.Maps
{
    public class PersonImageMap : IEntityTypeConfiguration<PersonImage>
    {
        public void Configure(EntityTypeBuilder<PersonImage> builder)
        {
            builder.ToTable("Person_Image");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(x => x.PersonId)
                .IsRequired()
                .HasColumnName("Person_Id");

            builder.Property(x => x.ImageUrl)
                .HasColumnName("Image_Url");
            
            builder.Property(x => x.ImageUrlCloudinary)
                .HasColumnName("Image_Url_Cloudinary");
            
            builder.Property(x => x.ImageBase)
                .HasColumnName("Image_Base");

            builder.HasOne(x => x.Person)
                .WithMany(x => x.PersonImages);
        }
    }
}
