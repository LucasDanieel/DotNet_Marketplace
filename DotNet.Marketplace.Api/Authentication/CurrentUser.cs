using DotNet.Marketplace.Domain.Authentication;

namespace DotNet.Marketplace.Api.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Permissions { get; set; }

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor?.HttpContext?.User?.Claims;
            if(claims.Any(x => x.Type == "Id"))
            {
                Id = Convert.ToInt32(claims.First(x => x.Type == "Id").Value);
            }
            
            if(claims.Any(x => x.Type == "Email"))
            {
                Email = claims.First(x => x.Type == "Email").Value;
            }

            if(claims.Any(x => x.Type == "Permissions"))
            {
                Permissions = claims.First(x => x.Type == "Permissions").Value;
            }
        }
    }
}
