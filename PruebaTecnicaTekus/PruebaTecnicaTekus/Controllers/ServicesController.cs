using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Queries.Services;

namespace PruebaTecnicaTekus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet("GetServicesByCountry")]
        public async Task<ActionResult<List<ServicesByCountryDto>>> GetServicesByCountry() {
            var query = new GetServicesByCountryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
