using AutoMapper;
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.DTOs.Validations;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ICollection<ProductDTO>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return ResultService.Ok(_mapper.Map<ICollection<ProductDTO>>(products));
        }

        public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrado");

            return ResultService.Ok(_mapper.Map<ProductDTO>(product));
        }

        public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Produto deve ser informado");

            var validator = new ProductDTOValidator().Validate(productDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas com a validação dos campos", validator);

            var product = _mapper.Map<Product>(productDTO);

            product = await _productRepository.CreateAsync(product);

            return ResultService.Ok(_mapper.Map<ProductDTO>(product));
        }

        public async Task<ResultService> UpdateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Produto deve ser informado");

            var validator = new ProductDTOValidator().Validate(productDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas com a validação dos campos", validator);

            var product = await _productRepository.GetByIdAsync(productDTO.Id);
            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrado");

            if (product.CodErp != productDTO.CodErp)
                return ResultService.Fail("Codigo do produto invalido");

            product = _mapper.Map(productDTO, product);

            await _productRepository.EditAsync(product);

            return ResultService.Ok($"Produto com o Id:{product.Id} atualizado com sucesso");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrado");

            await _productRepository.DeleteAsync(product);

            return ResultService.Ok($"Produto com o Id:{id} excluido com sucesso");
        }
    }
}
