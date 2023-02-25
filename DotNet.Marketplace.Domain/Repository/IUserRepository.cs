
using DotNet.Marketplace.Domain.Entity;

namespace DotNet.Marketplace.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
    }
}
