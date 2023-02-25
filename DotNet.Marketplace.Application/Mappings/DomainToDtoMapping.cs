
using AutoMapper;
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Domain.Entity;

namespace DotNet.Marketplace.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Purchase, PurchaseDetailDTO>()
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var purchase = new PurchaseDetailDTO
                    {
                        Id = model.Id,
                        Person = model.Person.Name,
                        Product = model.Product.Name,
                        DateTime = model.DateTime
                    };

                    return purchase;
                });
            CreateMap<PersonImage, PersonImageDTO>()
                .ConstructUsing((model, context) =>
                {
                    var personImage = new PersonImageDTO
                    {
                        PersonId = model.PersonId,
                        Image = model.ImageBase ?? model.ImageUrl ?? model.ImageUrlCloudinary ?? string.Empty
                    };

                    return personImage;
                });
        }
    }
}
