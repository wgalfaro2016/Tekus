﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTekus.Commands.Providers;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Queries.CustomProviderField;
using PruebaTecnicaTekus.Queries.Providers;

namespace PruebaTecnicaTekus.Controllers
{
    [Authorize]
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
                return BadRequest($"There is an error: {response.ErrorMessage}");
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
                return BadRequest($"There is an error: {response.ErrorMessage}");
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

        [HttpGet("GetProviderWithCustomFields/{id}")]
        public async Task<ActionResult<ProviderWithCustomFieldsDto>> GetProviderWithCustomFields(int id) 
        {
            var query = new GetProviderWithCustomFieldsQuery { ID = id };
            var providerWithCustomFields = await _mediator.Send(query);
            if (providerWithCustomFields == null) {
                return NotFound();
            }
            return Ok(providerWithCustomFields);
        }

        [HttpGet("providers-by-country")]
        public async Task<ActionResult<List<ProvidersByCountryDto>>> GetProvidersByCountry() {
            var query = new GetProvidersByCountryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
