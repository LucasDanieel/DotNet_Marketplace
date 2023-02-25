using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Domain.FiltersDb;
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResultService<PersonDTO>> GetPersonByIdAsync(int id);
        Task<ResultService<ICollection<PersonDTO>>> GetAllAsync();
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService> UpdateAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedPersonAsync(PersonFilterDb filterDb);
    }
}
