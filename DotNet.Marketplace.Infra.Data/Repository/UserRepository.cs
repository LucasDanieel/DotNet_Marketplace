
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDb _db;

        public UserRepository(ApplicationDb db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _db.Users
                        .Include(x => x.UserPermissions)
                        .ThenInclude(x => x.Permission).FirstOrDefaultAsync();
        }
    }
}
