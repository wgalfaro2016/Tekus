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
            var response = await _mediator.Send(command);
            if (response.IsSuccess && response.ProviderServiceId.HasValue) {
                return CreatedAtAction(nameof(GetProviderServiceById), new { id = response.ProviderServiceId.Value }, new { id = response.ProviderServiceId });
            }
            else {
                return BadRequest($"There is an error: {response.ErrorMessage}");
            }
        }

        [HttpPut("UpdateProviderService")]
        public async Task<IActionResult> UpdateProviderService([FromBody] UpdateProviderServiceCommand command) 
        {
            
            var response = await _mediator.Send(command);
            if (response.IsSuccess && response.ProviderServiceId.HasValue) {
                return CreatedAtAction(nameof(GetProviderServiceById), new { id = response.ProviderServiceId.Value }, new { id = response.ProviderServiceId });
            }
            else {
                return BadRequest($"There is an error: {response.ErrorMessage}");
            }
        }
    }
}