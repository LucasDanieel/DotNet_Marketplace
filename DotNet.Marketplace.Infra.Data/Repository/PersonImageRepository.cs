
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public class PersonImageRepository : IPersonImageRepository
    {
        private readonly ApplicationDb _db;

        public PersonImageRepository(ApplicationDb db)
        {
            _db = db;
        }

        public async Task<PersonImage> GetByIdAsync(int id)
        {
            return await _db.PersonImages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<PersonImage>> GetByPersonIdAsync(int personId)
        {
            return await _db.PersonImages.AsNoTracking().Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<PersonImage> CreateAsync(PersonImage personImage)
        {
            _db.PersonImages.Add(personImage);
            await _db.SaveChangesAsync();
            return personImage;
        }

        public async Task EditAsync(PersonImage personImage)
        {
            _db.PersonImages.Update(personImage);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(PersonImage personImage)
        {
            _db.PersonImages.Remove(personImage);
            await _db.SaveChangesAsync();
        }
    }
}
