
using DotNet.Marketplace.Application.DTOs;

namespace DotNet.Marketplace.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultService<dynamic>> TokenGenerator(UserDTO userDTO);
    }
}
