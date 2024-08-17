using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTekus.Commands.CustomProviderField;
using PruebaTecnicaTekus.Queries.CustomProviderField;

namespace PruebaTecnicaTekus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomProviderFieldsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomProviderFieldsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("AddCustomProviderField")]
        public async Task<IActionResult> AddCustomProviderField([FromBody] CreateCustomFieldCommand command) {
            var response = await _mediator.Send(command);
            if (response.IsSuccess && response.Id.HasValue) {
                return CreatedAtAction(nameof(GetCustomProviderFieldById), new { id = response.Id.Value }, new { id = response.Id });
            }
            else {
                return BadRequest("Unable to create Customer provider field.");
            }
        }

        [HttpGet("GetCustomProviderFieldById/{id}")]
        public async Task<IActionResult> GetCustomProviderFieldById(int id) {
            var query = new GetCustomProviderFieldByIdQuery { ID = id };
            var field = await _mediator.Send(query);
            if (field == null) {
                return NotFound();
            }
            return Ok(field);
        }
    }
}
