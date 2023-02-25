
using DotNet.Marketplace.Domain.Entity;

namespace DotNet.Marketplace.Domain.Authentication
{
    public interface ITokenGenerator
    {
        dynamic Generator(User user);
    }
}
