using AutoMapper;
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.DTOs.Validations;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseService(
            IPurchaseRepository purchaseRepository,
            IMapper mapper,
            IPersonRepository personRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _personRepository = personRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAllAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();

            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada");

            return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetByDocumentAsync(string document)
        {
            var personId = await _personRepository.GetIdByDocumentAsync(document);
            if (personId == 0)
                return ResultService.Fail<ICollection<PurchaseDetailDTO>>("Pessoa não encontrada");

            var purchases = await _purchaseRepository.GetByPersonIdAsync(personId);

            if (purchases == null)
                return ResultService.Fail<ICollection<PurchaseDetailDTO>>("Compras não encontrada");

            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetByCodErpAsync(string codErp)
        {
            var productId = await _productRepository.GetIdByCodErpAsync(codErp);
            if (productId == 0)
                return ResultService.Fail<ICollection<PurchaseDetailDTO>>("Produto não encontrada");

            var purchases = await _purchaseRepository.GetByProductIdAsync(productId);
            if (purchases == null)
                return ResultService.Fail<ICollection<PurchaseDetailDTO>>("Compras não encontrada");

            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Compra deve ser informada");

            var validator = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas com a validação dos campos", validator);

            try
            {
                await _unitOfWork.BeginTransaction();

                var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
                if(productId == 0)
                {
                    var product = new Product(purchaseDTO.ProductName ?? string.Empty, purchaseDTO.CodErp, purchaseDTO.Price ?? 0);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }

                var purchase = new Purchase(personId, productId);

                await _purchaseRepository.CreateAsync(purchase);
                purchaseDTO.Id = purchase.Id;

                await _unitOfWork.Commit();
                return ResultService.Ok(purchaseDTO);

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PurchaseDTO>(ex.Message);
            }
        }
        public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Compra deve ser informada");

            var validator = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas com a validação dos campos", validator);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada");

            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);

            purchase.Edit(purchaseDTO.Id, personId, productId);
            await _purchaseRepository.EditAsync(purchase);

            return ResultService.Ok(purchaseDTO);
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if(purchase == null)
                return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada");

            await _purchaseRepository.DeleteAsync(purchase);
            return ResultService.Ok("Compra excluida com sucesso");
        }

    }
}
