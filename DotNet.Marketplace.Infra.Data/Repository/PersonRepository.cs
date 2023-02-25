using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.FiltersDb;
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDb _db;

        public PersonRepository(ApplicationDb db)
        {
            _db = db;
        }

        public async Task<ICollection<Person>> GetAlldAsync()
        {
            return await _db.Peoples.AsNoTracking().ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _db.Peoples.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetIdByDocumentAsync(string document)
        {
            return (await _db.Peoples.FirstOrDefaultAsync(x => x.Document == document))?.Id ?? 0;
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _db.Peoples.Add(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task EditAsync(Person person)
        {
            _db.Peoples.Update(person);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Person person)
        {
            _db.Peoples.Remove(person);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request)
        {
            var peoples = _db.Peoples.AsQueryable();
            if (!string.IsNullOrEmpty(request.Name))
                peoples = peoples.Where(x => x.Name.Contains(request.Name));

            return await PagedBaseResponseHelper.Pagination<PagedBaseResponse<Person>, Person>(peoples, request);
        }
    }
}
