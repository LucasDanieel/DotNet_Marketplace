
using AutoMapper;
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Domain.Entity;

namespace DotNet.Marketplace.Application.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<PersonDTO, Person>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
