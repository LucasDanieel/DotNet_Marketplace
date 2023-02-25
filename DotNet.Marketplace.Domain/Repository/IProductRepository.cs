
using DotNet.Marketplace.Domain.Entity;

namespace DotNet.Marketplace.Domain.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<ICollection<Product>> GetAllAsync();
        Task<int> GetIdByCodErpAsync(string codErp);
        Task<Product> CreateAsync(Product product);
        Task EditAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
