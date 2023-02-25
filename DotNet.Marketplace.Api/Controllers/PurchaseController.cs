using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Marketplace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _purchaseService.GetAllAsync();
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _purchaseService.GetByIdAsync(id);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("document/{document}")]
        public async Task<ActionResult> GetByDocument(string document)
        {
            var result = await _purchaseService.GetByDocumentAsync(document);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("codErp/{codErp}")]
        public async Task<ActionResult> GetByCodErp(string codErp)
        {
            var result = await _purchaseService.GetByCodErpAsync(codErp);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PurchaseDTO purchaseDTO)
        {
            var result = await _purchaseService.CreateAsync(purchaseDTO);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }
        
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PurchaseDTO purchaseDTO)
        {
            var result = await _purchaseService.UpdateAsync(purchaseDTO);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _purchaseService.DeleteAsync(id);
            if (result.IsSucesse)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
