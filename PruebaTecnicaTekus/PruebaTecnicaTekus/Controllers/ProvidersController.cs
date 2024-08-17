using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTekus.Commands.Providers;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Queries.Providers;

namespace PruebaTecnicaTekus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvidersController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("CreateProvider")]
        public async Task<IActionResult> CreateProvider([FromBody] CreateProviderCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsSuccess && response.ProviderId.HasValue) {
                return CreatedAtAction(nameof(GetProviderById), new { id = response.ProviderId.Value }, new { id = response.ProviderId });
            }
            else {
                return BadRequest("Unable to create provider.");
            }
        }

        [HttpPut("UpdateProvider")]
        public async Task<IActionResult> UpdateProvider([FromBody] UpdateProviderCommand command) 
        {
            var response = await _mediator.Send(command);
            if (response.IsSuccess && response.ProviderId.HasValue) {
                return CreatedAtAction(nameof(GetProviderById), new { id = response.ProviderId.Value }, new { id = response.ProviderId });
            }
            else {
                return BadRequest("Unable to update provider.");
            }
        }

        [HttpGet("GetProviders")]
        public async Task<ActionResult<List<ProviderDto>>> GetProviders() 
        {
            var query = new GetProvidersQuery();
            var providers = await _mediator.Send(query);
            return Ok(providers);
        }

        [HttpGet("GetProviderById/{id}")]
        public async Task<ActionResult<ProviderDto>> GetProviderById(int id) 
        {
            var query = new GetProviderByIdQuery { ProviderID = id };
            var provider = await _mediator.Send(query);
            if (provider == null) {
                return NotFound();
            }
            return Ok(provider);
        }

    }
}
