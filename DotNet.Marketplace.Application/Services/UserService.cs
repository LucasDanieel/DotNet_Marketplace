
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.DTOs.Validations;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Authentication;
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResultService<dynamic>> TokenGenerator(UserDTO userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<dynamic>("Usuario deve ser informado");

            var validator = new UserDTOValidator().Validate(userDTO);
            if(!validator.IsValid)
                return ResultService.RequestError<dynamic>("Problemas com a validação dos campos", validator);

            var user = await _userRepository.GetByEmailAndPasswordAsync(userDTO.Email, userDTO.Password);
            if (user == null)
                return ResultService.Fail<dynamic>("Usuario não encontrado");

            return ResultService.Ok(_tokenGenerator.Generator(user));
        }
    }
}
