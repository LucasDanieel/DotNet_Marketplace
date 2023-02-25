
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDb _db;

        public ProductRepository(ApplicationDb db)
        {
            _db = db;
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _db.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetIdByCodErpAsync(string codErp)
        {
            return (await _db.Products.FirstOrDefaultAsync(x => x.CodErp == codErp))?.Id ?? 0;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task EditAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}
