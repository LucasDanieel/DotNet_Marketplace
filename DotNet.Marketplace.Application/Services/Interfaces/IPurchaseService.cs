
using DotNet.Marketplace.Application.DTOs;

namespace DotNet.Marketplace.Application.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAllAsync();
        Task<ResultService<ICollection<PurchaseDetailDTO>>> GetByDocumentAsync(string document);
        Task<ResultService<ICollection<PurchaseDetailDTO>>> GetByCodErpAsync(string codErp);
        Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
