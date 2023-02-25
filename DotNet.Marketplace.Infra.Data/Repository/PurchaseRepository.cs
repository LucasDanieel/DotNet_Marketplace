
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDb _db;

        public PurchaseRepository(ApplicationDb db)
        {
            _db = db;
        }

        public async Task<ICollection<Purchase>> GetAllAsync()
        {
            return await _db.Purchases
                    .Include(x => x.Person)
                    .Include(x => x.Product).AsNoTracking().ToListAsync();
        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            return await _db.Purchases
                    .Include(x => x.Person)
                    .Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Purchase>> GetByPersonIdAsync(int personId)
        {
            return await _db.Purchases
                    .Include(x => x.Person)
                    .Include(x => x.Product).Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<ICollection<Purchase>> GetByProductIdAsync(int productId)
        {
            return await _db.Purchases
                    .Include(x => x.Person)
                    .Include(x => x.Product).Where(x => x.ProductId == productId).ToListAsync();
        }

        public async Task<Purchase> CreateAsync(Purchase purchase)
        {
            _db.Purchases.Add(purchase);
            await _db.SaveChangesAsync();
            return purchase;
        }

        public async Task EditAsync(Purchase purchase)
        {
            _db.Purchases.Update(purchase);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Purchase purchase)
        {
            _db.Purchases.Remove(purchase);
            await _db.SaveChangesAsync();
        }
    }
}
