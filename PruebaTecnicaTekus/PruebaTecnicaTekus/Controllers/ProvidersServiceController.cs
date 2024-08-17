using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTekus.Commands.ProviderServices;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Queries.ProvidersServices;

namespace PruebaTecnicaTekus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvidersServiceController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet("GetProviderServices")]
        public async Task<ActionResult<List<ProviderServiceDto>>> GetProviderServices() 
        {
            var result = await _mediator.Send(new GetProviderServicesQuery());
            return Ok(result);
        }

        [HttpGet("GetProviderServiceById/{id}")]
        public async Task<ActionResult<ProviderServiceDto>> GetProviderServiceById(int id) 
        {
            var result = await _mediator.Send(new GetProviderServiceByIdQuery { Id = id });
            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("CreateProviderService")]
        public async Task<ActionResult<ProviderServiceDto>> CreateProviderService([FromBody] CreateProviderServiceCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProviderServiceById), new { id = result.ProviderServiceId }, result);
        }

        [HttpPut("UpdateProviderService")]
        public async Task<IActionResult> UpdateProviderService([FromBody] UpdateProviderServiceCommand command) 
        {
            
            var result = await _mediator.Send(command);
            if (result == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}