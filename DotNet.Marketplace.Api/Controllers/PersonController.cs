using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Authentication;
using DotNet.Marketplace.Domain.FiltersDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Marketplace.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ICurrentUser _currentUser;
        private List<string> permissionsNeeded = new List<string>() { "Admin" };
        private readonly List<string> userPermissions;

        public PersonController(IPersonService personService, ICurrentUser currentUser)
        {
            _personService = personService;
            _currentUser = currentUser;
            userPermissions = _currentUser?.Permissions?.Split(",").ToList() ?? new List<string>();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            permissionsNeeded.Add("BuscaPessoa");
            if (!Validator(permissionsNeeded, userPermissions))
                return Forbidden();

            var result = await _personService.GetAllAsync();
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            permissionsNeeded.Add("BuscaPessoa");
            if (!Validator(permissionsNeeded, userPermissions))
                return Forbidden();

            var result = await _personService.GetPersonByIdAsync(id);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetById([FromBody] PersonDTO personDTO)
        {
            permissionsNeeded.Add("BuscaPessoa");
            if (!Validator(permissionsNeeded, userPermissions))
                return Forbidden();

            var result = await _personService.CreateAsync(personDTO);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }
        
        [HttpPost]
        [Route("paged")]
        public async Task<IActionResult> GetPagedPerson([FromBody] PersonFilterDb filterDb)
        {
            permissionsNeeded.Add("CadastraPessoa");
            if (!Validator(permissionsNeeded, userPermissions))
                return Forbidden();

            var result = await _personService.GetPagedPersonAsync(filterDb);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonDTO personDTO)
        {
            permissionsNeeded.Add("EditaPessoa");
            if (!Validator(permissionsNeeded, userPermissions))
                return Forbidden();

            var result = await _personService.UpdateAsync(personDTO);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            permissionsNeeded.Add("DeletaPessoa");
            if (!Validator(permissionsNeeded, userPermissions))
                return Forbidden();

            var result = await _personService.DeleteAsync(id);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
