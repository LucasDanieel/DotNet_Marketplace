
using FluentValidation;

namespace DotNet.Marketplace.Application.DTOs.Validations
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email do usuario deve ser informado");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Senha do usuario deve ser informado");
        }
    }
}
