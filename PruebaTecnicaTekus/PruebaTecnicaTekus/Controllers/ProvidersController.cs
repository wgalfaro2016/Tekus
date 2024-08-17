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

        public ProvidersController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] CreateProviderCommand command) {
            var providerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProviderById), new { id = providerId }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, [FromBody] UpdateProviderCommand command) {
            if (id != command.ProviderID) {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProviderDto>>> GetProviders() {
            var query = new GetProvidersQuery();
            var providers = await _mediator.Send(query);
            return Ok(providers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderDto>> GetProviderById(int id) {
            var query = new GetProviderByIdQuery { ProviderID = id };
            var provider = await _mediator.Send(query);
            if (provider == null) {
                return NotFound();
            }
            return Ok(provider);
        }

    }
}
