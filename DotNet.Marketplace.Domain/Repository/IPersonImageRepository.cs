
using DotNet.Marketplace.Domain.Entity;

namespace DotNet.Marketplace.Domain.Repository
{
    public interface IPersonImageRepository
    {
        Task<PersonImage> GetByIdAsync(int id);
        Task<ICollection<PersonImage>> GetByPersonIdAsync(int personId);
        Task<PersonImage> CreateAsync(PersonImage personImage);
        Task EditAsync(PersonImage personImage);
        Task DeleteAsync(PersonImage personImage);
    }
}
