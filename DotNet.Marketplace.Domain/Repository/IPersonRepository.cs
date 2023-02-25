
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.FiltersDb;

namespace DotNet.Marketplace.Domain.Repository
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<ICollection<Person>> GetAlldAsync();
        Task<int> GetIdByDocumentAsync(string document);
        Task<Person> CreateAsync(Person person);
        Task EditAsync(Person person);
        Task DeleteAsync(Person person);
        Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request);
    }
}
