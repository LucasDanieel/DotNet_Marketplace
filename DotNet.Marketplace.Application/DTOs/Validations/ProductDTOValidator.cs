
using FluentValidation;

namespace DotNet.Marketplace.Application.DTOs.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Nome do produto deve ser informado");
            RuleFor(x => x.CodErp).NotEmpty().NotNull().WithMessage("Codigo erp do produto deve ser informado");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Preço do produto deve ser maior que zero");
        }
    }
}
