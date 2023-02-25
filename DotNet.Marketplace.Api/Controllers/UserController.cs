using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Marketplace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> GetToken([FromBody] UserDTO userDTO)
        {
            var result = await _userService.TokenGenerator(userDTO);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
