using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Marketplace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonImageController : ControllerBase
    {
        private readonly IPersonImageService _personImageService;

        public PersonImageController(IPersonImageService personImageService)
        {
            _personImageService = personImageService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _personImageService.GetByIdAsync(id);
            if(result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("person/{personId}")]
        public async Task<ActionResult> GetByPersonId(int personId)
        {
            var result = await _personImageService.GetByPersonIdAsync(personId);
            if(result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("base")]
        public async Task<ActionResult> CreateImageBase([FromBody] PersonImageDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageBase64Async(personImageDTO);
            if(result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("url")]
        public async Task<ActionResult> CreateImageUrl([FromBody] PersonImageDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageUrlAsync(personImageDTO);
            if(result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("cloudinary")]
        public async Task<ActionResult> CreateImageUrlCloudinary([FromBody] PersonImageDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageUrlCloudinaryAsync(personImageDTO);
            if(result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
