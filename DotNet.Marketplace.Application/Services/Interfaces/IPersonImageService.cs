
using DotNet.Marketplace.Application.DTOs;

namespace DotNet.Marketplace.Application.Services.Interfaces
{
    public interface IPersonImageService
    {
        Task<ResultService<PersonImageDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<PersonImageDTO>>> GetByPersonIdAsync(int personId);
        Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO);
        Task<ResultService> CreateImageUrlAsync(PersonImageDTO personImageDTO);
        Task<ResultService> CreateImageUrlCloudinaryAsync(PersonImageDTO personImageDTO);
    }
}
