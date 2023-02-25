using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Marketplace.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public bool Validator(List<string> permissionsNeeded, List<string> userPermissions)
        {
            return permissionsNeeded.Any(x => userPermissions.Contains(x));
        }

        [NonAction]
        public IActionResult Forbidden()
        {
            var obj = new
            {
                code = "permissão_negada",
                message = "Usuario não contem as devidas permissões necessarias para executar esse comando."
            };

            return new ObjectResult(obj){ StatusCode = 403};
        }
    }
}
