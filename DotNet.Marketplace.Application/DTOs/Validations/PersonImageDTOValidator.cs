
using FluentValidation;

namespace DotNet.Marketplace.Application.DTOs.Validations
{
    public class PersonImageDTOValidator : AbstractValidator<PersonImageDTO>
    {
        public PersonImageDTOValidator()
        {
            RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("Id da pessoa deve ser informado");
            RuleFor(x => x.Image).NotEmpty().NotNull().WithMessage("Imagem deve ser informado");
        }
    }
}
