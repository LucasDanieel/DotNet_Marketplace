
using DotNet.Marketplace.Application.Mappings;
using DotNet.Marketplace.Application.Services;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Authentication;
using DotNet.Marketplace.Domain.Integrations;
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Authentication;
using DotNet.Marketplace.Infra.Data.Context;
using DotNet.Marketplace.Infra.Data.Integrations;
using DotNet.Marketplace.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.Marketplace.Infra.IoC
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDb>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPersonImageRepository, PersonImageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISavePersonImage, SavePersonImage>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPersonImageService, PersonImageService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
