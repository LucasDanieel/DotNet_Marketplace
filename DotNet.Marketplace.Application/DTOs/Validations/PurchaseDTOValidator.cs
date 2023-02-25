
using FluentValidation;

namespace DotNet.Marketplace.Application.DTOs.Validations
{
    public class PurchaseDTOValidator : AbstractValidator<PurchaseDTO>
    {
        public PurchaseDTOValidator()
        {
            RuleFor(x => x.Document).NotEmpty().NotNull().WithMessage("Documento da pessoa deve ser informado");
            RuleFor(x => x.CodErp).NotEmpty().NotNull().WithMessage("Codigo do produto deve ser informado");
        }
    }
}
