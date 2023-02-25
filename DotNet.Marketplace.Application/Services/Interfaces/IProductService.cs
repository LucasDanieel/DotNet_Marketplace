using DotNet.Marketplace.Application.DTOs;

namespace DotNet.Marketplace.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultService<ProductDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<ProductDTO>>> GetAllAsync();
        Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultService> UpdateAsync(ProductDTO productDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
