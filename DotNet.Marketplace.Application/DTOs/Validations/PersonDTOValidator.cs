
using FluentValidation;

namespace DotNet.Marketplace.Application.DTOs.Validations
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Nome da pessoa deve ser informado");
            RuleFor(x => x.Document).NotEmpty().NotNull().WithMessage("Documento da pessoa deve ser informado");
            RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("Telefone ou celular da pessoa deve ser informado");
        }
    }
}
