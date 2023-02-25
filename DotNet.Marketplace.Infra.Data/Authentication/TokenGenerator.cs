
using DotNet.Marketplace.Domain.Authentication;
using DotNet.Marketplace.Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet.Marketplace.Infra.Data.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public dynamic Generator(User user)
        {
            var permissions = string.Join(",", user.UserPermissions.Select(x => x.Permission.PermissionName));

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Permissions", permissions),
            };

            var expires = DateTime.Now.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Secret));
            var tokenDat = new JwtSecurityToken(
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                    expires: expires,
                    claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDat);
            return new
            {
                acess_token = token,
                expires = expires
            };
        }
    }
}
